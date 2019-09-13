using DomainLayer.Models;
using System;
using System.Linq;

namespace RepositoryLayer
{
    public class ImageRepo
    {
        //takes the domain imageModel, converts the byte array image to base64 string image
        //maps it with repo imageModel & stores the model, it returns the imageID
        public int StoreImage(ImageModel imageModel)
        {
            Models.ImageModel image = new Models.ImageModel()
            {
                ImageId = GetNewId(),
                Name = imageModel.Name,
                Image = Convert.ToBase64String(imageModel.Image)
            };
            ImageStore.images.Add(image);
            return image.ImageId;
        }

        //takes id as an input, gets the imagemodel with appropriate id
        //converts the base64 string image to byte array image
        public ImageModel GetImageById(int id)
        {
            Models.ImageModel image = ImageStore.images.Where(u => u.ImageId == id).FirstOrDefault();
            if(image == null)
            {
                return null;
            }
            ImageModel imageModel = new ImageModel()
            {
                Name = image.Name,
                Image = Convert.FromBase64String(image.Image)
            };
            return imageModel;
        }

        //generates new image id
        private int GetNewId()
        {
            try
            {
                return ImageStore.images.Max(u => u.ImageId) + 1;
            }
            catch(InvalidOperationException)
            {
                return 1;
            }
        }
    }
}
