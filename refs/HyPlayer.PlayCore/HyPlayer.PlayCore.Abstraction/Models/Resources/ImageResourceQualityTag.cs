namespace HyPlayer.PlayCore.Abstraction.Models.Resources;

public class ImageResourceQualityTag : ResourceQualityTag
{
    public ImageResourceQualityTag(int pixelX, int pixelY)
    {
        this.PixelX = pixelX;
        this.PixelY = pixelY;
    }
    public int PixelX { get; }
    public int PixelY { get; }
}