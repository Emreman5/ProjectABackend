using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Model.Abstract
{
    public class ImageBaseClass : DatabaseEntity
    {
        public string? Base64Image1 { get; set; }
        public string? Base64Image2 { get; set; }
        public string? Base64Image3 { get; set; }
        public string? Base64Image4 { get; set; }
        public string? Base64Image5 { get; set; }
    }
}
