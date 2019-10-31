using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;

namespace ConvolutionWpf.Commands
{
    public class BlurCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public BlurCommand(Func<WriteableBitmap> imageFactory)
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


            int kernelSize = 15;
            int beginX, endX, beginY, endY;

            beginX = (kernelSize - 1) / 2;
            endX = image.PixelWidth - 1 - (kernelSize - 1) / 2;

            beginY = (kernelSize - 1) / 2;
            endY = image.PixelHeight - 1 - (kernelSize - 1) / 2;

            int[,] kernel = new int[15, 15];

            for (int i = 0; i < kernelSize; ++i)
            {
                for (int j = 0; j < kernelSize; ++j)
                {
                    kernel[i, j] = 1;
                }
            }

            int kernelSum = 0;
            foreach (int i in kernel)
            {
                kernelSum += i;
            }

            for (int i = 0; i < image.PixelWidth; ++i)
            {
                for (int j = 0; j < image.PixelHeight; ++j)
                {
                    int index = j * image.BackBufferStride + 4 * i;

                    if ((i >= beginX) && (i <= endX) && (j >= beginY) && (j <= endY))
                    {
                        double[] convolution = new double[3] { 0, 0, 0 };

                        for (int i2 = -(kernelSize - 1) / 2; i2 <= (kernelSize - 1) / 2; ++i2)
                        {
                            for (int j2 = -(kernelSize - 1) / 2; j2 <= (kernelSize - 1) / 2; ++j2)
                            {
                                int index2 = (j + j2) * image.BackBufferStride + 4 * (i + i2);

                                for (int c = 0; c < 3; ++c)
                                {
                                    convolution[c] += pixels[index2 + c] *
                                        kernel[(i2 + (kernelSize - 1) / 2), (j2 + (kernelSize - 1) / 2)];
                                }
                            }
                        }

                        for (int c = 0; c < 3; ++c)
                        {
                            resultPixels[index + c] = (byte)(convolution[c] / kernelSum);
                        }
                    }

                    else
                    {
                        for (int c = 0; c < 3; ++c)
                        {
                            resultPixels[index + c] = pixels[index + c];
                        }
                    }
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