using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

public class ProductController : Controller
{
    private readonly IProductRepository _repo;
    private readonly IWebHostEnvironment _env;

    public ProductController(IProductRepository repo, IWebHostEnvironment env)
    {
        _repo = repo;
        _env = env;
    }

    public IActionResult Index() => View(_repo.GetAll());

    public IActionResult Details(int id) => View(_repo.GetById(id));

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Product product, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            if (image != null && image.Length > 0)
            {
                var path = Path.Combine(_env.WebRootPath, "images", image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                product.ImageUrl = "/images/" + image.FileName;
            }

            _repo.Add(product);
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public IActionResult Edit(int id) => View(_repo.GetById(id));

    [HttpPost]
    public IActionResult Edit(Product product, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            if (image != null && image.Length > 0)
            {
                var path = Path.Combine(_env.WebRootPath, "images", image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                product.ImageUrl = "/images/" + image.FileName;
            }

            _repo.Update(product);
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public IActionResult Delete(int id) => View(_repo.GetById(id));

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _repo.Delete(id);
        return RedirectToAction("Index");
    }
}