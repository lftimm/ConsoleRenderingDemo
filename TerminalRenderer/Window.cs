namespace TerminalRenderer;

public class Window(int c, int r)
{
    public const int SleepTime = 32;
    public int ColumnNumber { get; init; } = c;
    public int RowNumber { get; init; } = r;

    private static View Camera = new (
        new Vector3(-10, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 1, 0)
    );
    public ScreenBuffer Screen { get; init; } = new (c, r, Camera);

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
