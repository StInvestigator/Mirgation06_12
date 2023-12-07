using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class TournamendContext : DbContext
    {
        public string connectionString = @"Data Source=DESKTOP-OF66R01\SQLEXPRESS;Initial Catalog=TournamentTopDB;Integrated Security=True";
        public DbSet<Game> Games { set; get; }
        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandPlayer> CommandPlayers { set; get; }
        public DbSet<GameGoal> GameGoals { set; get; }
        public DbSet<Player> Players { set; get; }

        public TournamendContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public void GetScoreMinusLose()
        {
            var games = Games;
            var commands = Commands;

            var query = from game in games
                        join command1 in commands on game.Command1Id equals command1.Id
                        join command2 in commands on game.Command2Id equals command2.Id
                        select new
                        {
                            command1Name = command1.Name,
                            command2Name = command2.Name,
                            command1Score = game.Command1Goals,
                            command2Score = game.Command2Goals,
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"Command: {item.command1Name}, ScoreRate: {item.command1Score*100/(item.command2Score == 0 ? 1 : item.command2Score)}%\n" +
                    $"Command: {item.command2Name}, ScoreRate: {item.command2Score * 100 / (item.command1Score == 0 ? 1 : item.command1Score)}%");
            }
        }
        public void GetAllMatchInfo(DateTime? matchsDate = null)
        {
            var games = Games;
            var commands = Commands;

            var query = from game in games
                        join command1 in commands on game.Command1Id equals command1.Id
                        join command2 in commands on game.Command2Id equals command2.Id
                        select new
                        {
                            command1Name = command1.Name,
                            command2Name = command2.Name,
                            command1Score = game.Command1Goals,
                            command2Score = game.Command2Goals,
                            date = game.Date
                        };
            if (matchsDate != null)
            {
                 query = query.Where(x => x.date == matchsDate);
            }
            foreach (var item in query)
            {
                Console.WriteLine($"Command 1: {item.command1Name}, Score: {item.command1Score}\n" +
                    $"Command 2: {item.command2Name}, Score: {item.command2Score}\nDate: {item.date}\n");
            }
        }
        public void GetPlayerThatScoredOnDate(DateTime matchsDate)
        {

            var query = from game in Games
                        join gameGoals in GameGoals on game.Id equals gameGoals.GameId
                        join players in Players on gameGoals.PlayerId equals players.Id
                        where game.Date == matchsDate
                        select new
                        {
                            playerName = players.Name,
                            ingameNumber = players.IngameNumber,
                            position = players.Position
                        };
            foreach (var item in query.GroupBy(x => x.playerName).Select(g => g.First()).ToList())
            {
                Console.WriteLine($"Name: {item.playerName}, Ingame Number: {item.ingameNumber}, Ingame Position: {item.position}");
            }
        }
        public void GetAllTeamsMarches(string teamName)
        {

            var query = from game in Games
                        join command1 in Commands on game.Command1Id equals command1.Id
                        join command2 in Commands on game.Command2Id equals command2.Id
                        where command1.Name == teamName || command2.Name == teamName
                        select new
                        {
                            command1Name = command1.Name,
                            command2Name = command2.Name,
                            command1Score = game.Command1Goals,
                            command2Score = game.Command2Goals,
                            date = game.Date
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"Command 1: {item.command1Name}, Score: {item.command1Score}\n" +
                    $"Command 2: {item.command2Name}, Score: {item.command2Score}\nDate: {item.date}\n");
            }
        }
        public void AddMatch(int Id, string Command1, string Command2, int Score1, int Score2, DateTime Date)
        {
            if (Games.Where(x => x.Command1Id == Commands.Where(y => y.Name == Command1).First().Id && x.Command2Id == Commands.Where(y => y.Name == Command2).First().Id && x.Date == Date || x.Id == Id).Count() == 0)
            {
                Games.Add(new Game(Id,Commands.Where(x => x.Name == Command1).First().Id, Commands.Where(x => x.Name == Command2).First().Id,Score1,Score2,Date));
                SaveChanges();
            }
        }
        public void UpdateMatch(int Id, string Command1, string Command2, int Score1, int Score2, DateTime Date)
        {
            if (Games.Where(x => x.Id == Id) != null)
            {
                Games.Remove(Games.Where(x => x.Id == Id).First());
                Games.Add(new Game(Id, Commands.Where(x => x.Name == Command1).First().Id, Commands.Where(x => x.Name == Command2).First().Id, Score1, Score2, Date));
                SaveChanges();
            }
        }
        public void DeleteMatch(string Command1, string Command2, DateTime Date)
        {
            Console.WriteLine("Are you sure want to delete this game from database? \nY - Yes");
            if (Console.ReadLine()?.ToLower().Trim() != "y") return;
            Games.Remove(Games.Where(x => 
                                    x.Command1Id == Commands.Where(y => y.Name == Command1).First().Id && 
                                    x.Command2Id == Commands.Where(y => y.Name == Command2).First().Id && 
                                    x.Date == Date).First());
            SaveChanges();
        }
    } 
}
