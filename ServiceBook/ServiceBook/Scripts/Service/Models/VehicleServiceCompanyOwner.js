


    function VehicleServiceCompanyOwner(data)
    {
        this.id=null;
        this.phoneNumber=null;
        this.firstName=null;
        this.lastName=null;
        this.email=null;
        this.vehicles=null;
        this.uid = null;
        this.cco = null;
        if(data!=null)
        {
            
            this.id=data.ID;
            this.phoneNumber=data.PhoneNumber;
            this.firstName=data.FirstName;
            this.lastName=data.LastName;
            this.email=data.email;
            this.uid=data.UID;
            this.cco = _.map(data.CCO, function (cco, index) {
                return new CCO(cco);

            });
           
        }
    }