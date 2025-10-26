using System.Diagnostics;
using TerminalRenderer;

/*
This code is my companion to reading Peter Shirley's Fundamentals of Computer Graphics
It's a simple library for drawing and rendering to Console.

Stuff i worked with so far:

- Rendering Loop, with double buffering and basic brightness levels
- Rendering 2D Points, Lines and Triangles using their respective equations
- Postprocessing with a simple box blur filter, appling convolution
- OOP Modeling with Vector3, Matrix4, Axis, Plane, Window, ScreenBuffer etc.
- 3D Transformations: Translation, Scaling, Rotation, Sheering
- Canonical Matrix to map from world coordinates to screen coordinates

----------------------------------------------------------------------------
When using it be mindful to your terminal's fontsize and window size.
Enjoy !!

*/

try
{
    var x = Console.WindowWidth;
    var y = Console.WindowHeight-1;

    var window = new Window(x,y);


    var t1 = new Vector3(-0.5, 0.5, 0);
    var t2 = new Vector3(-0.5, -0.5, 0);
    var t3 = new Vector3(0.5, -0.5, 0);
    var t4 = new Vector3(0.5, 0.5, 0);


    var stopwatch = Stopwatch.StartNew();
    var rotationSpeed = 45.0;
    window.Render(s =>
    {
        var time = stopwatch.Elapsed.TotalSeconds;

        var angle = time * rotationSpeed;

        var rotation = Matrix4.Rotate(Axis.Y, angle);

        var r1 = rotation * t1;
        var r2 = rotation * t2;
        var r3 = rotation * t3;
        var r4 = rotation * t4;

        s.DrawTriangle(r1, r2, r3);
        s.DrawTriangle(r3, r4, r1);
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
