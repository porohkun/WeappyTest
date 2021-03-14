using UnityEngine;

namespace WeappyTest.Character
{
    public class CharacterContext : BaseContext
    {
        public Character Character { get; private set; }
        public Ball.Ball TouchingBall { get; set; }

        public bool TouchFloor { get; set; }
        public bool TouchBall => TouchingBall != null;
        public bool OnAir { get; set; }
        public bool CarryBall { get; set; }

        public CharacterContext(Character character, SpriteRenderer spriteRenderer, Animator animator) : base(spriteRenderer, animator)
        {
            Character = character;
        }
    }
}
