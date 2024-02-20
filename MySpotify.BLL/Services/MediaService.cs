using AutoMapper;
using Microsoft.AspNetCore.Http;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Infrastructure;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.DAL.Interfaces;
using System;

namespace MySpotify.BLL.Services
{
    public class MediaService : IMediaService
    {
        IUnitOfWork Database { get; set; }
        public MediaService(IUnitOfWork unitOfWork) => Database = unitOfWork; 
        
        public async Task CreateMedia(MediaDTO mediaDto , IFormFile upPoster , IFormFile upMedia)
        {
            var Media = new Media
            {
                Id = mediaDto.Id,
                Name = mediaDto.Name,
                Artist = mediaDto.Artist,
                FileAdress = mediaDto.FileAdress,
                Genre = Database.Genres.Get(mediaDto.Genre).Result,
                Poster = mediaDto.Poster,
                User = Database.Users.Get(mediaDto.UserId).Result

            };
            await UpLoadMedia(upPoster, mediaDto.rootPath + mediaDto.Poster);
            await UpLoadMedia(upMedia, mediaDto.rootPath + Media.FileAdress);
            await Database.Medias.Create(Media);
            await Database.Save();
        }

        async Task UpLoadMedia(IFormFile? formFile, string FullMediaPath)
        {
            
             await FileDownloadService.UpLoadMedia(formFile, FullMediaPath);
        }

        public async Task<IEnumerable<MediaDTO>> GetMediaList()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Media , MediaDTO>()
                                 .ForMember("UserId" , opt => opt.MapFrom(c=>c.User.id)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Media>, IEnumerable<MediaDTO>>(await Database.Medias.GetMediaList());

        }

        public async Task<MediaDTO> GetMedia(int id)
        {
           var media = await Database.Medias.Get(id);
            if (media == null)
                throw new ValidationException("Неверный медиаФайл", "");
            return new MediaDTO
            {
                Artist = media.Artist,
                Genre = media.Genre.Name,
                FileAdress = media.FileAdress,
                Id = media.Id,
                Name = media.Name,
                Poster = media.Poster,
                UserId = media.User.id
                
            };
        }

        public async Task<MediaDTO> GetMedia(string name)
        {
            var media = await Database.Medias.Get(name);
            if (media == null) throw new ValidationException("Неверный медиаФайл", "");
            return new MediaDTO
            {
              
                Artist = media.Artist,
                Genre = media.Genre.Name,
                FileAdress = media.FileAdress,
                Id = media.Id,
                Name = media.Name,
                Poster = media.Poster,
                UserId = media.User.id
            };

        }
        public async Task DeleteMedia(int id)
        {
           await Database.Medias.Delete(id);
        }
        public async Task UpdateMedia(MediaDTO mediaDto)
        {
            var Media = new Media
            {
                Id = mediaDto.Id,
                Name = mediaDto.Name,
                Artist = mediaDto.Artist,
                FileAdress = mediaDto.FileAdress,
                Genre = Database.Genres.Get(mediaDto.Genre).Result,
                Poster = mediaDto.Poster,
             
                User = Database.Users.Get(mediaDto.UserId).Result

            };
            Database.Medias.UpdateMedia(Media);
            await Database.Save();
        }

    }
}
