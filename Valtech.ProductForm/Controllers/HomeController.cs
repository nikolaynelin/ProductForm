using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Valtech.ProductForm.Context;
using Valtech.ProductForm.Models;
using Valtech.ProductForm.Repositories;

namespace Valtech.ProductForm.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public HomeController(IMapper mapper, IRepository repository) : base(mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public ActionResult Index()
        {
            var domain = _repository.Get<Product>().Include(x => x.Category).ToList();
            var result = MapTo<ProductVm>(domain);
            return View(result);
        }

        public ActionResult PreEdit(int productId)
        {
            var product = _repository.Get<Product>(productId);
            var productVm = _mapper.Map<ProductVm>(product);

            productVm.Categories = GetCategories();
            TempData["product"] = productVm;
            return View("LicenceTerm");
        }

        private List<CategoryVm> GetCategories()
        {
            var categories = _repository.Get<Category>().ToList();
            return MapTo<CategoryVm>(categories);
        }

        public ActionResult Edit()
        {
            if (TempData["product"] == null)
            {
                return RedirectToAction("Index");
            }
            var product = TempData["product"];

            return View("AddEdit", product);
        }

        public ActionResult Save(ProductVm product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = GetErrors();
                product.Categories = GetCategories();
                return View("AddEdit", product);
            }

            SaveProduct(product);

            return RedirectToAction("Index");
        }

        private void SaveProduct(ProductVm product)
        {
            var productToSave = _mapper.Map<Product>(product);
            productToSave.CategoryId = _repository.Get<Category>(x => x.Name == productToSave.Category.Name).First().Id;
            productToSave.Category = null;

            if (productToSave.Id == 0)
            {
                _repository.Add(productToSave);
            }
            else
            {
                _repository.Update(productToSave);
            }
            _repository.Commit();
        }
    }
}