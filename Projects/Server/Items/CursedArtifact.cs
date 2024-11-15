using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class CursedArtifactHelper
    {
        private Timer _curseTimer;
        public bool IsCursed { get; private set; } = true;

        // Start a timer to manage curse duration
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

        // Apply effects when the curse is active
        public void ApplyCursedEffects(Mobile owner)
        {
            if (IsCursed)
            {
                owner.Hidden = false; // Prevents hiding
                owner.SendMessage("The cursed artifact prevents you from hiding or becoming invisible.");

                if (owner.Mount != null) // Example restriction on mounts
                {
                    owner.Mount.Rider = null;
                }
            }
        }

        // Clean up effects when the curse lifts
        public void RemoveCursedEffects(Mobile owner)
        {
            owner.SendMessage("You feel the curse's effects lift temporarily, but the item remains cursed.");
        }
    }
}
