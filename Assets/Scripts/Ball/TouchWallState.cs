namespace WeappyTest.Ball
{
    public class TouchWallState : BallState
    {
        protected override void OnEnter(BallContext context)
        {
            context.TouchWall = false;
            context.WallTouchesLeft -= 1;
            if (context.WallTouchesLeft > 0)
                context.Direction = (Direction)(-(int)context.Direction);
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;
        }
    }
}
