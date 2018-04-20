function WorkingPoint(data) {
    this.id = ko.observable(null);
    this.country = ko.observable(null);
    this.city = ko.observable(null);
    this.street = ko.observable(null);
    this.nr = ko.observable(null);
    
    this.rate = ko.observable(null);
    this.company = "";
    this.reviews = [];
    if (data != null) {
        this.id(data.ID);
        this.country(data.Country);
        this.city(data.City);
        this.street(data.Street);
        this.nr(data.Nr);
        this.flag = data.Flag;
        this.rate(data.Rate);
        this.company = data.CompanyName;
        this.reviews = _.map(data.Reviews, function (review, index) {
            return review;
        });
        }

}