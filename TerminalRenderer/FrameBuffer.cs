namespace TerminalRenderer;

public class FrameBuffer
{
    private readonly Pixel[] _buffer;
    public int Width { get; }
    public int Height { get; } 


    public FrameBuffer(int width, int height)
    {
        Width = width;
        Height = height;
        _buffer = new Pixel[Height*Width];
    }

    public Pixel GetPixel(int x, int y) => _buffer[x+y*Width];
    public void SetPixel(int x, int y, Pixel pix) => _buffer[x+y*Width] = pix;

    public void Clear(Brightness brightness)
    {
        for(int i = 0; i < _buffer.Length; i++)
            _buffer[i] = new Pixel(brightness); 
    }
}
