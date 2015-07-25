using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curse.Models.Repository
{
    public interface IPlayerRepository
    {
        bool Add(Player player);
        IQueryable<Player> FindAll();
        Player Find(int id);
        bool Update(Player player);
        bool Delete(int id);
    }
}
