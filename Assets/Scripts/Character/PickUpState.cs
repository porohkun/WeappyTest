namespace WeappyTest.Character
{
    public class PickUpState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Crouch"));
            context.HorizontalSpeed = 0f;
            context.Character.PickUpBall(context.TouchingBall);
        }

        protected override void OnExit(CharacterContext context)
        {
        }
    }
}
