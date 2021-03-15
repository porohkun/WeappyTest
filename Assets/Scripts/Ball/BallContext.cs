using UnityEngine;

namespace WeappyTest.Ball
{
    public class BallContext : BaseContext
    {
        public BallContext(EffectKeeper effects, SpriteRenderer spriteRenderer, Animator animator) : base(effects, spriteRenderer, animator)
        {
        }

        public override Direction Direction { get; set; }

        public bool TouchWall { get; internal set; }
        public bool TouchFloor { get; internal set; }
        public int WallTouchesLeft { get; internal set; }
        public bool TouchCeiling { get; internal set; }
        public bool IsProjectile { get; internal set; }
    }
}
