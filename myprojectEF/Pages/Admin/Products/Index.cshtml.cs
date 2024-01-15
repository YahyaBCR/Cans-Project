using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myprojectEF.Models;
using myprojectEF.Services;

namespace myprojectEF.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        //Pagination
        public int pageIndex = 1;
        public int totalPages = 0;
        private readonly int pageSize = 5;

        public string search = "";

        //trier
        public string column = "Id";
        public string orderBy = "desc";




        public List<Product> Products { get; set; } = new List<Product>();

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void OnGet(int? pageIndex, string? search, string? column, string? orderBy)
        {
            IQueryable<Product> query = context.Products;


            //chercher des produits !
            if(search != null)
            {
                this.search = search;
                query = query.Where(p => p.Nom.Contains(search) || p.Brand.Contains(search));
            }


            //trier les produits
            string[] validColumns = { "Id", "Nom", "Brand", "Categorie", "Prix", "CreatedAt" };
            string[] validOrderBy = { "desc", "asc" };


            if (!validColumns.Contains(column))
            {
                column = "Id";
            }

            if (!validOrderBy.Contains(orderBy))
            {
                orderBy = "desc";
            }

            this.column = column;
            this.orderBy = orderBy;

            if(column == "Nom")
            {
                if(orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Nom);                
                }
                else
                {
                    query = query.OrderByDescending(p => p.Nom);
                }
            }
            else if (column == "Brand")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Brand);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Brand);
                }
            }
            else if (column == "Categorie")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Categorie);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Categorie);
                }
            }
            else if (column == "Prix")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Prix);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Prix);
                }
            }
            else if (column == "CreatedAt")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.CreatedAt);
                }
                else
                {
                    query = query.OrderByDescending(p => p.CreatedAt);
                }
            }
            else 
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Id);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }
            //query = query.OrderByDescending(p => p.Id);
            //Products = context.Products.OrderByDescending(p => p.Id).ToList();

            //pagination
            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            this.pageIndex = (int) pageIndex;

            decimal count = query.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((this.pageIndex - 1) * pageSize)
                .Take(pageSize);


            Products = query.ToList();

        }
    }
}
