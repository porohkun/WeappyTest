namespace WeappyTest
{
    public class JumpState : CharacterState
    {
        public override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Jump");
            context.TouchFloor = false;
            context.VerticalSpeed = _settings.JumpSpeed;
            context.VerticalAcceleration = _settings.JumpGravity;
        }

        public override void OnExit(CharacterContext context)
        {

        }

        public override void Update(CharacterContext context)
        {
            if (InputWrapper.Left && !InputWrapper.Right)
                context.Direction = Direction.Left;
            if (InputWrapper.Right && !InputWrapper.Left)
                context.Direction = Direction.Right;

            if (InputWrapper.Left != InputWrapper.Right)
                context.HorizontalSpeed = _settings.FlySpeed;
            else
                context.HorizontalSpeed = 0f;
        }
    }
}
