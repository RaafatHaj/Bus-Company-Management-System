using AutoMapper;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Infrastructure.Persistence.Entities;

namespace Travel_Company_MVC.Mappping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            CreateMap<ScheduleTripsViewModel, ScheduleDTO>()
                .ForMember(dest => dest.CustomDates, opt => opt.Ignore())
                .ForMember(dest => dest.WeekDays, opt => opt.Ignore());

            CreateMap<ScheduledTravelVeiwModel, ScheduledTravelsMainViewDTO>().ReverseMap();
            CreateMap<ScheduledTravelDetialViewModel, ScheduledTravelDetailDTO>().ReverseMap();

            CreateMap<ScheduledTripViewModel,TripTimingDTO>().ReverseMap();



            CreateMap<Trip, TripViewModel>().ReverseMap();
            CreateMap<TripPattern, TripsPatternViewModel>().ReverseMap();

           


            CreateMap<SuitableTripDTO,SuitableTravelViewModel>().ReverseMap();

            CreateMap<ApplicationUser, UserViewModel>();

            CreateMap<ApplicationUser, UserFormViewModel>();
           
           
            CreateMap<UserFormViewModel, ApplicationUser>()
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()));


            //CreateMap<GetAvaliableSeatsDTO, BookTicketViewModel>();
            
            CreateMap<BookTicketViewModel,BookTicketDTO>();



            CreateMap<route,RouteViewModel>().ReverseMap();

        }

    }
}
