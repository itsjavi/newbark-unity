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
        if (movement != null && !movement.IsMoving)
        {
            currentTilesToMove = tilesToMove;
        }
        Move(dir, currentTilesToMove);
    }

    public bool MoveTo(DIRECTION_BUTTON dir, Vector3 destinationPosition)
    {
        //if (dir == DIRECTION_BUTTON.NONE || destinationPosition == Vector3.zero)
        //{
        //    return false;
        //}

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
        //if (dir == DIRECTION_BUTTON.NONE || tiles == 0)
        //{
        //    return false;
        //}

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
        // Debug.Log("Collision: " + EditorJsonUtility.ToJson(col.gameObject, true));
        Debug.Log("Collision ENTER between " + this.name + " and " + col.gameObject.name);

        lastCollidedObject = col.gameObject;
        lastCollisionDir = movement.LastDirection;

        ClampCurrentPosition();

        PlayCollisionSound(lastCollidedObject);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("Collision STAY between " + this.name + " and " + col.gameObject.name);

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