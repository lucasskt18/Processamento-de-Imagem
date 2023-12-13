using System;
using System.Drawing;
using System.Drawing.Imaging;

class ProcessamentoImagens
{
    static void Main()
    {
        string inputImagePath = "input.jpg";
        string outputImagePath = "output.jpg";

        // Carrega a imagem original
        using (Bitmap originalImage = new Bitmap(inputImagePath))
        {
            Bitmap resizedImage = ResizeImage(originalImage, 300, 300);

            Rectangle cropArea = new Rectangle(50, 50, 200, 200);
            Bitmap croppedImage = CropImage(resizedImage, cropArea);

            Bitmap grayscaleImage = ApplyGrayscaleFilter(croppedImage);

            grayscaleImage.Save(outputImagePath, ImageFormat.Jpeg);

            Console.WriteLine("Processamento de imagem concluído. Imagem salva em " + outputImagePath);
        }
    }

    static Bitmap ResizeImage(Bitmap image, int width, int height)
    {
        Bitmap resizedImage = new Bitmap(width, height);
        using (Graphics g = Graphics.FromImage(resizedImage))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, width, height);
        }
        return resizedImage;
    }

    static Bitmap CropImage(Bitmap image, Rectangle cropArea)
    {
        Bitmap croppedImage = image.Clone(cropArea, image.PixelFormat);
        return croppedImage;
    }

    static Bitmap ApplyGrayscaleFilter(Bitmap image)
    {
        Bitmap grayscaleImage = new Bitmap(image.Width, image.Height);

        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                Color originalColor = image.GetPixel(x, y);
                int grayValue = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);
                Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                grayscaleImage.SetPixel(x, y, grayColor);
            }
        }

        return grayscaleImage;
    }
}
