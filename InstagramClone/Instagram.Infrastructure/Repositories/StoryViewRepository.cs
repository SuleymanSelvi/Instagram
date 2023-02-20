using Infrastructure.Repositories;
using Instagram.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class StoryViewRepository : GenericRepository<StoryView>
    {
        private InstagramDbContext _instagramDbContext;
        public StoryViewRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<StoryViewDTO> UploadStoryView(int storyId, int sessionId)
        {
            var story = _instagramDbContext.Storys.Any(x => x.Id == storyId);

            if (story != null)
            {
                var isView = Get(x => x.StoryId == storyId && x.UserId == sessionId);

                if (isView == null)
                {
                    var newView = new StoryView
                    {
                        StoryId = storyId,
                        UserId = sessionId
                    };

                    Add(newView);
                    SaveChanges();

                    return new InstagramApiResponse<StoryViewDTO>
                    {
                        Success = true,
                        Message = "Story izlendi",
                        Data = new StoryViewDTO
                        {
                            IsSeen = true
                        }
                    };
                }
                else
                {
                    return new InstagramApiResponse<StoryViewDTO>
                    {
                        Success = false,
                        Message = "Story daha önceden izlendi",
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<StoryViewDTO>
                {
                    Success = false,
                    Message = "Story bulunamadı"
                };
            }
        }

    }
}
