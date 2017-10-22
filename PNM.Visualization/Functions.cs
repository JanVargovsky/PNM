namespace PNM.Visualization
{
    public class Functions
    {
        public static float Quadratic(float x) => x * x;
        public static float Constant(float x) => 50;
    }

    public class DoubleIntegrateFunctions
    {
        public static float Quadratic(float x) => x + x * x * x * x / 12;
        public static float Constant(float x) => x + 25 * x * x;
    }
}
