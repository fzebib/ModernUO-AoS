using ModernUO.Serialization;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class OrnamentOfTheMagician : GoldBracelet
    {
        [Constructible]
        public OrnamentOfTheMagician()
        {
            Hue = 0x554;
            Attributes.CastRecovery = 3;
            Attributes.CastSpeed = 2;
            Attributes.LowerManaCost = 10;
            Attributes.LowerRegCost = 20;
            Resistances.Energy = 15;
            IsCursed = true; // Set the item as cursed by default
        }

        public override int LabelNumber => 1061105; // Ornament of the Magician
        public override int ArtifactRarity => 11;

        // Custom property to track if the item is cursed
        public bool IsCursed { get; set; } = true;

        // Override to prevent insurance on cursed items
        public override bool CanBeInsured => !IsCursed;

        // Override the name property to add a "Cursed" label if the item is cursed
        public override void AddNameProperty(ObjectPropertyList list)
        {
            base.AddNameProperty(list);
            if (IsCursed)
            {
                list.Add("Cursed"); // Adds "Cursed" to the item's name in-game
            }
        }

        // Prevent hiding or invisibility if the player is holding a cursed item
        public static bool CanUseHiding(Mobile holder)
        {
            foreach (Item item in holder.Items)
            {
                if (item is OrnamentOfTheMagician ornament && ornament.IsCursed)
                {
                    holder.SendMessage("You cannot hide while carrying a cursed item.");
                    return false;
                }
            }
            return true;
        }

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);
            if (IsCursed)
            {
                LabelTo(from, "(Cursed)"); // Displays "(Cursed)" when single-clicking the item in-game
            }
        }

        // Serialization logic
        public OrnamentOfTheMagician(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
            writer.Write(IsCursed);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            IsCursed = reader.ReadBool();
        }
    }
}
