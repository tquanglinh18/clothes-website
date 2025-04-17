using System;
using ClothesWebUI.Models;

namespace ClothesWebAPI.Models
{
	public class ResponseModels<T>
	{
        public int Code { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = "";
        public bool IsSuccessed { get; set; } = false;
    }
}

