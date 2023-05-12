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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult aboutUs()
        {
            return View(cursovayadb.CategoriesInDocuments.Include(p => p.IdDocumentNavigation));
        }

        public IActionResult advertisement()
        {
            var adverts = cursovayadb.Posters;
            return View(adverts);
        }

        public IActionResult pageActivities2()
        {
            return View();
        }

        public IActionResult pageActivities3()
        {
            return View();
        }

        public IActionResult pageActivities4()
        {
            return View();
        }

        public IActionResult pageActivities5()
        {
            return View();
        }

        public IActionResult pageActivities6()
        {
            return View();
        }

        [HttpGet]
        public IActionResult pageCalendarEvent(int idEvent)
        {
            var tempEvent = cursovayadb.CategoryInEvents.Include(p => p.IdEventNavigation).Include(p => p.IdCategoryInEventsNavigation).SingleOrDefault(p => p.IdEvent == idEvent); 

            return View(tempEvent);
        }

        public async Task<IActionResult> news(int? page)
        {
            //cursovayadb.News.Add

            //var newsItems = cursovayadb.News.Include(p => p.PhotoInNews).ToList(); /////////// // // last variant

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var newsItems = await PaginatedList<News>.CreateAsync(cursovayadb.News.Include(p => p.PhotoInNews).AsNoTracking(), pageNumber, pageSize);


            return View(newsItems);
        }

        public IActionResult ourActivities()
        {
            return View();
        }

        public IActionResult pageActivities()
        {
            return View();
        }

        public IActionResult partners()
        {
            return View();
        }

        public IActionResult reports()
        {
            return View(cursovayadb.CategoryInReports.Include(p => p.IdReportNavigation).AsNoTracking());
        }

        public async Task<IActionResult> pageNews(int id, int? numberOfPage)
        {
            //var tempNews = cursovayadb.News.Include(p => p.PhotoInNews).SingleOrDefault(p => p.IdNews == id); // разобраться почему контроллер вызывается 2 раза
            var temp = cursovayadb.PhotoInNews.Include(p => p.IdNewsNavigation).SingleOrDefault(p => p.IdNews == id);
            
            int pageSize = 4;

            ViewBag.Id = id;

            int pageNumber = (numberOfPage ?? 1);
            var newsItems = await PaginatedList<News>.CreateAsync(cursovayadb.News.Include(p => p.PhotoInNews).Include(p => p.Comments).AsNoTracking(), pageNumber, pageSize);
            return View(newsItems);
        }

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

        public IActionResult writeUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult calendar_events()
        {
            return View(cursovayadb.Events);
        }

        [HttpGet]
        public IActionResult calend_events(CategoryInEvent categoryInEvent)
        {
            return View(categoryInEvent);
        }

        /////////// 25.04 Добавить вывод эвентов в представление календарь ивентов, разобраться с сопоставлением дат текущего месяца и даты проведения эвента



        [HttpGet("login/{username}/{password}")]
        public async Task<IActionResult> AddNewUserAsync(string username, string password)
        {
            Admin admin = new Admin();


            //if (username != "jone" && password != "123")
            //{
            //    ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
            //}

            var temp_user = cursovayadb.Admins.SingleOrDefault(p => p.Username == username && p.HashPassword == password);

            if (temp_user == null)
            {
                ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
                return View("index");
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("admin_index", "admin");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // код для удаления токена доступа из базы данных или очистки сессии пользователя
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "home");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
