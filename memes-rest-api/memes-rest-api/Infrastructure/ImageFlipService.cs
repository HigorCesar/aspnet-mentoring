using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using memes_rest_api.Domain;

namespace memes_rest_api
{
    public class ImageFlipService : IMemeSource
    {
        class FlipImageMeme
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
        class GetMemesResponsesData
        {
            public FlipImageMeme[] Memes { get; set; }
        }
        class GetMemesResponse
        {
            public Boolean Success { get; set; }
            public GetMemesResponsesData Data { get; set; }
        }
        public ImageFlipService()
        {

        }
        public async Task<IEnumerable<Meme>> GetMemes()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.imgflip.com/");

            var response = await httpClient.GetAsync("https://api.imgflip.com/get_memes");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rawContent = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<GetMemesResponse>(rawContent);
                return content
                    .Data.Memes
                    .Select(m => new Meme() 
                {
                    Description = m.Name,
                    Endpoint = m.Url,
                    Id = m.Id,
                    Height = m.Height,
                    Width = m.Width
                });
            }
            return null;

        }

        public async Task<IEnumerable<Meme>> GetByDescription(string description)
        {
            var memes = await GetMemes();
            return memes.Where(item => item.Description.ToLowerInvariant().Contains(description.ToLowerInvariant()));
        }

        public async Task<IEnumerable<Meme>> GetByWidth(int width)
        {
            var memes = await GetMemes();
            return memes.Where(item => item.Width > width);
        }
    }
}
