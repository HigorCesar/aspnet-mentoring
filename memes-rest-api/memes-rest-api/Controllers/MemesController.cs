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

            if (!String.IsNullOrEmpty(description))
            {  
                IEnumerable<Meme> desc = await memeSource.GetByDescription(description);
                desc.ToList().ForEach(i => response.AddMeme(i));
                return Ok(response);
            }

            if (width > 0)
            {
                IEnumerable<Meme> wid = await memeSource.GetByWidth(width);
                wid.ToList().ForEach(i => response.AddMeme(i));
                return Ok(response);
            }

            IEnumerable<Meme> allMemes = await memeSource.GetMemes();
            allMemes.ToList().ForEach(i => response.AddMeme(i));
            return Ok(response);


        }
    }
}