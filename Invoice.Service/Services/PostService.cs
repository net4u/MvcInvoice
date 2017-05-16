using Invoice.Database;
using Invoice.Service.Base;
using Invoice.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Invoice.Database.Interfaces;

namespace Invoice.Service.Services
{
    public class PostService : EntityService<Post, int>, IPostService
    {
        public IEnumerable<PostCategory_SDIC> GetAllCategories()
        {
            return _dbContext.PostCategorySdicRepository;
        }

        public override IEnumerable<Post> GetPaged(int pageIndex, int pageSize)
        {
            return _dbSet
                    .Include(e => e.PostCategory_SDIC)
                    .OrderBy(e => e.Id)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public PostService(IContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Post>();
        }
    }
}
