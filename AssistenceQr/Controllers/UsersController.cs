using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssistanceQr;
using AssistanceQr.Models;
using OfficeOpenXml.Table;
using OfficeOpenXml;

namespace AssistanceQr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyContext _context;

        public UsersController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetGrades()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.Id == id);
        }
        [HttpGet("export")]
        public ActionResult Export()
        {
            string reportname = "EXPORTAR_PARTICIPANTES.xlsx";
            var exportbytes = ExporttoExcel();
            return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
        }

        private byte[] ExporttoExcel()
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet Sheet = pack.Workbook.Worksheets.Add("FORMATO");

            var list = _context.User.ToList();

            var header = new[] { "N", "APELLIDOS Y NOMBRES", "DNI", "TELEFONO", "EMAIL", "IDENTIFICADOR QR" };


            for (int j = 1; j <= header.Length; j++)
            {
                Sheet.Cells[1, j].Value = header[j - 1];

            }
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                Sheet.Cells[i + 2, 1].Value = i + 1;
                Sheet.Cells[i + 2, 2].Value = item.PaternalSurname + " " + item.MaternalSurname + ", " + item.Name;
                Sheet.Cells[i + 2, 3].Value = item.DNI;
                Sheet.Cells[i + 2, 4].Value = item.Phonenumber;
                Sheet.Cells[i + 2, 5].Value = item.Email;
                Sheet.Cells[i + 2, 6].Value = item.Id;
            }


            ExcelRange range = Sheet.Cells[1, 1, Sheet.Dimension.End.Row, Sheet.Dimension.End.Column];

            //add a table to the range
            ExcelTable tab = Sheet.Tables.Add(range, "formato");

            //format the table
            tab.TableStyle = TableStyles.Medium10;

            range.AutoFitColumns();

            return pack.GetAsByteArray();
        }

    }
}
