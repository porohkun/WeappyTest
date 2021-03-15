namespace WeappyTest.Character
{
    public class ThrowUpState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Crouch"));
            context.HorizontalSpeed = 0f;
            if (context.TouchFloor)
            {
                context.VerticalSpeed = _settings.ThrowUpVerticalSpeed;
                context.VerticalAcceleration = _settings.ThrowUpVerticalAcceleration;
            }
            else
            {
                context.VerticalSpeed = 0f;
                context.VerticalAcceleration = 0f;
            }
            context.Character.ThrowBallUp();
        }
    }
}
