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
        public async Task<IActionResult> Get([FromQueryAttribute] string description, Int32 width)
        {
            var response = new GetMemesResponse();
            var memes = await memeSource.GetMemes();

            foreach (var item in memes)
            {
                if (!String.IsNullOrEmpty(description))
                {
                       if (item.Description.ToLowerInvariant().Contains(description.ToLowerInvariant()))
                    {
                        response.AddMeme(item);
                    }
                }
                else if (width > 0) 
                {
                    if (item.Width == width)
                    {
                        response.AddMeme(item);
                    }
                } 
                else 
                {
                    response.AddMeme(item);
                }

            }
            return Ok(response);
        }
    }
}