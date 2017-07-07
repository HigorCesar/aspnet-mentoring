using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace memes_rest_api.Controllers
{  
    public class GetMemesResponse
    {
        public void AddMeme(Meme value){
            Data.Memes.Add(value);
        }
        public class ResponseData
        {   public ResponseData()
            {
                Memes = new List<Meme>();
            }
            public List<Meme> Memes
            {
                get;
                set;
            }
        }
        public ResponseData Data
        {
            get;
            set;
        }

        public GetMemesResponse()
        {
            Data = new ResponseData();
        }

    }
    public class Meme
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }
    }
    [Route("api/[controller]")]
    public class MemesController : Controller 
    {
        private HttpClient httpClient;
		GetMemesResponse memes;

		public MemesController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.imgflip.com/");
			memes = new GetMemesResponse();
			memes.AddMeme(new Meme
			{
				Id = "1234",
				Name = "artito",
				Url = "http://i.imgflip.com/1bij.jpg",
				Width = 456,
				Height = 400

			});
		}


		// GET api/memes
		[HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await httpClient.GetAsync("https://api.imgflip.com/get_memes");

            if (response.StatusCode == System.Net.HttpStatusCode.OK){
                var content = await response.Content.ReadAsStringAsync();
                var memesAll = JsonConvert.DeserializeObject(content);
                return this.Ok(content);
            } else {
                return this.BadRequest("try again");
            }
			//response.Data.Memes = new[] { new Meme{
                //    Id="1234",
                //    Name="artito",
                //    Url="http://i.imgflip.com/1bij.jpg",
                //    Width= 456,
                //    Height=400

                //} };


        }
    }
}