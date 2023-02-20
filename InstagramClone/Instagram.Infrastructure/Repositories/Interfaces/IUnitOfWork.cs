using Instagram.Domain;
using Instagram.Infrastructure;
using Instagram.Infrastructure.Repositories;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        UsersRepository UsersRepository { get; }
        PostsRepository PostsRepository { get; }
        PostsLikeRepository PostsLikeRepository { get; }
        PostsCommentRepository PostsCommentRepository { get; }
        PostsCommentLikeRepository PostsCommentLikeRepository { get; }
        TagsRepository TagsRepository { get; }
        FollowersRepository FollowersRepository { get; }
        UserBlockRepository UserBlockRepository { get; }
        SavedPostsRepository SavedPostsRepository { get; }
        NotificationsRepository NotificationsRepository { get; }
        StorysRepository StorysRepository { get; }
        StoryViewRepository StoryViewRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        // Dependency Injection =  User s= new User();
        private readonly InstagramDbContext _context;
        private ICacheService _redisCacheService;

        public UnitOfWork(InstagramDbContext context, ICacheService redisCacheService)
        {
            _context = context;
            _redisCacheService = redisCacheService;
        }

        private UsersRepository _usersRepository;
        public UsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new UsersRepository(_context);
                }

                return _usersRepository;
            }
        }


        private PostsRepository _postsRepository;
        public PostsRepository PostsRepository
        {
            get
            {
                if (_postsRepository == null)
                {
                    _postsRepository = new PostsRepository(_context, _redisCacheService);
                }

                return _postsRepository;
            }
        }


        private PostsLikeRepository _postsLikeRepository;
        public PostsLikeRepository PostsLikeRepository
        {
            get
            {
                if (_postsLikeRepository == null)
                {
                    _postsLikeRepository = new PostsLikeRepository(_context);
                }

                return _postsLikeRepository;
            }
        }


        private PostsCommentRepository _postsCommentRepository;
        public PostsCommentRepository PostsCommentRepository
        {
            get
            {
                if (_postsCommentRepository == null)
                {
                    _postsCommentRepository = new PostsCommentRepository(_context);
                }

                return _postsCommentRepository;
            }
        }


        private PostsCommentLikeRepository _postsCommentLikeRepository;
        public PostsCommentLikeRepository PostsCommentLikeRepository
        {
            get
            {
                if (_postsCommentLikeRepository == null)
                {
                    _postsCommentLikeRepository = new PostsCommentLikeRepository(_context);
                }

                return _postsCommentLikeRepository;
            }
        }


        private TagsRepository _tagsRepository;
        public TagsRepository TagsRepository
        {
            get
            {
                if (_tagsRepository == null)
                {
                    _tagsRepository = new TagsRepository(_context);
                }

                return _tagsRepository;
            }
        }


        private FollowersRepository _followersRepository;
        public FollowersRepository FollowersRepository
        {
            get
            {
                if (_followersRepository == null)
                {
                    _followersRepository = new FollowersRepository(_context);
                }

                return _followersRepository;
            }
        }


        private UserBlockRepository _userBlockRepository;
        public UserBlockRepository UserBlockRepository
        {
            get
            {
                if (_userBlockRepository == null)
                {
                    _userBlockRepository = new UserBlockRepository(_context);
                }

                return _userBlockRepository;
            }
        }


        private SavedPostsRepository _savedPostsRepository;
        public SavedPostsRepository SavedPostsRepository
        {
            get
            {
                if (_savedPostsRepository == null)
                {
                    _savedPostsRepository = new SavedPostsRepository(_context);
                }

                return _savedPostsRepository;
            }
        }


        private NotificationsRepository _notificationsRepository;
        public NotificationsRepository NotificationsRepository
        {
            get
            {
                if (_notificationsRepository == null)
                {
                    _notificationsRepository = new NotificationsRepository(_context);
                }

                return _notificationsRepository;
            }
        }


        private StorysRepository _storysRepository;
        public StorysRepository StorysRepository
        {
            get
            {
                if (_storysRepository == null)
                {
                    _storysRepository = new StorysRepository(_context);
                }

                return _storysRepository;
            }
        }


        private StoryViewRepository _storyViewRepository;
        public StoryViewRepository StoryViewRepository
        {
            get
            {
                if (_storyViewRepository == null)
                {
                    _storyViewRepository = new StoryViewRepository(_context);
                }

                return _storyViewRepository;
            }
        }
    }
}
