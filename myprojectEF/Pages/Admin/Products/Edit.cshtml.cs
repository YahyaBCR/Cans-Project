using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myprojectEF.Models;
using myprojectEF.Services;

namespace myprojectEF.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto(); 

        public Product Product { get; set; } = new Product();

        public string errorMessage = "";
        public string successMessage = "";

        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if(id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }
            var product = context.Products.Find(id);
            if(product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            ProductDto.Nom = product.Nom;
            ProductDto.Brand = product.Brand;
            ProductDto.Categorie = product.Categorie;
            ProductDto.Prix = product.Prix;
            ProductDto.Description = product.Description;

            Product = product;
        }

        public void OnPost(int? id)
        {
            if(id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "svp remplir tous les champs !!";
                return;
            }

            var product = context.Products.Find(id);
            if(product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            //update the image
            string newFileName = product.ImageFileName;
            if(ProductDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(ProductDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductDto.ImageFile.CopyTo(stream);
                }
                //supprimer l'ancienne image 
                string oldImageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }




            //update the product 

            product.Nom = ProductDto.Nom;
            product.Brand = ProductDto.Brand;
            product.Categorie = ProductDto.Categorie;
            product.Prix = ProductDto.Prix;
            product.Description = ProductDto.Description ?? "";
            product.ImageFileName = newFileName;

            context.SaveChanges();

            Product = product;

            successMessage = "Produit modifié avec succes !";
            Response.Redirect("/Admin/Products/Index");

        }
    }
}
