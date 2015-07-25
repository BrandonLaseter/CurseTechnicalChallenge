using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Models.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private SportsDataContext _db;

        public TeamRepository()
        {
            _db = new SportsDataContext();
        }

        public TeamRepository(SportsDataContext db)
        {
            _db = db;
        }

        public bool Add(Team team)
        {
            bool result = false;

            try
            {
                _db.Teams.InsertOnSubmit(team);
                _db.SubmitChanges();
                result = true;
            }
            catch (Exception e)
            {
                //log error message
            }

            return result;
        }

        public IQueryable<Team> FindAll()
        {
            return _db.Teams;
        }

        public Team Find(int id)
        {
            return _db.Teams.Where(x => x.ID == id).FirstOrDefault();
        }

        public bool Update(Team team)
        {            
            bool result = false;
            try
            {
                Team currentTeam = _db.Teams.Where(x => x.ID == team.ID).FirstOrDefault();

                if (currentTeam != null)
                {
                    currentTeam.Name = team.Name;
                    currentTeam.AvatarName = team.AvatarName;

                    _db.SubmitChanges();
                }

                result = true;
            }
            catch (Exception e)
            {
                //log exception...
            }

            return result;
        }

        //team with any players cannot be deleted
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Team team = _db.Teams.Where(x => x.ID == id).FirstOrDefault();
                if (team != null)
                {
                    _db.Teams.DeleteOnSubmit(team);
                    _db.SubmitChanges();
                    result = true;
                }
            }
            catch (Exception e)
            {
                //log error
            }

            return result;
        }
    }
}