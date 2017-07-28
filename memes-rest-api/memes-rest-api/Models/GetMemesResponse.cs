using System;
using System.Collections.Generic;

namespace memes_rest_api.Models
{
	public class GetMemesResponse
	{
		public void AddMeme(Meme value)
		{
			Data.Memes.Add(value);
		}
		public class ResponseData
		{
			public ResponseData()
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
}
