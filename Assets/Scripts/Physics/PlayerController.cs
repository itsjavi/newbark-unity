using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CellMovement movement;

    [Header("Movement")]
    public Animator animator;
    public int speed = 6;
    public int inputDelay = 8;
    public int tilesToMove = 1;
    public float clampAt = 0.5f;
    public float raycastDistance = 1f;

    [Header("Debug")]
    private int currentTilesToMove = 1;
    public GameObject lastCollidedObject;
    public DIRECTION_BUTTON lastCollisionDir = DIRECTION_BUTTON.NONE;

    void Start()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }

        currentTilesToMove = tilesToMove;

        movement = new CellMovement(inputDelay, clampAt);
    }

    void FixedUpdate()
    {
        DIRECTION_BUTTON dir = InputController.GetPressedDirectionButton();
        ACTION_BUTTON action = InputController.GetPressedActionButton();

        if (!CanMove() && IsMoving())
        {
            StopMoving();
        }

        if (CanMove())
        {
            MovementUpdate(dir);
        }

        RaycastUpdate(dir, action);
    }

    public DIRECTION_BUTTON GetFaceDirection()
    {
        if (animator.GetFloat("LastMoveX") > 0)
        {
            return DIRECTION_BUTTON.RIGHT;
        }
        if (animator.GetFloat("LastMoveX") < 0)
        {
            return DIRECTION_BUTTON.LEFT;
        }
        if (animator.GetFloat("LastMoveY") > 0)
        {
            return DIRECTION_BUTTON.UP;
        }
        if (animator.GetFloat("LastMoveY") < 0)
        {
            return DIRECTION_BUTTON.DOWN;
        }
        return DIRECTION_BUTTON.DOWN;
    }

    public Vector2 GetFaceDirectionVector()
    {
        switch (GetFaceDirection())
        {
            case DIRECTION_BUTTON.UP:
                return Vector2.up;
            case DIRECTION_BUTTON.DOWN:
                return Vector2.down;
            case DIRECTION_BUTTON.LEFT:
                return Vector2.left;
            case DIRECTION_BUTTON.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    public bool CanMove()
    {
        return !FindObjectOfType<DialogManager>().InDialog();
    }

    public bool CanMoveManually()
    {
        return CanMove();
    }

    private void MovementUpdate(DIRECTION_BUTTON dir)
    {
        if (movement != null && !movement.IsMoving)
        {
            currentTilesToMove = tilesToMove;
        }
        Move(dir, currentTilesToMove);

        Vector3 dirVector = Vector3.zero;
    }

    private void RaycastUpdate(DIRECTION_BUTTON dir, ACTION_BUTTON action)
    {
        Vector3 dirVector = GetFaceDirectionVector();

        if (dirVector == Vector3.zero)
        {
            return;
        }

        RaycastHit2D hit = CheckRaycast(dirVector);
        // Debug.DrawRay(transform.position, dirVector, Color.green);
        if (hit.collider)
        {
            // Debug.DrawRay(transform.position, dirVector, Color.red);
            // Debug.DrawRay(transform.position, hit.point, Color.blue);

            if (hit.collider.gameObject.HasComponent<Interactable>())
            {
                // Debug.Log("[raycast hit] @interactable " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<Interactable>().Interact(dir, action);
            }
        }
    }

    private RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = (Vector2)transform.position;

        return Physics2D.Raycast(startingPosition, direction, raycastDistance);
    }

    private RaycastHit2D CheckFutureRaycast(Vector2 direction)
    {
        Vector2 startingPosition = (Vector2)transform.position;

        return Physics2D.Raycast(startingPosition, direction, raycastDistance * 2);
    }

    public bool MoveTo(DIRECTION_BUTTON dir, Vector3 destinationPosition)
    {
        UpdateAnimation();

        if (!movement.IsMoving || (destinationPosition == transform.position))
        {
            ClampCurrentPosition();
            return false;
        }

        if (movement.IsMoving && (dir != DIRECTION_BUTTON.NONE) && (dir == lastCollisionDir))
        {
            ClampCurrentPosition();
            if (lastCollidedObject is GameObject)
            {
                PlayCollisionSound(lastCollidedObject);
            }
            return true;
        }
        else if (movement.IsMoving)
        {
            lastCollidedObject = null;
            lastCollisionDir = DIRECTION_BUTTON.NONE;
        }


        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, Time.deltaTime * speed);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        return true;
    }

    public bool Move(DIRECTION_BUTTON dir, int tiles)
    {
        currentTilesToMove = tiles;

        Vector3 destinationPosition = movement.CalculateDestinationPosition(
            transform.position, dir, tiles
        );

        return MoveTo(dir, destinationPosition);
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
        // Debug.Log("Collision ENTER between " + this.name + " and " + col.gameObject.name);

        lastCollidedObject = col.gameObject;
        lastCollisionDir = movement.LastDirection;

        ClampCurrentPosition();

        PlayCollisionSound(lastCollidedObject);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        // Debug.Log("Collision STAY between " + this.name + " and " + col.gameObject.name);

        lastCollidedObject = col.gameObject;
        lastCollisionDir = movement.LastDirection;

        if (movement.IsMoving)
        {
            PlayCollisionSound(lastCollidedObject);
        }

        StopMoving();
    }

    bool HasCollisionSound(GameObject gobj)
    {
        return gobj.HasComponent<AudioSource>();
    }

    AudioSource GetCollisionSound(GameObject gobj)
    {
        if (gobj.HasComponent<AudioSource>())
        {
            return gobj.GetComponent<AudioSource>();
        }
        return null;
    }

    void PlayCollisionSound(GameObject gobj)
    {
        AudioSource audioSource = GetCollisionSound(gobj);

        if (audioSource is AudioSource && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ClampCurrentPosition()
    {
        ClampPositionTo(transform.position);
    }

    public void StopMoving()
    {
        movement.Stop();
        UpdateAnimation();
        ClampCurrentPosition();
    }

    public bool IsMoving()
    {
        return movement.IsMoving;
    }

    public void ClampPositionTo(Vector3 position)
    {
        transform.position = movement.ClampPosition(position);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}