using Infrastructure.Repositories;
using Instagram.Domain;
using Instagram.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class FollowersRepository : GenericRepository<Followers>
    {
        private InstagramDbContext _instagramDbContext;
        public FollowersRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<FollowersDTO> FollowUser(int followId, int followerId)
        {
            var follow = _instagramDbContext.Users.FirstOrDefault(x => x.Id == followId);
            var follower = _instagramDbContext.Users.FirstOrDefault(x => x.Id == followerId);

            if (follow != null && follower != null)
            {
                var existFollow = Find(x => x.Follow == followId && x.Follower == followerId).FirstOrDefault();

                if (existFollow == null)
                {
                    var newFollow = new Followers
                    {
                        Follow = followId,
                        UsersFollow = follow,
                        Follower = followerId,
                        UsersFollower = follower,
                        CreatedDate = DateTime.Now
                    };

                    Add(newFollow);
                    SaveChanges();

                    return new InstagramApiResponse<FollowersDTO>
                    {
                        Success = true,
                        Message = "Kullanıcı takip edildi",
                        Data = new FollowersDTO
                        {
                            IsFollow = true,
                            FollowCount = _instagramDbContext.Followers.Count(f => f.Follow == followId),
                            FollowerCount = _instagramDbContext.Followers.Count(f => f.Follower == followId)
                        }
                    };
                }
                else if (existFollow != null)
                {
                    _instagramDbContext.Followers.Remove(existFollow);
                    SaveChanges();

                    return new InstagramApiResponse<FollowersDTO>
                    {
                        Success = true,
                        Message = "Kullanıcı takipten çıkarıldı",
                        Data = new FollowersDTO
                        {
                            IsFollow = false,
                            FollowCount = _instagramDbContext.Followers.Count(f => f.Follow == followId),
                            FollowerCount = _instagramDbContext.Followers.Count(f => f.Follower == followId)
                        }
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<FollowersDTO>
                {
                    Success = true,
                    Message = "Kullanıcı takip edildi"
                };
            }
            return null;
        }

        public InstagramApiResponse<ChatDTO> GetUserFollowingForChat(int userId)
        {
            var following = _instagramDbContext.Followers.Where(x => x.Follower == userId).Select(x => new ChatDTO
            {
                Id = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follow).Id,
                Images = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follow).Image,
                Name = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follow).Name,
                About = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follow).About,
                MessageCount = 0,
                isOnline = false,
                isWriting = 0,
                LastMessage = "",
                IsCheck = false
            }).ToList();

            return new InstagramApiResponse<ChatDTO>
            {
                Success = true,
                DataList = following
            };
        }

        public InstagramApiResponse<UserDTO> GetUserFollowing(int userId)
        {
            var following = _instagramDbContext.Followers.Where(x => x.Follower == userId).Select(x => new UserDTO
            {
                Id = _instagramDbContext.Users.FirstOrDefault(u=> u.Id == x.Follow).Id,
                Images = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follow).Image,
                Name = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follow).Name,
                About = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follow).About
            }).ToList();

            return new InstagramApiResponse<UserDTO>
            {
                Success = true,
                DataList = following
            };
        }

        public InstagramApiResponse<UserDTO> GetUserFollowers(int userId)
        {
            var following = _instagramDbContext.Followers.Where(x => x.Follow == userId).Select(x => new UserDTO
            {
                Id = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follower).Id,
                Images = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follower).Image,
                Name = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follower).Name,
                About = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.Follower).About
            }).ToList();

            return new InstagramApiResponse<UserDTO>
            {
                Success = true,
                DataList = following
            };
        }
    }
}
