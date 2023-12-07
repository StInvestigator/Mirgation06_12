

using Microsoft.Data.SqlClient;
using Mirgation06_12;

namespace Migration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (TournamendContext db = new TournamendContext())
                {
                    //db.GetScoreMinusLose();
                    //db.GetAllMatchInfo(new DateTime(2023,01,01,18,0,0));
                    //db.GetPlayerThatScoredOnDate(new DateTime(2023,01,01,18,0,0));
                    //db.GetAllTeamsMarches("TeamA");
                    db.AddMatch(4, "TeamA", "TeamB", 3, 2, new DateTime(2023, 12, 07, 19, 45, 00));
                    db.GetAllMatchInfo();
                    Console.WriteLine("----------");
                    db.UpdateMatch(4, "TeamB", "TeamA", 2, 3, new DateTime(2023, 12, 07, 19, 45, 00));
                    db.GetAllMatchInfo();
                    Console.WriteLine("----------");
                    db.DeleteMatch("TeamB", "TeamA", new DateTime(2023, 12, 07, 19, 45, 00));
                    db.GetAllMatchInfo();
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
