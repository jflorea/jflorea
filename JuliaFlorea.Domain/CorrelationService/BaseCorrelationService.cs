using JuliaFlorea.DataModel;

namespace JuliaFlorea.Domain.CorrelationService
{
    public abstract class BaseCorrelationService<T>
    {
        //protected readonly SystemDataSet DataSet;
        protected readonly AppDbContext DbContext;
        protected BaseCorrelationService(AppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public abstract T CorrelateData();
    }
}