using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public class Repository<T> : IRepository<T> where T : class //recebemos uma interface e implementamos ela 
    {
        protected AppDbContext _context;
        //injetando o context
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        //OBS - Toda vez que usar o Set<T> é uma refencia do contexto retorna uma instancia DbSet<T> para o acesso pelo contexto 

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking(); //lista de entidades e otimizando a resposta da api com AsNoTracking
        }
        public async Task<T> GetById(Expression<Func<T, bool>> predicate) //transformar em async
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }



        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}
