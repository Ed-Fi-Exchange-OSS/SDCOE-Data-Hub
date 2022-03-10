using System.Linq;
using AutoMapper;
using DataHub.Api.Data.Entities;
using DataHub.Api.Data.Models;
using DataHub.Api.Services;

namespace DataHub.Api.Data
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<Organization, OrganizationDto>();
            CreateMap<Announcement, AnnouncementDto>();
            CreateMap<CRMContact, CrmContactDto>()
                .ForMember(dest => dest.LocalOrganizationId,
                    opt => opt.MapFrom(src => src.Organization.LocalOrganizationID));
            CreateMap<Support, SupportDto>();
            CreateMap<Extract, ExtractDto>()
                .ForMember(dest => dest.OrganizationAbbreviation,
                opt => opt.MapFrom(src => src.Organization.OrganizationAbbreviation));
            CreateMap<Offering, OfferingDto>();
            CreateMap<User, UserDto>()
                .ForMember(dest =>
                    dest.LocalOrganizationId,
                    opt => opt.MapFrom(src => src.Organization.LocalOrganizationID));
            CreateMap<EdFiRequest, EdFiRequestDto>().ReverseMap();
            CreateMap<EdFiODSClient, EdFiOdsClient>();
            CreateMap<EdFiODS, EdFiOdsStatusDto>();
        }
    }
}