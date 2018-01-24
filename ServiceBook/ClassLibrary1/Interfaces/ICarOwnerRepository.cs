using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceBook.DAL.Models;


namespace ServiceBook.Repository.Interfaces
{
    interface ICarOwnerRepository
    {
       VehicleOwner CarOwnerById(int id);

        List<VehicleOwner>GetCarOwners();
    }
}
