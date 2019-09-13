using DomainLayer;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using System;
using System.IO;

namespace ImageStore.Controllers
{
    [ApiController, Route("api")]
    public class ImageController : ControllerBase
    {
        //PostImage handles post request which should contain an image file
        //the image is then stored in db and the imageID is returned
        [HttpPost, Route("upload")]
        public string PostImage()
        {
            try
            {
                IFormFile image = HttpContext.Request.Form.Files[0];
                if (image.ContentType.Substring(0,5) != "image")
                {
                    throw new Exception(StringLiterals._invalidImage);
                }

                byte[] img;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.CopyTo(memoryStream);
                    img = memoryStream.ToArray();
                }

                ImageModel imageModel = new ImageModel
                {
                    Name = image.Name,
                    Image = img
                };
                int imageID = _imageRepo.StoreImage(imageModel);
                return imageID.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //GetImageById handles the get request, it needs int id as parameter
        //imageModel is returned based on the id
        [HttpGet, Route("download")]
        public ActionResult<ImageModel> GetImageById(int id)
        {
            return _imageRepo.GetImageById(id);
        }

        private ImageRepo _imageRepo;
        public ImageController()
        {
            _imageRepo = new ImageRepo();
        }
    }
}
