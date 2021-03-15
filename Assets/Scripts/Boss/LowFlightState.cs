namespace WeappyTest.Boss
{
    public class LowFlightState : BossState
    {
        protected override void OnEnter(BossContext context)
        {
            context.HorizontalSpeed = _settings.LowFlightSpeed;
        }
    }
}
