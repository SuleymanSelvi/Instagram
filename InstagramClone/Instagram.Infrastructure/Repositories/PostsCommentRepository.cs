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
    public class PostsCommentRepository : GenericRepository<PostsComment>
    {
        private InstagramDbContext _instagramDbContext;
        public PostsCommentRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<PostCommentsDTO> GetPostComments(int postId)
        {
            var postComments = Find(x => x.PostId == postId && x.SubComment == 0).Select(x => new PostCommentsDTO
            {
                Id = x.Id,
                PostId = x.PostId,
                UserId = x.UserId,
                Comment = x.Comment,
                CreatedDate = DateTime.Now,
                UserName = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Name,
                UserImage = _instagramDbContext.Users.FirstOrDefault(u => u.Id == x.UserId).Image,
                CreatedDateAgo = Ultitites.RelativeDate(x.CreatedDate),
                SubComment = Find(j => j.SubComment == x.Id).Select(s => new PostCommentsDTO
                {
                    Id = s.Id,
                    PostId = s.PostId,
                    UserId = s.UserId,
                    Comment = s.Comment,
                    CreatedDate = DateTime.Now,
                    UserName = _instagramDbContext.Users.FirstOrDefault(u => u.Id == s.UserId).Name,
                    UserImage = _instagramDbContext.Users.FirstOrDefault(u => u.Id == s.UserId).Image,
                    CreatedDateAgo = Ultitites.RelativeDate(s.CreatedDate),
                }).ToList()
            }).ToList();

            return new InstagramApiResponse<PostCommentsDTO>
            {
                DataList = postComments,
                Success = true
            };
        }

        public InstagramApiResponse<PostCommentsDTO> UploadComment(int postId, int userId, string comment, int subComment)
        {
            var post = _instagramDbContext.Posts.FirstOrDefault(x => x.Id == postId);

            if (post != null && !string.IsNullOrEmpty(postId.ToString()) && !string.IsNullOrEmpty(userId.ToString()) && !string.IsNullOrEmpty(comment))
            {
                var newComment = new PostsComment
                {
                    PostId = postId,
                    UserId = userId,
                    Comment = comment,
                    CreatedDate = DateTime.Now,
                    SubComment = subComment,
                };

                Add(newComment);
                SaveChanges();

                return new InstagramApiResponse<PostCommentsDTO>
                {
                    Data = new PostCommentsDTO()
                    {
                        Comment = comment,
                    },
                    Success = true,
                    Message = "Yorum Eklendi",
                    Count = _instagramDbContext.PostsComment.Count(p => p.PostId == postId)
                };
            }
            else
            {
                return new InstagramApiResponse<PostCommentsDTO>
                {
                    Success = false,
                    Error = "Bir hata oluştu"
                };
            }

            return null;
        }

        public InstagramApiResponse<PostsComment> DeletePostComment(int postCommentId)
        {
            var postComment = Get(x => x.Id == postCommentId);

            if (postComment != null)
            {
                var subComment = Get(x => x.SubComment == postCommentId);

                _instagramDbContext.Remove(postComment);

                if (subComment != null)
                {
                    _instagramDbContext.Remove(subComment);
                }

                SaveChanges();

                return new InstagramApiResponse<PostsComment>
                {
                    Success = true,
                    Count = _instagramDbContext.PostsComment.Count(p => p.PostId == postComment.PostId)
                };
            }
            else
            {
                return new InstagramApiResponse<PostsComment>
                {
                    Success = false
                };
            }
        }
    }
}
