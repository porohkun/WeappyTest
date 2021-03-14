namespace WeappyTest.Ball
{
    public class FallState : BallState
    {
        protected override void OnEnter(BallContext context)
        {
            context.VerticalSpeed = _settings.FallSpeed;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;
        }
    }
}
