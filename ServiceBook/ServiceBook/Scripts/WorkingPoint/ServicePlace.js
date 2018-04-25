function ServicePlace() {
    var _self = this;
    this.workingPoints = ko.observableArray(null);
    this.reviews = ko.observableArray(null);
    this.id = ko.observable(null);
    this.length = ko.observable(0);
    this.copyWorkingPoints = [];
    this.initialize = function (data) {
        if (data.WorkingPoints != null) {
            var workingPoints = _.map(data.WorkingPoints, function (workingPoint, index) {
                _self.copyWorkingPoints[index] = new WorkingPoint(workingPoint);
                return new WorkingPoint(workingPoint);

            });
            _self.workingPoints(workingPoints);
        }

    }
    
   
    this.getReviews=function(data)
    {
        _self.length(0);
        _self.length(data.reviews.length);
        if (data.id() == _self.id())
        {
            _self.id(null);
        }
        else
        {
            _self.id(data.id());
        }
      
    }
    this.country = ko.observable("");
    this.city = ko.observable("");
    this.search = function()
    {
        _self.workingPoints([]);
        if(_self.country()!="" && _self.city()!="")
        {
            _self.copyWorkingPoints.forEach(function (element) {
                if (element.country() == _self.country() && element.city() == _self.city()) {
                    _self.workingPoints.push(element);
                }
            });
        }
        else {
            if(_self.country()!="")
            {
                _self.copyWorkingPoints.forEach(function (element) {
                    if (element.country() == _self.country()) {
                        _self.workingPoints.push(element);
                    }
                });
            }
            if (_self.city() != "") {
                _self.copyWorkingPoints.forEach(function (element) {
                    if (element.city() == _self.city()) {
                        _self.workingPoints.push(element);
                    }
                });
            }
        }
        
        
    }


    
}
   
function InitializeServicePlace(data){
        ServicePlace.instance = new ServicePlace();
        ServicePlace.instance.initialize(data);
        ko.applyBindings(ServicePlace.instance);
    }