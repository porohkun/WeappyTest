using UnityEngine;

namespace WeappyTest.Character
{
    public class CharacterContext : BaseContext
    {
        public Character Character { get; private set; }
        public Ball.Ball TouchingBall { get; set; }

        public bool TouchFloor { get; set; }
        public bool TouchBall => TouchingBall != null;
        public bool CarryBall => Character.CarryBall != null;
        public bool Blinking => Effects.Active<BlinkEffect>();

        public CharacterContext(Character character, EffectKeeper effects, SpriteRenderer spriteRenderer, Animator animator) : base(effects, spriteRenderer, animator)
        {
            Character = character;
        }
    }
}
