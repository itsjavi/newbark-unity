using UnityEngine;

public class MovementController : MonoBehaviour
{
    private CellMovement movement;

    [Header("Movement")] public Animator animator;
    public int speed = 6;
    public int inputDelay = 8;
    public int tilesToMove = 1;
    public float clampAt = 0.5f;
    public float raycastDistance = 1f;

    [Header("Debug")] private int currentTilesToMove = 1;
    public GameObject lastCollidedObject;
    public MoveDirection lastCollisionDir = MoveDirection.NONE;

    // TODO: REMOVE
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
        if (!CanMove() && IsMoving())
        {
            StopMoving();
        }

        MoveDirection dir = InputManager.GetPressedDirectionButton();
        ActionButton action = InputManager.GetPressedActionButton();

        TriggerButtons(dir, action);
    }

    // TODO: REMOVE
    public MoveDirection GetFaceDirection()
    {
        if (animator.GetFloat("LastMoveX") > 0)
        {
            return MoveDirection.RIGHT;
        }

        if (animator.GetFloat("LastMoveX") < 0)
        {
            return MoveDirection.LEFT;
        }

        if (animator.GetFloat("LastMoveY") > 0)
        {
            return MoveDirection.UP;
        }

        if (animator.GetFloat("LastMoveY") < 0)
        {
            return MoveDirection.DOWN;
        }

        return MoveDirection.DOWN;
    }

    // TODO: REMOVE
    public Vector2 GetFaceDirectionVector()
    {
        switch (GetFaceDirection())
        {
            case MoveDirection.UP:
                return Vector2.up;
            case MoveDirection.DOWN:
                return Vector2.down;
            case MoveDirection.LEFT:
                return Vector2.left;
            case MoveDirection.RIGHT:
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

    private void MovementUpdate(MoveDirection dir)
    {
        if (movement != null && !movement.IsMoving)
        {
            currentTilesToMove = tilesToMove;
        }

        Move(dir, currentTilesToMove);
    }

    // TODO: REMOVE
    private void RaycastUpdate(MoveDirection dir, ActionButton action)
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

    // TODO: REMOVE
    private RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = (Vector2) transform.position;

        return Physics2D.Raycast(startingPosition, direction, raycastDistance);
    }

    public bool MoveTo(MoveDirection dir, Vector3 destinationPosition)
    {
        UpdateAnimation();

        if (!movement.IsMoving || (destinationPosition == transform.position))
        {
            ClampCurrentPosition();
            return false;
        }

        if (movement.IsMoving && (dir != MoveDirection.NONE) && (dir == lastCollisionDir))
        {
            ClampCurrentPosition();
            if (!(lastCollidedObject is null))
            {
                PlayCollisionSound(lastCollidedObject);
            }

            return true;
        }
        else if (movement.IsMoving)
        {
            lastCollidedObject = null;
            lastCollisionDir = MoveDirection.NONE;
        }


        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, Time.deltaTime * speed);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        return true;
    }

    public bool Move(MoveDirection dir, int tiles)
    {
        currentTilesToMove = tiles;

        Vector3 destinationPosition = movement.CalculateDestinationPosition(
            transform.position, dir, tiles
        );

        return MoveTo(dir, destinationPosition);
    }

    // TODO: REMOVE
    private void UpdateAnimation()
    {
        animator.SetFloat("MoveX", movement.PositionDiff.x);
        animator.SetFloat("MoveY", movement.PositionDiff.y);
        animator.SetFloat("LastMoveX", movement.LastPositionDiff.x);
        animator.SetFloat("LastMoveY", movement.LastPositionDiff.y);
        animator.SetBool("Moving", movement.IsMoving);
    }

    public void TriggerButtons(MoveDirection dir, ActionButton action)
    {
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

    // TODO: REMOVE
    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("Collision ENTER between " + this.name + " and " + col.gameObject.name);

        lastCollidedObject = col.gameObject;
        lastCollisionDir = movement.LastMoveDirection;

        ClampCurrentPosition();

        PlayCollisionSound(lastCollidedObject);
    }

    // TODO: REMOVE
    void OnCollisionStay2D(Collision2D col)
    {
        // Debug.Log("Collision STAY between " + this.name + " and " + col.gameObject.name);

        lastCollidedObject = col.gameObject;
        lastCollisionDir = movement.LastMoveDirection;

        if (movement.IsMoving)
        {
            PlayCollisionSound(lastCollidedObject);
        }

        StopMoving();
    }

    // TODO: REMOVE
    bool HasCollisionSound(GameObject gobj)
    {
        return gobj.HasComponent<AudioSource>();
    }

    // TODO: REMOVE
    AudioSource GetCollisionSound(GameObject gobj)
    {
        if (!HasCollisionSound(gobj))
        {
            return null;
        }

        gobj.TryGetComponent(typeof(AudioSource), out Component aud);
        return (AudioSource) aud;
    }

    // TODO: REMOVE
    void PlayCollisionSound(GameObject gobj)
    {
        AudioSource audioSource = GetCollisionSound(gobj);

        if (audioSource is AudioSource && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // TODO: REMOVE
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

    // TODO: REMOVE
    public bool IsMoving()
    {
        return movement.IsMoving;
    }

    // TODO: REMOVE
    public void ClampPositionTo(Vector3 position)
    {
        transform.position = movement.ClampPosition(position);

        // override in case collision physics caused object rotation
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}