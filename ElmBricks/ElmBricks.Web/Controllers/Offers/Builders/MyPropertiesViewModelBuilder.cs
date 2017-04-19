using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class MyPropertiesOfferedViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyPropertiesOfferedViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MyPropertiesViewModel Build(string buyerId)
        {
            return new MyPropertiesViewModel
            {
                Properties = _context.Properties
                    .Where(p => p.Offers.Any(o => o.BuyerId == buyerId))
                    .Select(p => new PropertyViewModel
                    {
                        Id = p.Id,
                        StreetName = p.StreetName,
                        Description = p.Description,
                        NumberOfBedrooms = p.NumberOfBedrooms,
                        PropertyType = p.PropertyType,
                        IsListedForSale = p.IsListedForSale
                    })
                   .ToList()
            };
        }
    }
}