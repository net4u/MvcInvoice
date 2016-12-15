using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Database.Context
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<RssChannel_SDIC> RssChannelSdicRepository { get; }
        IGenericRepository<RssSiteChannel> RssSiteChannelRepository { get; }
        IGenericRepository<ParameterGlobal> ParameterGlobalRepository { get; }
        IGenericRepository<Post> PostRepository { get; }
        IGenericRepository<PostCategory_SDIC> PostCategorySdicRepository { get; }

        void Commit();
    }
}
