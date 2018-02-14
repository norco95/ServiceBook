function Currenty(data)
{
    this.id = null;
    this.name = null;
    this.serviceIntervention = null;
    if(data!=null)
    {
        this.id = data.ID;
        this.name = data.Name;
        this.serviceIntervention = _.map(data.ServiceIntervention, function (serviceIntervnention, index) {
            return new ServiceIntervention(serviceIntervnention);

        });
       
    }
}