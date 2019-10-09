using UnityEngine;

public class MovementController : MonoBehaviour
{
    private bool canReadInput = true;
    private int coolDown = 0;

    private bool isMoving = false;

    private DIRECTION_BUTTON lastDirection = DIRECTION_BUTTON.NONE;
    public DIRECTION_BUTTON LastDirection => lastDirection;

    private Vector3 destinationPosition;
    private Vector3 positionDiff;
    public Vector3 PositionDiff => positionDiff;

    private Vector3 lastPositionDiff;
    public Vector3 LastPositionDiff => lastPositionDiff;

    [Header("Movement")] public Animator animator;
    public int speed = 6;
    public int inputDelay = 8;
    public int tilesToMove = 1;
    public float clampAt = 0.5f;
    public float raycastDistance = 1f;

    [Header("Debug")] private int currentTilesToMove = 1;
    public GameObject lastCollidedObject;
    public DIRECTION_BUTTON lastCollisionDir = DIRECTION_BUTTON.NONE;

    void Start()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }

        currentTilesToMove = tilesToMove;
    }

    void FixedUpdate()
    {
        if (!CanMove() && IsMoving())
        {
            StopMoving();
        }

        DIRECTION_BUTTON dir = InputController.GetPressedDirectionButton();
        ACTION_BUTTON action = InputController.GetPressedActionButton();

        TriggerButtons(dir, action);
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
        // TODO optimize with events
        return !FindObjectOfType<DialogManager>().InDialog();
    }

    public bool CanMoveManually()
    {
        return CanMove();
    }

    private void MovementUpdate(DIRECTION_BUTTON dir)
    {
        if (!isMoving)
        {
            currentTilesToMove = tilesToMove;
        }

        Move(dir, currentTilesToMove);
    }

    private void RaycastUpdate(ACTION_BUTTON action)
    {
        Vector3 dirVector = GetFaceDirectionVector();
        RaycastHit2D hit = CheckRaycast(dirVector);

        if (!hit.collider || !hit.collider.gameObject.HasComponent<Interactable>())
        {
            return;
        }

        hit.collider.gameObject.GetComponent<Interactable>().Interact(action);
    }

    private RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = (Vector2) transform.position;

        return Physics2D.Raycast(startingPosition, direction, raycastDistance);
    }

    private RaycastHit2D CheckFutureRaycast(Vector2 direction)
    {
        Vector2 startingPosition = (Vector2) transform.position;

        return Physics2D.Raycast(startingPosition, direction, raycastDistance * 2);
    }

    public bool MoveTo(DIRECTION_BUTTON dir, Vector3 destinationPosition)
    {
        UpdateAnimation();

        if (!isMoving || (destinationPosition == transform.position))
        {
            ClampCurrentPosition();
            return false;
        }

        if (isMoving && (dir != DIRECTION_BUTTON.NONE) && (dir == lastCollisionDir))
        {
            ClampCurrentPosition();
            if (!(lastCollidedObject is null))
            {
                PlayCollisionSound(lastCollidedObject);
            }

            return true;
        }
        else if (isMoving)
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

        Vector3 destinationPosition = CalculateDestinationPosition(
            transform.position, dir, tiles
        );

        return MoveTo(dir, destinationPosition);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("MoveX", PositionDiff.x);
        animator.SetFloat("MoveY", PositionDiff.y);
        animator.SetFloat("LastMoveX", LastPositionDiff.x);
        animator.SetFloat("LastMoveY", LastPositionDiff.y);
        animator.SetBool("Moving", isMoving);
    }

    public void TriggerButtons(DIRECTION_BUTTON dir, ACTION_BUTTON action)
    {
        if (!CanMove() && IsMoving())
        {
            StopMoving();
        }

        if (CanMove())
        {
            MovementUpdate(dir);
        }

        RaycastUpdate(action);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("Collision ENTER between " + this.name + " and " + col.gameObject.name);

        lastCollidedObject = col.gameObject;
        lastCollisionDir = LastDirection;

        ClampCurrentPosition();

        PlayCollisionSound(lastCollidedObject);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        // Debug.Log("Collision STAY between " + this.name + " and " + col.gameObject.name);

        lastCollidedObject = col.gameObject;
        lastCollisionDir = LastDirection;

        if (isMoving)
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
        if (!HasCollisionSound(gobj))
        {
            return null;
        }

        gobj.TryGetComponent(typeof(AudioSource), out Component aud);
        return (AudioSource) aud;
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
        Stop();
        UpdateAnimation();
        ClampCurrentPosition();
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void ClampPositionTo(Vector3 position)
    {
        transform.position = ClampPosition(position);

        // override in case collision physics caused object rotation
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }


    public Vector3 CalculateDestinationPosition(Vector3 origin, DIRECTION_BUTTON dir, int tilesToMove = 1)
    {
        if (coolDown > 0)
        {
            coolDown--;
        }

        if (canReadInput)
        {
            destinationPosition = origin;
            CalculateMovement(dir, tilesToMove);
        }

        if (isMoving)
        {
            if (origin == destinationPosition)
            {
                // done moving in a tile
                isMoving = false;
                canReadInput = true;
                CalculateMovement(dir, tilesToMove);
            }

            if (origin == destinationPosition)
            {
                return origin;
            }

            return destinationPosition;
        }
        else
        {
            positionDiff.x = 0;
            positionDiff.y = 0;
            positionDiff.z = 0;
        }

        return ClampPosition(origin);
    }

    public Vector3 ClampPosition(Vector3 position)
    {
        Vector3 fixedPos = new Vector3(ClampPositionAxis(position.x), ClampPositionAxis(position.y), 0);
        return fixedPos;
    }

    private float ClampPositionAxis(float val)
    {
        float mod = val % 1f;

        if (System.Math.Abs(mod - clampAt) < double.Epsilon) // more precise than: if (mod == fraction)
        {
            return val;
        }

        if (val < 0f)
        {
            return (val - mod) - clampAt;
        }

        return (val - mod) + clampAt;
    }

    public void Stop()
    {
        positionDiff.x = 0;
        positionDiff.y = 0;
        coolDown = 0;
        isMoving = false;
        canReadInput = true;
    }

    // Returns the calculated final destination vector
    private void CalculateMovement(DIRECTION_BUTTON dir, int tilesToMove = 1)
    {
        if (coolDown > 0)
        {
            return;
        }

        if (dir == DIRECTION_BUTTON.NONE)
        {
            return;
        }

        float x = 0, y = 0, z = 0;

        switch (dir)
        {
            case DIRECTION_BUTTON.UP:
            {
                y = tilesToMove;
            }
                break;
            case DIRECTION_BUTTON.RIGHT:
            {
                x = tilesToMove;
            }
                break;
            case DIRECTION_BUTTON.DOWN:
            {
                y = tilesToMove * -1;
            }
                break;
            case DIRECTION_BUTTON.LEFT:
            {
                x = tilesToMove * -1;
            }
                break;
            default:
                break;
        }

        lastPositionDiff.x = x;
        lastPositionDiff.y = y;
        lastPositionDiff.z = z;

        if (lastDirection != dir)
        {
            coolDown = inputDelay;
            lastDirection = dir;

            return;
        }

        canReadInput = false;
        isMoving = true;

        positionDiff.x = x;
        positionDiff.y = y;
        positionDiff.z = z;

        destinationPosition += positionDiff;
        destinationPosition = ClampPosition(destinationPosition);
    }
}
