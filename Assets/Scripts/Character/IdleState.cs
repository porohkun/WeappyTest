namespace WeappyTest
{
    public class IdleState : IState<CharacterContext>
    {
        public void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Idle");
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;
        }

        public void OnExit(CharacterContext context)
        {

        }

        public void Update(CharacterContext context)
        {
            if (InputWrapper.Left && !InputWrapper.Right)
                context.Direction = Direction.Left;
            if (InputWrapper.Right && !InputWrapper.Left)
                context.Direction = Direction.Right;
        }
    }
}
