using System.Diagnostics;

namespace TerminalRenderer;

public class Window(int Columns, int Rows)
{
    public const double FrameTime = 1000/100;
    public ScreenBuffer Screen { get; init; } = new (Columns, Rows);

    public void Render(Action<ScreenBuffer, double> drawActions)
    {
        Console.CursorVisible = false;

        var stopwatch = Stopwatch.StartNew();
        while (true)
        {
            var frameStart = stopwatch.Elapsed.TotalMilliseconds;
            Screen.Draw(s => drawActions(s, frameStart/1000));
        }
    }
}
