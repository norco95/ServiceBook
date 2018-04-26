function MainModel()
{   
    var _self = this;

    this.years = ko.observableArray(null);
    this.selectedYear = ko.observable((new Date()).getFullYear());
    var monthNames = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];
    this.months = ko.observableArray(monthNames);
    this.selectedMonth = ko.observable(null);
    this.t=null;
    

    //// ATRIBUTES \\\\

    //Error
    this.companyNameError = ko.observable("");
    this.editCompanyError = ko.observable("");
    this.addWorkingPointError = ko.observable("");
    this.addInterventionError = ko.observable("");
    this.addEmployeeError = ko.observable("");
    this.saveRepairError = ko.observable("");
    this.inputVehicleError = ko.observable("");


    //Company
    this.vehicleServiceCompanyIsSelected = ko.observable(false);
    this.selectedCompany = null;
    this.serviceName = ko.observable("");
    this.editServiceName = ko.observable("");
    this.vehicleServiceCompanies = ko.observableArray(null);
    this.selectedToEditCompany = ko.observable(null);


    //Working Point
    this.workingPointIsSelected = ko.observable(false);
    this.selectedWorkingPoint = null;
    this.workingPoints = ko.observableArray(null);
    this.newWorkingPoint = new WorkingPoint();
    this.workingPointIsASelectedToEdit = ko.observable(false);


    //Intervention
    this.editInterventionsSelected = null;
    this.selectedIntervention = ko.observable(null);
    this.selectedInterventions = ko.observableArray(null);
    this.interventionId = null;
    this.interventions = ko.observableArray(null);
    this.editInterventionIsSelected = ko.observable(false);
    this.interventionName = ko.observable("");
    this.interventionPrice = ko.observable("");


    //Service
    this.VIN = ko.observable(null);
    this.identifier = ko.observable(null);
    this.vehicles = ko.observableArray(null);
    this.selectedVehicle = new Service();
    this.vehicleOwnerFirstName = ko.observable(null);
    this.vehicleOwnerLastName = ko.observable(null);
    this.vehicleOwnerEmail = ko.observable(null);
    this.vehicleOwnerPhoneNumber = ko.observable(null);
    this.vehicleIsSelectedToEdit = ko.observable(false);
    this.vehicleIdToEdit = null;
    this.selectedVehicleCurrentKm = 0;


    //Employee 
    this.employeeId = null;
    this.employeeFirstName = ko.observable("");
    this.employeeLastName = ko.observable("");
    this.employeeEmail = ko.observable("");
    this.employeePhoneNumber = ko.observable("");
    this.employees = ko.observableArray(null);
    this.selectedWorkingPointToEmployees = null;
    this.employeAddAndEditTab = ko.observable("AddEmployee");
    this.employeeIsSelectedToEdit = ko.observable(false);
    this.selectedEmployees = ko.observableArray(null);
    this.selectedEmployee = ko.observable(null);

   

    //History
    this.serviceDate = ko.observable(null);
    this.vehicleHistories = null;
    this.vehicleHistoriesByVin = ko.observableArray(null);
    this.vehicleHistory = null;
    this.vehicleHistoryIndex = null;
    this.vehicleHistoryInterventions = ko.observableArray(null);
    this.vehicleHistoryServiceCompany = ko.observable(null);
    this.vehicleHistoryWorkingPoint = ko.observable(null);
    this.vehicleHistoryCurrentKm = ko.observable(null);
    this.vehicleHistoryNextVisitDate = ko.observable(null);
    this.vehicleHistoryNextVisitKm = ko.observable(null);
    this.vehicleHistoryEmployees = ko.observableArray(null);
    this.vehicleVin = ko.observable("");


    //// METHODS \\\\


    //Initialize
    this.initialize = function (data) {
        if (data.VehicleServiceCompanies != null) {
            var vehicleServiceCompanies = _.map(data.VehicleServiceCompanies, function (vehicleServiceCompanies, index) {
                return new VehicleServiceCompany(vehicleServiceCompanies);
            });
            _self.vehicleServiceCompanies(vehicleServiceCompanies);
        }

        for (var i = 2017; i <= (new Date()).getFullYear() ;i++)
        _self.years.push(i);
        
        
    }
 

    //Company 
    this.deletCompany=function(data)
    {
        if (confirm("Are you sure you want to delet?") == true) {
            $.ajax({
                type: "POST",
                url: "/Service/DeletCompany/",
                data: {
                    ID: data.id
                },
                success: function (msg) {
                     _self.vehicleServiceCompanies.remove(data);
                },
                dataType: "json"
            });
        }
    }
    this.addCompany=function()
    {
        _self.companyNameError("");
        if (_self.serviceName() == null || _self.serviceName()=="")
        {
            _self.companyNameError("*Your company name is incorrect!");
        }
        if (_self.companyNameError() == "") {
            $.ajax({
                type: "POST",
                url: "/Service/AddCompany/",
                data: {

                    ServiceName: _self.serviceName
                },
                success: function (msg) {  
                    if (msg.success == true) {
                        _self.vehicleServiceCompanies.push(new VehicleServiceCompany(msg.VehicleServiceCompany));
                        _self.serviceName("");
                        _self.companyNameError("");
                    }
                    else
                    {
                        _self.companyNameError(msg.messages);
                    }
                },
                dataType: "json"
            });
        }
    }
    this.editCompany = function (data) {
        $("#EditCompany").modal("show");
        _self.editServiceName(data.serviceName());
        _self.selectedToEditCompany.id = data.id;
    }
    this.saveCompany = function () {
        $.ajax({
            type: "POST",
            url: "/Service/EditServiceCompany/",
            data: {
                ID: _self.selectedToEditCompany.id,
                ServiceName: _self.editServiceName
            },
            success: function (msg) {
                if (msg.success == true) {
                    $("#EditCompany").modal("hide");
                    _self.vehicleServiceCompanies().forEach(function (element) {
                        if (element.id == _self.selectedToEditCompany.id) {
                            element.serviceName(_self.editServiceName());
                        }
                    });
                    _self.editCompanyError("");
                }
                else
                {
                    _self.editCompanyError(msg.messages)
                }
            },
            dataType: "json"
        });

    }
    this.selectCompany = function (data) {
        _self.vehicleServiceCompanyIsSelected(true);
        $('.nav-tabs a[href="#menu1"]').tab('show');
        _self.selectedCompany = data;
        _self.workingPoints(data.workingPoints());
    }

   // WorkingPoint
    this.openaddWorkingPointModal=function()
    {
        _self.newWorkingPoint.country("");
        _self.newWorkingPoint.city("");
        _self.newWorkingPoint.nr("");
        _self.newWorkingPoint.street("");
        _self.workingPointIsASelectedToEdit(false);
    }
    this.addWorkingPoint = function ()
    {
        _self.addWorkingPointError("");
        if (_self.newWorkingPoint.country() == "") {
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the country field" + " <br /> ");
        }
        if (_self.newWorkingPoint.city() == "") {
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the city field" + " <br /> ");
        }
        if (_self.newWorkingPoint.street() == "")
        {
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the street field"+ " <br /> ");
        }
        if (_self.newWorkingPoint.nr() == "") {
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the nr field" + " <br /> ");
        }
        if (_self.addWorkingPointError() == "") {
            $.ajax({
                type: "POST",
                url: "/Service/AddWorkingPoint/",
                data: {
                    Street: _self.newWorkingPoint.street,
                    City: _self.newWorkingPoint.city,
                    Country: _self.newWorkingPoint.country,
                    Nr: _self.newWorkingPoint.nr,
                    VSCID: _self.selectedCompany.id
                },
                success: function (msg) {
                    if (msg.success == true) {
                        var workingPoint = new WorkingPoint(msg.WorkingPoint)
                        _self.workingPoints.push(workingPoint);
                        _self.vehicleServiceCompanies().forEach(function (element) {
                            if (element.id == _self.selectedCompany.id) {
                                element.workingPoints(_self.workingPoints());
                            }
                        });
                        _self.addWorkingPointError("");
                        $("#AddWorkingPoint").modal("hide");
                    }
                    else
                    {
                        _self.addWorkingPointError(msg.messages);
                    }
                },
                dataType: "json"
            });
        }
    }
    this.selectWorkingPoint=function(data)
    {
        _self.workingPointIsSelected(true);
        _self.vehicles([]);
                $.ajax({
                type: "POST",
                url: "/Service/SelectWorkingPoint/",
                data: {
                         ID:data.id
                },
                success: function (msg) {
                if(msg.success==true)
                    {
                    _self.selectedWorkingPoint = data;
                    _self.vehicles(data.services());
                    $('.nav-tabs a[href="#menu2"]').tab('show');
                    }
                },
                dataType: "json"
            });             
    }
    this.deletWorkingPoint = function (data) {
        if (confirm("Are you sure you want to delet?") == true) {
            $.ajax({
                type: "POST",
                url: "/Service/DeletWorkingPoint/",
                data: {
                    ID: data.id
                },
                success: function (msg) {
                    var deletElement;
                    _self.workingPoints.remove(data);
                    _self.vehicleServiceCompanies().forEach(function (element) {
                        if (element.id == _self.selectedCompany.id) {
                            element.workingPoints(_self.workingPoints());
                        }
                    });
                },
                dataType: "json"
            });
        }
    }
    this.editWorkingPoint = function (data) {
        _self.newWorkingPoint.country(data.country());
        _self.newWorkingPoint.city(data.city());
        _self.newWorkingPoint.nr(data.nr());
        _self.newWorkingPoint.street(data.street());
        _self.newWorkingPoint.id=data.id;
        _self.workingPointIsASelectedToEdit(true);
        _self.addWorkingPointError("");
    }
    this.saveEditWorkingPoint = function () {
        _self.addWorkingPointError("");
        var errors = 0;
        if (_self.newWorkingPoint.country() == "") {
            errors++;
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the country field" + " <br /> ");
        }
        if (_self.newWorkingPoint.city() == "") {
            errors++;
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the city field" + " <br /> ");
        }
        if (_self.newWorkingPoint.street() == "") {
            errors++;
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the street field" + " <br /> ");
        }
        if (_self.newWorkingPoint.nr() == "") {
            errors++;
            _self.addWorkingPointError(_self.addWorkingPointError() + "*Required to enter the nr field" + " <br /> ");
        }
        if (errors == 0) {
            $.ajax({
                type: "POST",
                url: "/Service/EditWorkingPoint/",
                data: {
                    ID: _self.newWorkingPoint.id,
                    Street: _self.newWorkingPoint.street,
                    City: _self.newWorkingPoint.city,
                    Country: _self.newWorkingPoint.country,
                    Nr: _self.newWorkingPoint.nr
                },
                success: function (msg) {
                    var workingPoint = new WorkingPoint(msg.WorkingPoint)
                    _self.workingPoints().forEach(function (element) {
                        if (element.id == _self.newWorkingPoint.id) {
                            element.street(_self.newWorkingPoint.street());
                            element.country(_self.newWorkingPoint.country());
                            element.city(_self.newWorkingPoint.city());
                            element.nr(_self.newWorkingPoint.nr());
                        }
                    });
                    _self.vehicleServiceCompanies().forEach(function (element) {
                        if (element.id == _self.selectedCompany.id) {
                            element.workingPoints(_self.workingPoints());
                        }
                    });
                    $("#AddWorkingPoint").modal("hide");
                },
                dataType: "json"
            });
        }
    }
    this.workingPointEmployees = function(data)
    {
        $('.nav-tabs a[href="#menu6"]').tab('show');
        _self.employeAddAndEditTab("AddEmployee");
        _self.employeeIsSelectedToEdit(false);
        _self.selectedWorkingPointToEmployees = data.id;
        _self.employees(data.employees());
    }

    //Interventions
    this.editInterventions = function (data) {
        _self.addInterventionError("");
        _self.editInterventionsSelected = data;
        _self.interventions(data.serviceInterventions());
    }
    this.addIntervention = function () {
       
        if (_self.interventionPrice() == "" || _self.interventionName()=="")
        {
            _self.addInterventionError("Invalid name or price");
        }
        else
        {
            $.ajax({
                type: "POST",
                url: "/Service/AddIntervention/",
                data: {
                    Price: _self.interventionPrice,
                    Name: _self.interventionName,
                    WPID: _self.editInterventionsSelected.id
                },
                success: function (msg) {
                    if (msg.success == true) {
                        _self.interventions.push(new ServiceIntervention(msg.Intervention));
                        _self.vehicleServiceCompanies().forEach(function (element) {
                            if (element.id == _self.selectedCompany.id) {
                                element.workingPoints().forEach(function (elmt) {
                                    if (elmt.id == _self.editInterventionsSelected.id) {
                                        elmt.serviceInterventions(_self.interventions());
                                    }
                                });
                            }
                        });
                        _self.workingPoints().forEach(function (element) {
                            if (element.id == _self.editInterventionsSelected.id) {
                                element.serviceInterventions(_self.interventions());
                            }
                        });
                        _self.addInterventionError("");
                        _self.interventionName("");
                        _self.interventionPrice("");
                    }
                    else
                    {

                        _self.addInterventionError(msg.messages);
                    }
                },
                dataType: "json"
            });
        }
       
    }
    this.deletIntervention = function (data) {
        $.ajax({
            type: "POST",
            url: "/Service/DeletIntervention/",
            data: {
                ID: data.id,
                WP: _self.editInterventionsSelected.id
            },
            success: function (msg) {
                var deletElement;
                _self.interventions().forEach(function (element) {
                    if (element.id == data.id) {
                        deletElement = element;
                    }
                });
                _self.interventions.remove(deletElement);
                _self.vehicleServiceCompanies().forEach(function (element) {
                    if (element.id == _self.selectedCompany.id) {
                        element.workingPoints().forEach(function (elmt) {
                            if (elmt.id == _self.editInterventionsSelected.id) {
                                elmt.serviceInterventions(_self.interventions());
                            }
                        });
                    }
                });
                _self.workingPoints().forEach(function (element) {
                    if (element.id == _self.editInterventionsSelected.id) {
                        element.serviceInterventions(_self.interventions());
                    }
                });
            },
            dataType: "json"
        });
    }
    this.editIntervention=function(data)
    {
        _self.interventionName(data.name);
        _self.interventionPrice(data.price);
        _self.interventionId = data.id;
        _self.editInterventionIsSelected(true);
    }
    this.saveEditIntervention = function (data) {
        if (_self.interventionPrice() == "" || _self.interventionName() == "") {
            _self.addInterventionError("Invalid name or price");
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Service/EditIntervention/",
                data: {
                    Price: _self.interventionPrice,
                    Name: _self.interventionName,
                    ID: _self.interventionId
                },
                success: function (msg) {
                    if (msg.success == true) {
                        _self.interventions().forEach(function (element) {
                            if (element.id == _self.interventionId) {
                                element.name = _self.interventionName();
                                element.price = _self.interventionPrice();
                            }
                        });
                        var interventions = _self.interventions();
                        _self.interventions([]);
                        _self.interventions(interventions);
                        _self.vehicleServiceCompanies().forEach(function (element) {
                            if (element.id == _self.selectedCompany.id) {
                                element.workingPoints().forEach(function (elmt) {
                                    if (elmt.id == _self.editInterventionsSelected.id) {
                                        elmt.serviceInterventions(_self.interventions());
                                    }
                                });
                            }
                        });
                        _self.workingPoints().forEach(function (element) {
                            if (element.id == _self.editInterventionsSelected.id) {
                                element.serviceInterventions(_self.interventions());
                            }
                        });
                        _self.editInterventionIsSelected(false);
                        _self.interventionName("");
                        _self.interventionPrice("");
                    }
                    else
                    {
                        _self.addInterventionError(msg.messages);
                    }
                },
                dataType: "json"
            });
        }
    }
    
    //Service
    this.openAddServiceModal=function()
    {
        _self.inputVehicleError("");
        _self.VIN("");
        _self.identifier(""),
        _self.vehicleOwnerFirstName("");
        _self.vehicleOwnerLastName("");
        _self.vehicleOwnerEmail("");
        _self.vehicleOwnerPhoneNumber("");
        _self.vehicleIsSelectedToEdit(false);
    }
    this.addService = function ()
    {
        var errors = 0;
        _self.inputVehicleError("");

        if (_self.VIN() == "" || _self.identifier()=="" ||_self.vehicleOwnerFirstName()=="" ||_self.vehicleOwnerLastName()=="" || _self.vehicleOwnerPhoneNumber()=="" || _self.vehicleOwnerEmail()=="")
        {
            errors++;
            _self.inputVehicleError("All input is required");
        }
        if (errors == 0) {
            $.ajax({
                type: "POST",
                url: "/Service/AddService/",
                data: {
                    VIN: _self.VIN,
                    Identifier: _self.identifier,
                    VehicleOwner: {
                        FirstName: _self.vehicleOwnerFirstName,
                        LastName: _self.vehicleOwnerLastName,
                        Email: _self.vehicleOwnerEmail,
                        PhoneNumber: _self.vehicleOwnerPhoneNumber,
                    }
                },
                success: function (msg) {
                    if (msg.success == true) {
                        _self.vehicles.push(new Service(msg.Service));
                        $("#inputServiceModal").modal("hide");
                    }
                },
                dataType: "json"
            });
        }
    }
    this.deletService = function (data)
    {
        if (confirm("Are you sure you want to delet?") == true) {
            $.ajax({
                type: "POST",
                url: "/Service/DeletService/",
                data: {
                    ID: data.id
                },
                success: function (msg) {
                _self.vehicles.remove(data);
                },
                dataType: "json"
            });
        }
    }
    this.endedService = function (data) {
        $.ajax({
            type: "POST",
            url: "/Service/EndedService/",
            data: {
                ID: data.id,
                NextVisitKm: data.nextVisitKm,
                Price: data.price,
                NextVisitDate: data.nextVisitDate,
                CurrentKm: data.currentKm,
                ServiceInterventions: data.serviceInterventions(),
                Vehicle: {
                    Identifier: data.vehicle.identifier,
                    VehicleOwner: {
                        Email: data.vehicle.vehicleOwner.email,
                        PhoneNumber: data.vehicle.vehicleOwner.phoneNumber,
                        FirstName: data.vehicle.vehicleOwner.firstName,
                        LastName: data.vehicle.vehicleOwner.lastName
                    }
                }  
            },
            success: function (msg) {
                if (msg.success == true) {
                    _self.vehicles.remove(data);
                }
            },
            dataType: "json"
        });
    }
    this.editService = function (data) {
        _self.vehicleIdToEdit=data.id;
        _self.VIN(data.vehicle.vin);
        _self.identifier(data.vehicle.identifier),
        _self.vehicleOwnerFirstName(data.vehicle.vehicleOwner.firstName);
        _self.vehicleOwnerLastName(data.vehicle.vehicleOwner.lastName);
        _self.vehicleOwnerEmail(data.vehicle.vehicleOwner.email);
        _self.vehicleOwnerPhoneNumber(data.vehicle.vehicleOwner.phoneNumber);
        _self.vehicleIsSelectedToEdit(true);
        _self.inputVehicleError("");

    }
    this.saveEditService=function(data)
    {
        var errors = 0;
        _self.inputVehicleError("");

        if (_self.VIN() == "" || _self.identifier()=="" ||_self.vehicleOwnerFirstName()=="" ||_self.vehicleOwnerLastName()=="" || _self.vehicleOwnerPhoneNumber()=="" || _self.vehicleOwnerEmail()=="")
        {
            errors++;
            _self.inputVehicleError("All input is required");
        }
        if (errors == 0) {
            $.ajax({
                type: "POST",
                url: "/Service/EditService/",
                data: {
                    ID: _self.vehicleIdToEdit,
                    Vehicle: {
                        VIN: _self.VIN,
                        Identifier: _self.identifier,
                        VehicleOwner: {
                            FirstName: _self.vehicleOwnerFirstName,
                            LastName: _self.vehicleOwnerLastName,
                            Email: _self.vehicleOwnerEmail,
                            PhoneNumber: _self.vehicleOwnerPhoneNumber
                        }
                    }
                },
                success: function (msg) {
                    if (msg.success == true) {
                        _self.vehicles().forEach(function (element) {
                            if (element.id == _self.vehicleIdToEdit) {
                                element.vehicle.vin = _self.VIN();
                                element.vehicle.identifier = _self.identifier();
                                element.vehicle.vehicleOwner.firstName = _self.vehicleOwnerFirstName();
                                element.vehicle.vehicleOwner.lastName = _self.vehicleOwnerLastName();
                                element.vehicle.vehicleOwner.email = _self.vehicleOwnerEmail();
                                element.vehicle.vehicleOwner.phoneNumber = _self.vehicleOwnerPhoneNumber();
                            }
                        });
                        $("#inputServiceModal").modal("hide");
                    }
                },
                dataType: "json"
            });
        }
    }


    //Employee
    this.addEmployee = function () {
        
        if (_self.employeeFirstName() == "" || _self.employeeLastName() == "" || _self.employeeEmail() == "" || _self.employeePhoneNumber() == "") {
            _self.addEmployeeError("Invalid input");
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Service/AddEmployee",
                data: {
                    FirstName: _self.employeeFirstName,
                    LastName: _self.employeeLastName,
                    Email: _self.employeeEmail,
                    PhoneNumber: _self.employeePhoneNumber,
                    WPID: _self.selectedWorkingPointToEmployees

                },
                success: function (msg) {
                    _self.addEmployeeError("");
                    if (msg.success == true)
                    {
                        _self.employees.push(new Employee(msg.Employee));
                        _self.vehicleServiceCompanies().forEach(function (element) {
                            if (element.id == _self.selectedCompany.id) {
                                element.workingPoints().forEach(function (elmt) {
                                    if (elmt.id == msg.Employee.WPID) {
                                        elmt.employees(_self.employees());
                                    }
                                });
                            }
                        });
                        $('.nav-tabs a[href="#menu6"]').tab('show');
                    }
                    else
                    {
                        _self.addEmployeeError(msg.messages);
                    }
              
                },
                dataType: "json"
            });
        }
    }
    this.openAddEmployeTab = function () {
        _self.addEmployeeError("");
        _self.employeeFirstName("");
        _self.employeeLastName("");
        _self.employeeEmail("");
        _self.employeePhoneNumber("");
        _self.employeAddAndEditTab("AddEmployee");
        _self.employeeIsSelectedToEdit(false);
        $('.nav-tabs a[href="#menu7"]').tab('show');
    }
    this.openEmployeesTab = function () {
        _self.employeAddAndEditTab("AddEmployees");
        _self.employeeIsSelectedToEdit(false);
    }
    this.deletEmployee = function (data) {
        if (confirm("Are you sure you want to delet?") == true) {
            $.ajax({
                type: "POST",
                url: "/Service/DeletEmployee",
                data: {
                    ID: data.id

                },
                success: function (msg) {
                    var deletEmployee = null;
                    _self.employees().forEach(function (element) {
                        if (element.id == data.id) {
                            deletEmployee = element;
                        }
                    });
                    if (deletEmployee != null) {
                        _self.employees.remove(deletEmployee);
                    }
                },
                dataType: "json"
            });
        }
    }
    this.editEmployee = function (data) {
        _self.employeeFirstName(data.firstName);
        _self.employeeLastName(data.lastName);
        _self.employeeEmail(data.email);
        _self.employeePhoneNumber(data.phoneNumber);
        _self.employeeId = data.id;
        $('.nav-tabs a[href="#menu7"]').tab('show');
        _self.employeeIsSelectedToEdit(true);
        _self.employeAddAndEditTab("EditEmployee");
    }
    this.saveEditEmployee = function (data) {
        _self.addEmployeeError("");
        if (_self.employeeFirstName() == "" || _self.employeeLastName() == "" || _self.employeeEmail() == "" || _self.employeePhoneNumber() == "") {
            _self.addEmployeeError("Invalid input");
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Service/EditEmployee",
                data: {
                    FirstName: _self.employeeFirstName,
                    LastName: _self.employeeLastName,
                    Email: _self.employeeEmail,
                    PhoneNumber: _self.employeePhoneNumber,
                    ID: _self.employeeId
                },
                success: function (msg) {
                    if (msg.success == true)
                    {
                        _self.employees().forEach(function (element) {
                            if (element.id == _self.employeeId) {
                                element.firstName = _self.employeeFirstName();
                                element.lastName = _self.employeeLastName();
                                element.phoneNumber = _self.employeePhoneNumber();
                                element.email = _self.employeeEmail();
                            }
                        });
                        var employees = _self.employees();
                        _self.employees([]);
                        _self.employees(employees);
                        $('.nav-tabs a[href="#menu6"]').tab('show');
                    }
                    else
                    {
                        _self.addEmployeeError(msg.messages);
                    }
                  
                   
                },
                dataType: "json"
            });
        }
    }
    this.addEmployeeToSelectedEmployees = function () {
        var exist = false;
        _self.selectedEmployees().forEach(function (element) {
            if (element.id == _self.selectedEmployee().id) {
                exist = true;
            }
        });
        if (exist == false) {
            _self.selectedEmployees.push(_self.selectedEmployee());
        }
    }
    this.deletEmployeeFromSelectedEmployees = function (data) {
            _self.selectedEmployees.remove(data);
     }

    //Repairs
    this.addRepairs = function (data) {
        document.getElementById("date").value =data.nextVisitDate();
        _self.interventions(_self.selectedWorkingPoint.serviceInterventions());
        _self.employees(_self.selectedWorkingPoint.employees());
        _self.selectedInterventions(data.serviceInterventions());
        _self.selectedEmployees(data.employees());
        _self.selectedVehicle.price(data.price());
        _self.selectedVehicle.id=data.id;
        _self.selectedVehicle.nextVisitKm(data.nextVisitKm());
        _self.selectedVehicle.currentKm(data.currentKm());
        _self.selectedVehicle.nextVisitDate(data.nextVisitDate());
        _self.selectedVehicleCurrentKm = data.currentKm();
    }
    this.addSelectedInterventionToRepairs = function ()
    {
        var exist = false;
        _self.selectedInterventions().forEach(function (element) {
            if(element.id==_self.selectedIntervention().id)
            {
                exist = true;
            }
        });
        if (exist == false) {
            _self.selectedInterventions.push(_self.selectedIntervention());
        }
    }
    this.saveRepairs = function () {
        _self.saveRepairError("");
        var errors = 0;
        if (_self.selectedVehicleCurrentKm > _self.selectedVehicle.currentKm())
        {
            _self.saveRepairError(_self.saveRepairError() + "Current Km is smaller them last service Km <br>");
            errors++;
        }
        if (_self.selectedVehicle.currentKm() > _self.selectedVehicle.nextVisitKm())
        {
            _self.saveRepairError(_self.saveRepairError() + "Current Km is smaller them next visit Km <br>");
            errors++;
        }
        var today = new Date();
        if( today !== _self.selectedVehicle.nextVisitDate())
        {
            _self.saveRepairError(_self.saveRepairError() + "The next visit date is smaller them today");
            errors++;
        }
        if (errors == 0) {
            $.ajax({
                type: "POST",
                url: "/Service/SaveRepairs/",
                data: {
                    ID: _self.selectedVehicle.id,
                    ServiceInterventions: _self.selectedInterventions(),
                    CurrentKm: _self.selectedVehicle.currentKm,
                    NextVisitKm: _self.selectedVehicle.nextVisitKm,
                    NextVisitDate: _self.selectedVehicle.nextVisitDate(),
                    Employees: _self.selectedEmployees()
                },
                success: function (msg) {
                    _self.vehicles().forEach(function (element) {
                        if (element.id == _self.selectedVehicle.id) {
                            element.price(msg.Price);
                            element.nextVisitDate(_self.selectedVehicle.nextVisitDate());
                            element.nextVisitKm(_self.selectedVehicle.nextVisitKm());
                            element.currentKm(_self.selectedVehicle.currentKm());
                            element.employees(_self.selectedEmployees());
                            element.serviceInterventions(_self.selectedInterventions());
                        }
                    });
                    $("#inputRepairsModal").modal("hide");
                },
                dataType: "json"
            });
        }
    }
    this.deletFromSelectedInterventions = function (data) {
            _self.selectedInterventions.remove(data);          
    }

    //History
    this.getVehicleHistoryByVin = function () {
        $.ajax({
            type: "POST",
            url: "/Service/GetVehicleHistory/",
            data: {
                Vehicle: {
                    VIN: _self.vehicleVin
                }

            },
            success: function (msg) {
                if (msg.success == true) {
                    var vehicleHistories = _.map(msg.VehicleHistory, function (vehicleHistory, index) {
                        return new Service(vehicleHistory);
                    });
                    _self.vehicleHistoryInterventions([]);
                    _self.vehicleHistoriesByVin(vehicleHistories);
                    _self.vehicleHistories = vehicleHistories;
                    _self.vehicleHistoryIndex = vehicleHistories.length - 1;
                    _self.setVehicleHistory();
                }
            },
            dataType: "json"
        });
    }
    this.getVehicleHistory = function (data) {
        $.ajax({
            type: "POST",
            url: "/Service/GetVehicleHistory/",
            data: {
                Vehicle:{
                    ID: data.vehicle.id
                }
                                
            },
            success: function (msg) {  
                if(msg.success==true)
                {
                    var vehicleHistories = _.map(msg.VehicleHistory, function (vehicleHistory, index) {
                            return new Service(vehicleHistory);
                        });
                    _self.vehicleHistoryInterventions([]);
                    _self.vehicleHistories = vehicleHistories;
                    _self.vehicleHistoryIndex = vehicleHistories.length - 1;
                    _self.setVehicleHistory();
                }
            },
            dataType: "json"
        });
    }
    this.setVehicleHistory = function () {
        _self.vehicleHistoryInterventions([]);
        _self.vehicleHistoryEmployees([]);
        _self.vehicleHistory=_self.vehicleHistories[_self.vehicleHistoryIndex];
        _self.serviceDate(_self.vehicleHistory.serviceDate);
        _self.vehicleHistoryCurrentKm(_self.vehicleHistory.currentKm());
        _self.vehicleHistoryNextVisitDate(_self.vehicleHistory.nextVisitDate());
        _self.vehicleHistoryNextVisitKm(_self.vehicleHistory.nextVisitKm());
        _self.vehicleHistoryInterventions(_self.vehicleHistory.serviceInterventions());
        _self.vehicleHistoryEmployees(_self.vehicleHistory.employees());
        _self.vehicleHistoryWorkingPoint(_self.vehicleHistory.workingPoint());
        _self.vehicleHistoryServiceCompany("Service Name: " + _self.vehicleHistory.companyName());
      
       
    }
    this.firstHistory = function () {
        _self.vehicleHistoryIndex = 0
        _self.setVehicleHistory();
    }
    this.lastHistory = function () {
        _self.vehicleHistoryIndex = _self.vehicleHistories.length - 1;
        _self.setVehicleHistory();
    }
    this.nextHistory = function () {    
        if (_self.vehicleHistoryIndex < _self.vehicleHistories.length - 1) {       
            _self.vehicleHistoryIndex++;
            _self.setVehicleHistory();
        }
    }
    this.previousHistory=function(){
        if (_self.vehicleHistoryIndex > 0) { 
            _self.vehicleHistoryIndex--;
            _self.setVehicleHistory();
        }
    }

    this.getYearGraphy=function()
    {
       

        $.ajax({
            type: "POST",
            url: "/Service/GetYearly/",
            data: {
                id:_self.selectedWorkingPoint.id,
                year:_self.selectedYear()

            },
            success: function (msg) {
                if (msg.success == true) {
                    var labels = [];
                    var values = [];
                    msg.Statistic.forEach(function (element) {
                        labels.push(element.Name);
                        values.push(element.Value);
                    });
                    var title = _self.selectedYear()+" WorkingPoint"
                    _self.loadGraph(values, labels,title);
                }
            },
            dataType: "json"
        });
    }

    this.getMonthGraphy = function () {
       
        var i = 0;
        var selectedMonth = 0;
        _self.months().forEach(function (element)
        {
            i++;
            if (element == _self.selectedMonth()) {
                selectedMonth = i;
            }
        });
     
        $.ajax({
            type: "POST",
            url: "/Service/GetMonthly/",
            data: {
                id: _self.selectedWorkingPoint.id,
                year: _self.selectedYear(),
                month: selectedMonth 

            },
            success: function (msg) {
                if (msg.success == true) {
                    var labels = [];
                    var values = [];
                    var j = 0;
                    msg.Statistic.forEach(function (element) {
                      
                        if (j == 3) {
                            labels.push(element.Name);
                            j = 0;
                        }
                        j++;
                        
                         values.push(element.Value);
                        
                       
                    });
                    var title = _self.selectedYear() +" "+ _self.selectedMonth() + " WorkingPoint"
                    _self.loadGraph1(values, labels, title);
                }
            },
            dataType: "json"
        });
    }

    this.loadGraph = function (data,labels,title)
    {
        $('#chart-container').remove();
        $('#graphContent').append('<div style="width: 750px; height: 300px" id="chart-container"></div>');

        _self.t=new RGraph.SVG.Bar({
            id: 'chart-container',
            data: data,
            options: {
                hmargin: 20,
                xaxisLabels: labels,
                tooltips: labels,
                title: title,
                colors: ['Gradient(#f00:#fcc)'],
                yaxis: false,
                yaxisUnitsPost: 'E',
                backgroundGridVlines: false,
                backgroundGridBorder: false
            }
        }).grow();
    }

    this.loadGraph1 = function (data, labels, title)
    {
        $('#chart-container').remove();
        $('#graphContent').append('<div style="width: 750px; height: 300px" id="chart-container"></div>');
        document.getElementById('chart-container').innerHTML = " ";
        _self.t=new RGraph.SVG.Line({
            id: 'chart-container',
            data: data,
            options: {
                linewidth: 2,
                xaxisLabels: labels,
                gutterLeft: 65,
                yaxisUnitsPost: 'E',
                
                gutterRight: 55,
                filled: true,
                filledColors: ['#C2D1F0'],
                filledClick: function (e, obj) {
                    alert('The fill was clicked!');
                },
                colors: ['#3366CB'],
                yaxis: false,
                xaxis: false,
                backgroundGridVlines: false,
                backgroundGridBorder: false,
                textSize: 16
            }
        }).trace()
    }
}
function InitializeMainModel(data) {
    MainModel.instance = new MainModel();
    MainModel.instance.initialize(data);
    ko.applyBindings(MainModel.instance);
}