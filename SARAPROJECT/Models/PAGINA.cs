﻿namespace SARAPROJECT.Models
{
    public class PAGINA
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public PAGINA()
        {
        }

        public PAGINA(int totalItems, int page, int pageSize = 10)
        {

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;

            }

            if (startPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {

                    startPage = endPage - 9;
                }

            }


            this.TotalItems = totalItems;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
            this.StartPage = startPage;
            this.EndPage = endPage;
        }
    }
}
