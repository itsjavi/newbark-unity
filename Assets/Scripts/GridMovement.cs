using UnityEngine;

public enum DIRECTION
{
    NONE, UP, DOWN, LEFT, RIGHT
}

public class GridMovement : MonoBehaviour
{
    public int speed = 5;
    public int inputSpeed = 1;

    private bool canReadInput = true, moving = false;
    private int buttonCooldown = 0;
    private DIRECTION dir = DIRECTION.DOWN;
    private Vector3 pos;
    private Animator animator;
    private Vector2 animatorMove;
    private Vector2 animatorLastMove;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonCooldown > 0)
        {
            buttonCooldown--;
        }
        if (canReadInput)
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
                canReadInput = true;
                calcMovement();
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        }
        else
        {
            animatorMove.x = 0;
            animatorMove.y = 0;
        }

        animator.SetFloat("MoveX", animatorMove.x);
        animator.SetFloat("MoveY", animatorMove.y);
        animator.SetFloat("LastMoveX", animatorLastMove.x);
        animator.SetFloat("LastMoveY", animatorLastMove.y);
        animator.SetBool("Moving", moving);
    }

    private DIRECTION getPressedDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return DIRECTION.UP;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return DIRECTION.DOWN;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return DIRECTION.LEFT;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return DIRECTION.RIGHT;
        }
        return DIRECTION.NONE;
    }

    private void calcMovement()
    {

        if (buttonCooldown > 0)
        {
            return;
        }

        DIRECTION newDir = getPressedDirection();

        if (newDir == DIRECTION.NONE)
        {
            return;
        }

        float x = 0, y = 0;

        switch (newDir)
        {
            case DIRECTION.UP:
                {
                    y = 1;
                }
                break;
            case DIRECTION.RIGHT:
                {
                    x = 1;
                }
                break;
            case DIRECTION.DOWN:
                {
                    y = -1;
                }
                break;
            case DIRECTION.LEFT:
                {
                    x = -1;
                }
                break;
            default:
                break;
        }

        animatorLastMove.x = x;
        animatorLastMove.y = y;

        if (dir != newDir)
        {
            buttonCooldown = inputSpeed;
            dir = newDir;

            return;
        }

        canReadInput = false;
        moving = true;
        pos += new Vector3(x, y, 0);
        animatorMove.x = x;
        animatorMove.y = y;
    }
}
