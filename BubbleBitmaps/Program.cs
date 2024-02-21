using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Bubbles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region settings
            // Output paths
            //string Path = @"c:\users\sam.davis\documents\visual studio 2012\Projects\ConsoleApplication1\ConsoleApplication1\Image1.bmp";
            //string InvertPath = @"c:\users\sam.davis\documents\visual studio 2012\Projects\ConsoleApplication1\ConsoleApplication1\Image1_INVERT.bmp";
            string Path = @"C:\Users\sam07\OneDrive\Code\VisualStudio\repos\Bubbles\BubbleBitmaps\Image1.bmp";
            string InvertPath = @"C:\Users\sam07\OneDrive\Code\VisualStudio\repos\Bubbles\BubbleBitmaps\Image1_INVERT.bmp";

            Random rdn = new Random();
            Size ImageSize = new Size(800, 600); // [Default = (800, 600)]
            int noPoints = 40; // [Default = 40]
            Bitmap bmp = new Bitmap(ImageSize.Width, ImageSize.Height);
            int[][] points = new int[noPoints][];

            // Note: for the inverted image, these will have the opposite effect, i.e lower redBias here will result in a redder invertedImage.
            // Example: { 0.0 <= xBias <= 1.0} [Defaults = 1]
            double redBias = 0.812;
            double greenBias = 0.165;
            double blueBias = 0.216;

            // Similar to biases, but can be > 1 [Default = 1]
            double weight = 1;
            #endregion

            #region Processing
            for (int i = 0; i < points.Length; i++)
            {
                // Create n number of random points. (Within image bounds)
                points[i] = new int[]
                {
                    rdn.Next(0, ImageSize.Width),
                    rdn.Next(0, ImageSize.Height)
                };
            }

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int[] Coord = new int[] { i, j };

                    // Check this pixel isn't one of the preselected ones.
                    if (points.Contains(Coord))
                        continue;

                    // Calc distance to nearest point * weight.
                    double minDisp = Extensions.minDisplacement(Coord, points) * weight;

                    // Cap at colour channel limit.
                    if (minDisp > 255)
                        minDisp = 255;

                    else
                    {
                        // Set the bitmap pixel to a colour based off the biases and distance.
                        bmp.SetPixel(i, j, Color.FromArgb((int)(minDisp * redBias), (int)(minDisp * greenBias), (int)(minDisp * blueBias)));
                    }
                }
            }

            bmp.Save(Path, ImageFormat.Bmp);
            bmp.Invert();
            bmp.Save(InvertPath, ImageFormat.Bmp);
            #endregion
        }
    }
}