
using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Service.Helpers;
using Service.Services;
using Service.Services.Interface;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config =>
            { config.DisableDataAnnotationsValidation = true; });

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IEducationService, EducationService>();


            //services.AddScoped<IValidator<CountryCreateDto>, CountryCreateDtoValidator>();
            return services;
        }
    }
}
