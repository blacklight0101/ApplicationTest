using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest.Application.Dtos
{
    public class DataJobCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string FilePathToProcess { get; set; }
    }
}
