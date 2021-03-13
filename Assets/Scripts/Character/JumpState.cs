namespace WeappyTest
{
    public class JumpState : IState<CharacterContext>
    {
        public virtual void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Jump");
            context.TouchFloor = false;
            context.VerticalSpeed = 2f;
            context.VerticalAcceleration = -5f;
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

            if (InputWrapper.Left != InputWrapper.Right)
                context.HorizontalSpeed = 0.75f;
            else
                context.HorizontalSpeed = 0f;
        }
    }
}
