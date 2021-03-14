namespace WeappyTest.Character
{
    public class LandingState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Crouch"));
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
        }
    }
}
