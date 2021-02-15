using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dde.api.Models
{
    public class ContentModel
    {
        public string date { get; set; }
        public string title { get; set; }

        public IFormFile content {get; set;}
    }
}
