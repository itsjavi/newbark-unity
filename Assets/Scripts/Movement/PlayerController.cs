using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 6;
    public int inputDelay = 8;
    public int tilesToMove = 1;
    public Animator animator;
    public Rigidbody2D body;
    private CellMovement movement;

    // for debugging in the editor:
    public CollisionData lastCollisionData;
    public DIRECTION_BUTTON lastCollisionDir = DIRECTION_BUTTON.NONE;

    // Use this for initialization
    void Start()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (!body)
        {
            body = GetComponent<Rigidbody2D>();
        }

        movement = new CellMovement(inputDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DIRECTION_BUTTON dir = InputController.GetPressedDirectionButton();

        Vector3 destinationPosition = movement.CalculateDestinationPosition(
            transform.position, dir, tilesToMove
        );

        UpdateAnimation();

        if (!movement.IsMoving || (destinationPosition == transform.position))
        {
            // No need to update
            StopMoving();
            return;
        }

        if (movement.IsMoving && (dir != DIRECTION_BUTTON.NONE) && (dir == lastCollisionDir))
        {
            // Debug.Log("cannot continue in this direction");
            StopMoving();
            if (lastCollisionData is CollisionData)
            {
                lastCollisionData.PlaySound();
            }
            return;
        }
        else if (movement.IsMoving)
        {
            // cleared
            lastCollisionData = null;
            lastCollisionDir = DIRECTION_BUTTON.NONE;
        }


        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, Time.deltaTime * speed);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        return;

        // Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);

        //Vector2 newPosition = new Vector2(((Vector3)(destinationPosition)).x * Time.deltaTime * speed, ((Vector3)(destinationPosition)).y * Time.deltaTime * speed);
        //body.MovePosition(newPosition);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("MoveX", movement.PositionDiff.x);
        animator.SetFloat("MoveY", movement.PositionDiff.y);
        animator.SetFloat("LastMoveX", movement.LastPositionDiff.x);
        animator.SetFloat("LastMoveY", movement.LastPositionDiff.y);
        animator.SetBool("Moving", movement.IsMoving);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        CollisionData data = col.gameObject.GetComponent<CollisionData>();
        COLLISION_TYPE collisionType = data ? data.type : COLLISION_TYPE.UNKNOWN;

        lastCollisionData = data;
        lastCollisionDir = movement.LastDirection;
        StopMoving();

        // detect collision type
        Debug.Log("COLLISION ENTER on: " + col.gameObject.transform.position.ToString() + ", button: " + lastCollisionDir + ", type: " + collisionType.ToString());
        // TODO: play collision sound

        if (data is CollisionData)
        {
            data.PlaySound();
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        var data = col.gameObject.GetComponent<CollisionData>();
        COLLISION_TYPE collisionType = data ? data.type : COLLISION_TYPE.UNKNOWN;

        lastCollisionData = data;
        lastCollisionDir = movement.LastDirection;
        movement.Stop();
        UpdateAnimation();
        transform.position = movement.FixPosition(transform.position);
        transform.rotation = new Quaternion(0, 0, 0, 0);

       // detect collision type
       Debug.Log("COLLISION STAY on: " + col.gameObject.transform.position.ToString() + ", button: " + lastCollisionDir + ", type: " + collisionType.ToString());
    }

    void StopMoving()
    {
        //movement.Stop();
        //UpdateAnimation();
        transform.position = movement.FixPosition(transform.position);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}