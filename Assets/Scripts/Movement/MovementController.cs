using UnityEngine;

public class MovementController : InputConsumer
{
    [Header("Movement")] public Animator animator;
    public int speed = 6;
    public int inputDelay = 2;
    public int tilesToMove = 1;
    public float clampAt = 0.5f;
    public float raycastDistance = 1f;

    [Header("Debug")] private int currentTilesToMove = 1;
    public GameObject lastCollidedObject;
    public DIRECTION_BUTTON lastCollisionDir = DIRECTION_BUTTON.NONE;

    public Vector3 destPosition;
    public DIRECTION_BUTTON lastMoveDir = DIRECTION_BUTTON.DOWN;    // hardcode init state
    public bool mIsMoving = false;                                  // whether the player animation is moving
    private int changeDirCoolDown = 0;

    private GameObject player;
    private int LAYER_MASK_INTERACTABLE = 1 << 8;
    private int LAYER_MASK_DEFAULT = 1 << 0;

    private float lastPlayCollisionSoundTime = 0.0f;

    void Start()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }

        destPosition = transform.position;
        currentTilesToMove = tilesToMove;

        InputConsumerCenter.Instance.Register(this);
    }

    private void FixedUpdate()
    {
        // Best practise: update physics in FixedUpdate(), but handle input in Update()
        if (IsMoving() && transform.position != destPosition) {
            float delta = Time.fixedDeltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, destPosition, delta);
        }
    }

    void Update()
    {
        if (InputConsumerCenter.Instance.GetCurrentConsumer() != this) {
            // only check auto move stop
            if (transform.position == destPosition) {
                StopMoving();
            }

            return;
        }

        // handle other logic in OnUpdateHandleInput
    }

    public override void OnUpdateHandleInput()
    {
        DIRECTION_BUTTON dir = InputController.GetPressedDirectionButton();
        ACTION_BUTTON action = InputController.GetPressedActionButton();
        HandleMoveInput(dir);

        if (action != ACTION_BUTTON.NONE) {
            TryInteract(lastMoveDir, action);
        }
    }

    public Vector3 GetMovementVector(DIRECTION_BUTTON dir, int tiles = 1)
    {
        switch (dir)
        {
            case DIRECTION_BUTTON.UP:
                return Vector3.up * tiles;
            case DIRECTION_BUTTON.DOWN:
                return Vector3.down * tiles;
            case DIRECTION_BUTTON.LEFT:
                return Vector3.left * tiles;
            case DIRECTION_BUTTON.RIGHT:
                return Vector3.right * tiles;
            default:
                return Vector3.zero;
        }
    }

    private RaycastHit2D CheckInteractableRaycast(Vector2 direction)
    {
        Vector2 startingPosition = (Vector2) transform.position;

        // todo: do not check DEFAULT layer, move posts to interactable
        return Physics2D.Raycast(startingPosition, direction, raycastDistance, LAYER_MASK_INTERACTABLE | LAYER_MASK_DEFAULT);
    }

    private void StartMove(DIRECTION_BUTTON dir, int tiles = 1)
    {
        lastMoveDir = dir;
        var movementVector = GetMovementVector(dir, tiles);

        bool canMove = CanMove(transform.position, dir, out Collider2D collidedObj);
        if (canMove) { 
            destPosition = transform.position + movementVector;
            StartMovingAnimation(movementVector);
        } else {
            StartCollisionMovingAnimation(movementVector);
            if (collidedObj) {
                PlayCollisionSound(collidedObj.gameObject);
            }
        }
    }

    private bool CanMove(Vector3 startPos, DIRECTION_BUTTON dir, out Collider2D collidedObj)
    {
        int mask = LAYER_MASK_DEFAULT;   // only check default layer (all portals are in TransparentFX layer)
        var dirVector = GetMovementVector(dir);
        var hit = Physics2D.Raycast(startPos, dirVector, raycastDistance, mask);

        collidedObj = hit.collider;
        return hit.collider == null;
    }

    public void HandleMoveInput(DIRECTION_BUTTON dir = DIRECTION_BUTTON.NONE, int tiles = 1)
    {
        if (IsMoving()) {
            // continue moving to destination
            float delta = Time.deltaTime * speed;
            if (transform.position == destPosition) {
                if (dir == DIRECTION_BUTTON.NONE) {
                    StopMoving();
                } else {
                    // another direction is pressed, turn soon
                    StartMove(dir);
                }
            } else if (Vector3.Distance(transform.position, destPosition) <= delta &&
                       dir == lastMoveDir &&
                       CanMove(destPosition, dir, out Collider2D collidedObj)) {
                // destination is close enough, update destination
                // in order to make it more smooth for long time key press
                destPosition += GetMovementVector(dir, tiles);
            }

            return;
        }

        // change facing and movement from idle state
        if (dir != DIRECTION_BUTTON.NONE) {
            if (--changeDirCoolDown > 0)
                return;
            changeDirCoolDown = 0;

            if (dir != lastMoveDir) {
                // face to that dir first
                lastMoveDir = dir;
                var movementVector = GetMovementVector(dir, tiles);
                FaceTo(movementVector);

                // skip 8 frames before player can move
                changeDirCoolDown = inputDelay;
            } else {
                StartMove(dir);
            }
        }
    }

    private bool TryInteract(DIRECTION_BUTTON dir, ACTION_BUTTON action)
    {
        if (action == ACTION_BUTTON.NONE)
            return false;

        Vector3 dirVector = GetMovementVector(dir); // todo: change function name
        if (dirVector == Vector3.zero)
            return false;

        RaycastHit2D hit = CheckInteractableRaycast(dirVector);
        // Debug.DrawRay(transform.position, dirVector, Color.green);
        if (hit.collider) {
            // Debug.DrawRay(transform.position, dirVector, Color.red);
            // Debug.DrawRay(transform.position, hit.point, Color.blue);

            if (hit.collider.gameObject.HasComponent<Interactable>()) {
                // Debug.Log("[raycast hit] @interactable " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<Interactable>().Interact(action);
                return true;
            }
        }

        return false;
    }

    private void StartCollisionMovingAnimation(Vector3 movement)
    {
        mIsMoving = true;

        animator.speed = 0.5f;
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("LastMoveX", movement.x);
        animator.SetFloat("LastMoveY", movement.y);
        animator.SetBool("Moving", true);
    }

    private void StartMovingAnimation(Vector3 movement)
    {
        mIsMoving = true;

        animator.speed = 1.0f;
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("LastMoveX", movement.x);
        animator.SetFloat("LastMoveY", movement.y);
        animator.SetBool("Moving", mIsMoving);
    }

    public void FaceToDir(DIRECTION_BUTTON dir)
    {
        if (dir == DIRECTION_BUTTON.NONE)
            dir = DIRECTION_BUTTON.DOWN;

        if (lastMoveDir == dir)
            return;

        var movementVector = GetMovementVector(dir);
        lastMoveDir = dir;
        FaceTo(movementVector);
    }

    private void FaceTo(Vector3 movement)
    {
        // mIsMoving = false;
        animator.speed = 1.0f;

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("LastMoveX", movement.x);
        animator.SetFloat("LastMoveY", movement.y);
        animator.SetBool("Moving", false);
    }

    private void StopMoving()
    {
        mIsMoving = false;
        animator.SetBool("Moving", mIsMoving);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
    }

    void OnCollisionStay2D(Collision2D col)
    {
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
            if (Time.time - lastPlayCollisionSoundTime >= 1.0) {
                audioSource.Play();
                lastPlayCollisionSoundTime = Time.time;
            }
        }
    }

    public void ClampCurrentPosition()
    {
        ClampPositionTo(transform.position);
    }

    public bool IsMoving()
    {
        return mIsMoving;
    }

    public void ClampPositionTo(Vector3 position)
    {
        transform.position = ClampPosition(position);

        // override in case collision physics caused object rotation
        transform.rotation = new Quaternion(0, 0, 0, 0);
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

        if (val < 0f) {
            return (val - mod) - clampAt;
        }

        return (val - mod) + clampAt;
    }
}
