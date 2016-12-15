using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Database.Context
{
    public class EfUnitOfWork : DbContext, IUnitOfWork
    {
        private readonly EfGenericRepository<Company> _companyRepo;
        private readonly EfGenericRepository<Customer> _customerRepo;
        private readonly EfGenericRepository<RssChannel_SDIC> _rssChannelSdicRepo;
        private readonly EfGenericRepository<RssSiteChannel> _rssSiteChannelRepo;
        private readonly EfGenericRepository<ParameterGlobal> _parameterGlobalRepo;
        private readonly EfGenericRepository<Post> _postRepo;
        private readonly EfGenericRepository<PostCategory_SDIC> _postCategorySdicRepo;

        //public virtual DbSet<AspNetUsers> Users { get; set; }
        //public virtual DbSet<AspNetUserClaims> UserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogins> UserLogins { get; set; }
        //public virtual DbSet<AspNetRoles> Roles { get; set; }

        public DbSet<Company> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RssChannel_SDIC> RssChannelsSdic { get; set; }
        public DbSet<RssSiteChannel> RssSiteChannels { get; set; }
        public DbSet<ParameterGlobal> ParametersGlobal { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory_SDIC> PostCategoriedSdic { get; set; }

        public EfUnitOfWork(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<EfUnitOfWork>(null);
            _companyRepo = new EfGenericRepository<Company>(Orders);
            _customerRepo = new EfGenericRepository<Customer>(Customers);
            _rssChannelSdicRepo = new EfGenericRepository<RssChannel_SDIC>(RssChannelsSdic);
            _rssSiteChannelRepo = new EfGenericRepository<RssSiteChannel>(RssSiteChannels);
            _parameterGlobalRepo = new EfGenericRepository<ParameterGlobal>(ParametersGlobal);
            _postRepo = new EfGenericRepository<Post>(Posts);
            _postCategorySdicRepo = new EfGenericRepository<PostCategory_SDIC>(PostCategoriedSdic);
        }

        #region IUnitOfWork Implementation

        public IGenericRepository<Company> CompanyRepository
        {
            get { return _companyRepo; }
        }

        public IGenericRepository<Customer> CustomerRepository
        {
            get { return _customerRepo; }
        }
    
        public IGenericRepository<RssChannel_SDIC> RssChannelSdicRepository
        {
            get { return _rssChannelSdicRepo; }
        }

        public IGenericRepository<RssSiteChannel> RssSiteChannelRepository
        {
            get { return _rssSiteChannelRepo; }
        }

        public IGenericRepository<ParameterGlobal> ParameterGlobalRepository
        {
            get { return _parameterGlobalRepo; }
        }

        public IGenericRepository<Post> PostRepository
        {
            get { return _postRepo; }
        }

        public IGenericRepository<PostCategory_SDIC> PostCategorySdicRepository
        {
            get { return _postCategorySdicRepo; }
        }

        public void Commit()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Post>()
                .HasMany(p => p.PostCategory_SDIC)
                .WithMany(p => p.Post)
                .Map(p =>
                {
                    p.MapLeftKey("PostId");
                    p.MapRightKey("PostCategoryId");
                    p.ToTable("PostCategories");
                });
        }

        #endregion

    }
}
