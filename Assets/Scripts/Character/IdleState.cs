namespace WeappyTest.Character
{
    public class IdleState : CharacterState
    {
        protected override void OnEnter(CharacterContext context)
        {
            SetDelay(0f, () => context.Animator.SetTrigger("Idle"));
            context.HorizontalSpeed = 0f;
        }
    }
}
