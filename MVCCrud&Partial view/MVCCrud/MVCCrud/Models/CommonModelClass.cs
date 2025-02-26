using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVCCrud.Models
{
    public class CommonModelClass
    {
        public List<EmployeeInfo> empListObj { get; set; }
        public EmployeeInfo empObj { get; set; }
        public List<ProductsInfo> prodListObj { get; set; }
        public ProductsInfo prodObj { get; set; }
    }

    //Without Primary Key in SQL Table
    public class EmployeeInfo
    {
        [Key] // This indicates `EmpId` will be primary key where we do not set primary key in SQL table
        public string ? EmpId { get; set; } // `?` denotes this string can also accept nullable values too.
        public string ? EmpName { get; set; }
        public int EmpAge { get; set; }
        public string? EmpPhNo { get; set; }
        public double EmpSalary { get; set; }
    }

    //With Primary Key in SQL Table
    public class ProductsInfo
    {
        [Key]
        public int ProdId { get; set; } // `ProdId` where it will be set as primary key with auto increment in SQL Table
        public string? ProdName { get; set; } 
        public int ProdQuantity { get; set; }
        public double ProdPrice { get; set; }
    }
}
