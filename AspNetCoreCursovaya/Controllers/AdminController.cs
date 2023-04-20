using AspNetCoreCursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;

namespace AspNetCoreCursovaya.Controllers
{
    public class AdminController : Controller
    {

        cursovayadbContext cursovayadbContext = new cursovayadbContext();

        [Authorize]
        public IActionResult admin_index()
        {
            return View();
        }

        public IActionResult addAdvertisementPage()
        {
            return View();
        }

        public IActionResult addEventPage()
        {
            return View();
        }

        public IActionResult addDocumentPage()
        {
            return View();
        }
        /// <summary>
        /// /////////////
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult addNewsPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addNews(News news, IFormFile photoInNews)
        {
            if (photoInNews != null && photoInNews.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ivano\\source\\repos\\AspNetCoreCursovaya" +
                    "\\AspNetCoreCursovaya\\wwwroot\\image\\", photoInNews.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photoInNews.CopyTo(stream);
                }
            }

            int MaxIdFnews = cursovayadbContext.News.Max(p => p.IdNews);
            news.IdNews = MaxIdFnews + 1;
            news.DatePublication = DateTime.Now;

            PhotoInNews NewPhoto = new PhotoInNews();
            int MaxIdFphotoList = cursovayadbContext.PhotoInNews.Max(p => p.IdPhoto);
            NewPhoto.IdPhoto = MaxIdFphotoList + 1;
            NewPhoto.Link = photoInNews.FileName;
            NewPhoto.IdNewsNavigation = news;
            NewPhoto.IdNews = news.IdNews;

            cursovayadbContext.PhotoInNews.Add(NewPhoto);
            cursovayadbContext.News.Add(news);
            cursovayadbContext.SaveChanges();

            return RedirectToAction("news", "home");
        }
        

        //[HttpPost]
        //public IActionResult addNewsPage([FromForm] news news)
        //{

        //    ///// Подключить Entity Framework, доделать бд и создать модели


        //    //tempNews = Path.Combine(_environment.WebRootPath, "images", model.imagelink.FileName);
        //    //using (var stream = new FileStream(filePath, FileMode.Create))
        //    //{
        //    //    model.imagelink.CopyTo(stream);
        //    //}

        //    list_News.newsList.Add(tempNews);

        //    return RedirectToAction("news", "home", news);
        //}

        /// <summary>
        /// ///////////////////////
        /// </summary>
        /// <returns></returns>

        public IActionResult addReportPage()
        {
            return View();
        }
        

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
