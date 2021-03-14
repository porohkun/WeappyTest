namespace WeappyTest.Ball
{
    public class BounceState : BallState
    {
        protected override void OnEnter(BallContext context)
        {
            context.TouchFloor = false;
            context.VerticalSpeed = _settings.BounceSpeed;
            context.VerticalAcceleration = _settings.BounceAcceleration;
            context.HorizontalSpeed = 0f;
        }
    }
}
