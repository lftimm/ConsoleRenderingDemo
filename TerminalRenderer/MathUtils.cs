namespace TerminalRenderer;

public record struct Vector3(double x, double y, double z) 
{
    public double X { get; init; } = x;
    public double Y { get; init; } = y;
    public double Z { get; init; } = z;


    public static Vector3 operator *(Vector3 v1, double s) => new(s * v1.X, s * v1.Y, s * v1.Z);
    public static Vector3 operator *(double s, Vector3 v1) => v1 * s;
    public static Vector3 operator +(Vector3 v1, Vector3 v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    public static Vector3 operator -(Vector3 v1, Vector3 v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
}

public record struct Matrix3(double[,] values)
{
    public readonly double this[int row, int col]
    { 
        get => values[row, col] ;
        init => values[row, col] = value;
    }


    public static Matrix3 New(Vector3 r1, Vector3 r2, Vector3 r3) => new(new double[3, 3]
    {
        { r1.X, r1.Y, r1.Z },
        { r2.X, r2.Y, r2.Z },
        { r3.X, r3.Y, r3.Z }
    });
    public static Matrix3 Identity() => new(new double[3, 3]
    {
        {1,0,0}, 
        {0,1,0},
        {0,0,1} 
    });
    public static Vector3 operator *(Matrix3 m, Vector3 v) => new(
        m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z,
        m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z,
        m[2, 1] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z
    );
}

