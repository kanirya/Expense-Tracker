

using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Expense_Tracker.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _client;
        Uri baseAddress= new Uri("https://localhost:7283/api/Employee");
        
        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // Get all employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Employee> employeesList= new List<Employee>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/GetAllEmployees").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employeesList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(employeesList);
        }

        [HttpGet]
        // Get employee by ID
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _client.GetFromJsonAsync<Employee>($"/GetEmployeeId/{id}");
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Add Employee (GET for form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Add Employee (POST to API)
        [HttpPost]
        public async Task<IActionResult> Create(AddEmployeeDto addEmployeeDto)
        {
            var response = await _client.PostAsJsonAsync("", addEmployeeDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to add employee");
            return View(addEmployeeDto);
        }

        // Update Employee (GET for form)
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _client.GetFromJsonAsync<UpdateEmployeeDto>($"/{id}");
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Update Employee (POST to API)
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var response = await _client.PutAsJsonAsync($"/{id}", updateEmployeeDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to update employee");
            return View(updateEmployeeDto);
        }

        // Delete Employee
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _client.DeleteAsync($"/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to delete employee");
            return RedirectToAction("Index");
        }
    }
}
