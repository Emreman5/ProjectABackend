using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Model.Abstract;

namespace Model
{
    public class MenuImage : DatabaseEntity
    {
        public int MenuId { get; set; }
        public string? ImagePath { get; set; }
        public DateTime Date { get; set; }

    }
}
