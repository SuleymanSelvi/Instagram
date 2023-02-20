using Infrastructure.Repositories;
using Instagram.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class PostsCommentLikeRepository : GenericRepository<PostsCommentLike>
    {
        private InstagramDbContext _instagramDbContext;
        public PostsCommentLikeRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }
    }
}
