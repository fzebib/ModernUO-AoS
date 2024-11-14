using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class CursedArtifactHelper
    {
        private Timer _curseTimer;

        public bool IsCursed { get; private set; } = true;

        public void StartCurseTimer(Mobile owner, TimeSpan duration)
        {
            _curseTimer?.Stop();

            _curseTimer = Timer.DelayCall(duration, () =>
            {
                IsCursed = false;
                _curseTimer = null;

                owner.SendMessage("The curse on the artifact has faded.");
                RemoveCursedEffects(owner);
            });
        }

        public void ApplyCursedEffects(Mobile owner)
        {
            owner.Hidden = false;

            if (owner.Mount != null)
            {
                owner.Mount.Rider = null;
            }

            owner.SendMessage("The cursed artifact prevents you from hiding or becoming invisible.");
        }

        public void RemoveCursedEffects(Mobile owner)
        {
            owner.SendMessage("You feel the curse's effects lift temporarily, but the item remains cursed.");
        }
    }
}
