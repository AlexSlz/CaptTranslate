using System;
using System.Drawing;
using System.IO;

namespace CaptTranslate;

internal static class ScreenManager
{
    public const string FileName = "screenshot.png";

    public static byte[] ImageToByte(string image)
    {
        return File.ReadAllBytes(image);
    }

    // public static Point ScreenPosition()
    // {
    //     var mousePos = Cursor.Position;
    //
    //     var screen = Screen.FromPoint(mousePos);
    //
    //     return new Point(
    //         screen.WorkingArea.Left,
    //         screen.WorkingArea.Top
    //     );
    // }

    public static string CaptureScreen()
    {
        var selectedArea = ImageData.SelectedArea;
        if (selectedArea.IsEmpty)
            return null;
        
        using var screenshot = new Bitmap(selectedArea.Width, selectedArea.Height);
        using (var g = Graphics.FromImage(screenshot))
        {
            g.CopyFromScreen(selectedArea.Left, selectedArea.Top, 0, 0, selectedArea.Size, CopyPixelOperation.SourceCopy);
        }
        screenshot.Save(FileName);
        GC.Collect();
        return FileName;
    }
}

