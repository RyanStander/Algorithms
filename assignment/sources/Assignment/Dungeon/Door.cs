using System.Drawing;

/**
 * This class represents (the data for) a Door, at this moment only a position in the dungeon.
 * Changes to this class might be required based on your specific implementation of the algorithm.
 */
class Door
{
	public readonly Point location;

	//Keeping tracks of the Rooms that this door connects to,
	//might make your life easier during some of the assignments
	public int roomA;
	public int roomB;

	//You can also keep track of additional information such as whether the door connects horizontally/vertically
	//Again, whether you need flags like this depends on how you implement the algorithm, maybe you need other flags
	public bool horizontal = false;

	public Door(Point pLocation,int room1,int room2)
	{
		location = pLocation;
        roomA = room1;
        roomB = room2;
	}
    public string ToString()
    {
        return ("Door: (" + location.X+ ", " +location.Y+")");
    }
}

