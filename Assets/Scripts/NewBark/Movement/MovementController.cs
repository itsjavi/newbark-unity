using NewBark.Dialog;
using NewBark.Input;
using NewBark.Extensions;
using UnityEngine;

namespace NewBark.Movement
{
    public class MovementController : MonoBehaviour
    {
        public AnimationController m_animationController;
        public DialogManager m_DialogManager;
    
        public int m_Speed = 6;
        public int m_InputDelay = 8;
        public int m_TilesPerStep = 1;
        public float m_ClampOffset = 0.5f;
    
        private bool _inputEnabled = true;
        private int _inputDelayCoolDown;
        private bool _isMoving;
        private int _currentTilesToMove = 1;
        private GameObject _lastCollidedObject;
        private Vector3 _destinationPosition;
        private Vector3 _positionDiff;
        private Vector3 _lastPositionDiff;
        private DirectionButton _lastDirection = DirectionButton.NONE;
        private DirectionButton _lastCollisionDir = DirectionButton.NONE;

        void FixedUpdate()
        {
            if (!CanMove() && IsMoving())
            {
                StopMoving();
            }

            DirectionButton dir = LegacyInputManager.GetPressedDirectionButton();

            TriggerDirectionButton(dir);
        }

        private bool CanMove()
        {
            return !m_DialogManager || !m_DialogManager.InDialog();
        }

        private void MovementUpdate(DirectionButton dir)
        {
            if (!_isMoving)
            {
                _currentTilesToMove = m_TilesPerStep;
            }

            Move(dir, _currentTilesToMove);
        }

        private bool MoveTo(DirectionButton dir, Vector3 destPosition)
        {
            m_animationController.UpdateAnimation(_positionDiff, _lastPositionDiff, _isMoving);

            if (!_isMoving || (destPosition == transform.position))
            {
                ClampCurrentPosition();
                return false;
            }

            if (_isMoving && (dir != DirectionButton.NONE) && (dir == _lastCollisionDir))
            {
                ClampCurrentPosition();
                if (!(_lastCollidedObject is null))
                {
                    PlayCollisionSound(_lastCollidedObject);
                }

                return true;
            }

            if (_isMoving)
            {
                _lastCollidedObject = null;
                _lastCollisionDir = DirectionButton.NONE;
            }

            var tr = transform;
        
            tr.position = Vector3.MoveTowards(tr.position, destPosition, Time.deltaTime * m_Speed);
            tr.rotation = new Quaternion(0, 0, 0, 0);
            return true;
        }

        public bool Move(DirectionButton dir, int tiles)
        {
            _currentTilesToMove = tiles;

            Vector3 destPosition = CalculateDestinationPosition(
                transform.position, dir, tiles
            );

            return MoveTo(dir, destPosition);
        }

        public void TriggerDirectionButton(DirectionButton dir)
        {
            if (!CanMove() && IsMoving())
            {
                StopMoving();
            }

            if (CanMove())
            {
                MovementUpdate(dir);
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            _lastCollidedObject = col.gameObject;
            _lastCollisionDir = _lastDirection;

            ClampCurrentPosition();

            PlayCollisionSound(_lastCollidedObject);
        }

        void OnCollisionStay2D(Collision2D col)
        {
            _lastCollidedObject = col.gameObject;
            _lastCollisionDir = _lastDirection;

            if (_isMoving)
            {
                PlayCollisionSound(_lastCollidedObject);
            }

            StopMoving();
        }

        private bool HasCollisionSound(GameObject gameObj)
        {
            return gameObj.HasComponent<AudioSource>();
        }

        private AudioSource GetCollisionSound(GameObject gameObj)
        {
            if (!HasCollisionSound(gameObj))
            {
                return null;
            }

            gameObj.TryGetComponent(typeof(AudioSource), out Component aud);
            return (AudioSource) aud;
        }

        private void PlayCollisionSound(GameObject gameObj)
        {
            AudioSource audioSource = GetCollisionSound(gameObj);

            if (!(audioSource is null) && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        private void ClampCurrentPosition()
        {
            ClampPositionTo(transform.position);
        }

        private void StopMoving()
        {
            Stop();
            m_animationController.UpdateAnimation(Vector2.zero, _lastPositionDiff, false);
            ClampCurrentPosition();
        }

        public bool IsMoving()
        {
            return _isMoving;
        }
        
        // ---------------------------------------------------

        public void ClampPositionTo(Vector3 position)
        {
            var tr = transform;
        
            tr.position = ClampPosition(position);

            // override in case collision physics caused object rotation
            tr.rotation = new Quaternion(0, 0, 0, 0);
        }


        private Vector3 CalculateDestinationPosition(Vector3 origin, DirectionButton dir, int tiles = 1)
        {
            if (_inputDelayCoolDown > 0)
            {
                _inputDelayCoolDown--;
            }

            if (_inputEnabled)
            {
                _destinationPosition = origin;
                CalculateMovement(dir, tiles);
            }

            if (_isMoving)
            {
                if (origin == _destinationPosition)
                {
                    // done moving in a tile
                    _isMoving = false;
                    _inputEnabled = true;
                    CalculateMovement(dir, tiles);
                }

                if (origin == _destinationPosition)
                {
                    return origin;
                }

                return _destinationPosition;
            }

            _positionDiff.x = 0;
            _positionDiff.y = 0;
            _positionDiff.z = 0;

            return ClampPosition(origin);
        }

        private Vector3 ClampPosition(Vector3 position)
        {
            Vector3 fixedPos = new Vector3(ClampPositionAxis(position.x), ClampPositionAxis(position.y), 0);
            return fixedPos;
        }

        private float ClampPositionAxis(float val)
        {
            float mod = val % 1f;

            if (System.Math.Abs(mod - m_ClampOffset) < double.Epsilon) // more precise than: if (mod == fraction)
            {
                return val;
            }

            if (val < 0f)
            {
                return (val - mod) - m_ClampOffset;
            }

            return (val - mod) + m_ClampOffset;
        }

        private void Stop()
        {
            _positionDiff = Vector2.zero;
            _inputDelayCoolDown = 0;
            _isMoving = false;
            _inputEnabled = true;
        }

        // Returns the calculated final destination vector
        private void CalculateMovement(DirectionButton dir, int tiles = 1)
        {
            if (_inputDelayCoolDown > 0)
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

            _lastPositionDiff.x = x;
            _lastPositionDiff.y = y;
            _lastPositionDiff.z = z;

            if (_lastDirection != dir)
            {
                _inputDelayCoolDown = m_InputDelay;
                _lastDirection = dir;

                return;
            }

            _inputEnabled = false;
            _isMoving = true;

            _positionDiff.x = x;
            _positionDiff.y = y;
            _positionDiff.z = z;

            _destinationPosition += _positionDiff;
            _destinationPosition = ClampPosition(_destinationPosition);
        }
    }
}
