using System;
using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{
    /* 
	 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
	 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
	 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
	 * https://en.wikipedia.org/wiki/Median_filter
	 * 
	 * Используйте окно размером 3х3 для не граничных пикселей,
	 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
	 */
    public static double[,] MedianFilter(double[,] original)
    {
        int width = original.GetLength(0);
        int height = original.GetLength(1);

        double[,] medianFiltered = new double[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                List<double> pixelNeighborhood = GetPixelNeighborhood(original, x, y, width, height);
                medianFiltered[x, y] = GetMedian(pixelNeighborhood);
            }
        }

        return medianFiltered;
    }

    private static double GetMedian(List<double> pixelNeighborhood)
    {
        if (pixelNeighborhood == null || pixelNeighborhood.Count <= 0)
        {
            throw new NullReferenceException("To fetch median value of list, list cannot be empty or null");
        }

        pixelNeighborhood.Sort();

        int count = pixelNeighborhood.Count;
        int middle = count / 2;

        return count % 2 == 0
            ? (pixelNeighborhood[middle - 1] + pixelNeighborhood[middle]) / 2
            : pixelNeighborhood[middle];
    }

    private static List<double> GetPixelNeighborhood(double[,] original, int x, int y, int width, int height)
    {
        var offsets = new (int dx, int dy)[]
        {
            (0, 0), (1, 0), (1, 1), (0, 1), (-1, 1),
            (-1, 0), (-1, -1), (0, -1), (1, -1)
        };

        var pixelNeighborhood = new List<double>();

        foreach (var (dx, dy) in offsets)
        {
            int neighborX = x + dx;
            int neighborY = y + dy;

            if (IsInBounds(neighborX, neighborY, width, height))
            {
                pixelNeighborhood.Add(original[neighborX, neighborY]);
            }
        }

        return pixelNeighborhood;
    }

    private static bool IsInBounds(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}