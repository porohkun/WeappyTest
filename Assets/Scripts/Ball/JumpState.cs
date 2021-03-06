namespace WeappyTest.Ball
{
    public class JumpState : BallState
    {
        protected override void OnEnter(BallContext context)
        {
            context.VerticalSpeed = _settings.VerticalSpeed;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;
            context.IsProjectile = true;
        }

        protected override void OnExit(BallContext context)
        {
            context.IsProjectile = false;
        }
    }
}
