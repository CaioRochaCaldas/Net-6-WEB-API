using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Pagination
{
    public class PagedList<T> : List<T> // regra de paginação
    {
        public PagedList(List<T> items,int count,int pageNumber,int pageSize) //informações a serem tratadas na pagina
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); //total de iten a paginar dividido por quanto ainda falta a paginar
            AddRange(items);
        }

        public async static Task<PagedList<T>> ToPagedList(IQueryable<T> source,int pageNumber,int pageSize)
        {
            var count = source.Count(); //calculo de quanto falta a paginar
            var items = await source.Skip((pageNumber - 1) + pageSize).Take(pageSize).ToListAsync();//itens a retornar 

            return new PagedList<T>(items, count, pageNumber, pageSize); //nova lista a paginar 
        }

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;    
        public bool HasNext => CurrentPage < TotalPages;
    }
}
