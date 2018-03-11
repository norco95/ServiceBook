function Service(data)
{
    
    this.id=null;
    this.serviceDate=null;
    this.flag=null;
    this.nextVisitDate=ko.observable(null);
    this.currentKm=ko.observable(0);
    this.nextVisitKm=ko.observable(0);
    this.vehicle = null;
    this.serviceInterventions = ko.observableArray(null);
    this.employees = ko.observableArray(null);
    this.price = ko.observable(0);
    this.workingPoint=ko.observable("");
    this.companyName=ko.observable("");
    if(data!=null)
    {
        this.id=data.ID;
        this.serviceDate=convertToFormatJs(data.ServiceDate);
        this.flag=data.Flag;
        this.nextVisitDate(convertToFormatJs(data.NextVisitDate));
        this.currentKm(data.CurrentKm);
        this.workingPoint(data.WorkingPoint);
        this.companyName(data.CompanyName);
        this.nextVisitKm(data.NextVisitKm);
        this.vehicle = new Vehicle(data.Vehicle);
        this.price(data.Price);
        var serviceInterventions = _.map(data.ServiceInterventions, function (serviceInterventions, index) {
            return new ServiceIntervention(serviceInterventions);

        });
        this.serviceInterventions(serviceInterventions);
      

        var employees = _.map(data.Employees, function (employee, index) {
            return new Employee(employee);

        });
        this.employees(employees);
    }
}


