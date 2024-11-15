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

        // Hook into OnLocationChange to check if item is placed in a player’s inventory
        public override void OnLocationChange(Point3D oldLoc, Point3D newLoc, Map oldMap, Map newMap)
        {
            base.OnLocationChange(oldLoc, newLoc, oldMap, newMap);

            if (Parent is Mobile owner && IsCursed)
            {
                ApplyCursedEffects(owner);
                _curseHelper.StartCurseTimer(owner);
            }
        }

        // Prevent insurance while the item is cursed
        public override bool CanBeInsured => !IsCursed;
    }
}
