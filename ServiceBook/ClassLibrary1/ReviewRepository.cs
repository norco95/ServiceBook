using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public class ReviewRepository
    {
        ServiceBookContext ServiceBookContext = new ServiceBookContext();
        public VehicleOwner AddReview(int ID , string Review , int Rate )
        {
            Service Service=ServiceBookContext.Service.FirstOrDefault(x=>x.ID==ID);
            if(Service.Review==null)
            {
                Service.Review = new Review();
                Service.Review.Rate = Rate;
                Service.Review.Service = Service;
            }
            
            
            Service.Review.Description += "\n" + Review;

            ServiceBookContext.SaveChanges();
            VehicleOwner VehicleOwner = Service.Vehicle.VehicleOwner;

            return VehicleOwner;

        }
        public int GetRevirewCount()
        {
            return ServiceBookContext.Review.Count();
        }
    }
}
