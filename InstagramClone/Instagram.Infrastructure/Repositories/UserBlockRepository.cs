using Infrastructure.Repositories;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class UserBlockRepository : GenericRepository<UserBlock>
    {
        private InstagramDbContext _instagramDbContext;
        public UserBlockRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<UserBlockDTO> BlockUser(int blockedId, int blockId)
        {
            var user = _instagramDbContext.Users.FirstOrDefault(x => x.Id == blockedId);

            if (user != null)
            {
                var exitsBlock = Get(x => x.BlockedId == blockedId && x.BlockId == blockId);

                if (exitsBlock == null)
                {
                    var newBlock = new UserBlock
                    {
                        BlockedId = blockedId,
                        BlockId = blockId,
                        CreatedDate = DateTime.Now
                    };
                    Add(newBlock);

                    var exitsFollow = _instagramDbContext.Followers.FirstOrDefault
                        (x => x.Follow == blockedId && x.Follower == blockId || x.Follow == blockId && x.Follower == blockedId);

                    if (exitsFollow != null)
                    {
                        _instagramDbContext.Followers.Remove(exitsFollow);
                    }
               
                    SaveChanges();

                    return new InstagramApiResponse<UserBlockDTO>
                    {
                        Success = true,
                        Message = "Kullanıcı engellendi",
                        Data = new UserBlockDTO
                        {
                            IsBlock = true,
                            IsFollow = false
                        }
                    };
                }
                else
                {
                    _instagramDbContext.UserBlock.Remove(exitsBlock);
                    SaveChanges();

                    return new InstagramApiResponse<UserBlockDTO>
                    {
                        Success = true,
                        Message = "Kullanıcı engeli kaldırıldı",
                        Data = new UserBlockDTO
                        {
                            IsBlock = false,
                        }
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<UserBlockDTO>
                {
                    Success = false,
                    Message = "Bir hata oluştu"
                };
            }
        }
    }
}
