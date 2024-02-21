using Microsoft.EntityFrameworkCore;
using MySpotify.DAL.Entities;

namespace MySpotify.DAL.EF
{
    public class MediaUserContext : DbContext
    {   

        public DbSet<User> Users {  get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MediaUserContext(DbContextOptions<MediaUserContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                /*login == azekaggg@gmail.com */
                /*pass == 1111*/
                

                Users?.Add(new User { FirstName = "Sergio", LastName = "Adminos", Email = "azekaggg@gmail.com", Status = Status.admin, Active = true , Password = "938E3782FCDEC612B9A5BB5FD9EB94E9", Salt = "4BFEA494ACEE92BB6ADBD53348F5CD52" });
                SaveChanges();

                Genres?.AddRange(new Genre { Name = "Pop" }, new Genre { Name = "Rock" }, new Genre { Name = "Rap" },
                    new Genre { Name = "Metal" }, new Genre { Name = "House" }, new Genre { Name = "Tehno" },
                    new Genre { Name = "Indi" }, new Genre { Name = "Post punk" }, new Genre { Name = "Reggae" });
                SaveChanges();

                Medias?.Add(new Media { Name = "Слышишь", Artist = "Dante", Genre = Genres?.First(x => x.Name == "Pop"), Poster = "/Media/Poster/Dantes.jpeg", FileAdress = "/Media/Music/Vladimir Dantes-Чуєш.mp3", User = Users.FirstOrDefault(x=>x.Status == Status.admin)});
                Medias?.Add(new Media { Name = "Ohne dich", Artist = "Rammstein", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/Rammstein.jpg", FileAdress = "Media/Music/ramshtajn_rammstein_-_ohne_dich_(z3.fm).mp3" , User = Users.FirstOrDefault(x => x.Status == Status.admin) });
                Medias?.Add(new Media { Name = "Слышишь", Artist = "ДзиДзьо", Genre = Genres?.First(x => x.Name == "Pop"), Poster = "/Media/Poster/DziDzio.jpg", FileAdress = "/Media/Music/DziDzio-pavuk.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "live like a dream.", Artist = "30 seconds to mars", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/30SecondsToMars.jpg", FileAdress = "/Media/Music/30_seconds_to_mars-live_like_a_dream.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "Dream on", Artist = "Aerosmith", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/aerosmith.jpg", FileAdress = "/Media/Music/aerosmith-dream_on.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "Тереза Марія", Artist = "Alyona Alyona_", Genre = Genres?.First(x => x.Name == "Pop"), Poster = "/Media/Poster/alyonaAlyona.jpg", FileAdress = "/Media/Music/alyona_alyona_feat_jerry_heil_-_teresa__maria_.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "Це зі мною", Artist = "Бумбокс", Genre = Genres?.First(x => x.Name == "Pop"), Poster = "/Media/Poster/Bumbox.jpeg", FileAdress = "/Media/Music/bumboks_i_okean_elzi_-_ce_zi_mnoju_.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "Пластинки", Artist = "Дурной вкус", Genre = Genres?.First(x => x.Name == "Post punk"), Poster = "/Media/Poster/DurnoyVkus.jpg", FileAdress = "/Media/Music/durnoj_vkus_-_plastinki_.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "If everyone cared", Artist = "Nickelback", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/Nickleback.jpg", FileAdress = "/Media/nickelback-if_everyone_cared.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "RockStar", Artist = "Nickelback", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/Nickleback.jpg", FileAdress = "/Media/Music/nickelback-rockstar.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "Worthy to say", Artist = "Nickelback", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/Nickleback2.jpg", FileAdress = "/Media/Music/nickelback-worthy_to_say.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "Ich will", Artist = "Rammstein", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/Rammstein2.jpg", FileAdress = "/Media/Music/rammstein_ramshtajn_-_ich_will_(z3.fm).mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });
				Medias?.Add(new Media { Name = "Ohne dich", Artist = "Rammstein", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Media/Poster/Rammstein.jpg", FileAdress = "/Media/Music/ramshtajn_rammstein_-_ohne_dich_(z3.fm).mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });

				SaveChanges();
            };
             
            
        }

    }
}
