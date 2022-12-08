using SARAPROJECT.Models;

namespace SARAPROJECT.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacion paginacion)
        {
            return queryable
                .Skip((paginacion.Pagina - 1) * paginacion.CantidadAMostrar)
                .Take(paginacion.CantidadAMostrar);
        }
    }
}
