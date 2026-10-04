using System.Runtime.ConstrainedExecution;
using atelier1.Models.Repositories;
using atelier1.Models;
using atelier1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace atelier1.Controllers
{
    public class EmployeeController : Controller
    {
        readonly IRepository<Employee> employeeRepository;

        // Injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            // Récupérer tous les employés
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();

            // Envoyer la liste à la vue Index
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            // Chercher un employé par son ID
            var employee = employeeRepository.FindByID(id);

            // Envoyer l'employé à la vue Details
            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee e)
        {
            try
            {
                // Ajouter le nouvel employé
                employeeRepository.Add(e);

                // Retourner à la liste
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(e);
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            // Récupérer l'employé à modifier
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee newemployee)
        {
            try
            {
                // Modifier l'employé
                employeeRepository.Update(id, newemployee);

                // Retourner à la liste
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(newemployee);
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            // Récupérer l'employé avant suppression
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Supprimer l'employé
                employeeRepository.Delete(id);

                // Retourner à la liste
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string term)
        {

            var result = employeeRepository.Search(term);
            return View("Index", result);
        }
    }
}


