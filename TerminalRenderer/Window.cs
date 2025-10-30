using System.Diagnostics;
using System.Text;

namespace TerminalRenderer;

public class Window
{
    public const double FrameTime = 1000/100;
    private FrameBuffer Buffer { get; }
    private Renderer Renderer { get; }
    private StringBuilder StringBuilder { get; }

    public Window(int width, int height)
    {
        Buffer = new FrameBuffer(width, height);
        Renderer = new Renderer(width, height);
        StringBuilder = new StringBuilder();
    }

    public void RenderScene(Triangle[] triangles)
    {
        var watch = Stopwatch.StartNew();
        while (true)
        {
            Renderer.Render(Buffer, triangles);
            DisplayBuffer();
            var frameTime = watch.ElapsedMilliseconds;
            if (frameTime < FrameTime)
                Thread.Sleep((int)(FrameTime - frameTime));
            watch.Restart();
        }
    }

    private void DisplayBuffer()
    {
        Console.SetCursorPosition(0, 0);
        Buffer.Clear(Brightness.Dark);
        StringBuilder.Clear();

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
