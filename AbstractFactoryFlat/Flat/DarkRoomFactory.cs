using System;
using System.Collections.Generic;
using System.Text;

namespace Flat
{
    class DarkRoomFactory : IRoomFactory
    {
        public Room CreateRoom()
        {
            Room room = new Room();
            room.Chandelier = new DarkChandelier();
            room.Wallpaper = new DarkWallpaper();

            return room;
        }
    }
}
