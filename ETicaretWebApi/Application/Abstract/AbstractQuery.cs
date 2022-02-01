using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Abstract
{
    public abstract class AbstractQuery<T>
    {
        public abstract ETicaretDbContext _context { get; set; }
        public abstract T Handle();

    }
}
