namespace WeappyTest
{
    public class RunState : CharacterState
    {
        public override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Run");
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
        }

        public override void OnExit(CharacterContext context)
        {

        }

        public override void Update(CharacterContext context)
        {
            if (InputWrapper.Left)
                context.Direction = Direction.Left;
            if (InputWrapper.Right)
                context.Direction = Direction.Right;

            if (InputWrapper.Left != InputWrapper.Right)
                context.HorizontalSpeed = _settings.RunSpeed;
            else
                context.HorizontalSpeed = 0f;
        }
    }
}
