namespace APICatalogo.Pagination
{
    public class QueryStringParameters
    {
        const int maxPageSize = 50; //numero maximo de itens por pagina

        public int PageNumber { get; set; } = 1; //pagina atual
        public int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; } //pegue o valor atual de pagina 

            set { _pageSize = _pageSize = (value > maxPageSize) ? maxPageSize : value; }

        }
    }
}
