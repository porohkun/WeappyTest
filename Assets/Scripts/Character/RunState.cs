namespace WeappyTest
{
    public class RunState : IState<CharacterContext>
    {
        public void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Run");
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
        }

        public void OnExit(CharacterContext context)
        {

        }

        public void Update(CharacterContext context)
        {
            if (InputWrapper.Left)
                context.Direction = Direction.Left;
            if (InputWrapper.Right)
                context.Direction = Direction.Right;

            if (InputWrapper.Left != InputWrapper.Right)
                context.HorizontalSpeed = 0.75f;
            else
                context.HorizontalSpeed = 0f;
        }
    }
}
