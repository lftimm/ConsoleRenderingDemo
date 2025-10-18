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
Currently using windows terminal cmd, font Cascadia Mono 4
Enjoy !!
*/

try
{
    var c = 475;
    var r = 124;
    var window = new Window(c,r);

    window.Render(s =>
    {
        s.ShowSize();
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
