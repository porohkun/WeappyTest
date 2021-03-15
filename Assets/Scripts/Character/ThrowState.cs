namespace WeappyTest.Character
{
    public class ThrowState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Throw"));
            context.HorizontalSpeed = 0f;
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.Character.ThrowBall();
        }
    }
}
