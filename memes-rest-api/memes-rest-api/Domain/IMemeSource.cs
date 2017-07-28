﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace memes_rest_api.Domain
{
    public interface IMemeSource
    {
        Task<IEnumerable<Meme>> GetMemes();


    }

}