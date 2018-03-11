    function VehicleServiceCompanyOwner(data)
    {
        this.id=null;
        this.phoneNumber=null;
        this.firstName=null;
        this.lastName=null;
        this.email=null;
        this.vehicles=null;
        this.uid = null;
        this.serviceCompanies = ko.observableArray(null);
        if(data!=null)
        {
            this.id=data.ID;
            this.phoneNumber=data.PhoneNumber;
            this.firstName=data.FirstName;
            this.lastName=data.LastName;
            this.email=data.email;
            this.uid=data.UID;
            var serviceCompanies = _.map(data.VehicleServiceCompanies, function (vehicleServieCompany, index) {
                return new VehicleServiceCompany(vehicleServieCompany);

            });
           
        }
    }