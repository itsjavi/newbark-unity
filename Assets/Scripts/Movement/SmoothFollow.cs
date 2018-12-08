using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool delayed = true;
    public float delaySpeed = 320f, delayTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame after it is rendered
    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;

        if (delayed)
        {
            newPosition = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, delayTime, delaySpeed, Time.deltaTime);
        }

        transform.position = newPosition;
    }
}
