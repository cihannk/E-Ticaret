namespace ETicaretWebApi.Application.Abstract
{
    public interface IQuery<T> where T : class
    {
        public T Handle();
    }
}
