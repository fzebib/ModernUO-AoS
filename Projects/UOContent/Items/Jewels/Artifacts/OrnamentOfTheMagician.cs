using ModernUO.Serialization;
using Server.Items;

namespace Server.Items;

[SerializationGenerator(0, false)]
public partial class OrnamentOfTheMagician : CursedArtifact
{
    [Constructible]
    public OrnamentOfTheMagician() : base(0x1088) // Item ID for a bracelet
    {
        Hue = 0x554; // Color of the ornament
        Attributes.CastRecovery = 3;
        Attributes.CastSpeed = 2;
        Attributes.LowerManaCost = 10;
        Attributes.LowerRegCost = 20;
        Resistances.Energy = 15;
    }

    public override int LabelNumber => 1061105; // Ornament of the Magician
    public override int ArtifactRarity => 11;
}
