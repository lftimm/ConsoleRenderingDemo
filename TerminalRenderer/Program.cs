using TerminalRenderer;

/*
This code is my companion to reading Peter Shirley's Fundamentals of Computer Graphics
It's a simple library for drawing and rendering to Console.

Planned stuff:
- Anti Aliasing
- 3d
- More shapes

----------------------------------------------------------------------------
When using it be mindful to your terminal's fontsize and window size.
Currently running cmd inside Windows Terminal, with Cascadia Mono 4
Enjoy !!
*/

try
{

    var x = 475;
    var y = 124;
    var window = new Window(x,y);

    var p1 = new Vector3(4, y / 2 + 10 , 0);
    var p2 = new Vector3(x/2-20, 10, 0);
    var p3 = new Vector3(x/2+60, y-3, 0);

    window.Render(s =>
    {
        s.LineFromTo(p1.x, p1.y, p1.x, p1.y);
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
