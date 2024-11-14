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
            Hue = 0x554; // Default cursed artifact hue
        }

        // Called when the item is equipped
        public override bool OnEquip(Mobile from)
        {
            if (IsCursed)
            {
                from.SendMessage("An immediate curse emanates from the artifact as you equip it!");
                ApplyCursedEffects(from);
                StartCurseTimer(TimeSpan.FromMinutes(5));
            }
            return base.OnEquip(from);
        }

        // Called when the item is added to the player's inventory or container
        public void OnAdded(object parent)
        {
            if (IsCursed && parent is Mobile owner)
            {
                owner.SendMessage("An immediate curse emanates from the artifact as you pick it up!");
                ApplyCursedEffects(owner);
                StartCurseTimer(TimeSpan.FromMinutes(5));
            }
        }

        // Custom method to manually remove curse effects when the timer expires
        public void RemoveCurse(Mobile owner)
        {
            if (IsCursed)
            {
                RemoveCursedEffects(owner); // Removes effects but not the curse itself
                IsCursed = false; // The item is no longer cursed
            }
        }

        private void StartCurseTimer(TimeSpan duration)
        {
            _curseTimer?.Stop(); // Stop any existing timer to avoid overlap

            // Start a new timer to remove the curse after the specified duration
            _curseTimer = Timer.DelayCall(duration, () =>
            {
                IsCursed = false;
                _curseTimer = null;

                if (RootParent is Mobile holder)
                {
                    holder.SendMessage("The curse on the artifact has faded.");
                }
            });
        }

        private void ApplyCursedEffects(Mobile owner)
        {
            // Ensure visibility if holding the artifact
            owner.Hidden = false;

            // Attempt to dismount if the server supports mounting
            if (owner.Mount != null)
            {
                owner.Mount.Rider = null; // Dismount the player if possible
            }

            owner.SendMessage("The cursed artifact prevents you from hiding or becoming invisible.");
        }

        private void RemoveCursedEffects(Mobile owner)
        {
            owner.SendMessage("You feel the curse's effects lift temporarily, but the item remains cursed.");
        }
    }
}
