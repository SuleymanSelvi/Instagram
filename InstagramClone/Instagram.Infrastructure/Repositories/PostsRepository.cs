using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Repositories
{
    public class PostsRepository : GenericRepository<Posts>
    {
        private ICacheService _redisCacheService;
        private InstagramDbContext _instagramDbContext;

        public PostsRepository(InstagramDbContext instagramDbContext, ICacheService redisCacheService) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
            _redisCacheService = redisCacheService;
        }

        public InstagramApiResponse<PostsDTO> GetTopPosts(int skip, int take, int sessionUserId)
        {
            Random random = new Random();

            var exitsRedis = _redisCacheService.Exist("PostList");

            if (!exitsRedis)
            {
                var posts = All().Skip(skip).Take(take).Select(x => new PostsDTO
                {
                    Id = x.Id,
                    Images = x.Images,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive,
                    //TagsId = x.TagsId,
                    UserId = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Id,
                    UserName = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Name,
                    UserImage = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Image,
                    LikeCount = _instagramDbContext.PostsLike.Count(l => l.PostId == x.Id && l.LikeType == 1),
                    CommentCount = _instagramDbContext.PostsComment.Count(l => l.PostId == x.Id),
                    existUserLike = _instagramDbContext.PostsLike.Any(l => l.PostId == x.Id && l.UserId == sessionUserId),
                    CreatedDateAgo = Ultitites.RelativeDate(x.CreatedDate)
                }).OrderBy(x => random.Next()).ToList();

                _redisCacheService.SetValueAsync("PostList", JsonConvert.SerializeObject(posts));

                return new InstagramApiResponse<PostsDTO>
                {
                    DataList = posts,
                    Success = true
                };
            }
            else
            {
                var redisValue = _redisCacheService.GetValueAsync("PostList").Result;
                var list = JsonConvert.DeserializeObject<List<PostsDTO>>(redisValue);

                return new InstagramApiResponse<PostsDTO>
                {
                    DataList = list,
                    Success = true
                };
            }
        }

        public InstagramApiResponse<PostsDTO> GetPostsByFollowId(int skip, int take, int sessionUserId)
        {
            Random random = new Random();

            var followsId = _instagramDbContext.Followers.Where(x => x.Follower == sessionUserId).Select(x => x.Follow);

            if (followsId != null)
            {
                var posts = _instagramDbContext.Posts.ToList().Where
                    (x => followsId.Contains(x.UserId) || x.UserId == sessionUserId && x.IsActive == true).Skip(skip).Take(take).Select(x => new PostsDTO
                {
                    Id = x.Id,
                    Images = x.Images,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive,
                    //TagsId = x.TagsId,
                    UserId = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Id,
                    UserName = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Name,
                    UserImage = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Image,
                    LikeCount = _instagramDbContext.PostsLike.Count(l => l.PostId == x.Id && l.LikeType == 1),
                    CommentCount = _instagramDbContext.PostsComment.Count(l => l.PostId == x.Id),
                    existUserLike = _instagramDbContext.PostsLike.Any(l => l.PostId == x.Id && l.UserId == sessionUserId),
                    existSaved = _instagramDbContext.SavedPosts.Any(s => s.PostId == x.Id && s.UserId == sessionUserId),
                    CreatedDateAgo = Ultitites.RelativeDate(x.CreatedDate)
                }).OrderBy(x => random.Next()).ToList();

                return new InstagramApiResponse<PostsDTO>
                {
                    DataList = posts,
                    Success = true
                };
            }
            else
            {
                return new InstagramApiResponse<PostsDTO>
                {
                    Success = false
                };
            }
        }

        public InstagramApiResponse<PostsDTO> GetPostDetail(int postId, int sessionUserId)
        {
            var post = Find(x => x.Id == postId).Select(x => new PostsDTO
            {
                Id = x.Id,
                Images = x.Images,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                //TagsId = x.TagsId,
                UserId = x.UserId,
                UserName = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Name,
                UserImage = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Image,
                LikeCount = _instagramDbContext.PostsLike.Count(l => l.PostId == x.Id && l.LikeType == 1),
                CommentCount = _instagramDbContext.PostsComment.Count(l => l.PostId == x.Id),
                existUserLike = _instagramDbContext.PostsLike.Any(l => l.PostId == x.Id && l.UserId == sessionUserId),
                existSaved = _instagramDbContext.SavedPosts.Any(s => s.PostId == x.Id && s.UserId == sessionUserId),
                CreatedDateAgo = Ultitites.RelativeDate(x.CreatedDate)
            }).FirstOrDefault();

            return new InstagramApiResponse<PostsDTO>
            {
                Data = post,
                Success = true
            };
        }

        public InstagramApiResponse<PostsDTO> GetPostsByUserId(int userId)
        {
            var posts = Find(x => x.UserId == userId && x.IsActive == true).Select(x => new PostsDTO
            {
                Id = x.Id,
                Images = x.Images,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                UserId = x.UserId,
                UserName = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Name,
                UserImage = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Image,
                LikeCount = _instagramDbContext.PostsLike.Count(l => l.PostId == x.Id && l.LikeType == 1),
                CommentCount = _instagramDbContext.PostsComment.Count(l => l.PostId == x.Id),
                existUserLike = _instagramDbContext.PostsLike.Any(l => l.PostId == x.Id && l.UserId == userId),
                existSaved = _instagramDbContext.SavedPosts.Any(s => s.PostId == x.Id && s.UserId == userId),
                CreatedDateAgo = Ultitites.RelativeDate(x.CreatedDate)
            }).OrderByDescending(x => x.CreatedDate).ToList();

            return new InstagramApiResponse<PostsDTO>
            {
                DataList = posts,
                Success = true
            };
        }

        public InstagramApiResponse<Posts> UploadPost(int userId, string images, string description)
        {
            var user = _instagramDbContext.Users.Any(x => x.Id == userId);

            if (user != null)
            {
                var newPost = new Posts
                {
                    Images = images,
                    UserId = userId,
                    Description = description,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    TagsId = 1
                };

                Add(newPost);
                SaveChanges();

                return new InstagramApiResponse<Posts>
                {
                    Success = true,
                    Data = newPost,
                    Message = "Post paylaşıldı"
                };
            }
            else
            {
                return new InstagramApiResponse<Posts>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı !"
                };
            }
        }

        public InstagramApiResponse<Posts> DeletePost(int postId)
        {
            var post = Get(x => x.Id == postId);

            if (post != null)
            {

                post.IsActive = false;
                //_instagramDbContext.Remove(post);
                SaveChanges();

                return new InstagramApiResponse<Posts>
                {
                    Success = true
                };
            }
            else
            {
                return new InstagramApiResponse<Posts>
                {
                    Success = false,
                    Message = "Bir hata oluştu"
                };
            }
        }
    }
}
