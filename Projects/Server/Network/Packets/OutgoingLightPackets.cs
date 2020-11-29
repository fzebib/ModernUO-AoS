/*************************************************************************
 * ModernUO                                                              *
 * Copyright 2019-2020 - ModernUO Development Team                       *
 * Email: hi@modernuo.com                                                *
 * File: OutgoingLightPackets.cs                                         *
 *                                                                       *
 * This program is free software: you can redistribute it and/or modify  *
 * it under the terms of the GNU General Public License as published by  *
 * the Free Software Foundation, either version 3 of the License, or     *
 * (at your option) any later version.                                   *
 *                                                                       *
 * You should have received a copy of the GNU General Public License     *
 * along with this program.  If not, see <http://www.gnu.org/licenses/>. *
 *************************************************************************/

using System.Buffers;

namespace Server.Network
{
    public static class OutgoingLightPackets
    {
        public static void SendPersonalLightLevel(this NetState ns, Serial serial, int level)
        {
            if (ns == null || !ns.GetSendBuffer(out var buffer))
            {
                return;
            }

            var writer = new CircularBufferWriter(buffer);
            writer.Write((byte)0x4E); // Packet ID
            writer.Write(serial);
            writer.Write((byte)level);

            ns.Send(ref buffer, writer.Position);
        }

        public static void SendGlobalLightLevel(this NetState ns, int level = 0)
        {
            if (ns == null || !ns.GetSendBuffer(out var buffer))
            {
                return;
            }

            buffer[0] = 0x4F; // Packet ID
            buffer[1] = (byte)level;

            ns.Send(ref buffer, 2);
        }
    }
}