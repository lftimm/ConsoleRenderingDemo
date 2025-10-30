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

    var triangle = new Triangle(
        new Vector3(-0.5f, -0.5f, 0),
        new Vector3(0.5f, -0.5f, 0),
        new Vector3(0.0f, 0.5f, 0)
    );
    
    window.RenderScene(new Triangle[] { triangle });

    //var rotationSpeed = 45.0;
    //var trans = Matrix4.Displace(0, -0.75, 0) *
    //            Matrix4.Scale(.30f, .30f, .30f);

    //var teapot = ObjImporter.Read(@"C:\Users\lftim\source\repos\TerminalRenderer\TerminalRenderer\teapot.obj")
    //    .Select(x => (trans * x.Item1, trans * x.Item2, trans * x.Item3)).ToList(); 

    //window.Render((s, t) =>
    //{
    //    var rotation = Matrix4.Rotate(Axis.Y, (float)(rotationSpeed * t)) * Matrix4.Rotate(Axis.Z, (float)(rotationSpeed * t));
    //    foreach (var v in teapot)
    //        s.DrawTriangle(rotation * v.Item1, rotation * v.Item2, rotation * v.Item3);
    //});

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
