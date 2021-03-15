namespace WeappyTest.Slime
{
    public class RunState : SlimeState
    {
        protected override void OnEnter(SlimeContext context)
        {
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = _settings.RunSpeed;
        }
    }
}
