using UnityEngine;

namespace WeappyTest.Ball
{
    public class BallContext : BaseContext
    {
        public BallContext(SpriteRenderer spriteRenderer, Animator animator) : base(spriteRenderer, animator)
        {
        }

        public bool TouchWall { get; internal set; }
        public bool TouchFloor { get; internal set; }
        public int WallTouchesLeft { get; internal set; }
        public bool TouchCeiling { get; internal set; }
    }
}
