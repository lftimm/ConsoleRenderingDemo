using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TerminalRenderer;

public class ScreenBuffer
{
    private const float AspectRatio = 1;
    private const int KernelSize = 3;
    private const int HalfKernelSize = KernelSize / 2;
    public StringBuilder StringBuilder = new ();
    
    private int Columns { get; set; } 
    private int Rows { get; set; } 
    private Matrix4 OrthogonalMatrix { get; set; }
    private Pixel[,] Screen { get; set; }

    public ScreenBuffer(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
        Screen = new Pixel[Rows,Columns];
        OrthogonalMatrix = CreateOrthogonalProjectionMatrix();
    }

    private Matrix4 CreateOrthogonalProjectionMatrix()
    {
        var aspectRatioFix = Matrix4.Scale(AspectRatio, 1, 1);

        var l = -1f;
        var r = 1f;
        var b = -1f;
        var t = 1f;
        var n = 1f;
        var f = -1f;

        var scaleX = Columns / 2;
        var scaleY = Rows / 2;
        var m1 = Matrix4.Displace(scaleX, scaleY, 0) *
                  Matrix4.Scale(scaleX, -scaleY, 1.0);
        var m2 = Matrix4.Scale(2/(r-l), 2/(t-b), 2/(n-f));
        var m3 = Matrix4.Displace(-(l+r)/2, -(b+t)/2, -(f+n)/2);

        return Matrix4.MultiplyInCorrectOrder(aspectRatioFix, m1, m2, m3); 
    }

    private void FlushScreen(Brightness brightness) => FlushScreen((int)brightness);    

    private void FlushScreen(int brightness)
    {
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                Screen[i, j] = new (brightness);
    }

    private void DumpBufferToConsole()
    {
        StringBuilder.Clear();

        for (int y = 0; y < Rows; y++) 
        {
            for (int x = 0; x < Columns; x++)
            {
                StringBuilder.Append(Screen[y, x].Display);
            }
            StringBuilder.AppendLine();
        }


        Console.Write(StringBuilder.ToString());
    }

    private void ApplyBlur()
    {
        var temp = Screen;

        for(int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                var brightness = CalculateBrightness(c, Columns, (k) => GetBrightnessIn(r,k));
                temp[r, c] = new Pixel(brightness);
            }
        }

        for(int c = 0; c < Columns; c++)
        {
            for (int r = 0; r < Rows; r++)
            {
                var brightness = CalculateBrightness(r, Rows, (k) => GetBrightnessIn(k,c));
                temp[r, c] = new Pixel(brightness);
            }
        }

        Array.Copy(temp, Screen, temp.Length);
    }

    private int CalculateBrightness(int index, int maxSize, Func<int, double> brightnessGet) 
    {
        var sum = 0.0;
        for(int k = -HalfKernelSize; k <= HalfKernelSize; k++)
        {
            var indexToCheck = k + index;
            if (indexToCheck < 0 || indexToCheck >= maxSize)  
                continue;

            sum += brightnessGet(indexToCheck) * (1.0 / KernelSize);
        }
    
        return (int)Math.Floor(sum);
    }

    private Pixel GetPixelIn(int r, int c) => Screen[r, c];
    private int GetBrightnessIn(int r, int c) => GetPixelIn(r,c).Brightness;

    public void Draw(Action<ScreenBuffer> drawThis)
    {
        FlushScreen(0);
        drawThis.Invoke(this);
        //ApplyBlur();
        DumpBufferToConsole();
        Clear();
    }
    
    public void ShowSize() => FlushScreen(Brightness.Normal);

    public void Clear()
    {
        StringBuilder.Clear();
        Console.SetCursorPosition(0, 0);
    }

    public void PointAt(Vector3 vec, Brightness brightness) 
    {
        var newVec = OrthogonalMatrix * vec;
        var screenX= (int)Math.Clamp(Math.Round(newVec.X), 0, Columns-1);
        var screenY= (int)Math.Clamp(Math.Round(newVec.Y),0, Rows-1);
        Screen[screenY, screenX] = new ((int)brightness);  
    }

    public void PointAt(double x, double y, double z, Brightness brightness)
    {
        var vec = new Vector3(x, y, z);
        PointAt(vec, brightness);
    }

    public void LineFromTo(Vector3 v1, Vector3 v2) => LineFromTo(v1.X, v1.Y, v2.X, v2.Y);

    public void LineFromTo(double x0, double y0, double x1, double y1)
    {
        var v0 = new Vector3(x0, y0, 0);
        var v1 = new Vector3(x1, y1, 0);

        Vector3 parametricLineEquation(double t) => v0 + t * (v1 - v0);

        var x = x0;
        var step = 5e-1;

        while(x < x1)
        {
            var t = (x - x0)/(x1- x0);

            var v = parametricLineEquation(t);

            PointAt(v.X, v.Y, 0, Brightness.Bright);

            x += step;
        }
    }

    public void DrawTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        var xmin = (float)Math.Min(Math.Min(a.X, b.X), c.X);
        var xmax = (float)Math.Max(Math.Max(a.X, b.X), c.X);
        var ymin = (float)Math.Min(Math.Min(a.Y, b.Y), c.Y);
        var ymax = (float)Math.Max(Math.Max(a.Y, b.Y), c.Y);

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

        float stepX = 1f / Columns;
        float stepY = 1f / Rows;
        for (float x = xmin; x <= xmax; x += stepX)
        {
            for(float y = ymin; y <= ymax; y += stepY)
            {
                var beta = getBeta(x, y);
                var gamma = getGamma(x, y);
                var alpha = 1 - beta - gamma;

                if (beta >= 0 && gamma >= 0 && (beta + gamma) <= 1)
                {
                    var z = alpha * a.Z + beta * b.Z + gamma * c.Z;
                    PointAt(x, y, z, Brightness.Bright);
                }
            }
        }
    }
}
