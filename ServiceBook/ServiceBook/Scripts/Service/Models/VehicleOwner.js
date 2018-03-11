function VehicleOwner(data)
{
    this.id=null;
    this.phoneNumber=null;
    this.firstName=null;
    this.lastName=null;
    this.email=null;

    if(data!=null)
    {
        this.id=data.ID;
        this.phoneNumber=data.PhoneNumber;
        this.firstName=data.FirstName;
        this.lastName=data.LastName;
        this.email=data.Email;
     
       
    }
}


