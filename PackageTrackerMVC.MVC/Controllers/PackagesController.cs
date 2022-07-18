using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PackageTrackerMVC.MVC.Models;

namespace PackageTrackerMVC.MVC.Controllers
{
    public class PackagesController : Controller
    {
        //GET api/packages
        public async Task<IActionResult> Index()
        {
            List<PackageViewModel> packages;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7284/api/");

                //HTTP GET
                var responseTask = await client.GetAsync("packages");

                var result = responseTask.StatusCode;

                if((int)result == 200) 
                {
                    var readTask = await responseTask.Content.ReadAsAsync<List<PackageViewModel>>();
                    packages = readTask.ToList();
                }
                else 
                {
                    var _result = result;

                    packages = new List<PackageViewModel>();
                }
            }
        return View(packages);
        }


        public IActionResult Cadastrar()
        {
            return View();
        }


        public IActionResult Rastrear()
        {
            return View();
        }

        public IActionResult Informacoes(PackageViewModel package) 
        {
            var package_ = package;
            return View(package_);
        }

        [HttpPost]
        public async Task<IActionResult> Rastrear(string code) 
        {
            PackageViewModel package;

            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("https://localhost:7284/api/");

                var responseTask = await client.GetAsync("packages/" + code.ToString());

                var result = responseTask.StatusCode;

                if ((int)result == 200)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<PackageViewModel>();

                    package = readTask;
                }
                else
                {
                    var _result = result;
                    package = new PackageViewModel();  
                }
            }
            return RedirectToAction("Informacoes", package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(PackageViewModel package) 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7284/api/");

                //HTTP GET
                var postTask = await client.PostAsJsonAsync<PackageViewModel>("packages", package);

                var result = postTask.StatusCode;

                if ((int)result == 201 || (int)result == 200)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact someome");

            return View(package);

        }

        [HttpGet]
        public async Task<IActionResult> Editar(string code)
        {
            PackageViewModel package;

            using (var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("https://localhost:7284/api/");

                var responseTask = await client.GetAsync("packages?code=" + code.ToString());

                var result = responseTask.StatusCode;
                if ((int)result == 201 || (int)result == 200 || (int)result == 204)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<PackageViewModel>();

                    package = readTask;
                }
                else 
                {
                    package = new PackageViewModel();
                }
            }

            return View(package);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string code,PackageViewModel package) 
        {
            using (var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("https://localhost:7284/api/");

                var putTask = await client.PutAsJsonAsync<PackageViewModel>("packages", package);

                var result = putTask.StatusCode;

                if ((int)result == 201 || (int)result == 200)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact someome");

            return View(package);
        }

        [HttpDelete]
        public async Task<IActionResult> Apagar(string code)
        {
            using (var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("https://localhost:7284/api/");

                var deleteTask = await client.DeleteAsync("packages/" + code.ToString());

                var result = deleteTask.StatusCode;

                if ((int)result == 204 || (int)result == 200)
                {
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Index");
        }


    }
}