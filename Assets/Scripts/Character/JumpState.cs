namespace WeappyTest.Character
{
    public class JumpState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Jump"));
            context.OnAir = true;
            context.TouchFloor = false;
            context.VerticalSpeed = _settings.JumpSpeed;
            context.VerticalAcceleration = _settings.JumpGravity;
        }

        protected override void OnUpdate(CharacterContext context)
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
