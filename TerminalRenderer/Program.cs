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

    List<Vector3> triangle = [
        new Vector3(0, 1, 0),
        new Vector3(-1, -1, 0),
        new Vector3(1, -1, 0)
    ];

    window.Render((s, t) =>
    {
        s.DrawTriangle(
            triangle[0],
            triangle[1],
            triangle[2]
        );
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
