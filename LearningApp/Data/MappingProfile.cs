using AutoMapper;
using LearningApp.CommandQueries;
using LearningApp.Models;

namespace LearningApp.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Command to Model
            CreateMap<DepartmentCommand, Department>();


            // Model to Query
            CreateMap<Department, DepartmentQuery>();
        }
    }
}
