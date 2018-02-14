function Employee(data)
{
    this.id=null;
    this.firstName=null;
    this.lastName=null;
    this.phoneNumber=null;
    this.email=null;
    this.wpid=null; 
    this.se=null;
    if(data!=null)
    {
        this.se = _.map(data.SE, function (se, index) {
            return new SE(se);

        });
       
        this.id = data.ID;
        this.firstName = data.FirstName;
        this.lastName = data.LastName;
        this.phoneNumber = data.PhoneNumber;
        this.email = data.Email;
        this.wpid = data.WPID;
    }
    
}