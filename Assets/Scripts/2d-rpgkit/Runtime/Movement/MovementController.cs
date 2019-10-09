using UnityEngine;

public class MovementController : MonoBehaviour
{
    private bool canReadInput = true;
    private int coolDown = 0;

    private bool isMoving = false;

    private DirectionButton lastDirection = DirectionButton.NONE;
    public DirectionButton LastDirection => lastDirection;

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
    public DirectionButton lastCollisionDir = DirectionButton.NONE;

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

        DirectionButton dir = InputController.GetPressedDirectionButton();
        ActionButton action = InputController.GetPressedActionButton();

        TriggerButtons(dir, action);
    }

    public DirectionButton GetFaceDirection()
    {
        if (animator.GetFloat("LastMoveX") > 0)
        {
            return DirectionButton.RIGHT;
        }

        if (animator.GetFloat("LastMoveX") < 0)
        {
            return DirectionButton.LEFT;
        }

        if (animator.GetFloat("LastMoveY") > 0)
        {
            return DirectionButton.UP;
        }

        if (animator.GetFloat("LastMoveY") < 0)
        {
            return DirectionButton.DOWN;
        }

        return DirectionButton.DOWN;
    }

    public Vector2 GetFaceDirectionVector()
    {
        switch (GetFaceDirection())
        {
            case DirectionButton.UP:
                return Vector2.up;
            case DirectionButton.DOWN:
                return Vector2.down;
            case DirectionButton.LEFT:
                return Vector2.left;
            case DirectionButton.RIGHT:
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

    private void MovementUpdate(DirectionButton dir)
    {
        if (!isMoving)
        {
            currentTilesToMove = tilesToMove;
        }

        Move(dir, currentTilesToMove);
    }

    private void RaycastUpdate(ActionButton action)
    {
        Vector3 dirVector = GetFaceDirectionVector();
        RaycastHit2D hit = CheckRaycast(dirVector);

        if (!hit.collider || !hit.collider.gameObject.HasComponent<Interactable>())
        {
            return;
        }

        hit.collider.gameObject.GetComponent<Interactable>().Interact(action);
    }

    private RaycastHit2D CheckRaycast(Vector2 direction, float distance)
    {
        Vector2 startingPosition = (Vector2) transform.position;

        return Physics2D.Raycast(startingPosition, direction, distance);
    }

    private RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = (Vector2) transform.position;

        return CheckRaycast(direction, raycastDistance);
    }

    public bool MoveTo(DirectionButton dir, Vector3 destPosition)
    {
        UpdateAnimation();

        if (!isMoving || (destPosition == transform.position))
        {
            ClampCurrentPosition();
            return false;
        }

        if (isMoving && (dir != DirectionButton.NONE) && (dir == lastCollisionDir))
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
            lastCollisionDir = DirectionButton.NONE;
        }


        transform.position = Vector3.MoveTowards(transform.position, destPosition, Time.deltaTime * speed);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        return true;
    }

    public bool Move(DirectionButton dir, int tiles)
    {
        currentTilesToMove = tiles;

        Vector3 destPosition = CalculateDestinationPosition(
            transform.position, dir, tiles
        );

        return MoveTo(dir, destPosition);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("MoveX", PositionDiff.x);
        animator.SetFloat("MoveY", PositionDiff.y);
        animator.SetFloat("LastMoveX", LastPositionDiff.x);
        animator.SetFloat("LastMoveY", LastPositionDiff.y);
        animator.SetBool("Moving", isMoving);
    }

    public void TriggerButtons(DirectionButton dir, ActionButton action)
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
        
    // ---------------------------------------------------

    public void ClampPositionTo(Vector3 position)
    {
        transform.position = ClampPosition(position);

        // override in case collision physics caused object rotation
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }


    public Vector3 CalculateDestinationPosition(Vector3 origin, DirectionButton dir, int tiles = 1)
    {
        if (coolDown > 0)
        {
            coolDown--;
        }

        if (canReadInput)
        {
            destinationPosition = origin;
            CalculateMovement(dir, tiles);
        }

        if (isMoving)
        {
            if (origin == destinationPosition)
            {
                // done moving in a tile
                isMoving = false;
                canReadInput = true;
                CalculateMovement(dir, tiles);
            }

            if (origin == destinationPosition)
            {
                return origin;
            }

            return destinationPosition;
        }

        positionDiff.x = 0;
        positionDiff.y = 0;
        positionDiff.z = 0;

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
    private void CalculateMovement(DirectionButton dir, int tiles = 1)
    {
        if (coolDown > 0)
        {
            return;
        }

        if (dir == DirectionButton.NONE)
        {
            return;
        }

        float x = 0, y = 0, z = 0;

        switch (dir)
        {
            case DirectionButton.UP:
            {
                y = tiles;
            }
                break;
            case DirectionButton.RIGHT:
            {
                x = tiles;
            }
                break;
            case DirectionButton.DOWN:
            {
                y = tiles * -1;
            }
                break;
            case DirectionButton.LEFT:
            {
                x = tiles * -1;
            }
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
