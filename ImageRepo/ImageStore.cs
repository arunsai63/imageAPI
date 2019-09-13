using RepositoryLayer.Models;
using System.Collections.Generic;

namespace RepositoryLayer
{
    internal class ImageStore
    {
        internal static List<ImageModel> images;

        static ImageStore()
        {
            images = new List<ImageModel>();
        }
    }
}
