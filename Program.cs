

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Runtime.InteropServices;

namespace Mirgation06_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (GameContext db = new GameContext())
                {
                    // Відображення кількості однокористувацьких ігор.
                    db.showSinglePlayerGames();
                    Console.WriteLine("\n---------------------\n");

                    // Відображення кількості багатокористувацьких ігор.
                    db.showMultiPlayerGames();
                    Console.WriteLine("\n---------------------\n");

                    // Показати гру з максимальною кількістю проданих ігор певного стилю.
                    db.showMaxSoldByStyleGames("ActionRPG");
                    Console.WriteLine("\n---------------------\n");

                    // Відображення Топ-5 ігор за найбільшою кількістю продажів певного стилю.
                    db.showTop5MaxSoldByStyleGames("ActionRPG");
                    Console.WriteLine("\n---------------------\n");

                    // Відображення Топ-5 ігор за найменшою кількістю продажів конкретного стилю.
                    db.showTop5MinSoldByStyleGames("ActionRPG");
                    Console.WriteLine("\n---------------------\n");

                    // Відобразити повну інформацію про гру.
                    db.showGames("SkyWars");
                    Console.WriteLine("\n---------------------\n");

                    // Відобразити назву кожної студії та стиль ігор, кількість яких переважає по створенню у цьому стилі.
                    db.showStudionAndHerBestStyle();
                    Console.WriteLine("\n---------------------\n");

                    db.addCreator(new GameDB2.Creator(10,"Creator"));
                    foreach (var item in db.Creators)
                    {
                        Console.WriteLine($"Name: {item.Name}\n");
                    }
                    Console.WriteLine("\n---------------------\n");
                    db.changeCreator(1,new GameDB2.Creator(1, "SuperStudio"));
                    foreach (var item in db.Creators)
                    {
                        Console.WriteLine($"Name: {item.Name}\n");
                    }
                    Console.WriteLine("\n---------------------\n");
                    db.deleteCreator("Creator");
                    foreach (var item in db.Creators)
                    {
                        Console.WriteLine($"Name: {item.Name}\n");
                    }
                    Console.WriteLine("\n---------------------\n");

                    db.showGames();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
