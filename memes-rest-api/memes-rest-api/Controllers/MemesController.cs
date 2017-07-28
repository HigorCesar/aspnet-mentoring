using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using memes_rest_api.Domain;
using memes_rest_api.Models;

namespace memes_rest_api.Controllers
{  
    
 
    [Route("api/[controller]")]
    public class MemesController : Controller 
    {
        IMemeSource memeSource;
		public MemesController(IMemeSource memeSource)
        {
            this.memeSource = memeSource;
           
		}
		// GET api/memes
		[HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new GetMemesResponse();
            var memes = await memeSource.GetMemes();

            foreach (var item in memes.Take(2))
            {
                response.AddMeme(item);

            }

            return Ok(response);

        }
    }
}