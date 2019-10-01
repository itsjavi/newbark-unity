using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using DG.Tweening;

/**
 * Warp Controller. Attach this controller to the object that moves an warps, usually the player.
 */
public class WarpController : InputConsumer
{
    public MovementController movementController;

    public GameObject fadeMask;
    private bool _isWarping = false;

    private void WarpToDropStart(WarpZone destination)
    {
        Vector2 coords = destination.dropZone.transform.position.AsVector2() + destination.dropZoneOffset;
        movementController.ClampPositionTo(new Vector3(coords.x, coords.y, 0));

        if (destination.postDropMove.direction != DIRECTION_BUTTON.NONE)
            movementController.FaceToDir(destination.postDropMove.direction);
    }

    public override void OnUpdateHandleInput()
    {
        // do nothing to eat input
    }

    private void MoveToDropEnd(WarpZone destination)
    {
        if (destination.postDropMove.steps > 0)
            movementController.HandleMoveInput(destination.postDropMove.direction, destination.postDropMove.steps);
    }

    private bool IsWarpZone(Collider2D other)
    {
        return other.gameObject.HasComponent<WarpZone>();
    }

    private WarpZone GetWarpZone(Collider2D other)
    {
        return other.gameObject.GetComponent<WarpZone>();
    }

    private WarpController weakToStrong(WeakReference<WarpController> weakRef)
    {
        bool isAlive = weakRef.TryGetTarget(out WarpController target);
        return target;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsWarpZone(other))
            return;

        if (_isWarping)
            return;

        _isWarping = true;
        InputConsumerCenter.Instance.Register(this);

        // start warping
        var image = fadeMask.GetComponent<Image>();

        Color color = image.color;
        color.a = 0;
        image.color = color;
        image.enabled = true;

        var weakThis = new WeakReference<WarpController>(this);
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOFade(1, 0.6f));
        sequence.AppendCallback(() => {
            // do move player
            var strongThis = weakToStrong(weakThis);
            if (!strongThis)
                return;

            var warpZone = GetWarpZone(other);
            if (warpZone) {
                WarpToDropStart(warpZone);
                // may support same scene warping in future. Same scene warping should not call onLeave/onEnter
                // use hierachy tree to check if src and dst zone in same (logic) scene
                // if (NotSameScene(warpZone, warpZone.dropZone))
                warpZone.onLeaveArea?.Invoke();
            }
        });
        sequence.Append(image.DOFade(0, 0.4f));
        sequence.AppendCallback(() => {
            // warping terminated
            var strongThis = weakToStrong(weakThis);
            if (!strongThis)
                return;

            var warpZone = GetWarpZone(other);
            if (warpZone) {
                MoveToDropEnd(warpZone);
                // may support same scene warping in future. Same scene warping should not call onLeave/onEnter
                // use hierachy tree to check if src and dst zone in same (logic) scene
                // if (NotSameScene(warpZone, warpZone.dropZone))
                warpZone.onEnterArea?.Invoke();
            }

            image.enabled = false;
            strongThis._isWarping = false;
            InputConsumerCenter.Instance.UnRegister(strongThis);
        });
    }

    void OnTriggerStay2D(Collider2D other)
    {
    }

    void OnTriggerExit2D(Collider2D other)
    {
    }
}