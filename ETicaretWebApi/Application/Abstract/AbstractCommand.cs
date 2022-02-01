using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Abstract
{
    public abstract class AbstractCommand
    {
        public abstract ETicaretDbContext _context { get; set; }
        public abstract void Handle();
    }
}
