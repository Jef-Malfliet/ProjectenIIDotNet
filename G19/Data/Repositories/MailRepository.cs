using G19.Models.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace G19.Data.Repositories {
    public class MailRepository: IMailRepository {

        public IConfiguration Configuration { get; set; }

        private readonly IOefeningRepository _oefeningRepository;

        public MailRepository(IConfiguration configuration, IOefeningRepository oefeningRepository) {
            Configuration = configuration;
            _oefeningRepository = oefeningRepository;
        }

        public async Task<bool> SendMailAsync(string comment, int oefId) {

            var oef = _oefeningRepository.GetById(oefId);

            MailAddress from = new MailAddress(Configuration["Mail:From"]);
            MailAddress to = new MailAddress(Configuration["Mail:To"]);
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = "Nieuwe feedback op oefening " + oef.Naam + " - " + oef.Graad.ToString("");
            mail.IsBodyHtml = true;
            mail.Body =
                "<h2>Er is nieuwe feedback toegevoegd aan oefening " + oef.Naam + " van graad " + oef.Graad.ToString("") + "</h2>"
                + "<br />"
                + comment;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["Mail:From"], Configuration["Mail:FromPass"]);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


            smtp.SendCompleted += (s, e) => {
                smtp.Dispose();
                mail.Dispose();
            };

            try {
                await smtp.SendMailAsync(mail);
            } catch (Exception e) {
                return false;
            }
            return true;
        }
    }
}
