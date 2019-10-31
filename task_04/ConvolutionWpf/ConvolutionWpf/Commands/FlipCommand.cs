using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;

namespace ConvolutionWpf.Commands
{
    public class FlipCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public event Action<WriteableBitmap> OnImageChanged;

        public FlipCommand(Func<WriteableBitmap> imageFactory)
            : base(() => { })
        {
            _imageFactory = imageFactory;
        }

        private static bool isEvenNumbered = false;

        public void ExecuteCommand()
        {

            var image = _imageFactory();
            if (image == null)
                return;

            var pixels = new byte[image.PixelHeight * image.BackBufferStride];
            image.CopyPixels(pixels, image.BackBufferStride, 0);

            WriteableBitmap imageRes;
            byte[] resultPixels;

            if (!isEvenNumbered)
            {
                // Отражение по горизонтали
                imageRes =
                    new WriteableBitmap(2 * image.PixelWidth, image.PixelHeight,
                    image.DpiX, image.DpiY, image.Format, image.Palette);
                resultPixels = new byte[imageRes.PixelHeight * imageRes.BackBufferStride];

                for (int i = 0; i < image.PixelWidth; ++i)
                {
                    for (int j = 0; j < image.PixelHeight; ++j)
                    {
                        // Исходное изображение
                        int initialSourceIndex = j * image.BackBufferStride + 4 * i;
                        int initialResultIndex = j * image.BackBufferStride * 2 + 4 * i;

                        // Изображение, отраженное по горизонтали
                        int flippedSourceIndex = j * image.BackBufferStride +
                            4 * (image.PixelWidth - 1 - i);
                        int flippedResultIndex = j * image.BackBufferStride * 2 +
                            4 * (image.PixelWidth + i);

                        for (int c = 0; c < 3; ++c)
                        {
                            resultPixels[initialResultIndex + c] =
                                pixels[initialSourceIndex + c];
                            resultPixels[flippedResultIndex + c] =
                                pixels[flippedSourceIndex + c];
                        }
                    }
                }
            }
            else
            {
                // Отражение по вертикали
                imageRes =
                    new WriteableBitmap(image.PixelWidth, 2 * image.PixelHeight,
                    image.DpiX, image.DpiY, image.Format, image.Palette);
                resultPixels = new byte[imageRes.PixelHeight * imageRes.BackBufferStride];

                for (int i = 0; i < image.PixelWidth; ++i)
                {
                    for (int j = 0; j < image.PixelHeight; ++j)
                    {
                        // Исходное изображение
                        int initialSourceIndex = j * image.BackBufferStride + 4 * i;
                        int initialResultIndex = initialSourceIndex;

                        // Изображение, отраженное по вертикали
                        int flippedSourceIndex = (image.PixelHeight - 1 - j) *
                            image.BackBufferStride + 4 * i;
                        int flippedResultIndex = (image.PixelHeight + j) *
                            image.BackBufferStride + 4 * i;

                        for (int c = 0; c < 3; ++c)
                        {
                            resultPixels[initialResultIndex + c] =
                                pixels[initialSourceIndex + c];
                            resultPixels[flippedResultIndex + c] =
                                pixels[flippedSourceIndex + c];
                        }
                    }
                }
            }

            isEvenNumbered = !isEvenNumbered;

            imageRes.WritePixels(new Int32Rect(0, 0,
                imageRes.PixelWidth, imageRes.PixelHeight),
                resultPixels, imageRes.BackBufferStride, 0);

            OnImageChanged?.Invoke(imageRes);
        }

        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}