using System;
using ModernUO.Serialization;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class OrnamentOfTheMagician : BaseJewel, ICursedItem
    {
        private readonly CursedArtifactHelper _cursedHelper = new();

        [Constructible]
        public OrnamentOfTheMagician() : base(0x1088, Layer.Bracelet)
        {
            Hue = 0x554;
            Attributes.CastRecovery = 3;
            Attributes.CastSpeed = 2;
            Attributes.LowerManaCost = 10;
            Attributes.LowerRegCost = 20;
            Resistances.Energy = 15;
        }

        public override int LabelNumber => 1061105; // Ornament of the Magician
        public int ArtifactRarity => 11;

        // Implement IsCursed property via the helper
        public bool IsCursed => _cursedHelper.IsCursed;

        // Apply the cursed effects when equipped or picked up
        public override bool OnEquip(Mobile from)
        {
            if (IsCursed)
            {
                _cursedHelper.ApplyCursedEffects(from);
                _cursedHelper.StartCurseTimer(from, TimeSpan.FromMinutes(5));
            }
            return base.OnEquip(from);
        }

        // Use OnDelete to clear curse effects if the item is deleted
        public override void OnDelete()
        {
            if (RootParent is Mobile owner)
            {
                _cursedHelper.RemoveCursedEffects(owner);
            }

            base.OnDelete();
        }

        // These methods are exposed from the helper to satisfy the ICursedItem interface
        public void ApplyCursedEffects(Mobile owner) => _cursedHelper.ApplyCursedEffects(owner);
        public void RemoveCursedEffects(Mobile owner) => _cursedHelper.RemoveCursedEffects(owner);
    }
}
