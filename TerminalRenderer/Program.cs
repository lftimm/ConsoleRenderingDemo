using TerminalRenderer;

/*
This code is my companion to reading Peter Shirley's Fundamentals of Computer Graphics
It's a simple library for drawing and rendering to Console.

Some things i did here:

- OpenGL-like rendering pipeline (Vertex, Tesselation, Fragment)
- Triangle Rasterization using Barycentric Coordinates
- Linear Alegbra Infraestructure, Matrix4 and Vector3 
- Post processing with box filter
- Clean architecture respecting SRP and DI
- Obj rendering

---------------------------------------------------------------------
When using it be mindful to your terminal's fontsize and window size.
Enjoy !!
*/


try
{
    var trans = Matrix4.Displace(0, -0.75f, 0) * Matrix4.Scale(.30f, .30f, .30f);
    var teapot = ObjImporter.Read(@"C:\Users\lftim\source\repos\TerminalRenderer\TerminalRenderer\Assets\teapot.obj")
        .Select(x => new Triangle(trans * x.A, trans * x.B, trans * x.C))
        .ToArray();

    var x = Console.WindowWidth;
    var y = Console.WindowHeight-1;

    var window = new ConsoleEngine(x,y);
    var rotationSpeed = 45.0f;
    window.RenderScene((t) =>
    {
        var rotate = Matrix4.Rotate(Axis.Y, rotationSpeed * t) * (Matrix4.Rotate(Axis.Z, rotationSpeed * t));
        return teapot.Select(x => new Triangle(rotate * x.A, rotate * x.B, rotate * x.C)).ToArray();
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
