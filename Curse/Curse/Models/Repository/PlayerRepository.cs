using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Models.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private SportsDataContext _db;

        public PlayerRepository()
        {
            _db = new SportsDataContext();
        }

        public PlayerRepository(SportsDataContext db)
        {
            _db = db;
        }

        public bool Add(Player player)
        {
            bool result = false;

            try
            {
                _db.Players.InsertOnSubmit(player);
                _db.SubmitChanges();
                result = true;
            }
            catch (Exception e)
            {
                //log exception
            }

            return result;
        }

        public IQueryable<Player> FindAll()
        {
            return _db.Players;
        }

        //should I use try/catch or let error bubble up?
        public Player Find(int id)
        {
            return _db.Players.Where(x => x.ID == id).FirstOrDefault();
        }

        public bool Update(Player player)
        {
            bool result = false;
            try
            {
                Player currentPlayer = _db.Players.Where(x => x.ID == player.ID).FirstOrDefault();

                if (currentPlayer != null)
                {
                    currentPlayer.Name = player.Name;
                    currentPlayer.AvatarName = player.AvatarName;

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

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Player player = _db.Players.Where(x => x.ID == id).FirstOrDefault();
                if (player != null)
                {
                    _db.Players.DeleteOnSubmit(player);
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