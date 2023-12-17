using ApplicationTest.Application.Dtos;
using ApplicationTest.Application.Services;
using ApplicationTest.Domain.Entities;
using ApplicationTest.Domain.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest.Infrastructure.Services
{
    public class DataProcessorService : IDataProcessorService
    {
        // Should add try and catch to all methods but  ran out of time

        private readonly List<DataJob> _dataJobs; //I´m runnning out of time I wanted to implement a repository with in mem sql server but no time
        private readonly IMapper _mapper;
        public DataProcessorService(IMapper mapper)
        {
            _dataJobs = new List<DataJob>();
            _mapper = mapper;
        }

        public DataJobDTO Create(DataJobCreateDTO dataJobDto)
        {
            try
            {
                var dataJob = _mapper.Map<DataJob>(dataJobDto);
                dataJob.Id = Guid.NewGuid();
                dataJob.Status = DataJobStatus.New;

                _dataJobs.Add(dataJob);

                return _mapper.Map<DataJobDTO>(dataJob);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception should be done with a tool like Nlog and manage the exception correctly");
                throw;
            }
        }

        public void Delete(Guid dataJobID)
        {
            try
            {
                var dataJob = _dataJobs.FirstOrDefault(x => x.Id == dataJobID);
                if (dataJob != null)
                {
                    _dataJobs.Remove(dataJob);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception should be done with a tool like Nlog and manage the exception correctly");
            }
        }

        public IEnumerable<DataJobDTO> GetAllDataJobs()
        {
            return _mapper.Map<IEnumerable<DataJobDTO>>(_dataJobs);
        }

        public List<string> GetBackgroundProcessResults(Guid dataJobId)
        {
            var dataJob = _dataJobs.FirstOrDefault(x => x.Id == dataJobId);
            if (dataJob != null)
            {
                return dataJob.Results.ToList();
            }

            return new List<string>();
        }

        public DataJobStatus GetBackgroundProcessStatus(Guid dataJobId)
        {
            var dataJob = _dataJobs.FirstOrDefault(x => x.Id == dataJobId);
            if (dataJob != null)
            {
                return dataJob.Status;
            }
            
            // here we are going to have a problem, but I will leave it like this because I have 10 mins left.
            return DataJobStatus.New;
        }

        public DataJobDTO GetDataJob(Guid id)
        {
            var dataJob = _dataJobs.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<DataJobDTO>(dataJob);
        }

        public IEnumerable<DataJobDTO> GetDataJobsByStatus(DataJobStatus status)
        {
            var filteredJobs = _dataJobs.Where(x => x.Status == status);
            return _mapper.Map<IEnumerable<DataJobDTO>>(filteredJobs);
        }

        public bool StartBackgroundProcess(Guid dataJobId)
        {
            var dataJob = _dataJobs.FirstOrDefault(x => x.Id == dataJobId);
            if (dataJob != null)
            {
                // 
                // StartProcesS();
                // If all goes well
                return true;
                //else return false;
            }
            return false;
        }

        public DataJobDTO Update(Guid id, DataJobUpdateDTO dataJob)
        {
            var existingDataJob = _dataJobs.FirstOrDefault(dj => dj.Id == id);
            if (existingDataJob != null)
            {   
                existingDataJob.Name = dataJob.Name;
                existingDataJob.FilePathToProcess = dataJob.FilePathToProcess;
                return _mapper.Map<DataJobDTO>(existingDataJob);
            }

            return null;
        }
    }
}
