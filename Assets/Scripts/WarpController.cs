using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour
{
    public Vector2 warpCoords;
    public int moveSteps = 0;
    public DIRECTION_BUTTON moveDirection = DIRECTION_BUTTON.NONE;
    private GameObject pendingWarp;
    private Vector2 pivot = new Vector2(0.5f, 0.5f);

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[warp] Trigger ENTER between " + this.name + " and " + other.gameObject.name);
        Warp(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("[warp] Trigger STAY between " + this.name + " and " + other.gameObject.name);
        Warp(other);
    }

    void Warp(Collider2D other)
    {
        if ((pendingWarp is GameObject) == false)
        {
            WarpAttempt(other.gameObject);
            return;
        }

        StopPlayer(pendingWarp);
        if (WarpAttempt(pendingWarp))
        {
            pendingWarp = null;
            Debug.LogWarning("[warp] warp pending OK");
        }
    }

    bool WarpAttempt(GameObject go)
    {
        PlayerController player = go.GetComponent<PlayerController>();

        if (player.IsMoving()) // is probably warping?
        {
            // Debug.Log("[warp] Player is still moving...");
            pendingWarp = go;
            return false;
        }


        if (go.HasComponent<BoxCollider2D>())
        {
            Debug.LogWarning("[warp] Disabling player collider");
            go.GetComponent<BoxCollider2D>().enabled = false;
        }

        // WARP
        Vector2 coords = warpCoords + pivot;
        player.ClampPositionTo(new Vector3(coords.x, coords.y, 0));


        // MOVE
        if (go.HasComponent<PlayerController>())
        {
            Debug.LogWarning("[warp] MOVING steps!");
            go.GetComponent<PlayerController>().Move(moveDirection, moveSteps);
        }

        if (go.HasComponent<BoxCollider2D>())
        {
            Debug.LogWarning("[warp] Enabling player collider");
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
