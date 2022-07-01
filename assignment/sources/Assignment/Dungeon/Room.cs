using System.Drawing;

/**
 * This class represents (the data for) a Room, at this moment only a rectangle in the dungeon.
 */
class Room
{
	public Rectangle area;

	public Room (Rectangle pArea)
	{
		area = pArea;
	}
    public string ToString()
    {
        return ("Room: (" +area.X+", "+area.Y+", "+area.Width+", "+area.Height+")");
    }
}
