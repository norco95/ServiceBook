function CCO(data) {
    this.cid=null;
    this.coid = null;
    if(data!=null)
    {
        this.cid = data.CID;
        this.coid = data.COID;
    }
}