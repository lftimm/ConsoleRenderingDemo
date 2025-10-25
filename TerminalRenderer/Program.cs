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
Currently running cmd inside Windows Terminal, with Cascadia Mono 4
Enjoy !!

*/

try
{
    var x = 150;
    var y = 75;


    var window = new Window(x,y);

    var p1 = new Vector3(-10, -10, 0);
    var p2 = new Vector3(10, -10, 0);
    var p3 = new Vector3(10,10,0);
    var p4 = new Vector3(-10, 10, 0);
    var p5 = new Vector3(10, -10, 10);
    var p6 = new Vector3(10, 10, 10);
    var p7 = new Vector3(-10, 10, 10);
    var p8 = new Vector3(-10, -10, 10);


    window.Render(s =>
    {
        s.PointAt(p1, Brightness.Bright);
        s.PointAt(p2, Brightness.Bright);
        s.PointAt(p3, Brightness.Bright);
        s.PointAt(p4, Brightness.Bright);
        s.PointAt(p5, Brightness.Bright);
        s.PointAt(p6, Brightness.Bright);
        s.PointAt(p7, Brightness.Bright);
        s.PointAt(p8, Brightness.Bright);
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
