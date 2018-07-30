[System.Serializable]
public struct Coord
{
    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    public int x;
    public int y;

    public override bool Equals(object obj)
    {
        if (!(obj is Coord))
            return false;

        Coord coordToEqual = (Coord)obj;
        return this.x == coordToEqual.x && this.y == coordToEqual.y;

    }

    public override int GetHashCode()
    {
        var hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(Coord c1, Coord c2)
    {
        return c1.x == c2.x && c1.y == c2.y;
    }

    public static bool operator !=(Coord c1, Coord c2)
    {
        return !(c1 == c2);
    }
}