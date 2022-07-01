using GXPEngine;
using System.Drawing;
using System;
using GXPEngine.OpenGL;
using System.Collections.Generic;


/**
 * An example of a dungeon implementation.  
 * This implementation places two rooms manually but your implementation has to do it procedurally.
 */
class SufficientDungeon : Dungeon
{
    public SufficientDungeon(Size pSize) : base(pSize) { }

    /**
	 * This method overrides the super class generate method to implement a two-room dungeon with a single door.
	 * The good news is, it's big enough to house an Ogre and his ugly children, the bad news your implementation
	 * should generate the dungeon procedurally, respecting the pMinimumRoomSize.
	 * 
	 * Hints/tips: 
	 * - start by generating random rooms in your own Dungeon class and placing random doors.
	 * - playing/experiment freely is the key to all success
	 * - this problem can be solved both iteratively or recursively
	 */
    protected override void generate(int pMinimumRoomSize)
    {
        /*
         * MAKING ROOMS
        Start off by creating 1 big room
        check which axis has most space
        one with most space will be split between min size and max size
        then create two seperate rooms one starting from the origin point to split point and one starting from the split point to end point
        repeat
        if no space left stop
        * MAKING DOORS
        Start off by checking if a wall is shared
        create door
        */
        //--------------------------
        //      Initialise      
        //--------------------------
        int spaceLeftCount = 1;
        int roomSplit = Utils.Random(pMinimumRoomSize, size.Width - pMinimumRoomSize);

        //--------------------------
        //      room creation    
        //--------------------------
        //splits the first 2 rooms so we have something for the algorithm to work with.
        rooms.Add(new Room(new Rectangle(0, 0, roomSplit, size.Height)));
        rooms.Add(new Room(new Rectangle(roomSplit - 1, 0, size.Width - roomSplit + 1, size.Height)));
        while (spaceLeftCount > 0)
        {
            List<Room> tempRooms = new List<Room>();
            List<Room> deletionRooms = new List<Room>();
            foreach (Room room in rooms)
            {
                //checks if the current room has enough space to create a division based on width or height (decides based on which is biggest)
                if (room.area.Width >= room.area.Height && room.area.Width / 2 >= pMinimumRoomSize)
                {
                    roomSplit = Utils.Random(pMinimumRoomSize, room.area.Width - pMinimumRoomSize);
                    tempRooms.Add(new Room(new Rectangle(room.area.X, room.area.Y, roomSplit, room.area.Height)));
                    tempRooms.Add(new Room(new Rectangle(room.area.X + roomSplit - 1, room.area.Y, room.area.Width - roomSplit + 1, room.area.Height)));
                    deletionRooms.Add(room);
                }
                else if (room.area.Width <= room.area.Height && room.area.Height / 2 >= pMinimumRoomSize)
                {
                    roomSplit = Utils.Random(pMinimumRoomSize, room.area.Height - pMinimumRoomSize);
                    tempRooms.Add(new Room(new Rectangle(room.area.X, room.area.Y, room.area.Width, roomSplit)));
                    tempRooms.Add(new Room(new Rectangle(room.area.X, room.area.Y + roomSplit - 1, room.area.Width, room.area.Height - roomSplit + 1)));
                    deletionRooms.Add(room);
                }
            }
            spaceLeftCount = 0;
            foreach (Room room in rooms)
            {
                //if there is no space left for any divisions the while loop ends.
                if ((room.area.Width / 2 > pMinimumRoomSize || room.area.Height / 2 > pMinimumRoomSize))
                {
                    spaceLeftCount++;
                }
            }
            //Room Deletion
            rooms.AddRange(tempRooms);
            foreach (var r in deletionRooms)
            {
                //removes any rooms that were previously split.
                rooms.Remove(r);
            }

        }
        //door creation
        foreach (Room room1 in rooms)
        {
            foreach (Room room2 in rooms)
            {
                //looks for 2 rooms that share a side and then splits them for that, allows for the possibility of a 3 way room.
                if (room1.area.Y == room2.area.Y + room2.area.Height - 1 && room1.area.X == room2.area.X)
                {
                   /*
                    for (int index=0; index<rooms.length; index ++ ) {
                    Room room = rooms[index]; bram tips
                    foreach (Room room in rooms)*/
                    doors.Add(new Door(new Point(Utils.Random(room1.area.X + 2, room1.area.X + room1.area.Width - 2), room1.area.Y), rooms.IndexOf(room1), rooms.IndexOf(room2)));
                    //Console.WriteLine("Room " + rooms.IndexOf(room1) + " and Room " + rooms.IndexOf(room2) + " match on search 1");
                    //Console.WriteLine("Room " + rooms.IndexOf(room1) + " dimensions" + room1.ToString() + "Room " + rooms.IndexOf(room2) + " dimensions" + room2.ToString());
                }
                else
                if (room1.area.X == room2.area.X + room2.area.Width - 1 && room1.area.Y == room2.area.Y)
                {
                    doors.Add(new Door(new Point(room1.area.X, Utils.Random(room1.area.Y + 2, room1.area.Y + room1.area.Height - 2)), rooms.IndexOf(room1), rooms.IndexOf(room2)));
                    //Console.WriteLine("Room " + rooms.IndexOf(room1) + " and Room " + rooms.IndexOf(room2) + " match on search 2");
                    //Console.WriteLine("Room " + rooms.IndexOf(room1) + " dimensions" + room1.ToString() + "Room " + rooms.IndexOf(room2) + " dimensions" + room2.ToString());
                }
            }
        } 
        //Console.WriteLine(ToString());
    }
}

/*
    room split (create 2 new rooms in an old room)

    a = new Room(new Rectangle(room.Area.X, room.Area.Y, room.Area.Width, split + 1));
    b = new Room(new Rectangle(room.Area.X, room.Area.Y + split, room.Area.Width, room.Area.Height - split));
   * Attempt 2
   * bool isSpaceLeft = true;
        int remRoomWidth = size.Width;
        int remRoomHeight = size.Height;
        int spaceTakenX = 0;
        int spaceTakenY = 0;
        int prevRoomWidth = Utils.Random(pMinimumRoomSize, remRoomHeight)-1;
        int prevRoomHeight = Utils.Random(pMinimumRoomSize, remRoomWidth)-1;
    while (isSpaceLeft)
        {
            if (remRoomHeight / 2 > pMinimumRoomSize || remRoomWidth / 2 > pMinimumRoomSize)
            {
                
                
                if (remRoomHeight > remRoomWidth)
                {
                    int roomHeight = Utils.Random(pMinimumRoomSize, remRoomHeight);
                    prevRoomHeight = roomHeight;
                    rooms.Add(new Room(new Rectangle(spaceTakenX, spaceTakenY, prevRoomWidth, roomHeight)));
                    spaceTakenY += roomHeight;
                    remRoomHeight -= roomHeight;
                    if (spaceTakenY > 0)
                    {
                        spaceTakenY--;
                    }
                }
                else
                {
                    int roomWidth = Utils.Random(pMinimumRoomSize, remRoomWidth);
                    prevRoomWidth = roomWidth;
                    rooms.Add(new Room(new Rectangle(spaceTakenX, spaceTakenY, roomWidth, prevRoomHeight)));
                    spaceTakenX += roomWidth;
                    remRoomWidth -= roomWidth;
                    if (spaceTakenX > 0)
                    {
                        spaceTakenX--;
                    }
                }
                
            }
            else
            {
                isSpaceLeft = false;
            }
        }

        Console.WriteLine(ToString());

    }
 * Attemp 1
 * while (isSpaceLeft)
        {
            int roomWidth = Utils.Random(pMinimumRoomSize, remRoomWidth);
            int roomHeight = Utils.Random(pMinimumRoomSize, remRoomHeight);
            rooms.Add(new Room(new Rectangle(spaceTakenX, spaceTakenY, roomWidth, roomHeight)));
            spaceTakenX += roomWidth;
            spaceTakenY += roomHeight;
            remRoomHeight -= roomWidth;
            remRoomWidth -= roomHeight;
            if (remRoomHeight < pMinimumRoomSize || remRoomWidth < pMinimumRoomSize)
            {
                isSpaceLeft = false;
            }
        }*/
