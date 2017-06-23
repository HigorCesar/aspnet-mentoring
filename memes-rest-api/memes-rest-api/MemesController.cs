using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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
        GetMemesResponse memes;
		// GET api/memes
		[HttpGet]
        public GetMemesResponse Get()
        {
			
			//response.Data.Memes = new[] { new Meme{
                //    Id="1234",
                //    Name="artito",
                //    Url="http://i.imgflip.com/1bij.jpg",
                //    Width= 456,
                //    Height=400

                //} };

            return memes;
        }

        public MemesController()
		{
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
    }
}