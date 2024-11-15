using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class CursedItemHelper
    {
        private Timer _curseTimer;
        public bool IsCursed { get; private set; } = true;

        // Starts a 5-minute timer to remove the curse after the duration
        public void StartCurseTimer(Mobile owner)
        {
            _curseTimer?.Stop(); // Stop any existing timer

            _curseTimer = Timer.DelayCall(TimeSpan.FromMinutes(5), () =>
            {
                IsCursed = false;
                _curseTimer = null;
                owner.SendMessage("The curse on the artifact has faded.");
                RemoveCursedEffects(owner);
            });
        }

        // Applies the cursed effects, like disabling insurance
        public void ApplyCursedEffects(Mobile owner)
        {
            if (IsCursed)
            {
                owner.SendMessage("This artifact is cursed and cannot be insured.");
            }
        }

        // Removes the curse effects after the timer ends
        public void RemoveCursedEffects(Mobile owner)
        {
            owner.SendMessage("You feel the curse's effects lift, and the item can now be insured.");
        }
    }
}
