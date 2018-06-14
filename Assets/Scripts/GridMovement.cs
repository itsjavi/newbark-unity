using UnityEngine;

public enum DIRECTION
{
    UP, DOWN, LEFT, RIGHT
}

public class GridMovement : MonoBehaviour
{
    public int speed = 5;
    public int inputThreshold = 5;

    private bool canMove = true, moving = false;
    private int buttonCooldown = 0;
    private DIRECTION dir = DIRECTION.DOWN;
    private Vector3 pos;
    private Animator animator;
    private Vector2 animatorMove;
    private Vector2 animatorLastMove;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonCooldown > 0)
        {
            buttonCooldown--;
        }
        if (canMove)
        {
            pos = transform.position;
            calcMovement();
        }
        if (moving)
        {
            if (transform.position == pos)
            {
                // done moving in a tile
                moving = false;
                canMove = true;
                calcMovement();
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        } else {
            animatorMove.x = 0;
            animatorMove.y = 0;
        }

        animator.SetFloat("MoveX", animatorMove.x);
        animator.SetFloat("MoveY", animatorMove.y);
        animator.SetFloat("LastMoveX", animatorLastMove.x);
        animator.SetFloat("LastMoveY", animatorLastMove.y);
        animator.SetBool("Moving", moving);
    }

    private void calcMovement()
    {
        if (buttonCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (dir != DIRECTION.UP)
                {
                    buttonCooldown = inputThreshold;
                    dir = DIRECTION.UP;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.up;
                    animatorMove.x = 0;
                    animatorMove.y = 1;
                    animatorLastMove.x = 0;
                    animatorLastMove.y = 1;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (dir != DIRECTION.DOWN)
                {
                    buttonCooldown = inputThreshold;
                    dir = DIRECTION.DOWN;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.down;
                    animatorMove.x = 0;
                    animatorMove.y = -1;
                    animatorLastMove.x = 0;
                    animatorLastMove.y = -1;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (dir != DIRECTION.LEFT)
                {
                    buttonCooldown = inputThreshold;
                    dir = DIRECTION.LEFT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.left;
                    animatorMove.x = -1;
                    animatorMove.y = 0;
                    animatorLastMove.x = -1;
                    animatorLastMove.y = 0;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (dir != DIRECTION.RIGHT)
                {
                    buttonCooldown = inputThreshold;
                    dir = DIRECTION.RIGHT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.right;
                    animatorMove.x = 1;
                    animatorMove.y = 0;
                    animatorLastMove.x = 1;
                    animatorLastMove.y = 0;
                }
            }
        }
    }
}
