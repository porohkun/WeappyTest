﻿namespace WeappyTest.Character
{
    public class HitState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Fall");
            context.Effects.Add<BlinkEffect>(_settings.FallBlinkTime);
            context.HorizontalSpeed = _settings.FallHorizontalSpeed;
            context.VerticalSpeed = _settings.FallVerticalSpeed;
            context.VerticalAcceleration = _settings.FallVerticalAcceleration;
        }
    }
}
