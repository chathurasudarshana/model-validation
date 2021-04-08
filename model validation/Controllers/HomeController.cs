namespace model_validation.Controllers
{
    using model_validation.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*
        https://www.programmingwithwolfgang.com/model-validation-in-asp-net-mvc/#:~:text=Model%20validation%20is%20the%20process,don't%20make%20any%20sense.
                Validation Methods
        Explicitly validating a Model
        Validation using the Model Binder
        Validation using Metadata
        client-side validation
        remote validation
        */

        public ViewResult RegisterCustomerExplicit()
        {
            return View(new CustomerExplicit { Birthday = new DateTime(1980,1,1)});
        }

        [HttpPost]
        public ViewResult RegisterCustomerExplicit(CustomerExplicit customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
            {
                ModelState.AddModelError("Name", "Please enter your name");
            }

            // Model Binder Error: Automatically add a validation if string is not a date
            
            if (ModelState.IsValidField("Birthday") && DateTime.Now < customer.Birthday)
            {
                ModelState.AddModelError("Name", "Please enter a date in past");
            }

            if (!customer.TermsAccepted)
            {
                ModelState.AddModelError("TermsAccepted", "Please Accept Terms");
            }

            // Model Level Validation
            if (customer.Name == "Wolf" && customer.Birthday.Date == DateTime.Now.Date.Add(new TimeSpan(-1, 0, 0, 0)))
            {
                ModelState.AddModelError("", "Wolf birthday can not be yesterday.");
            }

            ViewResult viewResult;
            if (ModelState.IsValid)
            {
                viewResult = View("RegisterComplete", customer);
            }
            else
            {
                viewResult = View();
            }

            return viewResult;
        }

        public ViewResult RegisterCustomerMetadata()
        {
            return View(new CustomerMetadata { Birthday = new DateTime(1980, 1, 1) });
        }

        [HttpPost]
        public ViewResult RegisterCustomerMetadata(CustomerMetadata customer)
        {
            // Model Level Validation
            if (customer.FirstName == "Wolf" && customer.Birthday.Date == DateTime.Now.Date.Add(new TimeSpan(-1, 0, 0, 0)))
            {
                ModelState.AddModelError("", "Wolf birthday can not be yesterday.");
            }

            if (customer.LastName == "Wolf")
            {
                ModelState.AddModelError("LastName", "The last name can't be wolf.");
            }

            ViewResult viewResult;
            if (ModelState.IsValid)
            {
                viewResult = View("RegisterComplete", customer);
            }
            else
            {
                viewResult = View();
            }

            return viewResult;
        }


        /*
For client validation
        set webconfig
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />

add NuGet packages and refer them in view/layout
        jQuery
        jQuery.Validation
        Microsoft.jQuery.Unobtrusive.Validation


         */


        // remote validation
        public JsonResult ValidateLastName(string LastName)
        {
            JsonResult jsonResult;

            if (LastName == "Wolf")
            {
                jsonResult = Json("The last name can't be wolf", JsonRequestBehavior.AllowGet);
            }
            else
            {
                jsonResult = Json(true, JsonRequestBehavior.AllowGet);
            }

            return jsonResult;
        }
    }
}