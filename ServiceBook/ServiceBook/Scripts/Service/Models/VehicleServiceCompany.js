function VehicleServiceCompany(data) {
    this.id=null;
    this.serviceName=ko.observable(null);
    this.cco = ko.observableArray(null);
    this.flag = null;
    this.workingPoints=ko.observableArray(null);
    if (data != null) 
    {
        this.id = data.ID;
        this.serviceName(data.ServiceName);
        this.flag = data.Flag;
        var cco = _.map(data.CCO, function (cco, index) {
            return new CCO(cco);
        });
        this.cco(cco);
        var workingPoints = _.map(data.WorkingPoints, function (workingPoints, index) {
            return new WorkingPoint(workingPoints);
        });
        this.workingPoints(workingPoints);
       
    }
}