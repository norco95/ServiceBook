function SSI(data) {
    this.sid = null;
    this.siid = null;
    this.service = null;
    if (data != null) {
        this.sid = data.SID;
        this.service = new Service(data.Service);
        this.siid = data.SIID;
        this.serviceIntervention = new ServiceIntervention(data.ServiceIntervention);
    }
}