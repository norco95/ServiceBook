function Service(data)
{
    
    this.id=null;
    this.serviceDate=null;
    this.flag=null;
    this.nextVisitDate=ko.observable(null);
    this.currentKm=ko.observable(0);
    this.nextVisitKm=ko.observable(0);
    this.vid = null;
    this.vehicle = new Vehicle();
    this.ssi = ko.observableArray(null);
    this.se = null;
    this.sw = null;
    this.price = ko.observable(0);
    if(data!=null)
    {
        this.id=data.ID;
        this.serviceDate=convertToFormatJs(data.ServiceDate);
        this.flag=data.Flag;
        this.nextVisitDate(convertToFormatJs(data.NextVisitDate));
        this.currentKm(data.CurrentKm);
        this.nextVisitKm(data.NextVisitKm);
        this.vid = data.VID;
        this.vehicle = new Vehicle(data.Vehicle);
        this.price(data.Price);
        var ssi = _.map(data.SSI, function (ssi, index) {
            return new SSI(ssi);

        });
        this.ssi(ssi);
        this.sw = _.map(data.SW, function (sw, index) {
            return new SW(sw);

        });
        this.se = _.map(data.SE, function (se, index) {
            return new SE(se);

        });
        
    }
}


