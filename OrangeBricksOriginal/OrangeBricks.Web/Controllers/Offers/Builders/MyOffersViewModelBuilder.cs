using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class MyOffersViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public OffersOnPropertyViewModel Build(string buyerId)
        {


            var offers = _context.Offers
                .Where(o => o.BuyerId == buyerId);

            //var property = _context.Properties
            //    .Where(p => p.Id == id)
            //    .Include(x => x.Offers)
            //    .SingleOrDefault();

            return new OffersOnPropertyViewModel
            {
                HasOffers = offers.Any(),
                Offers = offers.Select(x => new OfferViewModel
                {
                   
                    Id = x.Id,
                    Amount = x.Amount,
                    CreatedAt = x.CreatedAt,
                    IsPending = x.Status == OfferStatus.Pending,
                    Status = x.Status.ToString()


                })//,
                //PropertyId = property.Id, 
                //PropertyType = property.PropertyType,
                //StreetName = property.StreetName,
                //NumberOfBedrooms = property.NumberOfBedrooms
            };
        }
    }
}