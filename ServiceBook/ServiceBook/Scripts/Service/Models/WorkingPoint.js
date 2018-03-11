function WorkingPoint(data)
{
    this.id=null;
    this.country=ko.observable(null);
    this.city=ko.observable(null);
    this.street=ko.observable(null);
    this.nr=ko.observable(null);
    this.vscoid=null;
    this.employees=ko.observableArray(null);
    this.serviceInterventions = ko.observableArray(null);
    this.flag = null;
    this.vscid = null;
    this.services = ko.observableArray(null);
    this.serviceCompany = null;
    if (data != null)
    {
        this.id = data.ID;
        this.country(data.Country);
        this.city(data.City);
        this.street(data.Street);
        this.nr(data.Nr);
        this.vscid = data.VSCID;
        this.flag = data.Flag;
        var employees = _.map(data.Employees, function (employee, index) {
            return new Employee(employee);

        });
        this.employees(employees);
        var serviceInterventions=_.map(data.ServiceInterventions, function (serviceIntervention, index) {
            return new ServiceIntervention(serviceIntervention);

        });
        this.serviceInterventions(serviceInterventions);

        var services = _.map(data.Services, function (service, index) {
            return new Service(service);

        });
        this.services(services);
        this.serviceCompany = new VehicleServiceCompany(data.VehicleServiceCompany);
    }
}



