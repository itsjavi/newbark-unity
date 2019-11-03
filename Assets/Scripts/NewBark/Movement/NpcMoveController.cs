using System.Collections.Generic;
using UnityEngine;

namespace NewBark.Movement
{
    [RequireComponent(typeof(AnimationController))]
    public class NpcMoveController : MonoBehaviour
    {
        private Queue<ScheduledMove> _moveStack;

        [Tooltip("Time to turn around to a different direction, in milliseconds.")]
        public float turnAroundDelay = 125;

        public Vector2 clampOffset = new Vector2(0.5f, 0.5f);
        public Direction initialDirection = Direction.Down;
        public ScheduledMove[] moves;

        private MoveDirector _director;
        public MoveDirector Director => _director;
        private AnimationController Animation => GetComponent<AnimationController>();

        private void Initialize()
        {
            Debug.Log("NPC Init. Name=" + gameObject.name + ", pos=" + gameObject.transform.position);
            _director = new MoveDirector(gameObject, clampOffset, turnAroundDelay);
            _moveStack = new Queue<ScheduledMove>();
            foreach (var move in moves)
            {
                for (int i = 0; i < move.repeats; i++)
                {
                    _moveStack.Enqueue(move);
                }
            }
        }

        private void Start()
        {
            Debug.Log("NPC Ctrl: start");
            Initialize();
        }

        private void OnValidate()
        {
            Debug.Log("NPC Ctrl: validate");
            Initialize();
        }

        private bool HasMovesLeft()
        {
            return _moveStack.Count > 0;
        }

        private ScheduledMove NextMove()
        {
            if (!HasMovesLeft())
            {
                return null;
            }

            return _moveStack.Dequeue();
        }

        private void FixedUpdate()
        {
            if (initialDirection != Direction.None)
            {
                _director.LookAt(initialDirection, 0);
                initialDirection = Direction.None;
                return;
            }

            if (_director.UpdateMovement())
            {
                return;
            }

            // not moving and has repeats left
            // TODO: add delay support
            var move = NextMove();
            if (move is null)
            {
                return;
            }

            // Debug.Log(_currentMove.direction + ", steps: " + _currentMove.steps + ", speed: " + _currentMove.speed);
            Debug.Log("NPC Ctrl: It starts next move");
            _director.LookAt(move.direction, 0);
            _director.Move(new MovePath(transform.position, move, clampOffset, GameManager.CollisionsLayer));
        }

        public void OnMoveBeforeStart(MovePath path)
        {
            // var direction = path.Move.GetDirectionVector();
            // Animation.UpdateAnimation(direction, direction, path.Move.CalculateAnimationSpeed());
        }

        public void OnMoveDirectionChangeEnd()
        {
            _director.Path.Move.speed = 0;
            // Animation.UpdateAnimation(0f);
        }

        public void OnMoveEnd()
        {
            // Animation.StopAnimation();
        }

        public void OnMoveCollide()
        {
            // GameManager.Audio.PlaySfxWhenIdle(collisionSound, collisionSoundDelay);
            // Animation.UpdateAnimation(_director.Path.Move.CalculateAnimationSpeed(collisionSpeed));
        }
    }
}