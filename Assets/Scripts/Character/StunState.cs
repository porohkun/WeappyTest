namespace WeappyTest.Character
{
    public class StunState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Stun");
            context.Effects.Add<BlinkEffect>(_settings.StunBlinkTime);
            context.HorizontalSpeed = 0f;
            context.VerticalSpeed = _settings.StunVerticalSpeed;
            context.VerticalAcceleration = _settings.StunVerticalAcceleration;
        }
    }
}
