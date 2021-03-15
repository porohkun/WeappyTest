namespace WeappyTest.Boss
{
    public class HighFlightState : BossState
    {
        protected override void OnEnter(BossContext context)
        {
            context.HorizontalSpeed = _settings.HighFlightSpeed;
        }
    }
}
