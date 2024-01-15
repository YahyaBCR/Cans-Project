using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myprojectEF.Models;
using myprojectEF.Services;

namespace myprojectEF.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet()
        {

        }

        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
        {
            if(ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "The image file is required");
            }
            if(!ModelState.IsValid)
            {
                errorMessage = "SVP remplire tous les champs !";
                return;
            }

            // sauvegarder image
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/Products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                ProductDto.ImageFile.CopyTo(stream);
            }

            //ajouter dnas la base 
            Product product = new Product()
            {
                Nom = ProductDto.Nom,
                Brand = ProductDto.Brand,
                Categorie = ProductDto.Categorie,
                Prix = ProductDto.Prix,
                Description = ProductDto.Description ?? "",
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,
            };

            context.Products.Add(product);
            context.SaveChanges();

            //vider le formulqire
            ProductDto.Nom = "";
            ProductDto.Brand = "";
            ProductDto.Categorie = "";
            ProductDto.Prix = 0;
            ProductDto.Description = "";
            ProductDto.ImageFile = null;

            ModelState.Clear();

            successMessage = "Nouveau produit crée";

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
