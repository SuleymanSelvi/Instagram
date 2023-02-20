using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repositories
{
    public class UsersRepository : GenericRepository<Users>
    {
        private InstagramDbContext _instagramDbContext;
        public UsersRepository(InstagramDbContext instagramDbContext) : base(instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public InstagramApiResponse<Users> Login(string name, string password)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
            {
                var user = _instagramDbContext.Users.FirstOrDefault(x => x.Name == name && x.Password == password && x.IsActive == true);

                if (user != null)
                {
                    return new InstagramApiResponse<Users>
                    {
                        Success = true,
                        Data = user
                    };
                }
                else
                {
                    return new InstagramApiResponse<Users>
                    {
                        Success = false,
                        Message = "Lütfen bilgilerinizi kontrol ediniz"
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<Users>
                {
                    Success = false,
                    Error = "Lütfen tüm alanları doldurunuz"
                };
            }
        }

        public InstagramApiResponse<Users> Registir(string name, string password, string email, string about)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(about))
            {
                var isExist = _instagramDbContext.Users.Any(x => x.Name == name || x.Email == email);

                if (!isExist)
                {
                    var newUser = new Users
                    {
                        Name = name,
                        Password = password,
                        Email = email,
                        Image = "/Images/profile.jpeg",
                        About = about,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                    };

                    Add(newUser);
                    SaveChanges();

                    return new InstagramApiResponse<Users>
                    {
                        Success = true,
                        Data = newUser
                    };
                }
                else
                {
                    return new InstagramApiResponse<Users>
                    {
                        Success = false,
                        Message = "Bu Kullanıcı Adı veya Email adresi kullanılmaktadır !",
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<Users>
                {
                    Success = false,
                    Error = "Lütfen tüm alanları doldurunuz !",
                };
            }
        }

        public InstagramApiResponse<Users> DeleteAccount(int userId, string password)
        {
            var user = Get(x => x.Id == userId);

            if (user != null)
            {
                bool existPassword = user.Password == password;

                if (existPassword == true)
                {
                    user.IsActive = false;

                    SaveChanges();

                    return new InstagramApiResponse<Users>
                    {
                        Success = true,
                        Message = "Hesap silindi"
                    };
                }
                else
                {
                    return new InstagramApiResponse<Users>
                    {
                        Success = false,
                        Message = "Hatalı şifre"
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<Users>
                {
                    Success = false,
                    Message = "Bir hata oluştu"
                };
            }
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            Random random = new Random();

            var users = All().Where(x=> x.IsActive == true).Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Password = x.Password,
                Images = x.Image,
                Email = x.Email,
                About = x.About,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
            }).OrderBy(x => random.Next()).Take(6).ToList();

            return users;
        }

        public InstagramApiResponse<UserDTO> GetUserProfile(int userId, int sessionUserId)
        {
            var user = Find(x => x.Id == userId).Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Password = x.Password,
                Images = x.Image,
                Email = x.Email,
                About = x.About,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                FollowCount = _instagramDbContext.Followers.Count(f => f.Follow == x.Id),
                FollowerCount = _instagramDbContext.Followers.Count(f => f.Follower == x.Id),
                PostCount = _instagramDbContext.Posts.Count(x => x.UserId == userId),
                IsFollow = _instagramDbContext.Followers.Any(f => f.Follow == userId && f.Follower == sessionUserId),
                IsBlock = _instagramDbContext.UserBlock.Any(b => b.BlockedId == userId && b.BlockId == sessionUserId)
            }).FirstOrDefault();

            return new InstagramApiResponse<UserDTO>
            {
                Data = user,
                Success = true,
            };
        }

        public InstagramApiResponse<UserDTO> SearchUsers(string userName)
        {
            var users = Find(x => x.Name.Contains(userName) && x.IsActive == true).Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Password = x.Password,
                Images = x.Image,
                Email = x.Email,
                About = x.About,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive
            }).ToList();

            return new InstagramApiResponse<UserDTO>
            {
                Success = true,
                DataList = users
            };
        }

        public InstagramApiResponse<Users> UpdateProfile(int userId, string name, string password, string image, string email, string about)
        {
            var user = Get(x => x.Id == userId);

            if (user != null)
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password)/* && !string.IsNullOrEmpty(image)*/ && !string.IsNullOrEmpty(email))
                {
                    user.Name = name;
                    user.Password = password;
                    user.Image = image;
                    user.Email = email;
                    user.About = about;

                    SaveChanges();

                    return new InstagramApiResponse<Users>
                    {
                        Success = true,
                        Data = user
                    };
                }

                return new InstagramApiResponse<Users>
                {
                    Success = false,
                    Message = "Lütfen gerekli alanları doldurunuz"
                };
            }
            else
            {
                return new InstagramApiResponse<Users>
                {
                    Success = false,
                    Message = "Bir hata oluştu"
                };
            }
        }
    }
}
