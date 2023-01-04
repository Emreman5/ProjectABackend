using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Model
{
    public class Reservation : DatabaseEntity
    {
        public int UserId { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
