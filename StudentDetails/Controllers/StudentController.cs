using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentDetails.Data;
using StudentDetails.Models;
using System.Data;

namespace StudentDetails.Controllers
{
    [Route("Student")]
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;
        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            if(_context.Students != null)
            {
                return View(await _context.Students.ToListAsync());
            }
            else
            {
                return Problem(detail: "No data found", statusCode: 404);
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(Students students)
        {
            if(ModelState.IsValid)
            {
                if(students.RollNumber == 0)    //checking if its an isert or update operation
                {
                    //_context.Add(students);
                    //var RollNumber = new SqlParameter("@RollNumber", SqlDbType.Int)
                    //{
                    //    Value=students.RollNumber
                    //};
                    var StudentName = new SqlParameter("@StudentName", SqlDbType.NVarChar)
                    {
                        Value = students.StudentName
                    };
                    var PhoneNumber = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar)
                    {
                        Value = students.PhoneNumber
                    };
                    var Address = new SqlParameter("@Address", SqlDbType.NVarChar)
                    {
                        Value = students.Address
                    };
                    _context.Database.ExecuteSqlRaw("Exec sp_insertStudent @StudentName," +
                        "@PhoneNumber,@Address",StudentName,PhoneNumber,Address);
                }
                else
                {
                    _context.Update(students);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View(_context.Students.Find(id));
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var students = await _context.Students.FindAsync(id);
            if (students != null)
            {
                _context.Students.Remove(students);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
