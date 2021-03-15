namespace WeappyTest.Boss
{
    public class FlipState : BossState
    {
        protected override void OnEnter(BossContext context)
        {
            context.HorizontalSpeed = 0f;
            context.Direction = (Direction)(-(int)context.Direction);
            int spawner;
            context.FlightCount++;
            if (context.FlightCount % 4 == 3)
                spawner = 3;
            else
                spawner = 1;// UnityEngine.Random.Range(0, 3);
            context.LowFlightReady = context.Boss.TeleportToSpawner(spawner);
            context.HighFlightReady = !context.LowFlightReady;
        }
    }
}
