namespace WeappyTest
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<TParam> : ICommand
    {
        void Execute(TParam param);
    }
}
