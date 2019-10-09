using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public Animator m_Animator;
    [Layer] public int interactableLayer = 8;
    public float raycastDistance = 1f;
    private static readonly int LastMoveX = Animator.StringToHash("LastMoveX");
    private static readonly int LastMoveY = Animator.StringToHash("LastMoveY");

    void Update()
    {
        ActionButton action = InputController.GetPressedActionButton();
        DirectionButton dir = GetLastDirection();
        Vector3 dirVector = InputController.GetDirectionButtonVector(GetLastDirection());
        RaycastHit2D hit = CheckInteractableRaycast(dirVector);

        if (!hit || !hit.collider || !hit.collider.gameObject.HasComponent<Interactable>())
        {
            Debug.DrawRay(transform.position, dirVector, Color.red);
            Debug.DrawRay(transform.position, hit.point, Color.blue);
            return;
        }

        Debug.DrawRay(transform.position, dirVector, Color.green);
        Debug.DrawRay(transform.position, hit.point, Color.blue);
        hit.collider.gameObject.GetComponent<Interactable>().Interact(dir, action);
    }

    private RaycastHit2D CheckInteractableRaycast(Vector2 direction)
    {
        Vector2 startingPosition = transform.position;
        return Physics2D.Raycast(
            startingPosition, direction, raycastDistance, 1 << interactableLayer
        );
    }

    public DirectionButton GetLastDirection()
    {
        if (m_Animator.GetFloat(LastMoveX) > 0)
        {
            return DirectionButton.RIGHT;
        }

        if (m_Animator.GetFloat(LastMoveX) < 0)
        {
            return DirectionButton.LEFT;
        }

        if (m_Animator.GetFloat(LastMoveY) > 0)
        {
            return DirectionButton.UP;
        }

        if (m_Animator.GetFloat(LastMoveY) < 0)
        {
            return DirectionButton.DOWN;
        }

        return DirectionButton.DOWN;
    }
}
