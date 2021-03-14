﻿namespace WeappyTest.Ball
{
    public class JumpState : BallState
    {
        protected override void OnEnter(BallContext context)
        {
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;
        }
    }
}