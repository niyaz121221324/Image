using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        int width = original.GetLength(0);
        int height = original.GetLength(1);
        double thresholdValue = GetThresholdValue(original, whitePixelsFraction);

        double[,] thresholdFiltered = new double[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                thresholdFiltered[x, y] = original[x, y] >= thresholdValue ? 1.0 : 0.0;
            }
        }

        return thresholdFiltered;
    }

    private static double GetThresholdValue(double[,] original, double whitePixelsFraction)
    {
        int pixelCount = original.Length;
        int whitePixelCount = (int)(whitePixelsFraction * pixelCount);

        List<double> pixels = new List<double>();

        foreach (var pixel in original)
        {
            pixels.Add(pixel);
        }

        pixels.Sort();

        return whitePixelCount > 0 ? pixels[pixelCount - whitePixelCount] : double.MaxValue;
    }
}