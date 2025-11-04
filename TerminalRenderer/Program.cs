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
    float amount = 0f;
    object amountLock = new object();

    var trans = Matrix4.Displace(0, -0.75f, 0f) * Matrix4.Scale(.30f, .30f, .30f);
    var teapot = ObjImporter.Read(@"C:\Users\lftim\source\repos\TerminalRenderer\TerminalRenderer\Assets\teapot.obj")
        .Select(x => new Triangle(trans.Transform(x.A), trans.Transform(x.B), trans.Transform(x.C)))
        .ToArray();

    var cube = ObjImporter.Read(@"C:\Users\lftim\source\repos\TerminalRenderer\TerminalRenderer\Assets\cube.obj");



    var x = Console.WindowWidth;
    var y = Console.WindowHeight-1;

    var window = new ConsoleEngine(x,y);

    window.RenderScene((t) =>
    {
        float currentAmount;
        lock (amountLock)
        {
            currentAmount = amount;
        }

        //var amountD = -rotationSpeed * t;
        //var displace = Matrix4.Rotate(Axis.Z, amountD) * Matrix4.Displace(0, 0, currentAmount);
        return cube;
            //.Select(x => new Triangle(displace.Transform(x.A), displace.Transform(x.B), displace.Transform(x.C)))
            //.ToArray();
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
