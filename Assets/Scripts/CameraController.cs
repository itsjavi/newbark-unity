using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject followedTarget;
    public int pixelsPerUnit = 16;
    public int verticalResolution = 288;
    public float speed = 5;
    public BoxCollider2D boundBox;
    private Vector3 followedTargetPos;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Camera cameraComponent;
    private float halfHeight;
    private float halfWidth;

    // Use this for initialization
    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        //cameraComponent.orthographicSize = verticalResolution / pixelsPerUnit / 2;
    }

    // Update is called once per frame
    void Update()
    {
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
        halfHeight = cameraComponent.orthographicSize;
        halfWidth = halfHeight * cameraComponent.aspect;

        followedTargetPos = new Vector3(followedTarget.transform.position.x, followedTarget.transform.position.y - 1, transform.position.z);
        //transform.position = Vector3.MoveTowards(transform.position, followedTargetPos, Time.deltaTime * speed);
        transform.position = followedTargetPos;

        float clampX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
}
