using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        //Field or local variable for making queries to the DataBase
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            //Constructor
            _connection = connection;

        }

        public IEnumerable<Department> GetAllDepartments()
        {
            var depos = _connection.Query<Department>("SELECT * FROM departments");

            return depos;
        }

        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
                new { departmentName = newDepartmentName });

        }

       
    }
}
