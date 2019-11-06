using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reddit.Models;
using System.Web.Security;
namespace Reddit.Controllers
{
    public class UsersController : Controller
    {
        private void CreateformsIdentity(string userName, bool isAuthenticated)
        {
            FormsIdentity formsID;
            FormsAuthenticationTicket authenticationTicket;

            if (isAuthenticated)
            {
                // If authentication passed, create a ticket 
                // as a Manager that expires in 15 minutes.
                authenticationTicket = new FormsAuthenticationTicket(1, userName,
                    DateTime.Now, DateTime.Now.AddMinutes(15), false, "Manager");
                var i = 0;
                i++;
            }
            else
            {
                // If authentication failed, create a ticket 
                // as a guest that expired 5 minutes ago.
                authenticationTicket = new FormsAuthenticationTicket(1, userName,
                    DateTime.Now, DateTime.Now.Subtract(new TimeSpan(0, 5, 0)),
                    false, "Guest");
            }

            // Create form identity from FormsAuthenticationTicket.
            formsID = new FormsIdentity(authenticationTicket);
            Response.Clear();
            Response.Write("Authentication Type: " + formsID.AuthenticationType +
                "<BR>");

            // Get FormsAuthenticationTicket from the FormIdentity
            FormsAuthenticationTicket ticket = formsID.Ticket;
            if (ticket.Expired)
            {
                Response.Write("Authentication failed, so the role is set to " +
                    ticket.UserData);
            }
            else
            {
                Response.Write("Authentication succeeded, so the role is set to " +
                    ticket.UserData);
            }
        }
        // GET: Users
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UsernameOrEmail, user userLogin)
        {
            using (reddit_dbEntities userEnt = new reddit_dbEntities())
            {
                try
                {
                    user LoggedInUser = userEnt.users
                        .Where(
                            DbUsers => (DbUsers.Username == UsernameOrEmail || DbUsers.Email == UsernameOrEmail) && DbUsers.Password == userLogin.Password)
                        .First();
                    //if no exception thrown then get ID and return to Home Page
                    FormsAuthentication.SetAuthCookie(LoggedInUser.Username, false);
                    //should change to (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated == true) 
                    //instead of the true
                    CreateformsIdentity(LoggedInUser.Username, true);
                    Session["UserID"] = LoggedInUser.ID;
                    Session["Username"] = LoggedInUser.Username;
                    return RedirectToAction("Index", "newsfeedposts");
                }
                catch { 
                    return View("Login"); 
                }
                finally { 
                    /*Initially was going to use [finally] but its too much of a hassle to get Logged In User ID*/ 
                }
                
            }
        }
        public ActionResult SignUp()
        {
            return View("Signup");
        }
        [HttpPost]
        public ActionResult SignUp(user UserSignup)
        {
            using (reddit_dbEntities db = new reddit_dbEntities())
            {
                if (ModelState.IsValid)
                {
                    db.users.Add(UserSignup);
                    db.SaveChanges();
                    return RedirectToAction("Index","newsfeedposts");
                }
            }

            return View("Login");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "newsfeedposts");
        }
    }
}