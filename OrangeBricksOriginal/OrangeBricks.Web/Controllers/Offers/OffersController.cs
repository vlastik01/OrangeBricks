using System.Web.Mvc;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Offers.Builders;
using OrangeBricks.Web.Controllers.Offers.Commands;
using OrangeBricks.Web.Models;
using System.Collections;
using System.Linq;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Controllers.Property.ViewModels;


namespace OrangeBricks.Web.Controllers.Offers
{
    
    public class OffersController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public OffersController(IOrangeBricksContext context)
        {
            _context = context;
        }
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult OnProperty(int id)
        {
            var builder = new OffersOnPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id);

            return View(viewModel);
        }

        public ActionResult OnPropertyMyOffers(int id)
        {
            var builder = new MyOffersOnPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id, User.Identity.GetUserId());

            return View(viewModel);
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
       public ActionResult MyPropertiesOffers()
       {
           var builder = new MyPropertiesOfferedViewModelBuilder(_context);
           var viewModel = builder.Build(User.Identity.GetUserId());

           return View(viewModel);
       }

        //[OrangeBricksAuthorize(Roles = "Seller")]
        //public ActionResult MyProperties()
        //{
        //    var builder = new MyPropertiesViewModelBuilder(_context);
        //    var viewModel = builder.Build(User.Identity.GetUserId());

        //    return View(viewModel);
        //}


        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Accept(AcceptOfferCommand command)
        {
            var handler = new AcceptOfferCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Reject(RejectOfferCommand command)
        {
            var handler = new RejectOfferCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }
    }
}