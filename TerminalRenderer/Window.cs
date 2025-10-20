using System.Transactions;

namespace TerminalRenderer;

public class Window(int c, int r)
{
    public const int SleepTime = 32;
    public int ColumnNumber { get; init; } = c;
    public int RowNumber { get; init; } = r;
    public ScreenBuffer Screen { get; init; } = new (c, r);

    public void Render(Action<ScreenBuffer> drawActions)
    {
        Console.CursorVisible = false;

        while(true)
        {
            Screen.Draw(drawActions);
            Thread.Sleep(SleepTime);
        }
    }
}
