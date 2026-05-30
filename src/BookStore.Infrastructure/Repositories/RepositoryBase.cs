using BookStore.Application.Interfaces;
using BookStore.Infrastructure.Data;


namespace BookStore.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public RepositoryBase(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public virtual IEnumerable<T> GetAll() => _dbContext.Set<T>().ToList();

        public virtual IQueryable<T> GetAsQueryable()
        {
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;
            return ApplySoftDeleteFilter(_dbContext.Set<T>());
        }

        public virtual T? GetById(Guid id) => _dbContext.Set<T>().Find(id);
        
        public virtual bool Add(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception err)
            {
                return false;
            }
        }

        public virtual bool Update(T entity)
        {
            try
            {

                _dbContext.Set<T>().Update(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception err)
            {
                return false;
            }
        }

        public virtual bool Delete(Guid id)
        {
            var model = this.GetById(id);
            if(model != null) {
                _dbContext.Set<T>().Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public virtual bool Remove(Guid id) => this.Delete(id);

        protected virtual IQueryable<T> ApplySoftDeleteFilter(IQueryable<T> query)
        {
            var entityType = typeof(T);
            var deletedAtProperty = entityType.GetProperty("DeletedAt");
            
            if (deletedAtProperty != null && deletedAtProperty.PropertyType == typeof(DateTime?))
            {
                // Use reflection to create the filter
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType, "x");
                var property = System.Linq.Expressions.Expression.Property(parameter, "DeletedAt");
                var nullValue = System.Linq.Expressions.Expression.Constant(null, typeof(DateTime?));
                var condition = System.Linq.Expressions.Expression.Equal(property, nullValue);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(condition, parameter);
                
                return query.Where(lambda);
            }
            
            return query;
        }
    }
    
}