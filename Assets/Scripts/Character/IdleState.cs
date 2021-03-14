namespace WeappyTest
{
    public class IdleState : CharacterState
    {
        public override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Idle");
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;
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
        }
    }
}
