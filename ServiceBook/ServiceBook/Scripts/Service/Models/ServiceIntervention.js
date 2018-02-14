function ServiceIntervention(data)
{
    this.id=null;
    this.name=null;
    this.price=null;
    this.cid=null;
    this.wp = null;
    this.flag = null;
    this.ssi = null;
    this.workingPoint = null;
    if(data!=null)
    {
        this.ssi = _.map(data.SSI, function (ssi, index) {
            return new SSI(ssi);

        });
       
        this.id = data.ID;
        this.name = data.Name;
        this.price=data.Price;
        this.cid=data.CID;
        this.wp = data.WP;
        this.flag = data.Flag;
        this.workingPoint = new WorkingPoint(data.WorkingPoint);

    }
}





