function SW(data) {
    this.sid = null;
    this.wid = null;
    this.service = null;
    this.workingPoint = null;
    if (data != null) {
        this.sid = data.SID;
        this.wid = data.WID;
        this.service = new Service(data.Service);
        this.workingPoint=new WorkingPoint(data.WorkingPoint)
    }
}