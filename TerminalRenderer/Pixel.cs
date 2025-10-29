namespace TerminalRenderer;
public struct Pixel
{
    public static int BrightnessLevels = Enum.GetValues(typeof(Brightness)).Length -1;
    public static char[] Map = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ".Reverse().ToArray();
    public int Brightness { get;  }
    public char Display { get; }

    public Pixel(Brightness brightness) => new Pixel((int)brightness);
    public Pixel(int brightness)
    {
        Brightness = brightness;
        int brightnessPos = (int)((Brightness / (float)BrightnessLevels) * Map.Length);

        Display = Map[Math.Clamp(brightnessPos, 0, Map.Length-1)];
    }
}
