namespace WeappyTest
{
    public class FallState : JumpState
    {
        public override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Jump");
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = _settings.FallGravity;
        }
    }
}
