using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface IRepository<T>  //T é um tipo qualquer a gente quer uma classe
    {
        //Metodos que vamos ultilizar
        IQueryable<T> Get(); //faz chamadas async
        Task<T> GetById(Expression<Func<T, bool>> predicate); //transformasmos em async
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
