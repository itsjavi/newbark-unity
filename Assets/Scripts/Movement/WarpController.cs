using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using System.Collections;

/**
 * Warp Controller. Attach this controller to the object that moves an warps, usually the player.
 */
public class WarpController : InputConsumer
{
    public MovementController movementController;

    public GameObject fadeMask;

    public RectTransform popup;

    public Text popupText;

    [System.Serializable]
    public class OnEnterAreaEvent : UnityEvent<Transform> {}

    [System.Serializable]
    public class OnLeaveAreaEvent : UnityEvent<Transform> {}

    public OnEnterAreaEvent onEnterArea = new OnEnterAreaEvent();

    public OnLeaveAreaEvent onLeaveArea = new OnLeaveAreaEvent();

    private bool _isWarping = false;

    void Start()
    {
        this.onEnterArea.AddListener(PopZoneName);
        this.onLeaveArea.AddListener(HidePopedZoneName);
    }

    public void PopZoneName(Transform t)
    {
        var zoneInfo = t?.parent?.GetComponent<ZoneInfo>();
        if (zoneInfo && zoneInfo.popZoneNameOnEnter && zoneInfo.zoneName.Length > 0) {
            popup.anchoredPosition = new Vector2(popup.anchoredPosition.x, 0);
            popupText.text = zoneInfo.zoneName;
            iTween.ValueTo(fadeMask, iTween.Hash(
                "name", "AnimatePopupAnchoredPosY",
                "from", 0.0f,
                "to", popup.rect.height,
                "time", 0.2f,
                "delay", 2.5f,
                "onupdatetarget", this.gameObject,
                "onupdate", "UpdatePopupAnchoredPosY"
            ));
        }
    }

    private void UpdatePopupAnchoredPosY(float y)
    {
        popup.anchoredPosition = new Vector2(popup.anchoredPosition.x, y);
    }

    public void HidePopedZoneName(Transform t)
    {
        iTween.StopByName("AnimatePopupAnchoredPosY");
        popup.anchoredPosition = new Vector2(popup.anchoredPosition.x, popup.rect.height);

        // according to iTween document, WaitForSeconds is required after iTween.Stop() or StopByName()
        StartCoroutine(WaitInCoroutine(0.01f));
    }

    IEnumerator WaitInCoroutine(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

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

        var warpZone = GetWarpZone(other);

        iTween.ValueTo(fadeMask, iTween.Hash(
            "from", 0.0f,
            "to", 1.0f,
            "time", 0.6f,
            "onupdatetarget", this.gameObject,
            "onupdate", "UpdateFadeMaskAlpha",
            "oncompletetarget", this.gameObject,
            "oncomplete", "DropStart",
            "oncompleteparams", warpZone
        ));
    }

    // warping animation sequence begin
    private void UpdateFadeMaskAlpha(float alpha)
    {
        var image = fadeMask.GetComponent<Image>();
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    private void DropStart(WarpZone warpZone)
    {
        WarpToDropStart(warpZone);
        if (!InSameLogicScene(warpZone.transform, warpZone.dropZone)) {
            onLeaveArea?.Invoke(warpZone.transform);
            onEnterArea?.Invoke(warpZone.dropZone.transform);
        }

        // fade out
        iTween.ValueTo(fadeMask, iTween.Hash(
            "from", 1.0f,
            "to", 0.3f,
            "time", 0.6f,
            "delay", 0.1f,
            "onupdatetarget", this.gameObject,
            "onupdate", "UpdateFadeMaskAlpha",
            "oncompletetarget", this.gameObject,
            "oncomplete", "DropEnd",
            "oncompleteparams", warpZone
        ));
    }

    private void DropEnd(WarpZone warpZone)
    {
        MoveToDropEnd(warpZone);

        var image = fadeMask.GetComponent<Image>();
        image.enabled = false;
        this._isWarping = false;
        InputConsumerCenter.Instance.UnRegister(this);
    }
    // warping animation sequence end

    private bool InSameLogicScene(Transform src, Transform dst)
    {
        // now the hierachy is quite simpy, warp in same logic scene have same parent
        return src.parent == dst.parent;
    }

    void OnTriggerStay2D(Collider2D other)
    {
    }

    void OnTriggerExit2D(Collider2D other)
    {
    }
}