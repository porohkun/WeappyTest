namespace WeappyTest
{
    public interface IState<TContext> where TContext : IStateContext
    {
        void OnEnter(TContext context);
        void Update(TContext context);
        void OnExit(TContext context);
    }
}
