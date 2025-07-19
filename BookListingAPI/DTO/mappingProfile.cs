using AutoMapper;
using BookListingAPI.Models;
using BookListingAPI.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));
        CreateMap<BookDto, Book>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore());

        CreateMap<Author, AuthorDto>().ReverseMap();

        CreateMap<BookAuthor, AuthorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Author.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Author.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Author.LastName));
    }
}
