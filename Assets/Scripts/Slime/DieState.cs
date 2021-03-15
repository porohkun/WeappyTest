namespace WeappyTest.Slime
{
    public class DieState : SlimeState
    {
        protected override void OnEnter(SlimeContext context)
        {
            context.Dead = true;
            context.TouchFloor = false;
            context.VerticalSpeed = _settings.DieSpeed;
            context.VerticalAcceleration = _settings.DieAcceleration;
            context.HorizontalSpeed = 0f;
        }
    }
}
