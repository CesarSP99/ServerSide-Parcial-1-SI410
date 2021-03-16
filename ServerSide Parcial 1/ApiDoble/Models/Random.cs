using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDoble.Models
{
    public class Random
    {
        [Key]
        [DataType(DataType.DateTime)]
        public DateTime DateAndTime { get; set; }
        [Required]
        public int RandomNumber { get; set; }
    }
}
