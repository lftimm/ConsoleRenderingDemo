using System.Diagnostics;
using System.Text;

namespace TerminalRenderer;

public class ConsoleEngine
{
    public const double FrameTime = 1000.0/100;
    private FrameBuffer Buffer { get; }
    private Renderer Renderer { get; }
    private PostProcess PostProcess{ get; }
    private StringBuilder StringBuilder { get; }

    public ConsoleEngine(int width, int height)
    {
        Console.CursorVisible = false;
        Buffer = new FrameBuffer(width, height);
        Renderer = new Renderer(width, height);
        PostProcess = new PostProcess();
        StringBuilder = new StringBuilder();
    }

    public void RenderScene(Func<float, Triangle[]> draw)
    {
        var watch = Stopwatch.StartNew();
        while (true)
        {
            Buffer.Clear(Brightness.Dark);
            StringBuilder.Clear();

            Parallel.ForEach(draw((float)watch.Elapsed.TotalSeconds),(t) => Renderer.Render(Buffer, t));

            PostProcess.Apply(Buffer);

            DisplayBuffer();
        }
    }

    private void DisplayBuffer()
    {
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < Buffer.Height; y++)
        {
            for (int x = 0; x < Buffer.Width; x++)
            {
                var pixel = Buffer.GetPixel(x, y);

                StringBuilder.Append(pixel.Display);
            }
            StringBuilder.AppendLine();
        }

        Console.Write(StringBuilder.ToString());
    }
}
