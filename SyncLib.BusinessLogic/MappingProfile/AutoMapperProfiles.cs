using AutoMapper;
using SyncLib.Model.Dto;
using SyncLib.Repository.Database;

namespace SyncLib.BusinessLogic.MappingProfile
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Book Profiles
            //Book - Response Model Profile
            CreateMap<Book, BookResponseModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.AuthorName))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.LanguageName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Category))
                .ReverseMap();

            //Book - Request Model Profile
            CreateMap<Book, BookRequestModel>().ReverseMap();
            #endregion


            #region DropDown Model Mappings

            // Author- DropDown Model mapping
            CreateMap<Author, DropDownModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AuthorName))
                .ReverseMap();

            //BookCategory - DropDown Model Mapping
            CreateMap<BookCategory, DropDownModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            //ScriptLanguage - DropDownModel Mapping
            CreateMap<ScriptLanguage, DropDownModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LanguageName))
                .ReverseMap();
            #endregion


            #region ApplicationUser Profiles

            CreateMap<ApplicationUser, LoginResponseModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ReverseMap();

            #endregion
        }
    }
}
