namespace WeappyTest.Slime
{
    public class BounceState : SlimeState
    {
        protected override void OnEnter(SlimeContext context)
        {
            context.TouchFloor = false;
            context.VerticalSpeed = _settings.BounceSpeed;
            context.VerticalAcceleration = _settings.BounceAcceleration;
            context.HorizontalSpeed = 0f;
        }
    }
}
