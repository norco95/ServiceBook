function VehicleServiceCompany(data) {
    this.id=null;
    this.serviceName=ko.observable(null);
 
    this.flag = null;
    this.workingPoints=ko.observableArray(null);
    if (data != null) 
    {
        this.id = data.ID;
        this.serviceName(data.ServiceName);
        this.flag = data.Flag;
     
        var workingPoints = _.map(data.WorkingPoints, function (workingPoints, index) {
            return new WorkingPoint(workingPoints);
        });
        this.workingPoints(workingPoints);
       
    }
}