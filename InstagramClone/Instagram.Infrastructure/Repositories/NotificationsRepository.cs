using Infrastructure.Repositories;
using Instagram.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class NotificationsRepository : GenericRepository<Notifications>
    {
        private InstagramDbContext _instagramDbContext;
        public NotificationsRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<NotificationsDTO> GetNotifications(int userId)
        {
            if (userId != null)
            {
                //var postsId = Find(x => x.PostOwnerId == userId && x.IsSeen == false).Select(x => x.PostId);

                //var notificationOwnerName = Find(x => x.PostOwnerId == userId).Select(x => x.UserId);

                //var posts = _instagramDbContext.Posts.ToList().Where(x => postsId.Contains(x.Id)).Select(x => new NotificationsDTO
                //{
                //    PostId = x.Id,
                //    PostImage = x.Images,
                //    //UserId = _instagramDbContext.Users.FirstOrDefault(u => u.Id == notificationOwnerName.Select(u.Id)),
                //    //UserName = _instagramDbContext.Users.FirstOrDefault(u=> u.Id == notificationOwnerName.Select(u.Id))
                //}).ToList();

                var postNotifications = (from p in _instagramDbContext.Posts
                                         join n in _instagramDbContext.Notifications
                                         on p.Id equals n.PostId
                                         join u in _instagramDbContext.Users
                                         on n.UserId equals u.Id
                                         where /*n.IsSeen == false && */ n.PostOwnerId == userId
                                         orderby n.CreatedDate descending
                                         select new NotificationsDTO
                                         {
                                             PostId = p.Id,
                                             PostImage = p.Images,
                                             UserId = u.Id,
                                             UserName = u.Name,
                                             CreateDateAgo = Ultitites.RelativeDate(u.CreatedDate)
                                         }).ToList();

                return new InstagramApiResponse<NotificationsDTO>
                {
                    DataList = postNotifications,
                    Success = true,
                    //OptionalBoolean = postNotifications.Count() > 0 ? true : false,
                    //OptionalBoolean = postNotifications.Where(x=> x.PostOwnerId == userId && x.IsSeen == false).ToList().Count() > 0 ? true : false,
                    OptionalBoolean = _instagramDbContext.Notifications.Any(x => x.PostOwnerId == userId && x.IsSeen == false)
                };
            }
            else
            {
                return new InstagramApiResponse<NotificationsDTO>
                {
                    Success = false
                };
            }
        }

        public InstagramApiResponse<Notifications> UpdateNotifications(int userId)
        {
            var notifications = _instagramDbContext.Notifications.Where(x => x.PostOwnerId == userId);

            if (notifications != null)
            {
                //_instagramDbContext.Notifications.RemoveRange(notifications);

                foreach (var item in notifications)
                {
                    item.IsSeen = true;
                }

                SaveChanges();

                return new InstagramApiResponse<Notifications>
                {
                    Success = true,
                    Data = new Notifications
                    {
                        IsSeen = false,
                    }
                };
            }
            else
            {
                return new InstagramApiResponse<Notifications>
                {
                    Success = false
                };
            }
        }
    }
}
