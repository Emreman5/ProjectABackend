using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Model
{
    public class Adress : DatabaseEntity
    {

        public int CustomUserId { get; set; }
        public string? AdressDetail { get; set; }
    }
}
