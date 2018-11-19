using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Lab4.Models
{
    class MainModel
    {
        #region HelpFunctions
        private static void GetLimitBrightness(WriteableBitmap wb, double width, double height, out byte fmin, out byte fmax)
        {
            fmin = 255;
            fmax = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    byte f = wb.GetBrightness(j, i);
                    if (f > fmax)
                        fmax = f;
                    if (f < fmin)
                        fmin = f;
                }
            }
        }
        private static void GetLimitBrightness(WriteableBitmap wb, out byte fmin, out byte fmax)
        {
            double width = wb.Width;
            double height = wb.Height;
            fmin = 255;
            fmax = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    byte f = wb.GetBrightness(j, i);
                    if (f > fmax)
                        fmax = f;
                    if (f < fmin)
                        fmin = f;
                }
            }
        }
        private static int Check(int val, byte min, byte max)
        {
            return val < min ? min : val > max ? max : val;
        }
        private static int Median(int[] arr)
        {
            arr = arr.OrderBy(x => x).ToArray();
            return arr[arr.Length / 2];
        }
        #endregion

        public static void ToBlack(WriteableBitmap wb)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var c = wb.GetPixel(j, i);
                    var average = 0.3 * c.R + 0.59 * c.G + 0.11 * c.B;
                    wb.SetPixel(j, i, (byte)average, (byte)average, (byte)average);
                }
            }
        }
        public static void RandomPoints(WriteableBitmap wb)
        {
            Random rnd = new Random();
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int r = rnd.Next(100);
                    if (r < 5)
                        wb.SetPixel(j, i, 0, 0, 0);
                    else if(r > 95)
                        wb.SetPixel(j, i, 255, 255, 255);
                }
            }
        }
        public static void MedianFiltering(WriteableBitmap wb)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //var c = wb.GetPixel(j, i);
                    //var average = 0.3 * c.R + 0.59 * c.G + 0.11 * c.B;
                    //wb.SetPixel(j, i, (byte)average, (byte)average, (byte)average);

                    if (j + 1 < width && j - 1 > 0 && i - 1 >0 && i + 1 < height)
                    {
                        var arr = new int[3 * 3] 
                        {
                            wb.GetBrightness(j-1,i-1),
                            wb.GetBrightness(j,i-1),
                            wb.GetBrightness(j+1,i-1),

                            wb.GetBrightness(j-1,i),
                            wb.GetBrightness(j,i),
                            wb.GetBrightness(j+1,i),

                            wb.GetBrightness(j-1,i+1),
                            wb.GetBrightness(j,i+1),
                            wb.GetBrightness(j+1,i+1),
                        };

                        byte newRGB = (byte)Median(arr);
                        wb.SetPixel(j, i, newRGB, newRGB, newRGB);
                    }
                }
            }
        }
        public static void ContourSelection(WriteableBitmap wb)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //var c = wb.GetPixel(j, i);
                    //var average = 0.3 * c.R + 0.59 * c.G + 0.11 * c.B;
                    //wb.SetPixel(j, i, (byte)average, (byte)average, (byte)average);

                    if (j + 1 < width && i + 1 < height)
                    {
                        byte newRGB = (byte)Math.Sqrt(Math.Pow(wb.GetBrightness(j + 1, i) - wb.GetBrightness(j, i), 2)
                            + Math.Pow(wb.GetBrightness(j, i + 1) - wb.GetBrightness(j, i), 2));
                        wb.SetPixel(j, i, newRGB, newRGB, newRGB);
                    }
                }
            }
        }


        public static void LinearContrast(WriteableBitmap wb, byte gmin, byte gmax)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            byte fmin, fmax;
            GetLimitBrightness(wb,width,height, out fmin, out fmax);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var f = wb.GetBrightness(j, i);
                    var g = 0;
                    if(fmax - fmin != 0)
                        g = ((f - fmin) / (fmax - fmin)) * (gmax - gmin) + gmin;
                    Color c = wb.GetPixel(j, i);
                    int newR = c.R;
                    int newG = c.G;
                    int newB = c.B;
                    int value = (f - g);

                    newR = Check(newR + value, 0, 255);
                    newG = Check(newG + value, 0, 255);
                    newB = Check(newB + value, 0, 255);

                    wb.SetPixel(j, i, Color.FromRgb((byte)newR, (byte)newG, (byte)newB));
                }
            }
        }        
        public static void ThresholdProcessing(WriteableBitmap wb, byte f0)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var f = wb.GetBrightness(j, i);
                    if (f >= f0)
                        wb.SetPixel(j, i, Color.FromRgb(255, 255, 255));
                    else
                        wb.SetPixel(j, i, Color.FromRgb(0, 0, 0));
                }
            }
        }
        public static void BrightnessСut_A(WriteableBitmap wb, byte f0, byte f1)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var f = wb.GetBrightness(j, i);
                    if (f >= f0 && f < f1)
                        wb.SetPixel(j, i, Color.FromRgb(255, 255, 255));
                    else
                        wb.SetPixel(j, i, Color.FromRgb(0, 0, 0));
                }
            }
        }
        public static void BrightnessСut_B(WriteableBitmap wb, byte f0, byte f1)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var f = wb.GetBrightness(j, i);
                    if (f >= f0 && f < f1)
                        wb.SetPixel(j, i, Color.FromRgb(255, 255, 255));
                }
            }
        }
        public static void BrightnessСut_C(WriteableBitmap wb, byte f0)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var f = wb.GetBrightness(j, i);
                    if (f >= f0)
                        wb.SetPixel(j, i, Color.FromRgb(255, 255, 255));
                }
            }
        }
        public static void ContrastScaling_A(WriteableBitmap wb, byte f0, byte f1)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            int k = 255 / (f1- f0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var f = wb.GetBrightness(j, i);
                    Color c = wb.GetPixel(j, i);
                    int newR = c.R;
                    int newG = c.G;
                    int newB = c.B;

                    newR = Check(newR * k, 0, 255);
                    newG = Check(newG * k, 0, 255);
                    newB = Check(newB * k, 0, 255);
                    if (f >= f0 && f < f1)
                        wb.SetPixel(j, i, Color.FromRgb((byte)(newR), (byte)(newG), (byte)(newB)));
                    else if(f >= f1)
                        wb.SetPixel(j, i, Color.FromRgb(255, 255, 255));
                    else
                        wb.SetPixel(j, i, Color.FromRgb(0, 0, 0));
                }
            }
        }
        public static void ContrastScaling_B(WriteableBitmap wb, byte f0, byte f1)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            int k = 255 / (f1 - f0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var f = wb.GetBrightness(j, i);
                    Color c = wb.GetPixel(j, i);
                    int newR = c.R;
                    int newG = c.G;
                    int newB = c.B;

                    newR = Check(newR * k, 0, 255);
                    newG = Check(newG * k, 0, 255);
                    newB = Check(newB * k, 0, 255);
                    if (f >= f0 && f < f1)
                        wb.SetPixel(j, i, Color.FromRgb((byte)(newR), (byte)(newG), (byte)(newB)));
                    else if (f >= f1)
                        wb.SetPixel(j, i, Color.FromRgb(0, 0, 0));
                    else
                        wb.SetPixel(j, i, Color.FromRgb(0, 0, 0));
                }
            }
        }
        public static void Negative(WriteableBitmap wb)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            int k = -1;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var c = wb.GetPixel(j, i);
                    var average = (c.R + c.G + c.B) / 3;
                    var newColor = Color.FromRgb((byte)average, (byte)average, (byte)average);
                    wb.SetPixel(j, i, newColor);
                    var f = wb.GetBrightness(j, i);
                    wb.SetPixel(j, i, Color.FromRgb((byte)(f * k), (byte)(f * k), (byte)(f * k)));
                }
            }
        }
        [Obsolete]
        public static void TransformBrightness(WriteableBitmap wb, Func<int, int, Color> transform)
        {
            double width = wb.PixelWidth;
            double height = wb.PixelHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    wb.SetPixel(j, i, transform(j, i));
                }
            }
        }
    }
}
