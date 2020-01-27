using System;
using System.Collections.Generic;
using System.Text;

namespace Flat
{
    class LightRoomFactory : IRoomFactory
    {
        public Room CreateRoom()
        {
            Room room = new Room();
            room.Chandelier = new LightChandelier();
            room.Wallpaper = new LightWallpaper();

            return room;
        }
    }
}
