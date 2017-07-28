﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Linq;

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
 
    [Route("api/[controller]")]
    public class MemesController : Controller 
    {
        ImageFlipService imageFlipService;
		public MemesController()
        {
            imageFlipService = new ImageFlipService();
           
		}


		// GET api/memes
		[HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new GetMemesResponse();
            var memes = await imageFlipService.GetMemes();

            foreach (var item in memes.Take(2))
            {
                response.AddMeme(item);

            }

            return Ok(response);

        }
    }
}