using AutoMapper;
using GymApi.Dtos.Request;
using GymApi.Dtos.Response;
using GymApi.Models;

namespace GymApi.Profiles;

public class ExerciseDetailProfile : Profile
{
    public ExerciseDetailProfile()
    {
        CreateMap<CreateExerciseDetail, ExerciseDetail>();
        CreateMap<ReadExerciseDetailDto, ExerciseDetail>();
        CreateMap<UpdateExerciseDetailDto, ExerciseDetail>();

        CreateMap<CreateExerciseDetail, ExerciseDetail>()
               .ForMember(dest => dest.RestTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.RestTime)));
        CreateMap<ExerciseDetail, ReadExerciseDetailDto>();
    }
}
