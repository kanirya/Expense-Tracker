

using System.Text;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Expense_Tracker.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly HttpClient _client;
        Uri baseAddress = new Uri("https://localhost:7283/api/Employee");

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // Get all employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var myMap = new Dictionary<string, string>
                 {
                 { "Employee", "/Employee" },
                  { "New Employee", "/Employee/create" },
                  
    };
            ViewData["navButtons"] = myMap;
            List<Employee> employeesList = new List<Employee>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/GetAllEmployees").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employeesList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(employeesList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var myMap = new Dictionary<string, string>
                 {
                 { "Employee", "/Employee" },
                  { "New Employee", "/Employee/create" },

    };
            ViewData["navButtons"] = myMap;

            Employee employee = null; // Initialize to null to check later if the API response fails.

            // Make a GET request to the API with the employee ID.
            HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}/GetEmployeeId/{id}");

            // Check if the response is successful.
            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response content into an Employee object.
                string data = await response.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(data);
            }


            // Return the employee details to the view.
            return View(employee);
        }


        [HttpPost]
        //For update
        public async Task<IActionResult> Details(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateEmployeeDto);
            }

            // Make a PUT request to the API with the updated employee data.
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(updateEmployeeDto),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage putResponse = await _client.PutAsync($"{_client.BaseAddress}/UpdateEmployee/{id}", content);

            if (putResponse.IsSuccessStatusCode)
            {
                // Successfully updated the employee.
                return RedirectToAction("Index"); // Redirect to the list of employees.
            }

            // If the update fails, show an error message.
            ModelState.AddModelError("", "Unable to update employee. Please try again.");
            return View(updateEmployeeDto);
        }


        public IActionResult create()
        {
            var myMap = new Dictionary<string, string>
                 {
                 { "Employee", "/Employee" },
                  { "New Employee", "/Employee/create" },

    };
            ViewData["navButtons"] = myMap;

            var employee = new Employee
            {
                Name = null, // Replace with default or placeholder value
                Email = null, // Replace with default or placeholder value
                Phone = null, // Replace with default or placeholder value
                Salary = 0 // Replace with default or placeholder value
            };



            return View(employee);
        }



        // Add Employee (POST to API)
        [HttpPost]
        public async Task<IActionResult> create(AddEmployeeDto addEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Make a PUT request to the API with the updated employee data.
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(addEmployeeDto),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage putResponse = await _client.PostAsync($"{_client.BaseAddress}/AddEmployee", content);

            if (putResponse.IsSuccessStatusCode)
            {
                // Successfully updated the employee.
                return RedirectToAction("Index"); // Redirect to the list of employees.
            }

            // If the update fails, show an error message.
            ModelState.AddModelError("", "Unable to update employee. Please try again.");
            return View();

        }
        //var response = await _client.PostAsJsonAsync("", addEmployeeDto);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Failed to add employee");
        //    return View(addEmployeeDto);
        //}

        // Delete Employee
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            HttpResponseMessage DeleteResponse = await _client.DeleteAsync($"{_client.BaseAddress}/UpdateEmployee/{id}");

            if (DeleteResponse.IsSuccessStatusCode)
            {
                // Successfully updated the employee.
                return RedirectToAction("Index"); // Redirect to the list of employees.
            }


            ModelState.AddModelError("", "Failed to delete employee");
            return RedirectToAction("Index");
        }
    }
}
