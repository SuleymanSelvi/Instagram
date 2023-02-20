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
    public class PostsLikeRepository : GenericRepository<PostsLike>
    {
        private InstagramDbContext _instagramDbContext;
        public PostsLikeRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<PostLikeDTO> PostLike(int postId, int userId,int postOwnerId)
        {
            var post = _instagramDbContext.Posts.FirstOrDefault(x => x.Id == postId);

            if (post != null && !string.IsNullOrEmpty(postId.ToString()) && !string.IsNullOrEmpty(userId.ToString()))
            {
                var existLike = Find(x => x.PostId == postId && x.UserId == userId).FirstOrDefault();
                var existNotifications = _instagramDbContext.Notifications.Where(x => x.PostId == postId && x.UserId == userId).FirstOrDefault();

                if (existLike == null)
                {
                    var addLike = new PostsLike
                    {
                        PostId = postId,
                        UserId = userId,
                        LikeType = 1,
                        CreatedDate = DateTime.Now
                    };

                    if (existNotifications == null)
                    {
                        var newNotifications = new Notifications
                        {
                            PostId = postId,
                            PostOwnerId = postOwnerId,
                            UserId = userId,
                            CreatedDate = DateTime.Now,
                            IsSeen = false
                        };

                        _instagramDbContext.Add(newNotifications);
                    }

                    Add(addLike);
                 

                    SaveChanges();

                    return new InstagramApiResponse<PostLikeDTO>
                    {
                        Success = true,
                        Message = "Post Beğenildi",
                        Data = new PostLikeDTO
                        {
                            IsLike = true,
                            LikeCount = _instagramDbContext.PostsLike.Count(l => l.PostId == postId)
                        }
                    };
                }
                else if (existLike != null)
                {
                    _instagramDbContext.PostsLike.Remove(existLike);

                    if (existNotifications != null)
                    {
                        _instagramDbContext.Notifications.Remove(existNotifications);
                    }

                    SaveChanges();

                    return new InstagramApiResponse<PostLikeDTO>
                    {
                        Success = true,
                        Message = "Post beğenisi silindi",
                        Data = new PostLikeDTO
                        {
                            IsLike = false,
                            LikeCount = _instagramDbContext.PostsLike.Count(l => l.PostId == postId)
                        }
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<PostLikeDTO>
                {
                    Success = false,
                    Message = "Bir hata oluştu"
                };

            }
            return null;
        }

        public InstagramApiResponse<PostsDTO> GetLikedPosts(int userId)
        {
            if (userId != null)
            {
                var likePosts = _instagramDbContext.PostsLike.Where(x => x.UserId == userId).Select(x => new PostsDTO
                {
                    Id = _instagramDbContext.Posts.FirstOrDefault(p=> p.Id == x.PostId).Id,
                    Images = _instagramDbContext.Posts.FirstOrDefault(p=> p.Id == x.PostId).Images
                }).ToList();

                return new InstagramApiResponse<PostsDTO>
                {
                    DataList = likePosts,
                    Success = true
                };
            }
            else
            {
                return new InstagramApiResponse<PostsDTO> 
                { 
                    Success = false,
                    Message = "Bir hata oluştu"
                };
            }
        }
    }
}
