function ServiceIntervention(data)
{
    this.id=null;
    this.name=null;
    this.price=null;
    this.flag = null;
    this.currency = null;
    this.wpid = null;
    if(data!=null)
    {
        this.currency = new Currency(data.Currency);
        this.id = data.ID;
        this.name = data.Name;
        this.price=data.Price;
        this.flag = data.Flag;
        this.wpid = data.WPID;
    }
}





