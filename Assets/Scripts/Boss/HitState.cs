namespace WeappyTest.Boss
{
    public class HitState : BossState
    {
        protected override void OnEnter(BossContext context)
        {
            context.Lives--;
            context.Effects.Add<BlinkEffect>(_settings.BlinkTime);
        }
    }
}
