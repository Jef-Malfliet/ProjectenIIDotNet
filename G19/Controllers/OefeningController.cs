﻿using G19.Filters;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace G19.Controllers {
    [Authorize(Policy = "Lesgever")]
    public class OefeningController : Controller {
        private readonly IOefeningRepository _oefeningRepository;
        private readonly ILidRepository _lidRepository;
        public IConfiguration Configuration { get; set; }

        public OefeningController(IOefeningRepository oefeningRepository, ILidRepository lidRepository, IConfiguration configuration) {
            _oefeningRepository = oefeningRepository;
            _lidRepository = lidRepository;
            Configuration = configuration;
        }
        public IActionResult Index() {
            if (SessionState.OefeningenBekijkenState()) {
                IEnumerable<Oefening> oefeningen = _oefeningRepository.GetAll().OrderBy(o => o.Graad).ThenBy(o => o.Naam).ToList();
                ViewData["Graden"] = new List<GraadEnum>(new HashSet<GraadEnum>((GraadEnum[])Enum.GetValues(typeof(GraadEnum))));
                return View(oefeningen);
            }
            else {
                TempData["SessionStateMessage"] = "Je moet eerst je aanwezigheid registreren.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }

        [HttpPost]
        public IActionResult GeefCommentaar(_CommentsViewModel commentViewModel, int id) {
            if (SessionState.OefeningenBekijkenState()) {
                string comment = commentViewModel.Comments + '~' + SessionState.huidigLid.Voornaam + ' ' + SessionState.huidigLid.Familienaam;
                _oefeningRepository.AddComment(id, comment);
                _oefeningRepository.SaveChanges();
                IEnumerable<Oefening> oefeningen = _oefeningRepository.GetAll().OrderBy(o => o.Graad).ThenBy(o => o.Naam).ToList();
                bool succes = SendMailAsync(comment, id).Result;
                if (succes) {
                    TempData["Message"] = "Mail succesvol verzonden.";
                } else {
                    TempData["Error"] = "Er ging iets mis bij het versturen van de mail, gelieve de lesgever te waarschuwen.";
                }
                return View("~/Views/Oefening/Comments.cshtml", _oefeningRepository.GetById(id));
            }
            else {
                TempData["SessionStateMessage"] = "Je moet eerst je aanwezigheid registreren.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }
        [Route("Oefening/{graad}")]
        public IActionResult GeefOefeningenPerGraad(string graad) {
            TempData["Graad"] = SessionState.huidigLid.geefGraadinGetal();
            TempData["active"] = graad;
            if (SessionState.OefeningenBekijkenState()) {
                if (SessionState.ToegestaandOefeningenBekijken(graad)) {
                    if (graad != "ZWART" && graad != "ALLES") {
                        return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString() == graad).OrderBy(oef => oef.Graad));
                    }
                    else if (graad == "ZWART") {
                        return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString().StartsWith("DAN")).OrderBy(oef => oef.Graad));
                    }
                    else {
                        return View(nameof(Index), _oefeningRepository.GetAll().OrderBy(oef => oef.Graad));
                    }
                }
                else {
                    TempData["SessionStateMessage"] = "Lid beschikt niet over de juiste graad.";
                    return View("~/Views/Session/SessionStateMessage.cshtml");
                }
            }
            else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }

        public IActionResult GeefOefeningenLid(int lidId) {
            var lid = _lidRepository.GetById(lidId);
            TempData["Graad"] = lid.geefGraadinGetal();
            SessionState.VeranderHuidigLid(lid);
            if (SessionState.OefeningenBekijkenState()) {
                string graad = lid.Graad.ToString();
                if (graad != "ZWART" && graad != "ALLES") {
                   
                    return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString() == graad).OrderBy(oef => oef.Graad));
                }
                else if (graad == "ZWART") {
                    return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString().StartsWith("DAN")).OrderBy(oef => oef.Graad));
                }
                else {
                    return View(nameof(Index), _oefeningRepository.GetAll().OrderBy(oef => oef.Graad));
                }
            }
            else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
              
                return View("~/Views/Session/SessionStateMessage.cshtml");

            }
        }

        

        public ActionResult GeefTextView(int Id) {
            if (SessionState.OefeningenBekijkenState()) {

                return View("~/Views/Oefening/Text.cshtml", _oefeningRepository.GetById(Id));
            }
            else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }
        public ActionResult GeefVideoView(int Id) {
            if (SessionState.OefeningenBekijkenState()) {

                return View("~/Views/Oefening/Video.cshtml", _oefeningRepository.GetById(Id));
            }
            else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }
        public ActionResult GeefFotoView(int Id) {
            if (SessionState.OefeningenBekijkenState()) {

                return View("~/Views/Oefening/Fotos.cshtml", _oefeningRepository.GetById(Id));
            }
            else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }
        public ActionResult GeefCommentView(int Id) {
            if (SessionState.OefeningenBekijkenState()) {

                return View("~/Views/Oefening/Comments.cshtml", _oefeningRepository.GetById(Id));
            }
            else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }

        private async Task<bool> SendMailAsync(string comment, int oefId) {

            var oef = _oefeningRepository.GetById(oefId);

            MailAddress from = new MailAddress(Configuration["Mail:From"]);
            MailAddress to = new MailAddress(Configuration["Mail:To"]);
            MailMessage mail = new MailMessage(from,to);
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
            } catch(Exception e) {
                ModelState.AddModelError("", e.Message);
                return false;
            }
            return true;
        }
    }
}