namespace WeappyTest.Ball
{
    public class ThrowedRightState : BallState
    {
        protected override void OnEnter(BallContext context)
        {
            context.Direction = Direction.Right;
            context.WallTouchesLeft = 2;
        }
    }
}
