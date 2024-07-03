using AutoMapper;
using Domain.Entity;
using Service.DTOs.Education;
using Service.DTOs.Group;
using Service.DTOs.Room;
using Service.DTOs.Teacher;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GroupUpdateDto, Group>();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<Group, GroupDto>();
            CreateMap<EducationCreateDto, Education>();
            CreateMap<EducationUpdateDto, Education>();
            CreateMap<Education, EducationDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomCreateDto, Room>();
            CreateMap<RoomUpdateDto, Room>();
            CreateMap<TeacherCreateDto, Teacher>()
             .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupId.Select(id => new GroupTeacher { GroupId = id })));
            CreateMap<TeacherUpdateDto, Teacher>()
        .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupId.Select(id => new GroupTeacher { GroupId = id })));
            CreateMap<Teacher, TeacherDto>()
              .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupTeachers.Select(gt => gt.Group.Name)))
              .ForMember(dest => dest.EducationName, opt => opt.MapFrom(src => src.GroupTeachers.Select(gt => gt.Group.Education.Name).Distinct()));

        }
    }
}