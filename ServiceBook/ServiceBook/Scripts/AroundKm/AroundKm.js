function AroundKm() {
    var _self = this;
    this.lastKm = ko.observable(0);
    this.aroundKm = ko.observable(0);
    this.lastServiceDate = ko.observable(null);
    this.exist = ko.observable(false);
    this.existVin = ko.observable(false);
    this.vehicleVin = "";
    this.getAroundKm=function()
    {
        _self.exist(false);
        _self.existVin(false);
        $.ajax({
            type: "POST",
            url: "/AroundKm/GetAroundKm",
            data:
                {
                    VehicleVin: _self.vehicleVin
                },
            success: function (msg) {
                if (msg.Around.Exist == true) {
                    _self.exist(true);
                    _self.lastServiceDate(convertToFormatJs(msg.Around.LastRepairDate));
                    _self.lastKm(msg.Around.LastKm);
                    _self.aroundKm(msg.Around.AroundKm);
                }
                else {
                    _self.existVin(true);
                }
            },
            dataType: "json"
        });
    }

}

function InitializeAroundKm(data) {
    AroundKm.instance = new AroundKm();
    ko.applyBindings(AroundKm.instance);
}