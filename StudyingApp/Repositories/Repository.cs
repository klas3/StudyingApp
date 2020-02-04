using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyingApp.Data;

namespace StudyingApp.Repositories
{
    public class Repository : IRepository
    {
        private StudiyingAppContext _context;

        public Repository(StudiyingAppContext context)
        {
            _context = context;
        }
    }
}
