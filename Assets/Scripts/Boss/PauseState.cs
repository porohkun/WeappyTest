namespace WeappyTest.Boss
{
    public class PauseState : BossState
    {
        protected override void OnEnter(BossContext context)
        {
            context.Ready = false;
            context.HorizontalSpeed = 0f;
            SetDelay(UnityEngine.Random.Range(_settings.MinPauseLength, _settings.MaxPauseLength), () => context.Ready = true);
        }

        //protected override void OnExit(BossContext context)
        //{
        //    context.Ready = false;
        //}
    }
}
