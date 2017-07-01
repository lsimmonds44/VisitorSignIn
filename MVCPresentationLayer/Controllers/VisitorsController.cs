using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer.Controllers
{
    public class VisitorsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        LogicLayer.VisitorManager visitorManager = new LogicLayer.VisitorManager();

        // GET: Visitors
        public ActionResult Index()
        {
            return View(visitorManager.RetrieveSignedIn());
        }

        // GET: Visitors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Visitors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Company,PersonVisiting,Citizen,SignedIn,SignedInTime")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                if (visitorManager.InsertVisitor(visitor) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }
            }

            return View(visitor);
        }

        //// GET: Visitors
        //public ActionResult SignOut()
        //{
        //    return View(visitorManager.RetrieveSignedOut());
        //}

        // POST: Visitors/Edit
        
        public ActionResult SignOut(int? visitorID)
        {
            if (ModelState.IsValid)
            {
                if (visitorID == null)
                {
                    return View(visitorManager.RetrieveSignedOut());
                }

                else if (visitorManager.SignOutVisitor((int)visitorID) == true)
                {
                    return View(visitorManager.RetrieveSignedOut());
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }
            }
            return View(visitorID);
        }
    }
}
