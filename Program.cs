using System;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: WinOCR <imagePath>");
            return;
        }

        string imagePath = args[0];
        StorageFile file = await StorageFile.GetFileFromPathAsync(imagePath);
        using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
        {
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap bitmap = await decoder.GetSoftwareBitmapAsync();
            OcrEngine ocrEngine = OcrEngine.TryCreateFromLanguage(new Language("en"));
            OcrResult ocrResult = await ocrEngine.RecognizeAsync(bitmap);
            Console.WriteLine(ocrResult.Text);
        }
    }
}
