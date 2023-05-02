using AspNetCoreCursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        

        [HttpPost]
        public IActionResult addAdvertisement(Poster poster, string category)
        {
            int MaxIdAdvert = cursovayadbContext.Posters.Max(p => p.IdPoster);
            int? MaxIdCategoryPoster = cursovayadbContext.CategoryInPosters.Max(p => p.idCategoryInPoster);

            poster.DatePublication = poster.DateStart;
            poster.IdPoster = MaxIdAdvert + 1;

            CategoryInPoster newCategoryInPoster = new CategoryInPoster();

            newCategoryInPoster.idCategoryInPoster = MaxIdCategoryPoster + 1;
            newCategoryInPoster.IdCategory = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category).IdCategories;
            newCategoryInPoster.IdPoster = poster.IdPoster;

            newCategoryInPoster.IdCategoryNavigation = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category);
            newCategoryInPoster.IdPosterNavigation = poster;

            cursovayadbContext.CategoryInPosters.Add(newCategoryInPoster);
            cursovayadbContext.Posters.Add(poster);

            cursovayadbContext.SaveChanges();

            return RedirectToAction("advertisement", "home");
        }

        public IActionResult addEvent(Event @event, string category)
        {
            int MaxIdEvent = cursovayadbContext.Events.Max(p => p.IdEvents);
            int? MaxIdCategoryEvent = cursovayadbContext.CategoryInEvents.Max(p => p.id_category_event_record);

            @event.IdEvents = MaxIdEvent + 1;

            CategoryInEvent NewCategoryInEvent = new CategoryInEvent();

            NewCategoryInEvent.id_category_event_record = MaxIdCategoryEvent + 1;
            NewCategoryInEvent.IdEventNavigation = @event;
            NewCategoryInEvent.IdCategoryInEventsNavigation = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category);
            NewCategoryInEvent.IdEvent = @event.IdEvents;
            NewCategoryInEvent.IdCategoryInEvents = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category).IdCategories;

            cursovayadbContext.CategoryInEvents.Add(NewCategoryInEvent);
            cursovayadbContext.Events.Add(@event);

            cursovayadbContext.SaveChanges();

            return RedirectToAction("calendar_events", "home", cursovayadbContext.Events);
        }

        [HttpPost]
        public IActionResult addReport(IFormFile file, string category) // добавление нового отчета
        {
            string link = null; // ссылка на файл
            //.................................................................. ДОБАВЛЕНИЕ ФАЙЛА, найти ссылку расположения добавляемого файла
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ivano\\source\\repos\\AspNetCoreCursovaya" +
                    "\\AspNetCoreCursovaya\\wwwroot\\files\\", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                link = filePath;
            }

            int MaxIdReport = cursovayadbContext.Reports.Max(p => p.IdReport) + 1;
            int MaxIdCategory = cursovayadbContext.CategoryInReports.Max(p => p.idCategoryInReport) + 1;

            Report newReport = new Report(); // создание новых объектов и заполнение их полей для добавления в бд
            newReport.IdReport = MaxIdReport;
            newReport.DatePublication = DateTime.Now;
            newReport.Link = link;

            CategoryInReport categoryInReport = new CategoryInReport();
            categoryInReport.idCategoryInReport = MaxIdCategory;
            categoryInReport.IdCategory = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category).IdCategories;
            categoryInReport.IdReport = newReport.IdReport;
            categoryInReport.IdCategoryNavigation = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category);
            categoryInReport.IdReportNavigation = newReport;

            cursovayadbContext.CategoryInReports.Add(categoryInReport); // добавление объектов в бд
            cursovayadbContext.Reports.Add(newReport);

            cursovayadbContext.SaveChanges();


            return RedirectToAction("reports", "home");
        }
        
        public IActionResult addDocument(IFormFile file, string category)
        {
            string link = null; // ссылка на файл
            //.................................................................. ДОБАВЛЕНИЕ ФАЙЛА, найти ссылку расположения добавляемого файла
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ivano\\source\\repos\\AspNetCoreCursovaya" +
                    "\\AspNetCoreCursovaya\\wwwroot\\documents\\", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                link = filePath;
            }

            int MaxIdDocument = cursovayadbContext.Documents.Max(p => p.IdDocuments) + 1;
            int MaxIdCategory = cursovayadbContext.CategoriesInDocuments.Max(p => p.IdCategoryInDocument).GetValueOrDefault() + 1;

            Document newDocument = new Document();
            newDocument.IdDocuments = MaxIdDocument;
            newDocument.DatePublication = DateTime.Now;
            newDocument.Link = link;

            CategoriesInDocument categoriesInDocument = new CategoriesInDocument();
            categoriesInDocument.IdCategoryInDocument = MaxIdCategory;
            categoriesInDocument.IdCategoryNavigation = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category);
            categoriesInDocument.IdDocumentNavigation = newDocument;
            categoriesInDocument.IdCategory = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category).IdCategories;
            categoriesInDocument.IdDocument = newDocument.IdDocuments;

            cursovayadbContext.Documents.Add(newDocument);
            cursovayadbContext.CategoriesInDocuments.Add(categoriesInDocument);

            cursovayadbContext.SaveChanges();

            return RedirectToAction("aboutUs", "home");
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

        [HttpGet]
        public IActionResult changeNewsPage(int newsId)
        {
            var changedNews = cursovayadbContext.News.Include(p => p.PhotoInNews).SingleOrDefault(p => p.IdNews == newsId);

            return View("addNewsPage", changedNews);
        }

        [HttpPost]
        public IActionResult changeNewsPage(News changedNews, IFormFile photoInNews, int newsId) //................. 02.05. добавил возможность изменения новостей, сделать это для всех сущностей
        {
            if (photoInNews != null && photoInNews.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ivano\\source\\repos\\AspNetCoreCursovaya" +
                    "\\AspNetCoreCursovaya\\wwwroot\\image\\", photoInNews.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photoInNews.CopyTo(stream);
                }

                cursovayadbContext.PhotoInNews.Single(p => p.IdNews == newsId && p.IdNewsNavigation.TitleNews == changedNews.TitleNews).Link = photoInNews.FileName;
            }

            var tempNews = cursovayadbContext.News.SingleOrDefault(p => p.IdNews == newsId);

            if (tempNews.TitleNews != changedNews.TitleNews | tempNews.TextNews != changedNews.TextNews)
            {
                cursovayadbContext.News.SingleOrDefault(p => p.IdNews == newsId).TitleNews = changedNews.TitleNews;
                cursovayadbContext.News.SingleOrDefault(p => p.IdNews == newsId).TextNews = changedNews.TextNews;
            }

            cursovayadbContext.SaveChanges();

            ////////////////////////////////////////////////////////////////////////////////// oewjq;eklfj;kdlsajfkldjlkfajs;dlkjflksadjflkjdsa;jfkklj

            return RedirectToAction("news", "home");
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
