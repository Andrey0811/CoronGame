using System.Windows.Controls;
using CoronGame.Common;

namespace CoronGame.Factories
{
    public class GetImage
    {
        private readonly string[] movementImagesNames = {"up.png", "down.png", "left.png", "right.png"};

        public GetImage(string aliveImagesPath)
        {
            AliveImages = CreatMovementImagesArray(aliveImagesPath);
        }

        public Image[] AliveImages { get; }

        //private Image[] dyingImages { get; private set; };

        private Image[] CreatMovementImagesArray(string imagePath)
        {
            var images = new Image[4];
            for (var i = 0; i < 4; i++)
            {
                images[i] = new Image();
                images[i] = ImageParser.Parse(imagePath + movementImagesNames[i]);
            }

            return images;
        }

        /*private void CreatDyingPlayerImagesArray()
        {
            dyingImages = new Image[10];
            for (var i = 0; i < 10; i++)
            {
                dyingImages[i] = new Image();
                dyingImages[i] =
                    ImageParser.Parse(GlobalConstants.PlayerDyingImagePath + i + ImagesFileExtension);
            }
        }*/
    }
}