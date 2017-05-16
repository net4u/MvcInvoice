using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Database.Interfaces
{
    public interface IContext : IDisposable
    {
        IDbSet<Company> CompanyRepository { get; }
        IDbSet<Customer> CustomerRepository { get; }
        IDbSet<RssChannel_SDIC> RssChannelSdicRepository { get; }
        IDbSet<RssSiteChannel> RssSiteChannelRepository { get; }
        IDbSet<ParameterGlobal> ParameterGlobalRepository { get; }
        IDbSet<Post> PostRepository { get; }
        IDbSet<PostCategory_SDIC> PostCategorySdicRepository { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void Commit();

    }
}
