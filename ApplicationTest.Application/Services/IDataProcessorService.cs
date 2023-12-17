using ApplicationTest.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationTest.Domain.Enums;

namespace ApplicationTest.Application.Services
{
    public interface IDataProcessorService
    {
        IEnumerable<DataJobDTO> GetAllDataJobs();
        IEnumerable<DataJobDTO> GetDataJobsByStatus(DataJobStatus status);
        DataJobDTO GetDataJob(Guid id);
        DataJobDTO Create(DataJobCreateDTO dataJob);
        DataJobDTO Update(Guid id, DataJobUpdateDTO dataJob);
        void Delete(Guid dataJobID);
        bool StartBackgroundProcess(Guid dataJobId);
        DataJobStatus GetBackgroundProcessStatus(Guid dataJobId);
        List<string> GetBackgroundProcessResults(Guid dataJobId);
    }
}
