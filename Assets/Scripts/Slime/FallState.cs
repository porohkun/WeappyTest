namespace WeappyTest.Slime
{
    public class FallState : SlimeState
    {
        protected override void OnEnter(SlimeContext context)
        {
            context.VerticalSpeed = _settings.FallSpeed;
            context.VerticalAcceleration = _settings.FallAcceleration;
            context.HorizontalSpeed = 0f;
        }
    }
}
