using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;
using System.Diagnostics;

namespace ConvolutionWpf.Commands
{
    public class ContrastCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public ContrastCommand(Func<WriteableBitmap> imageFactory)
            : base(() => { })
        {
            _imageFactory = imageFactory;
        }

        public void ExecuteCommand()
        {
            var image = _imageFactory();
            if (image == null)
                return;

            var pixels = new byte[image.PixelHeight * image.BackBufferStride];
            image.CopyPixels(pixels, image.BackBufferStride, 0);

            var resultPixels = new byte[image.PixelHeight * image.BackBufferStride];

            //int index = j * image.BackBufferStride + 4 * i;
            //todo

            int[] histogram = new int[byte.MaxValue + 1];
            int[] cumulativeHistogram = new int[byte.MaxValue + 1];

            // Инициализация массивов для хранения данных гистограммы
            // и кумулятивной гистограммы
            for (int k = 0; k < histogram.Length; ++k)
            {
                histogram[k] = 0;
                cumulativeHistogram[k] = 0;
            }

            // Формирование гистограммы
            for (int i = 0; i < image.PixelWidth; ++i)
            {
                for (int j = 0; j < image.PixelHeight; ++j)
                {
                    int index = j * image.BackBufferStride + 4 * i;

                    double red = pixels[index];
                    double green = pixels[index + 1];
                    double blue = pixels[index + 2];

                    // Формула перевода в серый
                    byte gray = (byte)(0.2 * red + 0.7 * green + 0.1 * blue); 

                    histogram[gray]++;
                }
            }

            cumulativeHistogram[0] = histogram[0];

            // Формирование кумулятивной гистограммы
            for (int k = 1; k < cumulativeHistogram.Length; ++k)
            {
                cumulativeHistogram[k] = cumulativeHistogram[k - 1] + histogram[k];
            }

            // Проверка правильности формирования кумулятивной гистограммы
            Debug.Assert(cumulativeHistogram[byte.MaxValue] ==
                image.PixelWidth * image.PixelHeight, "Wrong cumulativeHistogram calculation");

            double p = 0.01;
            double aLow = 0;
            double aHigh = byte.MaxValue;
            byte b;

            for (int k = 0; k < cumulativeHistogram.Length; ++k)
            {
                if (cumulativeHistogram[k] >= image.PixelWidth * image.PixelHeight * p)
                {
                    aLow = k;
                    break;
                }
            }

            for (int k = cumulativeHistogram.Length - 1; k >= 0; --k)
            {
                if (cumulativeHistogram[k] <= image.PixelWidth * image.PixelHeight * (1- p))
                {
                    aHigh = k;
                    break;
                }
            }

            // Непосредственно реализация автоконтраста
            for (int i = 0; i < image.PixelWidth; ++i)
            {
                for (int j = 0; j < image.PixelHeight; ++j)
                {
                    int index = j * image.BackBufferStride + 4 * i;

                    double red = pixels[index];
                    double green = pixels[index + 1];
                    double blue = pixels[index + 2];

                    // Формула перевода в серый
                    byte gray = (byte)(0.2 * red + 0.7 * green + 0.1 * blue);

                    if (gray <= aLow)
                        b = 0;
                    else if (gray >= aHigh)
                        b = byte.MaxValue;
                    else
                        b = (byte)((gray - aLow) / (aHigh - aLow) * byte.MaxValue);

                    for (int c = 0; c < 3; ++c)
                    {
                        resultPixels[index + c] = b;
                    }

                    resultPixels[index + 3] = pixels[index + 3];
                }
            }

            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight),
                resultPixels, image.BackBufferStride, 0);
        }

        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}