using UnityEngine;
using Zenject;

namespace WeappyTest
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private MyBoxCollider2D _collider;

        private MyPhysicsService _myPhysicsService;
        private StateMachine<CharacterContext> _states;
        private CharacterContext _context;

        [Inject]
        void Inject(MyPhysicsService myPhysicsService)
        {
            _myPhysicsService = myPhysicsService;
        }

        private void Awake()
        {
            _context = new CharacterContext(this, _spriteRenderer, _animator);
            _states = new StateMachine<CharacterContext>(_context);
            _states.AddState<IdleState>();
            _states.AddState<RunState>();
            _states.AddState<JumpState>();
            _states.AddState<FallState>();

            _states.AddTransition<IdleState, RunState>(c => InputWrapper.Left ^ InputWrapper.Right);
            _states.AddTransition<IdleState, JumpState>(c => InputWrapper.BeginJump);
            _states.AddTransition<IdleState, FallState>(c => !c.TouchFloor);

            _states.AddTransition<RunState, IdleState>(c => InputWrapper.Left == InputWrapper.Right);
            _states.AddTransition<RunState, JumpState>(c => InputWrapper.BeginJump);
            _states.AddTransition<RunState, FallState>(c => !c.TouchFloor);

            _states.AddTransition<JumpState, FallState>(c => !InputWrapper.Jump);
            _states.AddTransition<JumpState, FallState>(c => c.VerticalSpeed <= 0f);

            _states.AddTransition<FallState, IdleState>(c => c.TouchFloor);
        }

        private void Update()
        {
            _context.VerticalSpeed += _context.VerticalAcceleration * Time.deltaTime;

            _states.Update();

            transform.position += new Vector3(_context.HorizontalSpeed * (int)_context.Direction, _context.VerticalSpeed);

            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Floor")
                {
                    _context.TouchFloor = true;
                    transform.position += new Vector3(0f, collision.Bounds.yMax - _collider.Bounds.yMin);
                }
                else if (collision.tag == "Wall")
                {
                    _context.HorizontalSpeed = 0;
                    if (_context.Direction == Direction.Left)
                        transform.position += new Vector3(collision.Bounds.xMax - _collider.Bounds.xMin, 0f);
                    else
                        transform.position += new Vector3(collision.Bounds.xMin - _collider.Bounds.xMax, 0f);
                }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("TRIGGERED!");
        }
    }
}
