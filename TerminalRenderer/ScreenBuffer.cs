using System.Text;

namespace TerminalRenderer;

public class ScreenBuffer(int columnNumber, int rowNumber)
{
    private char[,] Screen { get; } = new char[rowNumber, columnNumber];
    public int Rows { get; } = rowNumber;
    public int Columns { get; } = columnNumber;
    public StringBuilder StringBuilder = new ();

    private void FlushScreen(Brightness brightness)
    {
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                Screen[i, j] = (char)brightness; 
    }

    private void DumpBufferToConsole()
    {
        StringBuilder.Clear();

        for (int y = 0; y < Rows; y++) 
        {
            for (int x = 0; x < Columns; x++)
            {
                StringBuilder.Append(Screen[y, x]);
            }
            StringBuilder.AppendLine();
        }
        Console.Write(StringBuilder.ToString());
    }
    
    private (int,int) ConvertToScreenCoordinates(double x, double y)
    {
        if (x >= Columns || y >= Rows)  
            throw new OutOfScreenException();
        var screenCoordinates = ((int)Math.Round(x),(int)Math.Round(y));
        return screenCoordinates;
    }

    // Drawing methods
    public void Draw(Action<ScreenBuffer> drawThis)
    {
        FlushScreen(Brightness.Dark);
        drawThis.Invoke(this);
        DumpBufferToConsole();
        Clear();
    }
    
    public void Clear()
    {
        StringBuilder.Clear();
        Console.SetCursorPosition(0, 0);
    }
    
    public void PointAt(double x, double y, Brightness brightness = Brightness.Normal)
    {
        var (screenX, screenY) = ConvertToScreenCoordinates(x, y);
        Screen[screenY, screenX] = (char)brightness;  
    }

    public void LineFromTo(double x0, double y0, double x1, double y1)
    {
        var v0 = new Vector3(x0, y0, 0);
        var v1 = new Vector3(x1, y1, 0);

        Vector3 parametricLineEquation(double t) => v0 + t * (v1 - v0);

        var x = x0;
        var step = 1e-1;

        while(x < x1)
        {
            var t = (x - x0)/(x1- x0);

            var v = parametricLineEquation(t);

            PointAt(v.X, v.Y, Brightness.Bright);

            x += step;
        }
    }

    public void DrawTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        var xmin = (int)Math.Floor(Math.Min(Math.Min(a.X, b.X), c.X));
        var xmax = (int)Math.Ceiling(Math.Max(Math.Max(a.X, b.X), c.X));
        var ymin = (int)Math.Floor(Math.Min(Math.Min(a.Y, b.Y), c.Y));
        var ymax = (int)Math.Ceiling(Math.Max(Math.Max(a.Y, b.Y), c.Y));

        double getGamma(double x, double y)
        {
            var numerator = (a.Y - b.Y) * x + (b.X - a.X) * y + a.X * b.Y - b.X * a.Y;
            var denominator = (a.Y - b.Y) * c.X + (b.X - a.X) * c.Y + a.X * b.Y - b.X * a.Y;
            return numerator / denominator;
        }

        double getBeta(double x, double y)
        {
            var numerator = (a.Y - c.Y) * x + (c.X - a.X) * y + a.X * c.Y - c.X * a.Y;
            var denominator = (a.Y - c.Y) * b.X + (c.X - a.X) * b.Y + a.X * c.Y - c.X * a.Y;
            return numerator / denominator;
        }

        for (int x = xmin; x < xmax; x++)
        {
            for(int  y = ymin; y<ymax; y++)
            {
                var beta = getBeta(x, y);
                var gamma = getGamma(x, y);

                if(beta >= 0 && gamma >= 0 && (beta + gamma) <= 1)
                    PointAt(x, y, Brightness.Bright);
            }
        }

    }
}
