using UnityEngine;

namespace WeappyTest.Slime
{
    public class SlimeContext : BaseContext
    {
        public SlimeContext(EffectKeeper effects, SpriteRenderer spriteRenderer, Animator animator) : base(effects, spriteRenderer, animator)
        {

        }

        public bool TouchFloor { get; internal set; }
        public bool Dead { get; internal set; }
    }
}
