using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WarpController : MonoBehaviour
{
    public Vector2 warpCoords;
    public int moveSteps = 0;
    public DIRECTION_BUTTON finalFacingDirection = DIRECTION_BUTTON.NONE;
    private GameObject _pendingWarp;
    private Vector2 _pivot = new Vector2(0.5f, 0.5f);

    void OnTriggerEnter2D(Collider2D other)
    {
        // warp when the player starts to touch the warp
        Warp(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // warp when the player is still inside the warp
        Warp(other);
    }

    void Warp(Collider2D other)
    {
        // we don't want other objects to warp when hitting this Warp object, only Player-tagged objects
        if(!other.CompareTag(GameManager.Tag.Player))
        {
            return;
        }
        
        if (_pendingWarp is null)
        {
            WarpAttempt(other.gameObject);
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


        if (go.HasComponent<BoxCollider2D>())
        {
            // Debug.LogWarning("[warp] Disabling player collider");
            go.GetComponent<BoxCollider2D>().enabled = false;
        }

        // WARP
        Vector2 coords = warpCoords + _pivot;
        player.ClampPositionTo(new Vector3(coords.x, coords.y, 0));


        // MOVE
        if (go.HasComponent<PlayerController>())
        {
            // Debug.LogWarning("[warp] MOVING steps!");
            go.GetComponent<PlayerController>().Move(finalFacingDirection, moveSteps);
        }

        if (go.HasComponent<BoxCollider2D>())
        {
            // Debug.LogWarning("[warp] Enabling player collider");
            go.GetComponent<BoxCollider2D>().enabled = true;
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
