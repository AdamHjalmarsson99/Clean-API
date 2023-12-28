using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<DogDto> Dogs { get; set; }
        public List<CatDto> Cats { get; set; }
        public List<BirdDto> Birds { get; set; }
    }
}
