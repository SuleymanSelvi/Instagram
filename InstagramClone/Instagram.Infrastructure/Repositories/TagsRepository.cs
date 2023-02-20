using Infrastructure.Repositories;
using Instagram.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class TagsRepository: GenericRepository<Tags>
    {
        private InstagramDbContext _instagramDbContext;
        public TagsRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }
    }
}
