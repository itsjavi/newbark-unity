using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/**
 * Warp Controller. Attach this controller to the object that moves an warps, usually the player.
 */
public class WarpController : MonoBehaviour
{
    public BoxCollider2D warperCollider;
    public MovementController movementController;
    public UnityEvent onWarpEnter;
    public UnityEvent onWarpStay;
    public UnityEvent onWarpFinish;

    private bool _warpingEnabled = true;
    private bool _isWarping = false;

    private void FixedUpdate()
    {
        if (_isWarping && !movementController.IsMoving())
        {
            Debug.Log("NOT WARPING ANYMORE");
            _isWarping = false;
        }
    }

    public bool IsWarping()
    {
        return _isWarping;
    }

    public void EnableWarping()
    {
        Debug.Log("Enabled Warping");
        _warpingEnabled = true;
    }

    public void DisableWarping()
    {
        Debug.Log("Disabled Warping");
        _warpingEnabled = false;
    }

    public bool IsWarpingEnabled()
    {
        return _warpingEnabled;
    }

    private void WarpToDropStart(WarpZone destination)
    {
        Debug.Log("WarpToDropStart");
        Vector2 coords = destination.dropZone.transform.position.AsVector2() + destination.dropZoneOffset;
        movementController.ClampPositionTo(new Vector3(coords.x, coords.y, 0));
    }

    private void MoveToDropEnd(WarpZone destination)
    {
        Debug.Log("MoveToDropEnd");
        if (destination.postDropMove.steps == 0)
        {
            return;
        }
        if (!movementController.Move(destination.postDropMove.direction, destination.postDropMove.steps))
        {
            Debug.LogWarning("!!! WARPER CANNOT BE MOVED");
        }
    }

//    private void StopMoving()
//    {
//        Debug.Log("Stopping moving");
//        if (movementController.IsMoving())
//        {
//            movementController.StopMoving();
//        }
//    }

    private bool IsWarpZone(Collider2D other)
    {
        return other.gameObject.HasComponent<WarpZone>();
    }

    private WarpZone GetWarpZone(Collider2D other)
    {
        return other.gameObject.GetComponent<WarpZone>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsWarpZone(other))
        {
            return;
        }

        if (IsWarping())
        {
            Debug.Log("OnTriggerEnter2D: STILL warping");
            return;
        }

        if (!IsWarpingEnabled())
        {
            Debug.Log("OnTriggerEnter2D: warping is disabled");
            // prevent warper to move around while fading
            //StopMoving();
            return;
        }

        Debug.Log("OnTriggerEnter2D");
        onWarpEnter.Invoke();
    }

    // For the OnTriggerStay2D event to work properly, the Rigid2D body Sleep Mode has to be on "Never Sleep", otherwise this is only triggered once
    void OnTriggerStay2D(Collider2D other)
    {
        if (!IsWarpZone(other))
        {
            return;
        }

        if (IsWarping())
        {
            Debug.Log("OnTriggerStay2D: STILL warping");
            return;
        }

        if (!IsWarpingEnabled())
        {
            Debug.Log("OnTriggerStay2D: warping is disabled");
            // prevent warper to move around while fading
            //StopMoving();
            return;
        }

        Debug.Log("OnTriggerStay2D: ------- OK");
        _isWarping = true;
        WarpToDropStart(GetWarpZone(other));
        onWarpStay.Invoke();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsWarpZone(other))
        {
            return;
        }

        if (IsWarping())
        {
            Debug.Log("OnTriggerExit2D: STILL warping");
            return;
        }

        if (!IsWarpingEnabled())
        {
            Debug.Log("OnTriggerExit2D: warping is disabled");
            // prevent warper to move around while fading
            //StopMoving();
            return;
        }

        Debug.Log("OnTriggerExit2D");
        MoveToDropEnd(GetWarpZone(other));
        onWarpFinish.Invoke();
    }
}