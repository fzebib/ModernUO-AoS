using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public abstract class CursedArtifact : Item
    {
        public virtual bool IsCursed { get; private set; } = true;
        private Timer _curseTimer;

        [Constructible]
        public CursedArtifact(int itemID) : base(itemID)
        {
            Hue = 0x554; // Default cursed artifact hue (can be changed per item)
        }

        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (IsCursed && parent is Mobile owner)
            {
                owner.SendMessage("An immediate curse emanates from the artifact as you pick it up!");
                ApplyCursedEffects(owner);

                // Start the timer to remove the curse after exactly 5 minutes
                StartCurseTimer(TimeSpan.FromMinutes(5));
            }
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile owner && IsCursed)
            {
                RemoveCursedEffects(owner); // Remove effects from the player, but the curse remains on the item
            }
        }

        public override bool CanInsure => !IsCursed; // Prevents item from being insured while cursed

        private void StartCurseTimer(TimeSpan duration)
        {
            // Stop any existing timer to prevent overlap
            _curseTimer?.Stop();

            // Start a new timer to remove the curse after the specified duration
            _curseTimer = Timer.DelayCall(duration, () =>
            {
                IsCursed = false; // The item is no longer cursed after 5 minutes
                _curseTimer = null; // Clear the timer reference

                // Notify the current holder if there's one
                if (RootParent is Mobile holder)
                {
                    holder.SendMessage("The curse on the artifact has faded.");
                }
            });
        }

        private void ApplyCursedEffects(Mobile owner)
        {
            // Prevent hiding, invisibility, and dismount the player
            owner.Dismount();
            owner.Hidden = false; // Ensure visibility if holding the artifact
            owner.SendMessage("The cursed artifact prevents you from hiding or becoming invisible.");
        }

        private void RemoveCursedEffects(Mobile owner)
        {
            owner.SendMessage("You feel the curse's effects lift temporarily, but the item remains cursed.");
        }
    }
}
