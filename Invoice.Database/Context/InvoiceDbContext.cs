using Invoice.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Database.Context
{
    public class InvoiceDbContext : DbContext, IContext
    {
        public IDbSet<Company> CompanyRepository { get; set; }
        public IDbSet<Customer> CustomerRepository { get; set; }
        public IDbSet<RssChannel_SDIC> RssChannelSdicRepository { get; set; }
        public IDbSet<RssSiteChannel> RssSiteChannelRepository { get; set; }
        public IDbSet<ParameterGlobal> ParameterGlobalRepository { get; set; }
        public IDbSet<Post> PostRepository { get; set; }
        public IDbSet<PostCategory_SDIC> PostCategorySdicRepository { get; set; }
        public IDbSet<Currency_SDIC> CurrencySdicRepository { get; set; }
        public IDbSet<Country_SDIC> CountrySdicRepository { get; set; }

        public InvoiceDbContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<InvoiceDbContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
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


        public void Commit()
        {
            this.SaveChanges();
        }
    }
}
