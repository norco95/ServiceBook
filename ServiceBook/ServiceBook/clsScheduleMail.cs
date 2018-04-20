using ServiceBook.DAL;
using System;
using System.Net;
using System.Net.Mail;

namespace ServiceBook
{
    internal class clsScheduleMail
    {
        public void SendScheduleMail()
        {
            ServiceRepository serviceRepository = new ServiceRepository();
            DateTime now = DateTime.Now;
            if (now.Hour == 8)
            {
                now = now.AddDays(3);
                var services = serviceRepository.GetListOfServies(now);
                if (services != null)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("info.servicebook@gmail.com");
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("info.servicebook@gmail.com", "allamvizsga");

                    foreach (var service in services)
                    {
                        mailMessage.To.Add(service.Vehicle.VehicleOwner.Email);
                        mailMessage.Subject = "It is the time to retaire Your vehicle with identifier:" + service.Vehicle.VIN;
                        mailMessage.Body = "";
                        mailMessage.Body += "Hello " + service.Vehicle.VehicleOwner.FirstName + " " + service.Vehicle.VehicleOwner.LastName + "\n";
                        mailMessage.Body += "It is the time to repaire Your vehicle with identifier:" + service.Vehicle.VIN;
                        mailMessage.Body += "\n" + "Waiting you for chacking your car!";
                        smtpClient.Send(mailMessage);
                    }
                }
            }
        }
    }
}