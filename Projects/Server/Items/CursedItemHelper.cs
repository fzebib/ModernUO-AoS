using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class CursedItemHelper
    {
        private readonly ICursedItem _item;
        private Timer _curseTimer;

        public bool IsCursed { get; private set; } = true;

        public CursedItemHelper(ICursedItem item)
        {
            _item = item;
        }

        // Start a 5-minute timer for the curse
        public void StartCurseTimer()
        {
            IsCursed = true;
            _curseTimer?.Stop(); // Stop any existing timer

            _curseTimer = Timer.DelayCall(TimeSpan.FromMinutes(5), () =>
            {
                IsCursed = false;
                _curseTimer = null;
                _item.EndCurse();
            });
        }
    }
}
