using UnityEngine;
using UnityEngine.Events;

/**
 * Warp Controller. Attach this controller to the object that moves an warps, usually the player.
 */
public class WarpController : MonoBehaviour
{
    public BoxCollider2D warperCollider;
    public MovementController movementController;
    public UnityEvent onWarpTrigger;
    public UnityEvent onWarpStart;
    public UnityEvent onWarpFinish;
    private bool _warpingEnabled = true;
    private WarpZone _currentWarpZone;

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

    public bool IsWarping()
    {
        return !(_currentWarpZone is null);
    }

    private bool IsDifferentWarpZone(Collider2D other)
    {
        return _currentWarpZone && (_currentWarpZone != GetWarpZone(other));
    }

    private void WarpToDropStart(WarpZone destination)
    {
        Debug.Log("WarpToDropStart");

        // disable to avoid collisions with the destination drop point (in case it is another collider) 
        // warperCollider.enabled = false;
        Vector2 correctionCoords = new Vector2(0f, 0f);
        Vector2 coords = destination.dropStartZone.transform.position.AsVector2() + correctionCoords;
        movementController.ClampPositionTo(new Vector3(coords.x, coords.y, 0));
    }

    private void MoveToDropEnd(WarpZone destination)
    {
        // warperCollider.enabled = true;
        Debug.Log("MoveToDropEnd");
        movementController.Move(destination.dropEndZone.direction, destination.dropEndZone.steps);
    }

    private void StopMoving()
    {
        Debug.Log("Stopping moving");
        if (movementController.IsMoving())
        {
            movementController.StopMoving();
        }
    }

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
        if (!IsWarpZone(other) || IsDifferentWarpZone(other))
        {
            return;
        }

        Debug.Log("OnTriggerEnter2D");
        _currentWarpZone = GetWarpZone(other);

        onWarpTrigger.Invoke();
    }

    // For the OnTriggerStay2D event to work properly, the Rigid2D body Sleep Mode has to be on "Never Sleep", otherwise this is only triggered once
    void OnTriggerStay2D(Collider2D other)
    {
        if (!IsWarpZone(other) || IsDifferentWarpZone(other))
        {
            return;
        }

        _currentWarpZone = GetWarpZone(other);

        Debug.Log("OnTriggerStay2D");

        if (!IsWarpingEnabled())
        {
            Debug.Log("OnTriggerStay2D: warping is disabled");
            // prevent warper to move around while fading
            //StopMoving();
            return;
        }

        onWarpStart.Invoke();
        WarpToDropStart(_currentWarpZone);
        MoveToDropEnd(_currentWarpZone);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsWarpZone(other) || IsDifferentWarpZone(other))
        {
            return;
        }

        Debug.Log("OnTriggerExit2D");

        onWarpFinish.Invoke();
        _currentWarpZone = null;
    }
}