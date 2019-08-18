using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WarpController : MonoBehaviour
{
    public Transform destination;
    public Vector2 warpCoords;
    public int moveSteps = 0;
    public DIRECTION_BUTTON finalFacingDirection = DIRECTION_BUTTON.NONE;
    private GameObject _pendingWarp;
    private readonly Vector2 _coordsOffset = new Vector2(0.5f, 0.5f); // pivot point
    private static ScreenFaderController _screenFaderController;

    void Awake()
    {
        LoadFader();
    }

    private static void LoadFader()
    {
        if (_screenFaderController)
        {
            return;
        }

        var screenFader = GameObject.FindGameObjectWithTag(GameManager.Tag.ScreenFader);

        if (!screenFader)
        {
            throw new MissingComponentException("There is no game object tagged as 'ScreenFader' in this scene.");
        }

        _screenFaderController = screenFader.GetComponent<ScreenFaderController>();

        if (!_screenFaderController)
        {
            throw new MissingComponentException("The ScreenFader object has no ScreenFaderController attached.");
        }
    }

    private static bool HasFader()
    {
        return !(_screenFaderController is null);
    }

    private static void FadeOut()
    {
        if (!HasFader())
        {
            return;
        }

        _screenFaderController.FadeOut();
    }

    private static void FadeIn()
    {
        if (!HasFader())
        {
            return;
        }

        _screenFaderController.FadeIn();
    }

    private bool CanWarp(Collider2D warper)
    {
        if (HasFader() && _screenFaderController.IsTransitioning())
        {
            return false;
        }

        // we want only Player-tagged objects to warp when hitting this Warp object
        if (!warper.CompareTag(GameManager.Tag.Player))
        {
            return false;
        }

        return true;
    }

    void OnTriggerEnter2D(Collider2D warper)
    {
        if (!CanWarp(warper))
        {
            return;
        }
        // warp when the player starts to touch the warp
        Warp(warper);
    }

    void OnTriggerStay2D(Collider2D warper)
    {
        if (!CanWarp(warper))
        {
            return;
        }
        // warp when the player is still inside the warp
        Warp(warper);
    }

    void Warp(Collider2D warper)
    {
        if (_pendingWarp is null)
        {
            WarpAttempt(warper.gameObject);
            return;
        }

        StopPlayer(_pendingWarp);
        if (WarpAttempt(_pendingWarp))
        {
            _pendingWarp = null;
            // Debug.LogWarning("[warp] warp pending OK");
        }
    }

    bool WarpAttempt(GameObject go)
    {
        PlayerController player = go.GetComponent<PlayerController>();

        if (player.IsMoving()) // is probably warping?
        {
            // Debug.Log("[warp] Player is still moving...");
            _pendingWarp = go;
            return false;
        }

        // WARP
        Vector2 coords = warpCoords + _coordsOffset;
        player.ClampPositionTo(new Vector3(coords.x, coords.y, 0));


        // MOVE
        if (go.HasComponent<PlayerController>())
        {
            // Debug.LogWarning("[warp] MOVING steps!");
            go.GetComponent<PlayerController>().Move(finalFacingDirection, moveSteps);
        }

        return true;
    }

    void StopPlayer(GameObject go)
    {
        if (!go.HasComponent<PlayerController>())
        {
            return;
        }

        PlayerController player = go.GetComponent<PlayerController>();

        if (player.IsMoving())
        {
            player.StopMoving();
        }
    }
}