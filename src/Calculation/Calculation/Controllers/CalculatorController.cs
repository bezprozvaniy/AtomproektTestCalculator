using System;
using System.Web.Mvc;
using Calculation.Extentions;
using Calculation.Models;

namespace Calculation.Controllers
{
    public class CalculatorController : Controller
    {
        public ActionResult Index()
        {
            return View(new CalculatorModel());
        }

        [HttpPost]
        public ActionResult Calculate(CalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Calculate();
                }
                catch (Exception e)
                {
                    model.ModelException = e.Message;
                }
            }

            return View("Index", model);
        }
    }
}