using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Curse.Models;
using Curse.Models.ViewModels;
using Curse.Models.Repository;
using System.IO;
using Curse.Helpers;
using Curse.Exceptions;

namespace Curse.Controllers
{
    //[Authorize] authorization/authenication not used in this demo
    public class TeamsController : BaseController
    {
        private ITeamRepository _teamDb;
        private IPlayerRepository _playerDb;

        public TeamsController()
        {
            SportsDataContext context = new SportsDataContext();

            _teamDb = new TeamRepository(context);
            _playerDb = new PlayerRepository(context);
        }

        //allow for Dependency Injection and Mocking
        public TeamsController(ITeamRepository teamDdb,IPlayerRepository playerDb)
        {
            _teamDb = teamDdb;
            _playerDb = playerDb;
        }
        //
        // GET: /Teams/
        public ActionResult Index()
        {
            //return a list of teams sorted by team name
            var teams = _teamDb.FindAll().Select(x => new TeamListViewModel{
              ID = x.ID,
              Name = x.Name,
              PlayerCount = x.Players.Count()
            })
            .OrderBy(x => x.Name)
            .ToList();
            
            return View(teams);
        }

        public ActionResult Create()
        {
            return View(new CreateTeamViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]//prevents CSRF attacks
        public ActionResult Create(CreateTeamViewModel viewmodel, HttpPostedFileBase avatar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                        Team team = new Team { Name = viewmodel.Name };

                        _teamDb.Add(team);

                        //if an image has been provided then upload it
                        if (avatar != null && avatar.ContentLength > 0)
                        {
                                                     
                            AvatarImage image = AvatarUploader.UploadFile2(Server, avatar, team.ID, AvatarTypeEnum.Team);

                            team.AvatarName = image.Name; 

                            if (!_teamDb.Update(team)) //update the avatar path after database has set PK
                            {
                                return Content("An error has occurred on the server");//just temporary, create error view
                            }
                        }
                    
                        return RedirectToAction("Details", new { id = team.ID });
                }
                catch (Exception e)
                {
                    //log exception
                    //return error message to user
                    ModelState.AddModelError("", "Exception occured on the server");
                }
                
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var team = _teamDb.Find(id);
            if (team == null) return HttpNotFound();

            TeamAvatarImage image = new TeamAvatarImage(Server, team.AvatarName);//find out the correct path for the avatar (if there is one)
            TeamViewModel viewmodel = new TeamViewModel { ID = id, Name = team.Name };
            viewmodel.Avatar = image.Exists ? image.RelativePath : null;

            //build the list of players for this team to display
            viewmodel.PlayerList = _playerDb.FindAll().Where(x => x.TeamId == team.ID).Select(x => new PlayerViewModel { Name = x.Name, ID = x.ID }).ToList();

            return View(viewmodel);
        }

        public ActionResult Edit(int id)
        {
            var team = _teamDb.Find(id);
            if (team == null) return HttpNotFound();

            TeamAvatarImage image = new TeamAvatarImage(Server, team.AvatarName);
            TeamViewModel viewmodel = new TeamViewModel { ID = id, Name = team.Name };
            viewmodel.Avatar = image.Exists ? image.RelativePath : null;

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamViewModel viewmodel, HttpPostedFileBase avatar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Team team = _teamDb.Find(viewmodel.ID);
                    if (team == null) return HttpNotFound();

                    team.Name = viewmodel.Name;

                    if (!_teamDb.Update(team))
                    {
                        //log error...
                        ModelState.AddModelError("", "An error occurred updating the use information");
                        return View(viewmodel);
                    }

                    //if an image has been provided then upload it
                    if (avatar != null && avatar.ContentLength > 0)
                    {
                        try
                        {
                            //delete the current avatar image
                            TeamAvatarImage currentImage = new TeamAvatarImage(Server, team.AvatarName);
                            System.IO.File.Delete(currentImage.AbsolutePath);
                        }
                        catch (CustomExceptionBase e)
                        {//safe error message for user
                            ModelState.AddModelError("", e.Message);
                            return View(viewmodel);
                        }
                        catch (Exception e)
                        {
                            //log error
                            ModelState.AddModelError("", "An error has occurred on the server.");
                            return View(viewmodel);
                        }

                        AvatarImage image = AvatarUploader.UploadFile2(Server, avatar, team.ID, AvatarTypeEnum.Team);

                        team.AvatarName = image.Name;
                        _teamDb.Update(team);

                    }

                    return RedirectToAction("Details", new { id = team.ID });
                }
                catch (CustomExceptionBase e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    //log exception
                    //return error message to user
                    ModelState.AddModelError("", "Exception occured on the server");
                }

            }

            return View(viewmodel);
        }

        public ActionResult Delete(int id)
        {
            var team = _teamDb.Find(id);
            if (team == null) return HttpNotFound();

            TeamAvatarImage image = new TeamAvatarImage(Server, team.AvatarName);

            TeamViewModel viewmodel = new TeamViewModel { ID = id, Name = team.Name };
            viewmodel.Avatar = image.Exists ? image.RelativePath : null;

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TeamViewModel viewmodel)
        {
            try
            {
                var team = _teamDb.Find(viewmodel.ID);
                if (team == null) return HttpNotFound();

                //if there is a current image then delete it
                if (!String.IsNullOrEmpty(team.AvatarName))
                {
                    TeamAvatarImage image = new TeamAvatarImage(Server, team.AvatarName);
                    System.IO.File.Delete(image.AbsolutePath);
                }
            }
            catch (Exception e)
            {
                //log error...
                ModelState.AddModelError("", GenericErrorMessage);
                return View(viewmodel);
            }

            try
            {
                var playersOnTeam = _playerDb.FindAll().Where(x => x.TeamId == viewmodel.ID).ToList();

                playersOnTeam.ForEach(x => 
                    {                        
                        try
                        {
                            //delete this player from the database
                            _playerDb.Delete(x.ID);

                            //delete the players avatar
                            PlayerAvatarImage image = new PlayerAvatarImage(Server, x.AvatarName);
                            if(!String.IsNullOrEmpty(x.AvatarName))System.IO.File.Delete(image.AbsolutePath);
                        }
                        catch (Exception e)
                        {
                            //log error...
                        }
                    });//delete all players on the team first
                _teamDb.Delete(viewmodel.ID);
            }
            catch (Exception e)
            {
                //log error
                return Content("Error");//temp
            }

            return RedirectToAction("Index");
        }

    }
}
