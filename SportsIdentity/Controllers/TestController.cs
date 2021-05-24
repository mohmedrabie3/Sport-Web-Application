using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SportsIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsIdentity.Controllers
{
    public class TestController : Controller
    {
        Modelcontext db = new Modelcontext();
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error() 
        {
            return View();
        }
        public ActionResult AddRole() 
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddRole(string RoleName)
        {
            if (RoleName != null) 
            {
                Modelcontext db = new Modelcontext();
                RoleStore<IdentityRole> Store = new RoleStore<IdentityRole>(db);
                RoleManager<IdentityRole> manger = new RoleManager<IdentityRole>(Store);
                IdentityRole role = new IdentityRole();
                role.Name = RoleName;
                IdentityResult result = await manger.CreateAsync(role);
                if (result.Succeeded) 
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    return View(RoleName);
                }

            }
            return View();
        }
        public ActionResult Login() 
        {
            return View();
        }
        public ActionResult LogOut()
        {
            IAuthenticationManager manger = HttpContext.GetOwinContext().Authentication;
            manger.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            ViewBag.mess = "logout is Done";
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<ActionResult> Login(MVLogin MVL)
        {
            if (ModelState.IsValid) 
            {
                Modelcontext db = new Modelcontext();
                UserStore<User> store = new UserStore<User>(db);
                UserManager<User> manger = new UserManager<User>(store);
                User us = await manger.FindAsync(MVL.UserName, MVL.PassWord);
                if(us!=null)
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    return View(MVL);
                }
            }
            return View(MVL);
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Registration( MVUserReg MVR)
        {
            //Checking  input
            if (!ModelState.IsValid) 
                return View(MVR);
            try
            {
                // Manger Add USer in DB
                Modelcontext db = new Modelcontext();
                UserStore<User> store = new UserStore<User>(db);
                UserManager<User> manger = new UserManager<User>(store);
                User us = new User();
                us.UserName = MVR.UserName;
                us.PasswordHash = MVR.PassWord;
                us.Email = MVR.Email;
                us.Address = MVR.Address;

                IdentityResult result = await manger.CreateAsync(us, MVR.PassWord);
                if (result.Succeeded)
                {
                    //add user in Role
                    manger.AddToRole(us.Id, "user");
                    //Add Cookie
                    IAuthenticationManager Owin = HttpContext.GetOwinContext().Authentication;
                    SignInManager<User, string> signInManager = new SignInManager<User, string>(manger, Owin);
                    signInManager.SignIn(us, true, true);
                    return RedirectToAction("Index");
                }
                else 
                {
                    //show what is error
                    ModelState.AddModelError("", (result.Errors.ToList())[0]);
                    return View(MVR);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(MVR);
            }
        }
        public ActionResult ADDTeam()
        {
            FootBallTeam FT = new FootBallTeam();
            return View(FT);
        }
        [HttpPost]
        public ActionResult ADDTeam(FootBallTeam FT, HttpPostedFileBase photo)
        {
            photo.SaveAs(Server.MapPath($"~/Assets/images/{photo.FileName}"));
            FT.Logo = photo.FileName;
            db.footBallTeams.Add(FT);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult ControlTeams()
        {
            List<FootBallTeam> TF = db.footBallTeams.ToList();
            return View(TF);
        }
        public ActionResult Edite(int id)
        {
            FootBallTeam FT = db.footBallTeams.Where(n => n.ID == id).FirstOrDefault();
            return View(FT);
        }
        public ActionResult Edite(FootBallTeam FT, HttpPostedFileBase photo)
        {
            photo.SaveAs(Server.MapPath($"~/Assets/img/{photo.FileName}"));
            FootBallTeam ftt = db.footBallTeams.Where(n => n.ID == FT.ID).FirstOrDefault();
            ftt.Name = FT.Name;
            ftt.Logo = photo.FileName;
            ftt.CoachName = FT.CoachName;
            ftt.Country = FT.Country;
            ftt.FoundationDate = FT.FoundationDate;
            db.SaveChanges();
            return RedirectToAction("ControlTeams");
        }
        public ActionResult Delete(int id)
        {
            FootBallTeam ft = db.footBallTeams.Where(n => n.ID == id).SingleOrDefault();
            db.footBallTeams.Remove(ft);
            db.SaveChanges();
            return RedirectToAction("ControlTeams");
        }
        public ActionResult ADDPlayer()
        {
            List<FootBallTeam> footBallTeams = db.footBallTeams.ToList();
            SelectList FTS = new SelectList(footBallTeams, "id", "name");
            ViewBag.FT = FTS;
            return View();
        }
        [HttpPost]
        public ActionResult ADDPlayer(Player p, HttpPostedFileBase photo)
        {
            photo.SaveAs(Server.MapPath($"~/Assets/images/{photo.FileName}"));
            p.image = photo.FileName;
            db.players.Add(p);
            db.SaveChanges();
            return RedirectToAction("index");

        }
        public ActionResult ControlPlayers()
        {
            List<Player> pl = db.players.ToList();
            return View(pl);
        }
        public ActionResult Change(int id)
        {
            Player pp = db.players.Where(n => n.ID == id).SingleOrDefault();
            List<FootBallTeam> footBallTeams = db.footBallTeams.ToList();
            SelectList ff = new SelectList(footBallTeams, "id", "name");
            ViewBag.fft = ff;
            return View(pp);
        }
        [HttpPost]
        public ActionResult Change(Player p, HttpPostedFileBase photo)
        {
            Player pp = db.players.Where(n => n.ID == p.ID).FirstOrDefault();
            if (photo != null)
            {
                photo.SaveAs(Server.MapPath($"~/Assets/images/{photo.FileName}"));
                pp.image = photo.FileName;

            }
            pp.national = p.national;
            pp.Name = p.Name;
            pp.DateOfBirth = p.DateOfBirth;
            pp.IDTeam = p.IDTeam;
            db.SaveChanges();
            return RedirectToAction("ControlPlayers");
        }
        public ActionResult Remove(int id)
        {
            Player p = db.players.Where(n => n.ID == id).FirstOrDefault();
            db.players.Remove(p);
            db.SaveChanges();
            return RedirectToAction("ControlPlayers");
        }
        public ActionResult Teams()
        {
            List<FootBallTeam> footBallTeams = db.footBallTeams.ToList();
            SelectList ft = new SelectList(footBallTeams, "id", "Name");
            ViewBag.fft = ft;
            return View(footBallTeams);
        }
        public ActionResult showPlayers(int id)
        {
            List<Player> players = db.players.Where(n => n.IDTeam == id).ToList();
            return View(players);
        }
        public ActionResult BackToHome()
        {
            return RedirectToAction("index");
        }

    }
}