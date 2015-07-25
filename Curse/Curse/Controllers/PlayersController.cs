using Curse.Exceptions;
using Curse.Helpers;
using Curse.Models;
using Curse.Models.Repository;
using Curse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Curse.Controllers
{
    public class PlayersController : BaseController
    {
        private IPlayerRepository _playerDb;
        private ITeamRepository _teamDb;

        public PlayersController()
        {
            SportsDataContext context = new SportsDataContext();

            _playerDb = new PlayerRepository(context);
            _teamDb = new TeamRepository(context);
        }

        //can be used for mocking and Dependency Injection
        public PlayersController(IPlayerRepository playerDb, ITeamRepository teamDb)
        {
            _playerDb = playerDb;
            _teamDb = teamDb;
        }
        //
        // GET: /Players/
        public ActionResult Index()
        {
            //get a list of all palyers ordered by name
            var players = _playerDb.FindAll().Select(x => new PlayerListViewModel
            {
                ID = x.ID,
                Name = x.Name,
                RBI = x.RBI,
                Homerun = x.HomeRun,
                Average = x.Average,
                TeamName = x.Team.Name ?? "",
                TeamId = x.Team.ID
            })
            .OrderBy(x => x.Name)
            .ToList();

            return View(players);
        }

        public ActionResult Create(int? team)
        {
            var viewmodel = new CreatePlayerViewModel
            {
                RBI = 0,
                HomeRuns = 0,
                Average = 0,
                TeamId = team
            };

            //items to populate in the drop down list
            viewmodel.TeamSelectList = DropDownHelper.BuildTeamSelectList(_teamDb);

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePlayerViewModel viewmodel, HttpPostedFileBase avatar)
        {
            if (ModelState.IsValid)
            {
                Player player = new Player();
                player.Name = viewmodel.Name;
                player.RBI = viewmodel.RBI;
                player.HomeRun = viewmodel.HomeRuns;
                player.Average = viewmodel.Average;

                player.TeamId = viewmodel.TeamId;

                //add new player to the database, after which player.ID is populated
                _playerDb.Add(player);
                
                if (avatar != null && avatar.ContentLength > 0)
                {
                    AvatarImage image = AvatarUploader.UploadFile2(Server, avatar, player.ID, AvatarTypeEnum.Player);
                    player.AvatarName = image.Name;

                    if (!_playerDb.Update(player))
                    {
                        ModelState.AddModelError("", GenericDatabaseError);
                        viewmodel.TeamSelectList = DropDownHelper.BuildTeamSelectList(_teamDb);
                        return View(viewmodel);
                    }
                }

                return RedirectToAction("Details", new { id = player.ID });
            }

            //this path is reached if the model is initially invalid
            viewmodel.TeamSelectList = DropDownHelper.BuildTeamSelectList(_teamDb);

            return View(viewmodel);
        }

        public ActionResult Details(int id)
        {
            Player model = _playerDb.Find(id);//can cause Exception. Currently bubbles up the the default error handler. It may be better do display a more specific error message in this method.
            if (model == null) return HttpNotFound();

            //PlayerAvatarImage holds the absolute and relative paths to the image
            PlayerAvatarImage image = new PlayerAvatarImage(Server, model.AvatarName);
            PlayerViewModel viewmodel = model.ToPlayerViewModel();
            viewmodel.Avatar = image.Exists ? image.RelativePath : null;
            
            return View(viewmodel);
        }

        public ActionResult Edit(int id)
        {
            var player = _playerDb.Find(id);
            if (player == null) return HttpNotFound();

            PlayerAvatarImage image = new PlayerAvatarImage(Server, player.AvatarName);
            PlayerViewModel viewmodel = player.ToPlayerViewModel();
            viewmodel.Avatar = image.Exists ? image.RelativePath : null;

            viewmodel.TeamSelectList = DropDownHelper.BuildTeamSelectList(_teamDb);

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerViewModel viewmodel, HttpPostedFileBase avatar)
        {
            if (ModelState.IsValid)
            {
                Player currentPlayer = _playerDb.Find(viewmodel.ID);
                if (currentPlayer == null) return HttpNotFound();

                currentPlayer.Name = viewmodel.Name;
                currentPlayer.RBI = viewmodel.RBI;
                currentPlayer.HomeRun = viewmodel.HomeRuns;
                currentPlayer.Average = viewmodel.Average;

                currentPlayer.TeamId = viewmodel.TeamId;

                try
                {
                    _playerDb.Update(currentPlayer);

                    if (avatar != null && avatar.ContentLength > 0)
                    {
                        PlayerAvatarImage currentImage = new PlayerAvatarImage(Server, currentPlayer.AvatarName);
                        try
                        {
                            System.IO.File.Delete(currentImage.AbsolutePath);
                        }
                        catch (Exception e)
                        {
                            //
                        }

                        var image = AvatarUploader.UploadFile2(Server, avatar, currentPlayer.ID, AvatarTypeEnum.Player);

                        currentPlayer.AvatarName = image.Name;
                        _playerDb.Update(currentPlayer);//updates the new image file name
                    }


                    return RedirectToAction("Details", new { id = currentPlayer.ID });
                }
                catch (CustomExceptionBase e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    //log error
                    ModelState.AddModelError("", GenericErrorMessage);
                }
            }

            viewmodel.TeamSelectList = DropDownHelper.BuildTeamSelectList(_teamDb);

            return View(viewmodel);
        }

        public ActionResult Delete(int id)
        {
            var player = _playerDb.Find(id);
            if (player == null) return HttpNotFound();

            PlayerAvatarImage image = new PlayerAvatarImage(Server, player.AvatarName);
            PlayerViewModel viewmodel = player.ToPlayerViewModel();
            viewmodel.Avatar = image.Exists ? image.RelativePath : null;

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PlayerViewModel viewmodel)
        {
            try
            {
                var player = _playerDb.Find(viewmodel.ID);
                if (player == null) return HttpNotFound();

                PlayerAvatarImage image = new PlayerAvatarImage(Server, player.AvatarName);

                try
                {
                    System.IO.File.Delete(image.AbsolutePath);
                }
                catch (Exception e)
                {
                    //log error...
                    //continue this method
                }
            }  
            catch (Exception e)
            {
                //log error
                ModelState.AddModelError("", GenericErrorMessage);
                return View(viewmodel);
            }

            try
            {
                _playerDb.Delete(viewmodel.ID);
            }
            catch (Exception e)
            {
                //log error, redirect to error view isntead
                ModelState.AddModelError("", GenericDatabaseError);
                return View(viewmodel);
            }

            return RedirectToAction("Index");
        }

    }
}
