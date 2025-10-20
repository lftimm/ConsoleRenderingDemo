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

    var p1 = new Vector3(4, y / 2 + 10 , 0);
    var p2 = new Vector3(x/2+1, 20, 0);
    var p3 = new Vector3(x/2+6, y-30, 0);

    window.Render(s =>
    {
        s.DrawTriangle(p1,p2,p3);
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
