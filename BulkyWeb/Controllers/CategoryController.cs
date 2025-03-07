﻿using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display Order Cannot Be Exactly Match  Category Name");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                TempData["sucess"] = "Create Category Sucessfully!";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id) {
            if(id == null || id <= 0)
            {
                return NotFound();
            }

            Category? category = _dbContext.Categories.Find(id);
            if(category is null)
            {
                NotFound();
            }


            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj) { 
            if(ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                TempData["sucess"] = "Update Category Sucessfully!";

                return RedirectToAction("Index");
            }

            return View();
        }


        public IActionResult Delete(int? id)
        {
            if(id is null || id <= 0)
            {
                return NotFound();
            }
            Category? category = _dbContext.Categories.Find(id);
            if(category is null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id) {

            Category? category = _dbContext.Categories.Find(id);
            if(category is null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["sucess"] = "Delete Category Sucessfully!";

            return RedirectToAction("Index");
        }
    }
}
