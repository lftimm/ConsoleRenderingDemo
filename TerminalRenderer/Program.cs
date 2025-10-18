using TerminalRenderer;

try
{
    var c = 120;
    var r = 77;
    var window = new Window(c,r);

    window.Render(s =>
    {
        s.DrawTriangle(new Vector3(0, r, 0), new Vector3(c/2, 0, 0), new Vector3(c, r, 0));
    });

} catch (Exception ex)
{
    Console.Clear();
    Console.Write(
        $"Exception thrown: {ex.GetType()}\n" +
        $"StackTrace:\n{ex.StackTrace}\n"
    );
}
