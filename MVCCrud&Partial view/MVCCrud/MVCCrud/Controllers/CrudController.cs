using MVCCrud.Data;
using MVCCrud.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace MVCCrud.Controllers
{
    public class CrudController : Controller
    {
        private readonly GeneralDbContext _generalContext;

        public CrudController(GeneralDbContext generalContext)
        {
            _generalContext = generalContext;
        }

        public IActionResult DetailsEmpData(string EmpId)
        {
            EmployeeInfo eObj = new EmployeeInfo();
            try
            {
                if(EmpId != null){
                    eObj = _generalContext.EmployeeInfo.FromSqlRaw("Select EmpId, EmpName, EmpAge, EmpPhNo, EmpSalary from EmployeeInfo where EmpId = {0}", EmpId).FirstOrDefault();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(eObj);
        }


        // Without Primary Key
        public IActionResult EmpGetData()
        {
            return View();
        }

        public IActionResult StoreEmpData(EmployeeInfo details)
        {
            try
            {
                if (details != null)
                {
                    _generalContext.Database.ExecuteSqlRaw("Insert into EmployeeInfo(EmpId, EmpName, EmpAge, EmpPhNo, EmpSalary) values ({0}, {1}, {2}, {3}, {4});", details.EmpId, details.EmpName, details.EmpAge, details.EmpPhNo, details.EmpSalary);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("EmpDisplayData");
            
        }

        public IActionResult EditEmployeeData(string EmpId) // `EmpId` parameter name is came from 'EmpDisplayData.cshtml => asp-route-EmpId`. So `asp-route name` and `parameter` must be same name.
        {
            EmployeeInfo empObj = new EmployeeInfo();
            try
            {
                if (EmpId != null)
                {
                    empObj = _generalContext.EmployeeInfo.FromSqlRaw("Select EmpId, EmpName, EmpAge, EmpPhNo, EmpSalary from EmployeeInfo where EmpId = {0}", EmpId).FirstOrDefault();

                }
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return View(empObj);
        }

        public IActionResult UpdateEmployeeData(EmployeeInfo generalObj) //`EmployeeInfo` class must be used bcoz this class is came from the view of the `EditEmployeeData.cshtml`.
        {
            try
            {
                if(generalObj != null)
                {
                    _generalContext.Database.ExecuteSqlRaw("Update EmployeeInfo set EmpName={0}, EmpAge={1}, EmpPhNo={2}, EmpSalary={3} where EmpId={4}", generalObj.EmpName, generalObj.EmpAge, generalObj.EmpPhNo, generalObj.EmpSalary, generalObj.EmpId);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("EmpDisplayData");
        }

        public IActionResult DeleteEmployeeData(string EmpId)
        {
            try
            {
                if (EmpId != null)
                {
                    _generalContext.Database.ExecuteSqlRaw("delete from EmployeeInfo where EmpId={0}", EmpId);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("EmpDisplayData");
        }

        public IActionResult EmpDisplayData()
        {
            List<EmployeeInfo> employeeObj = new List<EmployeeInfo>();
            employeeObj = _generalContext.EmployeeInfo.FromSqlRaw("Select EmpId, EmpName, EmpAge, EmpPhNo, EmpSalary from EmployeeInfo").ToList();
            return View(employeeObj);
        }


        // With Primary key which is created in the database and set with auto increment values
        public IActionResult ProductsDisplayData()
        {
            List<ProductsInfo> productsObj = new List<ProductsInfo>();
            productsObj = _generalContext.ProductsInfo.FromSqlRaw("Select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo").ToList();
            return View(productsObj);
        }

        public IActionResult GetProductsData()
        {
            return View();
        }

        public IActionResult AddNewProducts(CommonModelClass generalObj)
        {
            try
            {
                if(generalObj != null)
                {
                    _generalContext.Database.ExecuteSqlRaw("Insert into ProductsInfo (ProdName, ProdQuantity, ProdPrice) values ({0}, {1}, {2})",generalObj.prodObj.ProdName, generalObj.prodObj.ProdQuantity, generalObj.prodObj.ProdPrice);
                }
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            return RedirectToAction("ProductsDisplayData");
        }

        public IActionResult EditProductsInfo(int ProdId)
        {
            ProductsInfo proObj = new ProductsInfo();
            try
            {
                if (ProdId != null)
                {
                    proObj = _generalContext.ProductsInfo.FromSqlRaw("Select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo where ProdId={0}", ProdId).FirstOrDefault();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(proObj);
        }

        public IActionResult UpdateProductsInfo(ProductsInfo productObj)
        {
            try
            {
                if (productObj != null)
                {
                    _generalContext.Database.ExecuteSqlRaw("Update ProductsInfo set ProdName={0}, ProdQuantity={1}, ProdPrice={2} where ProdId={3}", productObj.ProdName, productObj.ProdQuantity, productObj.ProdPrice, productObj.ProdId);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("ProductsDisplayData");
        }

        public IActionResult DeleteProductsInfo(int ProdId)
        {
            try
            {
                if (ProdId != null)
                {
                    _generalContext.Database.ExecuteSqlRaw("delete from ProductsInfo where ProdId={0}", ProdId);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("ProductsDisplayData");
        }

        public IActionResult DetailProductsInfo(int ProdId)
        {
            ProductsInfo pObj = new ProductsInfo();
            try
            {
                if (ProdId != null)
                {
                    pObj = _generalContext.ProductsInfo.FromSqlRaw("Select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo where ProdId={0}", ProdId).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(pObj);
        }


    }
}
