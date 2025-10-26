namespace TerminalRenderer;

public class Window(int Columns, int Rows)
{
    public const int SleepTime = 32;
    public ScreenBuffer Screen { get; init; } = new (Columns, Rows);

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
