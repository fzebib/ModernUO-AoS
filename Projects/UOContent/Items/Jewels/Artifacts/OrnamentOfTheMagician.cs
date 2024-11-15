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

            _curseHelper = new CursedItemHelper(this);
            
            // Start the curse timer immediately when the item is created
            StartCurseTimer();
        }

        // Label number for "Ornament of the Magician"
        public override int LabelNumber => 1061105;

        // Rarity for the artifact system
        public override int ArtifactRarity => 11;

        // Property to check if the item is currently cursed
        public bool IsCursed => _curseHelper.IsCursed;

        // Start the curse timer, which will last 5 minutes
        public void StartCurseTimer()
        {
            _curseHelper.StartCurseTimer();
        }

        // Ends the curse and provides feedback to the player
        public void EndCurse()
        {
            if (RootParent is Mobile owner)
            {
                owner.SendMessage("The curse on the artifact has faded.");
            }
        }

        // Prevents the item from being insured while it is cursed
        public override bool CanBeInsured => !IsCursed;
    }
}
