using Bizland.DAL;
using Bizland.Models;
using Bizland.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bizland.Areas.admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var team = _context.Teams.ToList();
            return View(team);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var isExisted = await _context.Teams.AnyAsync(x => x.FullName.ToLower().Contains(vm.FullName.ToLower()));
            if (!isExisted)
            {
                ModelState.AddModelError("FullName", "Bu name movcuddur");
            }
            if (vm.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("image", "Tip yanlisdir");
            }
            if (vm.Image.Length < 4 * 1024 * 1024)
            {
                ModelState.AddModelError("image", "4 mb dan cox");
            }
            string filename = Guid.NewGuid() + vm.Image.FileName;
            string path = Path.Combine(_env.ContentRootPath, "wwwroot", "admin", "images", filename);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await vm.Image.CopyToAsync(stream);
            }
            Team team = new()
            {
                ImageUrl = filename,
                FullName = vm.FullName,
                Facebook = vm.Facebook,
                Twitter = vm.Twitter,
                Instagram = vm.Instagram,
                Linkedin = vm.Linkedin,
                Position = vm.Position,
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            var existed = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            TeamUpdateVM vm = new TeamUpdateVM()
            {
                
                FullName = existed.FullName,
                ImageUrl = existed.ImageUrl,
                Position = existed.Position,
                Facebook = existed.Facebook,
                Twitter = existed.Twitter,
                Instagram = existed.Instagram,
                Linkedin = existed.Linkedin,
            };



            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Update(TeamUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existed = await _context.Teams.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (existed == null) return BadRequest();
            var isExisted = await _context.Teams.AnyAsync(x => x.FullName.ToLower().Contains(vm.FullName.ToLower()) && vm.Id != x.Id);
            if (isExisted)
            {
                ModelState.AddModelError("FullName", "Bu team movcuddur");
            }
            if (vm.Image is not null)
            {
                string filename = Guid.NewGuid() + vm.Image.FileName;
                string path = Path.Combine(_env.ContentRootPath, "wwwroot", "admin", "images", filename);
                if (System.IO.File.Exists(path + "/" + vm.ImageUrl))
                {
                    System.IO.File.Delete(path + "/" + vm.ImageUrl);
                }
                filename = existed.ImageUrl;
            }
            existed.ImageUrl = vm.ImageUrl;
            existed.FullName = vm.FullName;
            existed.Facebook = vm.Facebook;
            existed.Twitter = vm.Twitter;
            existed.Position = vm.Position;
            existed.Linkedin = vm.Linkedin;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var existed = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (existed != null) return NotFound();
            string path = Path.Combine(_env.ContentRootPath, "assets/img", existed.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Teams.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}

