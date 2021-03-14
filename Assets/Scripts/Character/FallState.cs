namespace WeappyTest.Character
{
    public class FallState : JumpState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Jump"));
            context.OnAir = true;
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = _settings.FallGravity;
        }
    }
}
