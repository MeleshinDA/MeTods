using ScottPlot;

public class Program
{
    private static int count = 0;
    public static void Main(string[] args)
    {
        var a = 0;
        var b = 1;
        
        var startY = -3;
        var startX = 0;
        
        for (var N = 10; N <= 30; N += 10)
        {
            Рисовать(ТочноеРешение(startY, startX, N), N, "Точное решение");
            Рисовать(МетодЭйлера(startY, startX, N), N, "Метод Эйлера");
            Рисовать(ФормулаТейлора3Порядка(startY, startX, N), N, "ФормулаТейлора3Порядка");

        }
    }

    private static void Рисовать((double[], double[]) точки, int stepsCount, string methodName)
    {
        var plt = new ScottPlot.Plot(800, 600);
        plt.AddScatter(точки.Item1, точки.Item2);
        var path = $"Графики\\{methodName}";
        Directory.CreateDirectory(path);
        var pathF = path + $"\\{stepsCount}.png";
        plt.SaveFig(pathF);
    }
    
    private static (double[], double[]) ТочноеРешение(int startY, int startX, int stepsCount)
    {
        double h = 1.0 / stepsCount;
        double nextX = startX;
        double nextY = startY;
        var иксы = new List<double>();
        var игреки = new List<double>();
        var func = (double x) => -3 * Math.Sqrt(1 - x);
        for (var i = 0; i <= stepsCount; i++)
        {
            иксы.Add(nextX);
            игреки.Add(nextY);
            
            nextX += h;
            nextX = Math.Round(nextX, 2);
            nextY = func(nextX);
        }
        return (иксы.ToArray(), игреки.ToArray());
    }
    
    private static (double[], double[]) МетодЭйлера(int startY, int startX, int stepsCount)
    {
        double nextY = startY;
        double nextX = startX;
        double h = 1.0 / stepsCount;
        var иксы = new List<double>();
        var игреки = new List<double>();
        for (var counter = 0; counter < stepsCount; counter++)
        {
            иксы.Add(nextX);
            игреки.Add(nextY);
            nextY = nextY + h * МояФункция(nextX, nextY);
            nextX = Math.Round(nextX + h, 10);
        }

        
        return (иксы.ToArray(), игреки.ToArray());
    }

    private static (double[], double[]) ФормулаТейлора3Порядка(int startY, int startX, int stepsCount)
    {
        double x = startX;
        double y = startY;
        double h = 1.0 / stepsCount;
        var иксы = new List<double>() {x};
        var игреки = new List<double>() {y};
        for (var i = 0; i < stepsCount; i++)
        {
            var nextX = Math.Round(x + h, 2);
            var nextY = y + h * МояФункцияП1(x, y) + h * h * МояФункцияП2(x, y) / 2;
            иксы.Add(nextX);
            игреки.Add(nextY);
            x = nextX;
            y = nextY;
        }

        return (иксы.ToArray(), игреки.ToArray()); 
    }

    private static double МояФункция(double x, double y)
    {
        return y / 2*(x - 1);
    }

    private static double МояФункцияП1(double x, double y)
    {
        return -y / (4 * (x - 1) * (x-1));
    }

    private static double МояФункцияП2(double x, double y)
    {
        return y*(8*x-7)/(16 * Math.Pow((x - 4), 4));
    }
}