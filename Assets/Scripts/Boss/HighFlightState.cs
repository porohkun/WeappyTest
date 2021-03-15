namespace WeappyTest.Boss
{
    public class HighFlightState : BossState
    {
        private float _lastPosition;

        protected override void OnEnter(BossContext context)
        {
            _lastPosition = context.Boss.transform.position.x;
            context.HorizontalSpeed = _settings.HighFlightSpeed;
        }

        protected override void OnUpdate(BossContext context)
        {
            foreach (var point in context.Boss.SlimeSpawnPoints)
            {
                var pointPos = point.transform.position.x;
                if ((_lastPosition < pointPos && context.Boss.transform.position.x > pointPos) ||
                    (_lastPosition > pointPos && context.Boss.transform.position.x < pointPos))
                    context.Boss.SpawnSlimeAt(point);
            }
            _lastPosition = context.Boss.transform.position.x;
        }
    }
}
