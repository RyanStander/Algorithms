using GXPEngine;
using System;

/**
 * This is an example subclass of the TiledView that just generates random tiles.
 */
class TiledDungeonView : TiledView
{
    private Dungeon _dungeon;
	public TiledDungeonView(Dungeon pDungeon, TileType pDefaultTileType) : base(pDungeon.size.Width, pDungeon.size.Height, (int)pDungeon.scale, pDefaultTileType)
	{
        _dungeon = pDungeon;
	}

	//FOREACH room;
        //FOR 0 to the width of the room
            //FOR 0 to the height of the room
                //fill the room with floor tiles based on the FOR loop values
            //END FOR LOOP
        //END FOR LOOP
        //FOR 0 to the width of the room
            //Create wall tile from top of room based on start position and FOR loop value
            //create wall tile from bottom of room based on start position and FOR loop value
        //END FOR LOOP
        //FOR 0 to the height of the room
            //Create wall tile from top of room based on start position and FOR loop value
            //create wall tile from bottom of room based on start position and FOR loop value
        //END FOR LOOP
    //END FOREACH
    //FOREACH door
        //create floor tile at stored door location
    //END FOREACH
	protected override void generate()
	{
        //---------------------
        //      Room fill
        //---------------------
        foreach(Room room in _dungeon.rooms)
        {
            //-----------------------
            //     Floor Fill         
            //-----------------------
            for (int i = 0; i < room.area.Width; i++)
            {
                for (int t = 0; t < room.area.Height; t++)
                {
                    SetTileType(room.area.X+i, room.area.Y+t, TileType.GROUND);
                }
            }
            //-----------------------
            //     Wall Fill         
            //-----------------------
            for (int i = 0; i < room.area.Width; i++)
            {
                SetTileType(room.area.X+i, room.area.Y, TileType.WALL);
                SetTileType(room.area.X + i, room.area.Y+room.area.Height-1, TileType.WALL);
            }
            for (int i = 0; i < room.area.Height; i++)
            {
                SetTileType(room.area.X , room.area.Y + i, TileType.WALL);
                SetTileType(room.area.X+room.area.Width-1, room.area.Y + i, TileType.WALL);
            }  
        }
        //-----------------------
        //    Add doors          
        //-----------------------
        foreach (Door door in _dungeon.doors)
        {
            SetTileType(door.location.X, door.location.Y, TileType.GROUND);
        }
        //Console.WriteLine("The room: " + _dungeon.rooms.IndexOf(room) + "has a width of: " + tempWidth + "and a height of: "+ tempHeight);
    }
}

