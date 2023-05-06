using AspNetCoreCursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

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
        IActionResult delleteReport(int idReport)
        {
            cursovayadbContext.Reports.Remove(cursovayadbContext.Reports.SingleOrDefault(p => p.IdReport == idReport));
            cursovayadbContext.SaveChanges();

            return RedirectToAction("reports", "home");     ///..................................... 06.05 доделать удаление отчетов, добавить ко всем сущностям имеющим категорию в базе данных тригер на удаление записи в связующей таблице
        }


        [HttpPost]
        public IActionResult addAdvertisement(Poster poster, string category)
        {
            int MaxIdAdvert;
            int? MaxIdCategoryPoster;

            if (cursovayadbContext.Posters.Count() == 0)
            {
                MaxIdAdvert = 1;
                MaxIdCategoryPoster = 1;
            }
            else
            {
                MaxIdAdvert = cursovayadbContext.Posters.Max(p => p.IdPoster);
                MaxIdCategoryPoster = cursovayadbContext.CategoryInPosters.Max(p => p.idCategoryInPoster);
            }

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
            int MaxIdEvent;
            int? MaxIdCategoryEvent;

            if (cursovayadbContext.Events.Count() == 0)
            {
                MaxIdEvent = 1;
                MaxIdCategoryEvent = 1;
            }
            else
            {
                MaxIdEvent = cursovayadbContext.Events.Max(p => p.IdEvents);
                MaxIdCategoryEvent = cursovayadbContext.CategoryInEvents.Max(p => p.id_category_event_record);
            }

           

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
            int MaxIdReport;
            int MaxIdCategory;

            if (cursovayadbContext.Reports.Count() == 0)
            {
                MaxIdReport = 1;
                MaxIdCategory = 1;
            }
            else
            {
                MaxIdReport = cursovayadbContext.Reports.Max(p => p.IdReport) + 1;
                MaxIdCategory = cursovayadbContext.CategoryInReports.Max(p => p.idCategoryInReport) + 1;
            }

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
            int MaxIdDocument;
            int MaxIdCategory;

            if (cursovayadbContext.Documents.Count() == 0)
            {
                MaxIdDocument = 1;
                MaxIdCategory = 1;
            }
            else
            {
                MaxIdDocument = cursovayadbContext.Documents.Max(p => p.IdDocuments) + 1;
                MaxIdCategory = cursovayadbContext.CategoriesInDocuments.Max(p => p.IdCategoryInDocument).GetValueOrDefault() + 1;
            }

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

        [HttpGet]
        public IActionResult changeAdvertisement(int adsId)
        {
            var changedAds = cursovayadbContext.CategoryInPosters.Include(p => p.IdPosterNavigation).Include(p => p.IdCategoryNavigation).SingleOrDefault(p => p.IdPoster == adsId);

            return View("addAdvertisementPage", changedAds);
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
        public IActionResult changeAdvertisement(string title, string text, DateTime dateStart, DateTime dateEnd, string category, int adsCategoryId)
        {
            var tempAds = cursovayadbContext.CategoryInPosters.Include(p => p.IdPosterNavigation).Include(p => p.IdCategoryNavigation).SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId);

            //if (categoryInPoster.IdCategoryNavigation.TitleCategory != tempAds.IdCategoryNavigation.TitleCategory)
            //{
            //    cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.IdPoster == adsId).IdCategory = categoryInPoster.IdCategory;
            //    cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.IdPoster == adsId).IdCategoryNavigation = cursovayadbContext.Categories.SingleOrDefault();
            //}

            if (category != "Категория объявлений" && category != tempAds.IdCategoryNavigation.TitleCategory)
            {
                cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId).IdCategory = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category).IdCategories;
                cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId).IdCategoryNavigation = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category);
            }

            if (title != tempAds.IdPosterNavigation.TitlePoster | text != tempAds.IdPosterNavigation.TextPoster | dateStart != tempAds.IdPosterNavigation.DateStart | dateEnd != tempAds.IdPosterNavigation.DateEnd)
            {
                cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId).IdPosterNavigation.TitlePoster = title;
                cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId).IdPosterNavigation.TextPoster = text;
                cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId).IdPosterNavigation.DateStart = dateStart;
                cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId).IdPosterNavigation.DatePublication = dateStart; /////................................................................... Тут мы меняем дату публикации для изменения даты на форме
                cursovayadbContext.CategoryInPosters.SingleOrDefault(p => p.idCategoryInPoster == adsCategoryId).IdPosterNavigation.DateEnd = dateEnd;
            }

            cursovayadbContext.SaveChanges();

            return RedirectToAction("advertisement", "home");
        }

        [HttpPost]
        public IActionResult addNews(News news, IFormFile photoInNews)
        {
            int MaxIdFnews;

            if (cursovayadbContext.News.Count() == 0)
            {
                MaxIdFnews = 1;
            }
            else
            {
                MaxIdFnews = cursovayadbContext.News.Max(p => p.IdNews);
            }

            if (photoInNews != null && photoInNews.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ivano\\source\\repos\\AspNetCoreCursovaya" +
                    "\\AspNetCoreCursovaya\\wwwroot\\image\\", photoInNews.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photoInNews.CopyTo(stream);
                }
            }

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
