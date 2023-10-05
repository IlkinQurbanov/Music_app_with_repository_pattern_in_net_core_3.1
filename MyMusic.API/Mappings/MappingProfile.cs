using AutoMapper;
using MyMusic.API.Resources;
using MyMusic.API.Resourses;
using MyMusic.Core.Models;

namespace MyMusic.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to resource
            CreateMap<Music, MusicResource>();
            CreateMap<Artist, ArtistResource>();
            CreateMap<Music, SaveResourceMusic>();
            CreateMap<Artist, SaveArtistResource>();
            CreateMap<User, UserResource>();


            //Resource to Domain
            CreateMap<MusicResource, Music>();
            CreateMap<ArtistResource, Artist>();
            CreateMap<SaveResourceMusic, Music>();
            CreateMap<SaveArtistResource, Artist>();
            CreateMap<UserResource, User>();

        }

    }
}
