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
        }

        public override int LabelNumber => 1061105; // Ornament of the Magician
        public override int ArtifactRarity => 11;

        public bool IsCursed => _curseHelper.IsCursed;

        public void StartCurseTimer()
        {
            _curseHelper.StartCurseTimer();
        }

        public void EndCurse()
        {
            // For now, just send a message to the player when the curse ends
            if (Parent is Mobile owner)
            {
                owner.SendMessage("The curse on the artifact has faded.");
            }
        }

        // Start the curse when the item is dropped into a player’s inventory
        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile owner)
            {
                StartCurseTimer();
                owner.SendMessage("The artifact is cursed and will be for 5 minutes.");
            }
        }
    }
}
