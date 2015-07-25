using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curse.Models.Repository
{
    public interface ITeamRepository
    {
        bool Add(Team team);
        IQueryable<Team> FindAll();
        Team Find(int id);
        bool Update(Team team);
        bool Delete(int id);
    }
}
