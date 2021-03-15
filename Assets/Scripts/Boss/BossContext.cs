using UnityEngine;

namespace WeappyTest.Boss
{
    public class BossContext : BaseContext
    {
        public Boss Boss { get; private set; }

        public BossContext(Boss boss, EffectKeeper effects, SpriteRenderer spriteRenderer, Animator animator) : base(effects, spriteRenderer, animator)
        {
            Boss = boss;
        }

        public bool TouchSpawner { get; internal set; }
        public bool HighFlightReady { get; internal set; }
        public bool LowFlightReady { get; internal set; }
        public bool Ready { get; internal set; }
        public int FlightCount { get; internal set; }
        public int Lives { get; internal set; }
        public bool Blinking => Effects.Active<BlinkEffect>();
    }
}
