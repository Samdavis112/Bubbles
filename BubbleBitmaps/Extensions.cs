using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bubbles
{
    public static class Extensions
    {
        /// <summary>
        /// Fill a bitmap with the specified colour.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="color"></param>
        public static void Background(this Bitmap bmp, Color color)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, color);
                }
            }
        }

        /// <summary>
        /// Calculate the minimum distance between the closet point to a point, in an array of points.
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="listCoords"></param>
        /// <returns></returns>
        public static double minDisplacement(int[] coord1, int[][] listCoords)
        {
            double minDist = 10000000; // Arbitary big number.

            for (int i = 0; i < listCoords.Length; i++)
            {
                //Check if a single x or y direction displacement is > minDist, in which case it is useless to investigate further
                if (Math.Abs(listCoords[i][0] - coord1[0]) > minDist || Math.Abs(listCoords[i][1] - coord1[1]) > minDist) continue;

                double dist = Math.Sqrt((listCoords[i][0]-coord1[0])*(listCoords[i][0] - coord1[0]) + (listCoords[i][1]-coord1[1])*(listCoords[i][1] - coord1[1]));

                if (minDist > dist)
                    minDist = dist;
            }
            return minDist;
        }

        /// <summary>
        /// Invert every pixels colour in a bitmap. 
        /// </summary>
        /// <param name="bmp"></param>
        public static void Invert(this Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color ColourToInvert = bmp.GetPixel(i,j);
                    bmp.SetPixel(i, j, Color.FromArgb(ColourToInvert.ToArgb()^0xffffff));
                }
            }
        }
    }
}