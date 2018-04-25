function Currency(data)
{
    this.id = null;
    this.name = null;
   
    if(data!=null)
    {
        this.id = data.ID;
        this.name = data.Name;  
    }
}