using TerminalRenderer;

/*
This code is my companion to reading Peter Shirley's Fundamentals of Computer Graphics
It's a simple library for drawing and rendering to Console.

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

    var rot = Matrix4.Rotate(Axis.Z, 180);
    var trans = Matrix4.Displace(x/2, y/2, 0);
    var mat = Matrix4.Chain(rot, trans);

    var p1 = new Vector3(0, 20, 0);      // Top vertex
    var p2 = new Vector3(-15, -10, 0);   // Bottom left
    var p3 = new Vector3(15, -10, 0);    // Bottom right

    var tp1 = trans * p1;
    var tp2 = trans * p2;
    var tp3 = trans * p3;


    window.Render(s =>
    {
        //s.LineFromTo(l1, l2);
        s.DrawTriangle(tp1,tp2,tp3);
        //s.DrawTriangle(tp1,tp2,tp3);
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
