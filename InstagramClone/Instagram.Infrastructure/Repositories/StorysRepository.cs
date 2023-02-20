using Infrastructure.Repositories;
using Instagram.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class StorysRepository : GenericRepository<Storys>
    {
        private InstagramDbContext _instagramDbContext;
        public StorysRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<StoryDTO> GetStorys(int sessionId)
        {
            var user = _instagramDbContext.Users.Any(x => x.Id == sessionId);
            var storys = new List<StoryDTO>();

            if (user)
            {
                var activeStorysUser = (from f in _instagramDbContext.Followers
                                        join s in _instagramDbContext.Storys
                                        on f.Follow equals s.UserId
                                        where f.Follower == sessionId && s.IsActive == true || s.UserId == sessionId
                                        select s.UserId).Distinct().ToList();

                foreach (var userId in activeStorysUser)
                {
                    storys.Add(new StoryDTO
                    {
                        UserId = userId,
                        StoryList = (from s in _instagramDbContext.Storys
                                     join u in _instagramDbContext.Users
                                     on s.UserId equals u.Id
                                     where s.UserId == userId
                                     orderby s.CreatedDate descending
                                     select new StoryDetailDTO
                                     {
                                         Id = s.Id,
                                         Images = s.Images,
                                         UserId = s.UserId,
                                         CreatedDateAgo = Ultitites.RelativeDate(s.CreatedDate),
                                         IsActive = s.IsActive,
                                         UserImage = u.Image,
                                         UserName = u.Name,
                                         IsSeen = _instagramDbContext.StoryView.Any(sv => sv.StoryId == s.Id && sv.UserId == sessionId),
                                         StoryDuration = s.FileDuration == null ? 0 : s.FileDuration
                                     }).ToList()
                    });
                }

                return new InstagramApiResponse<StoryDTO>
                {
                    DataList = storys,
                    Success = true
                };
            }
            else
            {
                return new InstagramApiResponse<StoryDTO>
                {
                    Success = false
                };
            }
        }

        public InstagramApiResponse<Storys> UploadStory(int userId, string images, int fileDuration)
        {
            var user = _instagramDbContext.Users.Any(x => x.Id == userId);

            if (user)
            {
                var newStory = new Storys
                {
                    UserId = userId,
                    Images = images,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    FileDuration = fileDuration
                };

                Add(newStory);
                SaveChanges();

                return new InstagramApiResponse<Storys>
                {
                    Data = newStory,
                    Success = true,
                    Message = "Story kaydedildi"
                };
            }
            else
            {
                return new InstagramApiResponse<Storys>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }
        }

        public InstagramApiResponse<Storys> DeleteStory(int storyId)
        {
            var story = Get(x => x.Id == storyId);

            if (story != null)
            {
                _instagramDbContext.Storys.Remove(story);
                SaveChanges();

                return new InstagramApiResponse<Storys>
                {
                    Success = true,
                    Message = "Story silindi"
                };
            }
            else
            {
                return new InstagramApiResponse<Storys>
                {
                    Success = false,
                    Message = "Bir hata oluştu"
                };
            }
        }
    }
}
