using System;
using Server.Mobiles;

namespace Server.Items
{
    public interface ICursedItem
    {
        bool IsCursed { get; }
        void ApplyCursedEffects(Mobile owner);
        void RemoveCursedEffects(Mobile owner);
    }
}
