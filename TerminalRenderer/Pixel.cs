namespace TerminalRenderer;

public class Pixel
{
    public int Brightness { get;  }
    public char Display { get; }

    public Pixel(Brightness brightness) => Brightness = (int)brightness;
    public Pixel(int brightness)
    {
        Brightness = brightness;
        Display = brightness switch
        {
            >= 9 => '@',
            8 => '#',
            7 => '%',
            6 => '*',
            5 => '+',
            4 => '=',
            3 => '-',
            2 => ':',
            1 => '.',
            <= 0 => ' '
        };

    }
}
