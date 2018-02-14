function SE(data) {
    this.sid = null;
    this.eid = null;
    this.employee = null;
    this.service = null;
    if (data != null) {
        this.employee = new Employee(data.Employee);
        this.service = new Service(data.Service);
        this.sid = data.SID;
        this.eid = data.EID;
    }
}