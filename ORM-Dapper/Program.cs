using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection conn = new MySqlConnection(connString);

            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user! Here are the current departments to choose from:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();

            var depos = repo.GetAllDepartments();
            Print(depos);

            Console.WriteLine("DO you want to add a department?");
            string userResponse = Console.ReadLine();
            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your BRAND NEW DEPARTMENT!!?!?!?!");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());

            }

            Console.WriteLine("Have a fantastic day!");
        }
    private static void Print(IEnumerable<Department> depos)
    {
        foreach (var depo in depos)
        {
            Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
        }
    }
    }

}
