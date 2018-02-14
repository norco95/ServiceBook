function Vehicle(data)
{
    this.vehicleOwner = new VehicleOwner();
    this.id=null;
    this.vin=null;
    this.identifier=null;
    this.oid=null;
    this.services=null;
    if(data!=null)
    {
        this.id=data.ID;
        this.vin=data.VIN;
        this.identifier=data.Identifier;
        this.oid=data.OID;
        this.services = _.map(data.Services, function (service, index) {
            return new Service(service);

        });
        this.vehicleOwner = new VehicleOwner(data.VehicleOwner);

    }
}

