using AspNetCoreCursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace AspNetCoreCursovaya.Controllers
{
    public class AdminController : Controller
    {

        cursovayadbContext cursovayadbContext = new cursovayadbContext();

        // Обработка запроса на вывод страницы администратора
        [Authorize]
        public IActionResult admin_index()
        {
            return View();
        }

        // Обработка запроса на загрузку файла
        public IActionResult Download(string filename)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot//documents", filename.Replace("\\","//"));
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        // Метод для получения типа файла
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        // Метод возвращающий словарь соответсвия расширений файлов
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            };
        }

        // Обработка запроса на вывод страницы добавить партнера
        public IActionResult addParnterPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addParnterPage(partners partner, string category, IFormFile photoInPartner) //--------------------- 23.06 Скопировать адд с другого контроллера (они схожи по логике)
        {

            if (photoInPartner != null)
            {
                string fileExtension = Path.GetExtension(photoInPartner.FileName);
                if (fileExtension != ".img" && fileExtension != ".png" && fileExtension != ".jpeg")
                {
                    ModelState.AddModelError("file", "Неверный формат файла. Допустимые форматы: .img, .png, .jpeg");
                    ViewBag.Exeption = "Неверный формат файла. Допустимые форматы: .img, .png, .jpeg";
                    return RedirectToAction("ErorFile", "home");
                }
            }

            int MaxIdPartner;
            int MaxIdCategoryInPartner;

            if (cursovayadbContext.partners.Count() == 0)
            {
                MaxIdPartner = 1;
                MaxIdCategoryInPartner = 1;
            }
            else
            {
                MaxIdPartner = cursovayadbContext.partners.Max(p => p.idPartners) + 1;
                MaxIdCategoryInPartner = cursovayadbContext.category_in_partners.Max(p => p.idCategory_in_partners) + 1;
            }

            category_in_partners categoryInPartners = new category_in_partners();

            partner.idPartners = MaxIdPartner; //====================================== добавляем новые айдишники 
            categoryInPartners.idCategory_in_partners = MaxIdCategoryInPartner;

            string link = ""; // ссылка на файл
            //.................................................................. ДОБАВЛЕНИЕ ФАЙЛА, найти ссылку расположения добавляемого файла
            if (photoInPartner != null && photoInPartner.FileName.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ivano\\source\\repos\\AspNetCoreCursovaya" +
                    "\\AspNetCoreCursovaya\\wwwroot\\image\\", photoInPartner.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photoInPartner.CopyTo(stream);
                }

                link = filePath;
                partner.link_photo = link;
            }
            

            categoryInPartners.id_category =
                cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category).IdCategories;

            categoryInPartners.id_partner = partner.idPartners;

            cursovayadbContext.partners.Add(partner);
            cursovayadbContext.category_in_partners.Add(categoryInPartners);

            

            cursovayadbContext.SaveChanges();

            return RedirectToAction("partners", "home");

        }

        // Обработка запроса на вывод страницы добавить объявление
        public IActionResult addAdvertisementPage()
        {
            return View();
        }

        // Обработка запроса на вывод страницы добавить событие
        public IActionResult addEventPage()
        {
            return View();
        }

        // Обработка запроса на удаление отчета
        public IActionResult delleteReport(int idReport)  //......... Удаление отчета
        {
            cursovayadbContext.Reports.Remove(cursovayadbContext.Reports.SingleOrDefault(p => p.IdReport == idReport));
            cursovayadbContext.SaveChanges();

            return RedirectToAction("reports", "home");     ///..................................... 06.05 доделать удаление отчетов, добавить ко всем сущностям имеющим категорию в базе данных тригер на удаление записи в связующей таблице
        }

        public IActionResult delletePoster(int idPoster)  //......... Удаление объявления
        {
            cursovayadbContext.Posters.Remove(cursovayadbContext.Posters.SingleOrDefault(p => p.IdPoster == idPoster));
            cursovayadbContext.SaveChanges();

            return RedirectToAction("advertisement", "home");
        }

        public IActionResult delleteNews(int idNews)  //......... Удаление новости
        {
            cursovayadbContext.News.Remove(cursovayadbContext.News.SingleOrDefault(p => p.IdNews == idNews));
            cursovayadbContext.SaveChanges();            

            return RedirectToAction("news", "home", 1);
        }

        // Обработка запроса на Удаление события
        public IActionResult delleteEvent(int idEvent)
        {
            cursovayadbContext.Events.Remove(cursovayadbContext.Events.SingleOrDefault(p => p.IdEvents == idEvent));
            cursovayadbContext.SaveChanges();

            return RedirectToAction("calendar_events", "home", new { categoryInEvent =  cursovayadbContext.Events } );
        }

        // Обработка запроса на удаление документа
        public IActionResult delleteDocument(int idDocument)
        {
            cursovayadbContext.Documents.Remove(cursovayadbContext.Documents.SingleOrDefault(p => p.IdDocuments == idDocument));
            cursovayadbContext.SaveChanges();

            return RedirectToAction("aboutUs", "home");
        }

        // Обработка запроса на удаление коментария
        public IActionResult delleteComment(int idComment, int newsId, int pageIndex)
        {
            cursovayadbContext.Comments.Remove(cursovayadbContext.Comments.SingleOrDefault(p => p.Idcomments == idComment));
            cursovayadbContext.SaveChanges();

            return RedirectToAction("pageNews", "home", new { id = newsId, numberOfPage = pageIndex });
        }

        // Обработка запроса на добавление объявления
        [HttpPost]
        public IActionResult addAdvertisement(Poster poster, string category)
        {
            if (string.IsNullOrWhiteSpace(poster.TitlePoster) | string.IsNullOrWhiteSpace(poster.TitlePoster))
            {
                return RedirectToAction("advertisement", "home");
            }

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

        // Обработка запроса на добавление события
        public IActionResult addEvent(Event @event, string category)
        {
            if (string.IsNullOrWhiteSpace(@event.TitleEvent) | string.IsNullOrWhiteSpace(@event.TextEvent))
            {
                return RedirectToAction("calendar_events", "home", cursovayadbContext.Events);
            }

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

        // Обработка запроса на добавление отчета
        [HttpPost]
        public IActionResult addReport(IFormFile file, string category, DateTime dateStart) // добавление нового отчета
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".txt" && fileExtension != ".doc" && fileExtension != ".docx")
                {
                    ModelState.AddModelError("file", "Неверный формат файла. Допустимые форматы: .txt, .doc, .docx");
                    return RedirectToAction("ErorFile", "home");
                }
            }

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

            string link = ""; // ссылка на файл
            //.................................................................. ДОБАВЛЕНИЕ ФАЙЛА, найти ссылку расположения добавляемого файла
            if (file != null && file.FileName.Length > 0)
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
            if (dateStart.Year != 1)
            {
                newReport.DatePublication = dateStart;
            }
            else
            {
                newReport.DatePublication = DateTime.Now;
            }
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

        // Обработка запроса на добавление документа
        public IActionResult addDocument(IFormFile file, string category)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".txt" && fileExtension != ".doc" && fileExtension != ".docx")
                {
                    ModelState.AddModelError("file", "Неверный формат файла. Допустимые форматы: .txt, .doc, .docx");
                    ViewBag.Exeption = "Неверный формат файла. Допустимые форматы: .txt, .doc, .docx";
                    return RedirectToAction("ErorFile", "home");
                }
            }

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

            string link = ""; // ссылка на файл
            //.................................................................. ДОБАВЛЕНИЕ ФАЙЛА, найти ссылку расположения добавляемого файла
            if (file != null && file.FileName.Length > 0)
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

        // Обработка запроса на вывод страницы добавить документ
        public IActionResult addDocumentPage()
        {
            return View();
        }
        /// <summary>
        /// /////////////
        /// </summary>
        /// <returns></returns>

        // Обработка запроса на вывод страницы добавить новость
        [HttpGet]
        public IActionResult addNewsPage()
        {
            return View();
        }

        // Обработка запроса на вывод страницы изменить новость
        [HttpGet]
        [Authorize]
        public IActionResult changeNewsPage(int newsId)
        {
            var changedNews = cursovayadbContext.News.Include(p => p.PhotoInNews).SingleOrDefault(p => p.IdNews == newsId);

            if (changedNews == null)
            {
                return RedirectToAction("Eror", "home");
            }

            return View("addNewsPage", changedNews);
        }

        // Обработка запроса на вывод страницы изменить объявление
        [HttpGet]
        [Authorize]
        public IActionResult changeAdvertisement(int adsId)
        {
            var changedAds = cursovayadbContext.CategoryInPosters.Include(p => p.IdPosterNavigation).Include(p => p.IdCategoryNavigation).SingleOrDefault(p => p.IdPoster == adsId);

            if (changedAds == null)
            {
                return RedirectToAction("Eror", "home");
            }

            return View("addAdvertisementPage", changedAds);
        }

        // Обработка запроса на изменение новости
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

                cursovayadbContext.PhotoInNews.Single(p => p.IdNews == newsId).Link = photoInNews.FileName;
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

        // Обработка запроса на изменение объявления
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

        // Обработка запроса на вывод страницы изменить новость
        [HttpGet]
        [Authorize]
        public IActionResult changeEvent(int idEvent)
        {
            var tempEvent = cursovayadbContext.CategoryInEvents.Include(p => p.IdEventNavigation).Include(p => p.IdCategoryInEventsNavigation).SingleOrDefault(p => p.IdEvent == idEvent);

            return View("addEventPage", tempEvent);
        }

        // Обработка запроса на изменение события
        [HttpPost]
        public IActionResult changeEvent(string title, DateTime dateStart, DateTime dateEnd, TimeSpan timeStart, TimeSpan timeEnd, string Text , string category, int idEvent)
        {
            var tempEvent = cursovayadbContext.CategoryInEvents.Include(p => p.IdCategoryInEventsNavigation).Include(p => p.IdEventNavigation).SingleOrDefault(p => p.IdEvent == idEvent);

            if (category != "Категория события" && category != tempEvent.IdCategoryInEventsNavigation.TitleCategory)
            {
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdCategoryInEvents = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category).IdCategories;
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdCategoryInEventsNavigation = cursovayadbContext.Categories.SingleOrDefault(p => p.TitleCategory == category);
            }

            if (title != tempEvent.IdEventNavigation.TitleEvent | dateStart != tempEvent.IdEventNavigation.DateStart | dateEnd != tempEvent.IdEventNavigation.DateEnd | timeStart != tempEvent.IdEventNavigation.TimeStart | timeEnd != tempEvent.IdEventNavigation.TimeEnd | Text != tempEvent.IdEventNavigation.TextEvent)
            {
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdEventNavigation.TitleEvent = title;
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdEventNavigation.DateStart = dateStart;
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdEventNavigation.DateEnd = dateEnd;
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdEventNavigation.TimeStart = timeStart;
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdEventNavigation.TimeEnd = timeEnd;
                cursovayadbContext.CategoryInEvents.SingleOrDefault(p => p.IdEvent == idEvent).IdEventNavigation.TextEvent = Text;
            }

            cursovayadbContext.SaveChanges();

            return RedirectToAction("pageCalendarEvent", "home", new { idEvent = tempEvent.IdEvent });
        }

        // Обработка запроса на добавление новости
        [HttpPost]
        public IActionResult addNews(News news, IFormFile photoInNews)
        {

            
            //if (photoInNews != null)
            //{
            //    string fileExtension = Path.GetExtension(photoInNews.FileName);
            //    if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
            //    {
            //        ModelState.AddModelError("photoInNews", "Неверный формат файла. Допустимые форматы: .jpg, .jpeg, .png");
            //        return RedirectToAction("news", "home");
            //    }
            //}

            int MaxIdFnews;

            if (cursovayadbContext.News.Count() == 0)
            {
                MaxIdFnews = 1;
            }
            else
            {
                MaxIdFnews = cursovayadbContext.News.Max(p => p.IdNews);
            }

            news.IdNews = MaxIdFnews + 1;
            news.DatePublication = DateTime.Now;

            if (photoInNews != null && photoInNews.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ivano\\source\\repos\\AspNetCoreCursovaya" +
                    "\\AspNetCoreCursovaya\\wwwroot\\image\\", photoInNews.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photoInNews.CopyTo(stream);
                }

                string fileExtension = Path.GetExtension(photoInNews.FileName);

                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                {
                    ModelState.AddModelError("photoInNews", "Неверный формат файла. Допустимые форматы: .jpg, .jpeg, .png");
                    return RedirectToAction("news", "home");
                }

                PhotoInNews NewPhoto = new PhotoInNews();
                int MaxIdFphotoList = cursovayadbContext.PhotoInNews.Max(p => p.IdPhoto);
                NewPhoto.IdPhoto = MaxIdFphotoList + 1;
                NewPhoto.Link = photoInNews.FileName;
                NewPhoto.IdNewsNavigation = news;
                NewPhoto.IdNews = news.IdNews;

                cursovayadbContext.PhotoInNews.Add(NewPhoto);
            }

            
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

        // Обработка запроса на вывод страницы добавить отчет
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
