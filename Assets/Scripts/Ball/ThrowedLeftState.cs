namespace WeappyTest.Ball
{
    public class ThrowedLeftState : BallState
    {
        protected override void OnEnter(BallContext context)
        {
            context.Direction = Direction.Left;
            context.WallTouchesLeft = 2;
        }
    }
}
