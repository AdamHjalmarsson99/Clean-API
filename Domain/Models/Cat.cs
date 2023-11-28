using Domain.Models.Animal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        [Required]
        public bool LikesToPlay { get; set; }
    }
}
