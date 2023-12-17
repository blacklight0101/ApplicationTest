using AutoMapper;
using ApplicationTest.Domain.Entities;
using ApplicationTest.Application.Dtos;

namespace ApplicationTest.Application.MappingProfiles
{
    public class DataJobProfile : Profile
    {
        public DataJobProfile()
        {
            CreateMap<DataJob, DataJobDTO>();
            CreateMap<DataJobCreateDTO, DataJob>();
            CreateMap<DataJobUpdateDTO, DataJob>();
        }
    }
}
