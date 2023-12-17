using ApplicationTest.Domain.Entities;
using ApplicationTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest.Application.Dtos
{
    public class DataJobDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePathToProcess { get; set; }
        public DataJobStatus Status { get; set; } = DataJobStatus.New;
        public IEnumerable<string> Results { get; set; } = new List<string>();
        public IEnumerable<Link> Links { get; set; } = new List<Link>();
    }
}
