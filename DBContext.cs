using GameDB2;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class GameContext : DbContext
    {
        public string connectionString = @"Data Source=DESKTOP-OF66R01\SQLEXPRESS;Initial Catalog=GamesDB;Integrated Security=True";
        public DbSet<Game> Games { set; get; }
        public DbSet<Creator> Creators { set; get; }
        public DbSet<Style> Styles { set; get; }
        public DbSet<StudioCountry> StudioCountries { set; get; }
        public DbSet<FiliaStudioCountry> FiliaStudioCountries { set; get; }
        public GameContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public void showGames(string gameName = null)
        {
            var games = Games.ToList();
            var creators = Creators.ToList();
            var styles = Styles.ToList();

            var query = from game in games
                        join creator in creators on game.CreatorId equals creator.Id
                        join style in styles on game.StyleId equals style.Id
                        select new
                        {
                            GameName = game.Name,
                            Creator = creator.Name,
                            Style = style.Name,
                            ReleaseDate = game.ReleaseDate,
                            IsMultiplayer = game.IsMultiplayer,
                            SoldCopies = game.SoldCopies

                        };

            foreach (var item in gameName == null ? query : query.Where(x=>x.GameName == gameName))
            {
                Console.WriteLine($"Game name: {item.GameName}, Creator: {item.Creator}, Style: {item.Style}, \nRelease date: {item.ReleaseDate}, Is multiplayer: {item.IsMultiplayer}, Sold copies: {item.SoldCopies}\n");
            }
        }
        public void showSinglePlayerGames()
        {

            var games = Games.ToList();
            var creators = Creators.ToList();
            var styles = Styles.ToList();

            var query = from game in games
                        where game.IsMultiplayer == false
                        select new
                        {};
            Console.WriteLine($"Single players count: {query.Count()}\n");

        }
        public void showMultiPlayerGames()
        {


            var games = Games.ToList();
            var creators = Creators.ToList();
            var styles = Styles.ToList();

            var query = from game in games
                        where game.IsMultiplayer == true
                        select new
                        {};
            Console.WriteLine($"Multi players count: {query.Count()}\n");

        }

        public void showMaxSoldByStyleGames(string reqStyle)
        {


            var games = Games.ToList();
            var creators = Creators.ToList();
            var styles = Styles.ToList();

            var query = from game in games
                        join style in styles on game.StyleId equals style.Id
                        where style.Name == reqStyle
                        select new
                        {
                            GameName = game.Name,
                            Style = style.Name,
                            ReleaseDate = game.ReleaseDate,
                            IsMultiplayer = game.IsMultiplayer,
                            SoldCopies = game.SoldCopies
                        };
            var res = query.Where(x=> x.SoldCopies == query.Max(y => y.SoldCopies)).First();
            Console.WriteLine($"Game name: {res.GameName}, Style: {res.Style}, Release date: {res.ReleaseDate},\nIs multiplayer: {res.IsMultiplayer}, Sold copies: {res.SoldCopies}\n");
        }
        public void showTop5MaxSoldByStyleGames(string reqStyle)
        {


            var games = Games.ToList();
            var creators = Creators.ToList();
            var styles = Styles.ToList();

            var query = from game in games
                        join style in styles on game.StyleId equals style.Id
                        where style.Name == reqStyle
                        orderby game.SoldCopies descending
                        select new
                        {
                            GameName = game.Name,
                            Style = style.Name,
                            ReleaseDate = game.ReleaseDate,
                            IsMultiplayer = game.IsMultiplayer,
                            SoldCopies = game.SoldCopies
                        };
            var lenth = query.Count() > 5 ? 5 : query.Count();
            for ( var i = 0; i < lenth; i++)
            {
                Console.WriteLine($"Game name: {query.ElementAt(i).GameName}, Style: {query.ElementAt(i).Style}, Release date: {query.ElementAt(i).ReleaseDate},\nIs multiplayer: {query.ElementAt(i).IsMultiplayer}, Sold copies: {query.ElementAt(i).SoldCopies}\n");
            }
        }
        public void showTop5MinSoldByStyleGames(string reqStyle)
        {


            var games = Games.ToList();
            var creators = Creators.ToList();
            var styles = Styles.ToList();

            var query = from game in games
                        join style in styles on game.StyleId equals style.Id
                        where style.Name == reqStyle
                        orderby game.SoldCopies ascending
                        select new
                        {
                            GameName = game.Name,
                            Style = style.Name,
                            ReleaseDate = game.ReleaseDate,
                            IsMultiplayer = game.IsMultiplayer,
                            SoldCopies = game.SoldCopies
                        };
            var lenth = query.Count() > 5 ? 5 : query.Count();
            for (var i = 0; i < lenth; i++)
            {
                Console.WriteLine($"Game name: {query.ElementAt(i).GameName}, Style: {query.ElementAt(i).Style}, Release date: {query.ElementAt(i).ReleaseDate},\nIs multiplayer: {query.ElementAt(i).IsMultiplayer}, Sold copies: {query.ElementAt(i).SoldCopies}\n");
            }
        }
        public void showStudionAndHerBestStyle()
        {


            var games = Games.ToList();
            var creators = Creators.ToList();
            var styles = Styles.ToList();

            var query = from game in games
                        join style in styles on game.StyleId equals style.Id
                        join creator in creators on game.CreatorId equals creator.Id
                        select new
                        {
                            Style = style.Name,
                            Studio = creator.Name,
                        };
            var list = new List<KeyValuePair<string, KeyValuePair<int,string>>>();
            var res = new List<KeyValuePair<string, KeyValuePair<int,string>>>();
            foreach (var item in query)
            {
                list.Add(new KeyValuePair<string, KeyValuePair<int, string>>(item.Studio, new KeyValuePair<int, string>(query.Count(x=> x.Style == item.Style && x.Studio == item.Studio),item.Style)));
            }
            list = list.Distinct().ToList();
            foreach (var item in list)
            {
                var toAdd = list.Where(x => x.Value.Value == item.Value.Value && x.Key == item.Key && item.Value.Key == list.Where(x => x.Value.Value == item.Value.Value).Max(x => x.Value.Key));
                if (toAdd.Count() != 0)
                {
                    res.Add(toAdd.First());
                }
            }
            foreach (var item in res)
            {
                Console.WriteLine($"Studio: {item.Key}, Games in style count: {item.Value.Key}, Style: {item.Value.Value}\n");
            }
        }

        public void addCreator(Creator newCreator)
        {

            if (Games.ToList().
                Find(x => x.Id == newCreator.Id || (x.Name == newCreator.Name)) != null)
            {
                Console.WriteLine("This studio is already exists!");
                return;
            }
            Creators.Add(newCreator);
            SaveChanges();
        }
        public void changeCreator(int StudioId, Creator newCreator)
        {

            if (Games.ToList().
                Find(x => x.Id == StudioId && x.Id == newCreator.Id) == null)
            {
                Console.WriteLine("Wrong Id!");
                return;
            }

            Creators.Remove(Creators.ToList().Find(x => x.Id == StudioId));
            Creators.Add(newCreator);
            SaveChanges();
        }
        public void deleteCreator(string studioName)
        {
            Creators.Remove(Creators.ToList().Find(x => x.Name == studioName));
            SaveChanges();
        }
        public void addGame(Game newGame)
        {

            if (Games.ToList().
                Find(x => x.Id == newGame.Id || (x.Name == newGame.Name && x.CreatorId == newGame.CreatorId)) != null)
            {
                Console.WriteLine("This game is already exists!");
                return;
            }
            Games.Add(newGame);
            SaveChanges();

        }
        public void changeGame(int GameId, Game newGame)
        {

            if (Games.ToList().
                Find(x => x.Id == GameId && x.Id == newGame.Id) == null)
            {
                Console.WriteLine("Wrong input!");
                return;
            }

            Games.Remove(Games.ToList().Find(x => x.Id == GameId));
            Games.Add(newGame);
            SaveChanges();
        }
        public void deleteGame(string gameName, string creatorName)
        {
            int answ;
            Console.WriteLine($"\nAre you sure want to delete game {gameName} from {creatorName}?\n1 - Yes");
            answ = Convert.ToInt32(Console.ReadLine().ToString());
            if (answ != 1) { return; }
            Games.Remove(Games.ToList().Find(x => x.CreatorId == Creators.ToList().Find(y => y.Name == creatorName).Id && x.Name == gameName));
            SaveChanges();
        }
    }
}
