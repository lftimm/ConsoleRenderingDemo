
namespace TerminalRenderer;
public class View(Vector3 Eye, Vector3 Gaze, Vector3 Up)
{
    public Matrix4 Transform = CreateViewMatrix(Eye, Gaze, Up);

    private static Matrix4 CreateViewMatrix(Vector3 e, Vector3 g, Vector3 t)
    {
        Vector3 w = -1*g/Vector3.Length(g);
        Vector3 tCw = t.Cross(w);
        Vector3 u = tCw/ Vector3.Length(tCw);
        Vector3 v = w.Cross(u);

        return Matrix4.New(u, v, w) * Matrix4.Displace(-e.X, -e.Y, -e.Z);
    }
}