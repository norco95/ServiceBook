function MainModel()
{   
    var _self = this;


    //// ATRIBUTES \\\\

    //Error
    this.companyNameError = ko.observable("");
    this.editCompanyError = ko.observable("");
    this.addWorkingPointError = ko.observable("");


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
    this.interventionName = ko.observable(null);
    this.interventionPrice = ko.observable(null);


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


    //Employee 
    this.employeeId = null;
    this.employeeFirstName = ko.observable(null);
    this.employeeLastName = ko.observable(null);
    this.employeeEmail = ko.observable(null);
    this.employeePhoneNumber = ko.observable(null);
    this.employees = ko.observableArray(null);
    this.selectedWorkingPointToEmployees = null;
    this.employeAddAndEditTab = ko.observable("AddEmployee");
    this.employeeIsSelectedToEdit = ko.observable(false);
    this.selectedEmployees = ko.observableArray(null);
    this.selectedEmployee = ko.observable(null);

   
    //History
    this.serviceDate = ko.observable(null);
    this.vehicleHistories = null;
    this.vehicleHistory = null;
    this.vehicleHistoryIndex = null;
    this.vehicleHistoryInterventions = ko.observableArray(null);
    this.vehicleHistoryServiceCompany = ko.observable(null);
    this.vehicleHistoryWorkingPoint = ko.observable(null);
    this.vehicleHistoryCurrentKm = ko.observable(null);
    this.vehicleHistoryNextVisitDate = ko.observable(null);
    this.vehicleHistoryNextVisitKm = ko.observable(null);
    this.vehicleHistoryEmployees = ko.observableArray(null);


    //// METHODS \\\\


    //Initialize
    this.initialize = function (data) {
       
        if (data.VehicleServiceCompanies != null) {
            var vehicleServiceCompanies = _.map(data.VehicleServiceCompanies, function (vehicleServiceCompanies, index) {
                return new VehicleServiceCompany(vehicleServiceCompanies);

            });
            _self.vehicleServiceCompanies(vehicleServiceCompanies);
        }
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
                  //  var vehicleServiceCompanies = _.map(msg.VehicleServiceCompanies, function (vehicleServiceCompanies, index) {
                    //    return new VehicleServiceCompany(vehicleServiceCompanies);

//                    });
                    //                  _self.vehicleServiceCompanies(vehicleServiceCompanies);

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


    //WorkingPoint
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

                     //   var vehicles = _.map(msg.Vehicles, function (vehicle, index) {
                      //      return new Service(vehicle);

                        //                        });
                        
                       // _self.vehicles(vehicles);
                        _self.selectedWorkingPoint = data;
                        _self.selectedWorkingPoint.sw.forEach(function (element) {
                           // if (element.service.flag == 0) {
                                _self.vehicles.push(element.service);
                           // }
                        });

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
    }
    this.saveEditWorkingPoint = function () {
        
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
                //var _self.workingPoints = _.map(msg.VehicleServiceCompanies, function (vehicleServiceCompanies, index) {
                //    return new VehicleServiceCompany(vehicleServiceCompanies);

                //});
                //vehicleServiceCompanies.forEach(function (element) {
                //    if(element.id==_self.selectedCompany.id)
                //    {
                //        _self.workingPoints(element.workingPoints());
                //    }
                //});
                //_self.vehicleServiceCompanies(vehicleServiceCompanies);
                var workingPoint = new WorkingPoint(msg.WorkingPoint)
                _self.workingPoints().forEach(function (element) {
                    if (element.id == _self.newWorkingPoint.id)
                    {
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
        _self.editInterventionsSelected = data;
        _self.interventions(data.serviceInterventions());
    }
    this.addIntervention = function () {
        $.ajax({
            type: "POST",
            url: "/Service/AddIntervention/",
            data: {

                Price: _self.interventionPrice,
                Name: _self.interventionName,
                WorkingPoint: {
                    ID: _self.editInterventionsSelected.id
                }
            },

            success: function (msg) {
                if (msg.success == true) {
                    _self.interventions.push(new ServiceIntervention(msg.Intervention));
                }
                
                

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
                    if (element.id == _self.editInterventionsSelected.id)
                    {
                        element.serviceInterventions(_self.interventions());
                    }
                });

                _self.interventionName("");
                _self.interventionPrice("");

            },
            dataType: "json"
        });
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
                        if(element.id==_self.interventionId)
                        {
                            element.name = _self.interventionName();
                            element.price = _self.interventionPrice();
                        }
                    });
                }

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

            },
            dataType: "json"
        });
    }
    
    //Service
    this.openAddServiceModal=function()
    {
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
                if(msg.success==true)
                {
                    //var vehicles = _.map(msg.Vehicles, function (vehicle, index) {
                    //    return new Service(vehicle);

                    //});
                    //_self.vehicles(vehicles);

                    _self.vehicleServiceCompanies().forEach(function (element) {
                        if(element.id==_self.selectedCompany.id)
                        {
                            element.workingPoints().forEach(function (elmt) {
                                if(elmt.id==_self.selectedWorkingPoint.id)
                                {
                                    var sw = new SW(msg.SW);
                                    elmt.sw.push(sw);
                                }
                            });
                            
                        }
                    });
                    _self.vehicles.push(new Service(msg.SW.Service));
                    $("#inputServiceModal").modal("hide");
                }

            },
            dataType: "json"
        });
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
                    //var vehicles = _.map(msg.Vehicles, function (vehicle, index) {
                    //    return new Service(vehicle);

                    //});
                    //_self.vehicles(vehicles);
                    var swToDelet = null;
                    var i=0;
                    _self.vehicles.remove(data);
                    _self.vehicleServiceCompanies().forEach(function (element) {
                        if (element.id == _self.selectedCompany.id) {
                            element.workingPoints().forEach(function (elmt) {
                                swToDelet = null;
                                i = 0;
                                if (elmt.id == _self.selectedWorkingPoint.id) {
                                    elmt.sw.forEach(function (elm) {

                                        if (elm.service.id == data.id) {
                                            swToDelet = i;
                                        }
                                        i++;
                                    });
                                    if(swToDelet!=null)
                                    {
                                        elmt.sw.splice(swToDelet, 1);
                                    }
                                }
                            });

                        }
                    });

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
                SSI: data.ssi(),
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
                    //    var vehicles = _.map(msg.Vehicles, function (vehicle, index) {
                    //        return new Service(vehicle);

                    //    });
                    //    _self.vehicles(vehicles);
                    _self.vehicles.remove(data);
                    var swToDelet = null;
                    var i = 0;
                    _self.vehicles.remove(data);
                    _self.vehicleServiceCompanies().forEach(function (element) {
                        if (element.id == _self.selectedCompany.id) {
                            element.workingPoints().forEach(function (elmt) {
                                swToDelet = null;
                                i = 0;
                                if (elmt.id == _self.selectedWorkingPoint.id) {
                                    elmt.sw.forEach(function (elm) {

                                        if (elm.service.id == data.id) {
                                            swToDelet = i;
                                        }
                                        i++;
                                    });
                                    if (swToDelet != null) {
                                        elmt.sw.splice(swToDelet, 1);
                                    }
                                }
                            });

                        }
                    });


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
    }
    this.saveEditService=function(data)
    {
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
                    //var vehicles = _.map(msg.Vehicles, function (vehicle, index) {
                    //    return new Service(vehicle);

                    //});
                    //_self.vehicles(vehicles);
                    
                    _self.vehicleServiceCompanies().forEach(function (element) {
                        if (element.id == _self.selectedCompany.id) {
                            element.workingPoints().forEach(function (elmt) {
                                if (elmt.id == _self.selectedWorkingPoint.id) {
                                    elmt.sw.forEach(function (e) {
                                        if(e.service.id==_self.vehicleIdToEdit)
                                        {
                                            e.service.vehicle.vin = _self.VIN();
                                            e.service.vehicle.identifier = _self.identifier();
                                            e.service.vehicle.vehicleOwner.firstName = _self.vehicleOwnerFirstName();
                                            e.service.vehicle.vehicleOwner.lastName = _self.vehicleOwnerLastName();
                                            e.service.vehicle.vehicleOwner.email = _self.vehicleOwnerEmail();
                                            e.service.vehicle.vehicleOwner.phoneNumber = _self.vehicleOwnerPhoneNumber();
                                        }
                                    });
                                }
                            });

                        }
                    });
                    var vehicleServiceCompanies = _self.vehicleServiceCompanies();
                    _self.vehicleServiceCompanies([]);
                    _self.vehicleServiceCompanies(vehicleServiceCompanies);
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

                    var vehicles = _self.vehicles();
                    _self.vehicles([]);
                    _self.vehicles(vehicles);
                        $("#inputServiceModal").modal("hide");
                }

            },
            dataType: "json"
        });
    }


    //Employee
    this.addEmployee = function () {


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
                _self.employees.push(new Employee(msg.Employee));
                //_self.vehicleServiceCompanies().forEach(function (element) {
                //    if (element.id == _self.selectedCompany.id) {
                //        element.workingPoints().forEach(function (elmt) {
                //            if(elmt.id==_self.selectedWorkingPoint.id)
                //            {
                //                elmt.employees(_self.employees());
                //            }
                //        });

                //    }
                //});
                $('.nav-tabs a[href="#menu6"]').tab('show');
            },
            dataType: "json"
        });
    }
    this.openAddEmployeTab = function () {
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
            },
            dataType: "json"
        });
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
        _self.interventions(_self.selectedWorkingPoint.serviceInterventions());
        _self.employees(_self.selectedWorkingPoint.employees());
        // _self.selectedVehicle = data;
        _self.selectedInterventions([]);
        data.ssi().forEach(function (element) {
            _self.selectedInterventions.push(element.serviceIntervention)
        });
        _self.selectedEmployees([]);
        data.se.forEach(function (element) {
            _self.selectedEmployees.push(element.employee);
        });
        _self.selectedVehicle.ssi(data.ssi());
        _self.selectedVehicle.price(data.price());
        _self.selectedVehicle.id=data.id;
        _self.selectedVehicle.nextVisitKm(data.nextVisitKm());
        _self.selectedVehicle.currentKm(data.currentKm());
        _self.selectedVehicle.nextVisitDate(data.nextVisitDate());
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

        _self.selectedVehicle.ssi([]);
        _self.selectedInterventions().forEach(function (element) {
            var ssi = new SSI();
            ssi.serviceIntervention = element;
            _self.selectedVehicle.ssi.push(ssi);
        });
        _self.selectedVehicle.se=[];
        _self.selectedEmployees().forEach(function (element) {
            var se= new SE();
            se.employee = element;
            _self.selectedVehicle.se.push(se);
        });
    
        
        $.ajax({
            type: "POST",
            url: "/Service/SaveRepairs/",
            data: {
                ID: _self.selectedVehicle.id,
                SSI: _self.selectedVehicle.ssi(),
                CurrentKm: _self.selectedVehicle.currentKm,
                NextVisitKm: _self.selectedVehicle.nextVisitKm,
                NextVisitDate: _self.selectedVehicle.nextVisitDate(),
                SE: _self.selectedVehicle.se
                
            },

            success: function (msg) {

                //var serviceInterventions = _.map(msg.ServiceInterventions, function (si, index) {
                //    _self.selectedVehicle.price(_self.selectedVehicle.price()+si.Price);
                //    return new ServiceIntervention(si);

                //});
                
                //  _self.vehicles().forEach(function (element) {
                //    if(element.id==_self.selectedVehicle.id)
                //    {
                //        serviceInterventions.forEach(function (elmen) {
                //            var ssi=new SSI();
                //            ssi.serviceIntervention = elmen;
                //            element.ssi.push(ssi);
                           
                //        });
                //        element.nextVisitDate(_self.selectedVehicle.nextVisitDate());
                //        element.nextVisitKm (_self.selectedVehicle.nextVisitKm());
                //        element.currentKm ( _self.selectedVehicle.currentKm());
                //        element.price(_self.selectedVehicle.price());     
                //    }
                //});
               
                _self.vehicles().forEach(function (element) {
                    if (element.id == _self.selectedVehicle.id) {
                        var ssi = _.map(msg.Service.SSI, function (ssi, index) {
                            return new SSI(ssi);

                        });
                        element.ssi(ssi);

                        var se = _.map(msg.Service.SE, function (se, index) {
                            return new SE(se);

                        });
                        element.se = se;
                        element.price(msg.Service.Price);
                        element.nextVisitDate(convertToFormatJs(msg.Service.NextVisitDate));
                        element.nextVisitKm (msg.Service.NextVisitKm);
                        element.currentKm (msg.Service.CurrentKm);
                    }
                });
                $("#inputRepairsModal").modal("hide");

               
            },
            dataType: "json"
        });
    }
    this.deletFromSelectedInterventions = function (data) {
            _self.selectedInterventions.remove(data);          
    }
 

    //History
    this.getVehicleHistory = function (data) {
        $.ajax({
            type: "POST",
            url: "/Service/GetVehicleHistory/",
            data: {
                
                    ID: data.vehicle.id,
                    VIN:data.vehicle.vin
                
              
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
        _self.vehicleHistory.ssi().forEach(function (element) {
            _self.vehicleHistoryInterventions.push(element.serviceIntervention);
        });
        _self.vehicleHistory.se.forEach(function (element) {
            _self.vehicleHistoryEmployees.push(element.employee);
        });

        _self.vehicleHistory.sw.forEach(function (element) {
            _self.vehicleHistoryWorkingPoint(" " + element.workingPoint.country() + ", " + element.workingPoint.city() + ", " + element.workingPoint.street() + ", " + element.workingPoint.nr());
            _self.vehicleHistoryServiceCompany("Service Name: "+element.workingPoint.serviceCompany.serviceName());
        });
       
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


}
function InitializeMainModel(data) {
    MainModel.instance = new MainModel();

    MainModel.instance.initialize(data);

    ko.applyBindings(MainModel.instance);
}