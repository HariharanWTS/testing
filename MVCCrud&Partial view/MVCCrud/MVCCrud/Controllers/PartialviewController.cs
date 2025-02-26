using MVCCrud.Data;
using MVCCrud.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MVCCrud.Controllers
{
    public class PartialviewController : Controller
    {
        // We have to create this separate dbcontext for each controller
        private readonly GeneralDbContext _partialContext;

        // We have to create this separate dbcontext for each controller
        public PartialviewController(GeneralDbContext partialContext)
        {
            _partialContext = partialContext;
        }

        public IActionResult PartialViewMaster()
        {
            CommonModelClass proObj = new CommonModelClass(); // Creating object for `CommonModelClass` to access all the classess/tables 
            proObj.prodListObj = _partialContext.ProductsInfo.FromSqlRaw("Select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo").ToList();
            // `proObj.prodListObj` where `proObj` is an object created newly and `prodListObj` is an list object which is already created as a field in `CommonModelClass`.
            return View(proObj); // This object contains the full list of `ProductsInfo` table.
        }

        [HttpPost] // This name is used because the below action method is called/triggered from the AJAX request. For AJAX calling `[HttpPost]` is required bcoz after that it can send data to the database without loading the whole page. In the AJAX request we used `type:"POST"`
        public IActionResult AddNewProductsData([FromBody]ProductsInfo productObj)
        // `[FromBody]ProductsInfo productObj` where `[FromBody]` is an syntax for AJAX calling,
        // `ProductsInfo` is an table we already created, although we used `CommonModelClass` in the `_ProdCreateForm.cshtml` we were get the data through ajax(JSON.stringify), so we used the `ProductsInfo` class directly.
        // `productObj` is an object created newly to receive the data from ajax calling.
        {
            try
            {
                if (productObj != null)
                {
                    _partialContext.Database.ExecuteSqlRaw("Insert into ProductsInfo (ProdName, ProdQuantity, ProdPrice) values ({0}, {1}, {2})",productObj.ProdName, productObj.ProdQuantity, productObj.ProdPrice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            CommonModelClass generalObj = new CommonModelClass();
            generalObj.prodListObj = _partialContext.ProductsInfo.FromSqlRaw("select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo").ToList();
            return PartialView("_ProdListTable", generalObj); 
            // `_ProdListTable` is the place/file where the object `generalObj` which contains all the list of data (of ProductsInfo talbe) will be going to fix.
            // While we use AJAX, we use `return PartialView("<.cshtml file name>",<object name>)` instead of  `return View(<object name>)`.
        }

        public IActionResult EditProductData(int ProdId) // Here we are not used `[HttpPost]` bcoz although it is an ajax calling action method, it is not going to send the data to the database. It will retrieve data from the databse because in the AJAX request we used `type:"GET"`.
        //`ProdId` name used in the both AJAX and field of `ProductsInfo` class.
        {
            ProductsInfo pObj = new ProductsInfo();
            try
            {
                if (ProdId != 0)
                {
                    pObj = _partialContext.ProductsInfo.FromSqlRaw("select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo where ProdId={0}", ProdId).FirstOrDefault();
                    var productDetails = new CommonModelClass { prodObj = pObj }; // This another type of passing the data
                    //`productDetails` is an object created for `CommonModelClass` where `prodObj` is already a field created in that Model class ad we assign the `pObj` object to that `prodObj` field which already indicates `ProductsInfo` class.

                    return PartialView("_ProdEditForm", productDetails);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
            return PartialView("_ProdEditForm", new ProductsInfo());
        }
        // new changes 2

        [HttpPost]
        public IActionResult UpdateProductData([FromBody]ProductsInfo pObj)
        {
            try
            {
                if (pObj != null) {
                    _partialContext.Database.ExecuteSqlRaw("update ProductsInfo set ProdName={0}, ProdQuantity={1}, ProdPrice={2} where ProdId={3}", pObj.ProdName, pObj.ProdQuantity, pObj.ProdPrice, pObj.ProdId);
                }
                else
                {
                    return Json("Failed"); // It return to the AJAX `error: function()`
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            CommonModelClass generalObj = new CommonModelClass();
            generalObj.prodListObj = _partialContext.ProductsInfo.FromSqlRaw("select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo").ToList();
            return PartialView("_ProdListTable", generalObj);
        }

        [HttpPost]
        public IActionResult DeleteProductData(int ProdId)
        {
            Console.WriteLine(ProdId);
            try
            {
                if (ProdId != 0)
                {
                    _partialContext.Database.ExecuteSqlRaw("delete from ProductsInfo where ProdId={0}", ProdId);
                }
                else
                {
                    return Json("Failed");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            CommonModelClass generalObj = new CommonModelClass();
            generalObj.prodListObj = _partialContext.ProductsInfo.FromSqlRaw("select ProdId, ProdName, ProdQuantity, ProdPrice from ProductsInfo").ToList();
            return PartialView("_ProdListTable", generalObj);
        }
    }
}
