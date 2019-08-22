using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/**
 * Warp Controller. Attach this controller to the object that moves an warps, usually the player.
 */
public class LegacyWarpController : MonoBehaviour
{
    public BoxCollider2D warperCollider;
    public LegacyMovementController legacyMovementController;
    public UnityEvent onWarpEnter;
    public UnityEvent onWarpStay;
    public UnityEvent onWarpFinish;

    private bool _warpingEnabled = true;
    private bool _isWarping = false;

    private void FixedUpdate()
    {
        if (_isWarping && !legacyMovementController.IsMoving())
        {
            _isWarping = false;
        }
    }

    public bool IsWarping()
    {
        return _isWarping;
    }

    public void EnableWarping()
    {
        _warpingEnabled = true;
    }

    public void DisableWarping()
    {
        _warpingEnabled = false;
    }

    public bool IsWarpingEnabled()
    {
        return _warpingEnabled;
    }

    public void WarpToDropStart(WarpZone destination)
    {
        Vector2 coords = destination.dropZone.transform.position.AsVector2() + destination.dropZoneOffset;
        legacyMovementController.ClampPositionTo(new Vector3(coords.x, coords.y, 0));
    }

    public void MoveToDropEnd(WarpZone destination)
    {
        if (destination.postDropMove.steps == 0)
        {
            if (destination.postDropMove.direction != MoveDirection.NONE)
            {
                legacyMovementController.TriggerButtons(destination.postDropMove.direction, ActionButton.NONE);
            }

            return;
        }

        if (!legacyMovementController.Move(destination.postDropMove.direction, destination.postDropMove.steps))
        {
            Debug.LogError("!!! WARPER CANNOT BE MOVED");
        }
    }

    public bool IsWarpZone(Collider2D other)
    {
        return other.gameObject.HasComponent<WarpZone>();
    }

    public WarpZone GetWarpZone(Collider2D other)
    {
        return other.gameObject.GetComponent<WarpZone>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsWarpZone(other) || !IsWarpingEnabled() || IsWarping())
        {
            return;
        }

        //Debug.Log("[warp] OnTriggerEnter2D");
        onWarpEnter.Invoke();
    }

    // For the OnTriggerStay2D event to work properly, the Rigid2D body Sleep Mode has to be on "Never Sleep", otherwise this is only triggered once
    void OnTriggerStay2D(Collider2D other)
    {
        if (!IsWarpZone(other) || !IsWarpingEnabled() || IsWarping())
        {
            return;
        }


        //Debug.Log("[warp] OnTriggerStay2D");
        _isWarping = true;
        WarpToDropStart(GetWarpZone(other));
        onWarpStay.Invoke();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsWarpZone(other) || !IsWarpingEnabled() || IsWarping())
        {
            return;
        }

        //Debug.Log("[warp] OnTriggerExit2D");
        MoveToDropEnd(GetWarpZone(other));
        onWarpFinish.Invoke();
    }
}