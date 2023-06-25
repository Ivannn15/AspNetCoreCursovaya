using AspNetCoreCursovaya.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AspNetCoreCursovaya.helpingClasses;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.IO;

namespace AspNetCoreCursovaya.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        cursovayadbContext cursovayadb = new cursovayadbContext();

        // Обработка get запроса для вывода начальной страницы
        public IActionResult Index()
        {
            string myCookieValue = Request.Cookies["YourSessionCookieName"];
            return View();

            // Временно отключаем для проверки авторизации на хостинге
            if (Request.Cookies["name"] == "admin")
            {
                    // Авторизация пользователя как администратор
                    var claims = new List<Claim>
                    {
                        new Claim("IsAdmin", "true")
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            }
            return View();
        }

        public IActionResult partners ()
        {
            PartnersAndCategory partnersAndCategory = new PartnersAndCategory()
            {
                partners = cursovayadb.partners.ToList(),
                category_In_Partners = cursovayadb.category_in_partners.ToList()
            };

            return View(partnersAndCategory);
        }

        // Обработка get запроса для вывода ошибки
        public IActionResult Eror()
        {
            return View();
        }

        // Обработка get запроса для вывода ошибки связанной с неправильным форматом файла
        public IActionResult ErorFile()
        {
            return View();
        }

        // Обработка get запроса для вывода страницы о нас
        public IActionResult aboutUs()
        {
            return View(cursovayadb.CategoriesInDocuments.Include(p => p.IdDocumentNavigation));
        }

        // Обработка запросы для скачивания файла
        public IActionResult Download(string filename)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot//documents", filename.Replace("\\", "//"));
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        // Метод возвращающий тип файла
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public IActionResult PageAdvertisement(int idAdvert)
        {
            return View(cursovayadb.Posters.SingleOrDefault(p => p.IdPoster == idAdvert));
        }

        // метод который возвращает словарь соответствия расширений файлов и их MIME-типов. 
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

        // Обработка запроса на вывод страницы объявления
        public IActionResult advertisement()
        {
            var adverts = cursovayadb.Posters;
            return View(adverts);
        }

        // Обработка запроса на вывод страницы активности
        public IActionResult pageActivities2()
        {
            return View();
        }

        // Обработка запроса на вывод страницы активности
        public IActionResult pageActivities3()
        {
            return View();
        }

        // Обработка запроса на вывод страницы активности
        public IActionResult pageActivities4()
        {
            return View();
        }

        // Обработка запроса на вывод страницы активности
        public IActionResult pageActivities5()
        {
            return View();
        }

        // Обработка запроса на вывод страницы активности
        public IActionResult pageActivities6()
        {
            return View();
        }

        // Обработка запроса на вывод страницы календарь
        [HttpGet]
        public IActionResult pageCalendarEvent(int idEvent)
        {
            var tempEvent = cursovayadb.CategoryInEvents.Include(p => p.IdEventNavigation).Include(p => p.IdCategoryInEventsNavigation).SingleOrDefault(p => p.IdEvent == idEvent); 

            if (tempEvent == null)
            {
                return View("Eror");
            }

            return View(tempEvent);
        }

        // Обработка запроса на вывод страницы новости
        public async Task<IActionResult> news(int? page)
        {
            //cursovayadb.News.Add

            //var newsItems = cursovayadb.News.Include(p => p.PhotoInNews).ToList(); /////////// // // last variant

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var newsItems = await PaginatedList<News>.CreateAsync(cursovayadb.News.Include(p => p.PhotoInNews).AsNoTracking(), pageNumber, pageSize);


            return View(newsItems);
        }

        // Обработка запроса на вывод страницы наша активность
        public IActionResult ourActivities()
        {
            return View();
        }

        // Обработка запроса на вывод страницы активности
        public IActionResult pageActivities()
        {
            return View();
        }

        // Обработка запроса на вывод страницы партнеры
        //public IActionResult partners()
        //{
        //    return View();
        //}

        // Обработка запроса на вывод страницы отчеты
        public IActionResult reports()
        {
            return View(cursovayadb.CategoryInReports.Include(p => p.IdReportNavigation).AsNoTracking());
        }

        // Обработка запроса на вывод страницы новости
        public async Task<IActionResult> pageNews(int id, int? numberOfPage)
        {
            
            //var tempNews = cursovayadb.News.Include(p => p.PhotoInNews).SingleOrDefault(p => p.IdNews == id); // разобраться почему контроллер вызывается 2 раза
            var temp = cursovayadb.PhotoInNews.Include(p => p.IdNewsNavigation).SingleOrDefault(p => p.IdNews == id);
            
            int pageSize = 4;

            ViewBag.Id = id;

            int pageNumber = (numberOfPage ?? 1);
            var newsItems = await PaginatedList<News>.CreateAsync(cursovayadb.News.Include(p => p.PhotoInNews).Include(p => p.Comments).AsNoTracking(), pageNumber, pageSize);
            var a = newsItems.Count();
            return View(newsItems);
        }

        // Обработка запроса на добавление коментария
        [HttpPost]
        public async Task<IActionResult> addComment(string commentatorName, string commentatorText, int newsId, int pageIndex)
        {
            int MaxId;

            if (cursovayadb.Comments.Count() == 0)
            {
                MaxId = 1;
            }
            else
            {
                MaxId = cursovayadb.Comments.Max(p => p.Idcomments) +1;
            }

            if (string.IsNullOrWhiteSpace(commentatorName))
            {
                commentatorName = "Анонимный пользователь";
            }

            if (string.IsNullOrWhiteSpace(commentatorText))
            {
                return RedirectToAction("pageNews", new { id = newsId, numberOfPage = pageIndex });
            }



            var News = cursovayadb.News.SingleOrDefault(p => p.IdNews == newsId);

            Comment NewComment = new Comment
            {
                Idcomments = MaxId,
                TextComment = commentatorText,
                NameCommentator = commentatorName,
                DatePublication = DateTime.Now,
                IdNewsNavigation = News,
                IdNews = newsId,
            };

            

            cursovayadb.Comments.Add(NewComment);
            cursovayadb.SaveChanges();

            return RedirectToAction("pageNews", new { id = newsId, numberOfPage = pageIndex });;
        }

        // Обработка запроса на вывод страницы написать нам
        public IActionResult writeUs()
        {
            return View();
        }

        // Обработка запроса на переключение месяца в календаре
        [HttpGet]
        public IActionResult calendar_events(int monthNow)
        {
            if (monthNow == 0)
            {
                return View(cursovayadb.Events.Where(p => p.DateStart.Value.Month == DateTime.Now.Month).ToList());
            }
            else
            {
                var listEvent = cursovayadb.Events.Where(p => p.DateStart.Value.Month == monthNow).ToList();
                if (listEvent.Count == 0)
                {
                    var lastDayOfMonth = cursovayadb.Events.Where(p => p.DateStart.Value.Month == cursovayadb.Events.Max(e => e.DateStart.Value.Month)).Max(e => e.DateStart); // поиск последнего месяца в котором были события
                    listEvent = cursovayadb.Events.Where(p => p.DateStart.Value.Month == lastDayOfMonth.Value.Month).ToList();

                    return View(listEvent);
                }
                return View(listEvent);
            }
        }

        //[HttpGet]
        //public IActionResult lastMonth(int monthNow) 
        //{
        //    var listEvent = cursovayadb.Events.Where(p => p.DateStart.Value.Month == monthNow - 1).ToList();
        //    return RedirectToAction("calendar_events", "home", listEvent);
        //}

        //[HttpGet]
        //public IActionResult calendar_events(List<Event> events)
        //{
        //    return View(cursovayadb.Events);
        //}

        // Обработка запроса на вывод страницы события
        [HttpGet]
        public IActionResult calend_events(CategoryInEvent categoryInEvent)
        {
            return View(categoryInEvent);
        }

        /////////// 25.04 Добавить вывод эвентов в представление календарь ивентов, разобраться с сопоставлением дат текущего месяца и даты проведения эвента

        // Обработка запроса на вывод страницы авторизации
        [HttpGet("admin")]
        public IActionResult authorization()
        {
            return View(); 
        }

        // Обработка запроса для авторизации
        public async Task<IActionResult> authorizationPost(string username, string password)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////// 20.05 Проверить авторизацию с таким кодом перезалить сайт

            //string passwrd = password;

            //char[] charArray = username.ToCharArray();
            //Array.Reverse(charArray);
            //string salt = new string(charArray);

            //var sha256 = new SHA256CryptoServiceProvider();
            //byte[] bytes = Encoding.UTF8.GetBytes(passwrd + salt);
            //byte[] hashBytes = sha256.ComputeHash(bytes);
            //string hash = Convert.ToBase64String(hashBytes);

            //Admin admin = new Admin();

            //var temp_user = cursovayadb.Admins.SingleOrDefault(p => p.Username == username && p.HashPassword == hash);

            //if (temp_user == null)
            //{
            //    ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
            //    return View("Eror");
            //}

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            Response.Cookies.Append("name", "admin");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            ViewBag.AutorizeUser = true;

            return RedirectToAction("admin_index", "admin");
        }

        //[HttpGet("login/{username}/{password}")]
        //public async Task<IActionResult> AddNewUserAsync(string username, string password)
        //{
        //    Admin admin = new Admin();


        //    //if (username != "jone" && password != "123")
        //    //{
        //    //    ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
        //    //}

        //    var temp_user = cursovayadb.Admins.SingleOrDefault(p => p.Username == username && p.HashPassword == password);

        //    if (temp_user == null)
        //    {
        //        ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
        //        return View("index");
        //    }


        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, username),
        //    };
        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var principal = new ClaimsPrincipal(identity);
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //    return RedirectToAction("admin_index", "admin");
        //}

        // Обработка запроса на выход из аккаунта администратора
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("name"); // удаляем куку "name"

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // удаляем аутентификационную куку
            await HttpContext.SignOutAsync(); // удаляем все куки
            return RedirectToAction("Index", "Home");
        }

        //[AllowAnonymous]
        //[HttpGet("login/{username}/{password}")]
        //public IActionResult Login(string username, string password)
        //{
        //    // проверьте логин и пароль в базе данных
        //    if (username == "admin" && password == "123")
        //    {
        //        // создайте токен доступа
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes("my_secret_key_123");
        //        //var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JwtSecret"));

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Expires = DateTime.UtcNow.AddDays(7),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        //        admin.token = token.SigningCredentials.ToString();

        //        var client = new HttpClient();
        //        //var a = tokenHandler.WriteToken(token);
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenHandler.WriteToken(token));

        //        return RedirectToAction("admin_index", "admin");

        //    }
        //    else
        //    {
        //        // если логин и пароль неверные, верните ошибку
        //        return Unauthorized();
        //    }
        //}




        //[HttpGet("login/{username}/{password}")]
        //public IActionResult Login(string username, string password)
        //{
        //    if (username == "admin" && password == "123")
        //    {
        //        autor
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        // Обработка запроса на вывод ошибки
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
