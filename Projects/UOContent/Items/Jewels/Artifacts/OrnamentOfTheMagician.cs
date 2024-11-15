using ModernUO.Serialization;
using Server.Mobiles;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class OrnamentOfTheMagician : GoldBracelet, ICursedItem
    {
        private readonly CursedItemHelper _curseHelper;

        [Constructible]
        public OrnamentOfTheMagician()
        {
            Hue = 0x554;
            Attributes.CastRecovery = 3;
            Attributes.CastSpeed = 2;
            Attributes.LowerManaCost = 10;
            Attributes.LowerRegCost = 20;
            Resistances.Energy = 15;

            _curseHelper = new CursedItemHelper();
        }

        public override int LabelNumber => 1061105; // Ornament of the Magician
        public override int ArtifactRarity => 11;

        // ICursedItem implementation
        public bool IsCursed => _curseHelper.IsCursed;

        public void ApplyCursedEffects(Mobile owner)
        {
            _curseHelper.ApplyCursedEffects(owner);
        }

        public void RemoveCursedEffects(Mobile owner)
        {
            _curseHelper.RemoveCursedEffects(owner);
        }

        // Called when the item is dropped, which starts the curse timer
        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile owner && IsCursed)
            {
                ApplyCursedEffects(owner);
                _curseHelper.StartCurseTimer(owner);
            }
        }

        // Prevent insurance while the item is cursed
        public override bool CanBeInsured => !IsCursed;
    }
}
