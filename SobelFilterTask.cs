using System;

namespace Recognizer;

internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        int width = g.GetLength(0);
        int height = g.GetLength(1);
        int shift = sx.GetLength(0) / 2;
        double[,] sy = Transpose(sx);

        double[,] result = new double[width, height];
         
        for (int x = shift; x < width - shift; x++)
        {
            for (int y = shift; y < height - shift; y++)
            {
                double gx = ApplyConvolution(sx, g, x, y, shift);
                double gy = ApplyConvolution(sy, g, x, y, shift);

                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        }
            
        return result;
    }

    private static double ApplyConvolution(double[,] kernel, double[,] original, int x, int y, int shift)
    {
        int width = kernel.GetLength(0);
        int height = kernel.GetLength(1);

        double result = 0;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                result += kernel[i, j] * original[x - shift + i, y - shift + j];
            }
        }

        return result;
    }

    private static double[,] Transpose(double[,] matrix)
    {
        int width = matrix.GetLength(0);
        int height = matrix.GetLength(1);

        double[,] transposedMatrix = new double[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                transposedMatrix[i, j] = matrix[j, i];
            }
        }

        return transposedMatrix;
    }
}