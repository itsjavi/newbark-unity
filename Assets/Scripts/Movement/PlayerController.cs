using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 6;
    public int inputDelay = 8;
    public int tilesToMove = 1;
    public Animator animator;
    private CellMovement movement;

    // Use this for initialization
    void Start()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
        movement = new CellMovement(inputDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3? destinationPosition = movement.CalculateDestinationPosition(
            transform.position, InputController.GetPressedDirectionButton(), tilesToMove
        );

        animator.SetFloat("MoveX", movement.PositionDiff.x);
        animator.SetFloat("MoveY", movement.PositionDiff.y);
        animator.SetFloat("LastMoveX", movement.LastPositionDiff.x);
        animator.SetFloat("LastMoveY", movement.LastPositionDiff.y);
        animator.SetBool("Moving", movement.IsMoving);

        if (destinationPosition == null)
        {
            // No need to update
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, (Vector3)(destinationPosition), Time.deltaTime * speed);
    }
}
