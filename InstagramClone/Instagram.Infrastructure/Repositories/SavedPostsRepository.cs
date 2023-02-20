using Infrastructure.Repositories;
using Instagram.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class SavedPostsRepository : GenericRepository<SavedPosts>
    {
        private InstagramDbContext _instagramDbContext;
        public SavedPostsRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<SavedPostsDTO> SavedPosts(int postId, int userId)
        {
            var post = _instagramDbContext.Posts.FirstOrDefault(x => x.Id == postId);

            if (post != null)
            {
                var existSave = Get(x => x.PostId == postId && x.UserId == userId);

                if (existSave == null)
                {
                    var newSave = new SavedPosts
                    {
                        PostId = postId,
                        UserId = userId,
                        CreatedDate = DateTime.Now
                    };

                    Add(newSave);
                    SaveChanges();

                    return new InstagramApiResponse<SavedPostsDTO>
                    {
                        Success = true,
                        Message = "Gönderi kaydedildi",
                        Data = new SavedPostsDTO
                        {
                            IsSaved = true,
                        }
                    };
                }
                else
                {
                    _instagramDbContext.Remove(existSave);
                    SaveChanges();

                    return new InstagramApiResponse<SavedPostsDTO>
                    {
                        Success = true,
                        Message = "Gönderi kaydı silindi",
                        Data = new SavedPostsDTO
                        {
                            IsSaved = false,
                        }
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<SavedPostsDTO>
                {
                    Success = false,
                    Message = "Bir hata oluştu"
                };
            }
        }

        public InstagramApiResponse<PostsDTO> GetSavedPosts(int userId)
        {
            var user = _instagramDbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (user != null)
            {
                var savedPosts = _instagramDbContext.SavedPosts.Where(x => x.UserId == userId).Select(x => new PostsDTO
                {
                    Id = _instagramDbContext.Posts.FirstOrDefault(p => p.Id == x.PostId).Id,
                    Images = _instagramDbContext.Posts.FirstOrDefault(p => p.Id == x.PostId).Images
                }).ToList();

                return new InstagramApiResponse<PostsDTO>
                {
                    DataList = savedPosts,
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
