function VehicleOffer() {
    var _self = this;
    this.vehicleOffers = [];
    this.i = 0;
    this.sum = 0;
    this.initialize = function (data) {
     
      
        if (data != null && data.VehicleOffers != null) {
            data.VehicleOffers.forEach(function (element) {
           
                $.ajax({
                            type: "POST",
                            url: "https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVINValuesBatch/",
                            data:
                                {
                                    format: "json",
                                    data: element.Vin,
                                },
                            success: function (msg) {
                                _self.i++;
                                var vehicle = null;
                                for (x in msg.Results) {
                                       vehicle = msg.Results[x]["Make"];
                                }
                                var vehicleOfferModel = new VehicleOfferModel();
                                var get = 0;
                                for (var j = 0; j < _self.vehicleOffers.length; j++) {
                                  
                                    if (vehicle == _self.vehicleOffers[j].make) {
                                        get = 1;
                                        _self.vehicleOffers[j].avgRepairCost += element["AvgRepairCost"];
                                        _self.vehicleOffers[j].count++;
                                    }

                                   }
                                            if (get == 0) {

                                                vehicleOfferModel.make =vehicle;
                                                vehicleOfferModel.avgRepairCost += element["AvgRepairCost"];
                                                vehicleOfferModel.count = 1;

                                                _self.vehicleOffers.push(vehicleOfferModel);

                                            }
                                            if (_self.i == data.VehicleOffers.length) {
                                                var datas = [];
                                                var labels = [];
                                                _self.vehicleOffers.forEach(function (element) {
                                                    _self.sum += element.avgRepairCost/element.count;
                                                });
                                                _self.vehicleOffers.sort((a, b) => a.avgRepairCost - b.avgRepairCost);
                                                _self.vehicleOffers.forEach(function (element) {
                                                    labels.push(element.make);
                                                    datas.push(((element.avgRepairCost / element.count)*100)/_self.sum); 
                                                });


                                                for (var i = 0; i < datas.length / 2; i++) {
                                                    var tmp = datas[i];
                                                    datas[i] = datas[datas.length - i-1];
                                                    datas[datas.length - i - 1] = tmp;
                                                  
                                                }
                                                _self.getGraph(datas, labels);

                                            }
                                           
                            },
                            dataType: "json"
                        });
            });
           
        }

       
    }
    this.getGraph=function(data,labels)
    {
        for (var i = 0; i < data.length; ++i) {
            labels[i] = labels[i] + ', ' + Math.round(data[i] * 100) / 100 + '%';
        }

        new RGraph.SVG.Pie({
            id: 'chart-container',
            data: data,
            options: {
                labels: labels,
                tooltips: labels,
                colors: ['#EC0033', '#A0D300', '#FFCD00', '#00B869', '#999999', '#FF7300', '#004CB0'],
                strokestyle: 'white',
                linewidth: 2,
                shadow: true,
                shadowOffsetx: 2,
                shadowOffsety: 2,
                shadowBlur: 3,
                exploded: 7
            }
        }).draw();
    }
    
}

function InitializeVehicleOffer(data) {
    VehicleOffer.instance = new VehicleOffer();
    VehicleOffer.instance.initialize(data);
    ko.applyBindings(VehicleOffer.instance);
}