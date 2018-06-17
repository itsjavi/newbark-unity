using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 6;
    public int inputDelay = 6;
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
        movement.Move(transform, InputController.GetPressedDirectionButton(), Time.deltaTime * speed, tilesToMove);

        animator.SetFloat("MoveX", movement.PositionDiff.x);
        animator.SetFloat("MoveY", movement.PositionDiff.y);
        animator.SetFloat("LastMoveX", movement.LastPositionDiff.x);
        animator.SetFloat("LastMoveY", movement.LastPositionDiff.y);
        animator.SetBool("Moving", movement.IsMoving);
    }
}
