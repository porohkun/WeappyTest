namespace WeappyTest.Character
{
    public class RunState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Run"));
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
        }

        protected override void OnUpdate(CharacterContext context)
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
