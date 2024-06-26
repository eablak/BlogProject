﻿using BlogProject.Infrastructure.Context;
using BlogProject.Infrastructure.Repositories.Abstract;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Concrete
{
   public class AppUserRepository : BaseRepository<AppUser>,IAppUserRepository
    {

        public AppUserRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }

      
    }
}
