using DomainLayer;
using DomainLayer.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System;

namespace ImageConsole
{
    class ImageUtility
    {
        //takes path & url as input, gets image from the path & posts it to the url 
        //returns the response ,ie imageid
        internal string PostImage(string path, string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (MultipartFormDataContent formData = new MultipartFormDataContent())
                {
                    HttpContent bytesContent = new ByteArrayContent(File.ReadAllBytes(path));
                    bytesContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    formData.Add(bytesContent, Path.GetFileName(path), "image");
                    
                    HttpResponseMessage response = client.PostAsync(url, formData).Result; 
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(StringLiterals._postRequestFailed + response.StatusCode);
                    }
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
        }

        //gets the image from the url & id, saves it to the path
        internal string GetImage(string id, string url, string folderPath)
        {
            using (HttpClient client = new HttpClient())
            {
                string response = client.GetStringAsync($"{url}?id={id}").Result;
                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }
                ImageModel imageModel = JsonConvert.DeserializeObject<ImageModel>(response);
                File.WriteAllBytes(folderPath + imageModel.Name, imageModel.Image);
                return folderPath + imageModel.Name;
            }
        }
    }
}
