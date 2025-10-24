using System.Numerics;

namespace TerminalRenderer;

public enum Axis
{
    X,
    Y,
    Z
}

public enum Plane
{
    XY,
    XZ,
    YZ
}

public record struct Vector3(double x, double y, double z) 
{
    public double X { get; init; } = x;
    public double Y { get; init; } = y;
    public double Z { get; init; } = z;

    public double Dot(Vector3 v2) => X * v2.X + Y * v2.Y + Z * v2.Z;
    public Vector3 Cross(Vector3 v2) => new(
            Y * v2.Z - Z * v2.Y,
            Z * v2.X - X * v2.Z,
            X * v2.Y - Y * v2.X
    );
    public static double Length(Vector3 v) => Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
    public static Vector3 Normalize(Vector3 v) => v / Length(v);
    public static Vector3 operator *(Vector3 v1, double s) => new(s * v1.X, s * v1.Y, s * v1.Z);
    public static Vector3 operator *(double s, Vector3 v1) => v1 * s;
    public static Vector3 operator /(Vector3 v1, double s) => v1 * (1 / s);
    public static Vector3 operator +(Vector3 v1, Vector3 v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    public static Vector3 operator -(Vector3 v1, Vector3 v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    public static Vector3 operator *(Matrix4 m, Vector3 v) => new(
            m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z + m[0, 3] * 1,
            m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z + m[1, 3] * 1,
            m[2, 0] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z + m[2, 3] * 1
    );
}

public partial record struct Matrix4(double[,] values)
{
    public readonly double this[int row, int col]
    {
        get => values[row, col];
        private set => values[row, col] = value;
    }

    public static Matrix4 New(Vector3 r1, Vector3 r2, Vector3 r3) => new(new double[4, 4]
    {
        { r1.X, r1.Y, r1.Z, 0},
        { r2.X, r2.Y, r2.Z, 0},
        { r3.X, r3.Y, r3.Z, 0},
        {0,     0,     0,   1}
    });

    public static Matrix4 Identity() => new(new double[4, 4]
    {
        {1, 0, 0, 0},
        {0, 1, 0, 0},
        {0, 0, 1, 0},
        {0, 0, 0, 1}
    });

    public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
    {
        var result = new double[4, 4];
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                result[row, col] = 0;
                for (int k = 0; k < 4; k++)
                {
                    result[row, col] += m1[row, k] * m2[k, col];
                }
            }
        }
        return new Matrix4(result);
    }

    public static Matrix4 operator *(Matrix4 m, double s)
    {
        var result = new double[4, 4];
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                result[row, col] = m[row, col] * s;
            }
        }
        return new Matrix4(result);
    }
    
    public static Matrix4 operator *(double s, Matrix4 m) => m * s;

    public static Matrix4 Chain(params Matrix4[] matrices)
    {
        if (matrices.Length == 0)
            return Identity();

        var result = matrices.Last();

        for (int i = matrices.Length - 2; i >= 0; i--)
            result *= matrices[i]; 

        return result;
    }
}
