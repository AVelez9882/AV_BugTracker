using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AV_BugTracker.Models;
using AV_BugTracker.Helpers;

namespace AV_BugTracker.Controllers
{
    //[Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper projectHelper = new ProjectHelper();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private TicketManager ticketManager = new TicketManager();
        private HistoryHelper historyHelper = new HistoryHelper();


        // GET: Tickets
        [Authorize]
        public ActionResult Index()
        {
            //var userId = User.Identity.GetUserId();
            //var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            //List<Ticket> model = new List<Ticket>();
            //switch (myRole)
            //{
            //    case "Admin":
            //        model = db.Tickets.ToList();
            //        break;
            //    case "ProjectManager":
            //    case "Developer":
            //        model = projectHelper.ListUserProjects(userId).SelectMany(p => p.Tickets).ToList();
            //        break;
            //    case "Submitter":
            //        model = db.Tickets.Where(T => T.SubmitterId == userId).ToList();
            //        break;
            //    default:
            //        return RedirectToAction("Index", "Home");
            //}
            //return View(model);

            //var myUserId = User.Identity.GetUserId()
            //var myTickets = ticketManager.GetMyTickets(myUserId);
            //return View(myTickets);
            return View(db.Tickets.ToList());
        }

        public ActionResult GetProjectTickets()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var ticketList = new List<Ticket>();
            ticketList = user.Projects.SelectMany(p => p.Tickets).ToList();

            return View("Index", ticketList);
        }

        public ActionResult GetMyTickets()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var ticketList = new List<Ticket>();

            if (User.IsInRole("Developer"))
            {
                ticketList = db.Tickets.Where(t => t.DeveloperId == userId).ToList();
                return View("Index", ticketList);
            }

            if (User.IsInRole("Submitter"))
            {
                ticketList = db.Tickets.Where(t => t.SubmitterId == userId).ToList();
                return View();
            }
            else 
            {
                return RedirectToAction("GetProjectTickets");
            }
        }

        // GET: Tickets/Dashboard/5
        [Authorize]
        public ActionResult Dashboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        //[Authorize(Roles = "Submitter")]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId), "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        //[Authorize(Roles = "Submitter")]
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,TicketPriorityId,TicketTypeId,Issue,IssueDescription")] Ticket ticket, bool onPage)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                ticket.TicketStatusId = db.TicketStatuses.Where(ts => ts.Name == "Open").FirstOrDefault().Id;
                ticket.Created = DateTime.Now;
                ticket.SubmitterId = userId;
                db.Tickets.Add(ticket);
                db.SaveChanges();

                if (onPage)
                {
                    return RedirectToAction("Details", "Projects", new { id = ticket.ProjectId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId ), "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "LastName", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketPriorityId,TicketStatusId,TicketTypeId,SubmitterId,DeveloperId,Issue,IssueDescription,Created,Updated,IsResolved,IsArchived")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var thisTicket = db.Tickets.Find(ticket.Id);
                thisTicket.ProjectId = ticket.ProjectId;
                //go out and get an unedited copy of the ticket from the DB, as no tracking allows me 
                //to store it in the variable without loading it to the database 
                var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);

                //db.Entry(ticket).State = EntityState.Modified;  first two lines of code replace this so changes are maintained in the db/sql
                db.SaveChanges();

                //somehow compare the old ticket with the new ticket to make any number
                //of decisions that might be required 
                var newTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                ticketManager.ManageTicketNotifications(oldTicket, newTicket);
                historyHelper.ManageHistories(oldTicket, newTicket);

                return RedirectToAction("Index");
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "LastName", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
