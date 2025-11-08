using System.Diagnostics;
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

Note: If anyone ever touches this again, consider rewriting the math classes to use System.Numerics.
*/


try
{
    var trans = Matrix4.Displace(0, 0, 2f); 
    var teapot = ObjImporter.Read(@"Assets\teapot.obj")
        .Select(x => new Triangle(trans.Transform(x.A), trans.Transform(x.B), trans.Transform(x.C)))
        .ToArray();

    teapot = Matrix4.Scale(0.5f,0.5f,0.5f).Transform(teapot);

    var cubefactory = () => ObjImporter.Read(@"Assets\cube.obj");
    var cube = Matrix4.Displace(-2f,0,0).Transform(cubefactory());
    var cube2 = Matrix4.Displace(2f,0,0).Transform(cubefactory());
        
    var x = Console.WindowWidth;
    var y = Console.WindowHeight-1;

    var window = new ConsoleEngine(x,y);
    var rotationSpeed = 45.0f;

    window.RenderScene((t) =>
    {
        return [.. cube, ..cube2];
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
