using UnityEngine;

public class CellMovement
{
    private int inputDelay = 1;
    private bool canReadInput = true;
    private int coolDown = 0;

    private bool isMoving = false;
    public bool IsMoving => isMoving;

    private DIRECTION_BUTTON lastDirection = DIRECTION_BUTTON.NONE;
    public DIRECTION_BUTTON LastDirection => lastDirection;

    private Vector3 destinationPosition;
    private Vector3 positionDiff;
    public Vector3 PositionDiff => positionDiff;

    private Vector3 lastPositionDiff;
    public Vector3 LastPositionDiff => lastPositionDiff;

    private readonly float clampAt;

    public CellMovement(int inputDelay = 1, float clampAt = 0.5f)
    {
        this.clampAt = clampAt;
        this.inputDelay = inputDelay;
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
