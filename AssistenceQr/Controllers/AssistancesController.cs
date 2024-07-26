
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssistanceQr.Models;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Table;

namespace AssistanceQr.Controllers
{
    [Route("api/[controller]")]
    public class AssistanceController : Controller
    {
        private readonly MyContext _context;

        public AssistanceController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Assistances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assistance>>> GetAssistances()
        {
            return await _context.Assistances.ToListAsync();
        }

        // GET: api/Assistances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assistance>> GetAssistance(Guid id)
        {
            var assistance = await _context.Assistances.FindAsync(id);

            if (assistance == null)
            {
                return NotFound();
            }

            return assistance;
        }

        [HttpGet("templateexcel")]

        public IActionResult DownloadReport(IFormCollection obj)
        {
            string reportname = "FORMATO_IMPORTAR_PARTICIPANTES.xlsx";
            var exportbytes = ExporttoExcel();
            return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
        }

        private static byte[] ExporttoExcel()
        {
            using ExcelPackage pack = new ExcelPackage();

            ExcelWorksheet Sheet = pack.Workbook.Worksheets.Add("FORMATO");



            var header = Utils.headers;

            for (int i = 1; i <= header.Length; i++)
            {
                Sheet.Cells[1, i].Value = header[i - 1];
                Sheet.Column(i).Style.Numberformat.Format = "@";
            }


            ExcelRange range = Sheet.Cells[1, 1, 10, Sheet.Dimension.End.Column];

            //add a table to the range
            ExcelTable tab = Sheet.Tables.Add(range, "formato");

            //format the table
            tab.TableStyle = TableStyles.Medium1;

            range.AutoFitColumns();

            return pack.GetAsByteArray();
        }


        [HttpGet("list/{date}")]
        public IActionResult LisAssistance(string date)
        {
            try
            {
                var assistance = _context.Assistances.Where(x => x.Date.Year + "" + x.Date.Month + "" + x.Date.Day == date).Select(x => new
                {
                    x.Date,
                    fullname = string.Concat(x.User.PaternalSurname, " ", x.User.MaternalSurname, ", ", x.User.Name),
                    x.User.DNI,
                    x.User.ParticipantType
                }).ToList();
                return Ok(assistance);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/Assistances/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("save")]
        public IActionResult PutAssistance([FromBody] AssistanceParameter assistance)
        {
            try
            {
                var user = _context.User.Where(x => x.Id == assistance.UserId).FirstOrDefault();
                if (user == null) return NotFound();

                var newId = Guid.NewGuid();
                var date = "" + assistance.Date.Year + assistance.Date.Month + assistance.Date.Day;

                var assistanceInfo = _context.Assistances.Where(x => x.UserId == assistance.UserId && (x.Date.Year + "" + x.Date.Month + "" + x.Date.Day) == date).FirstOrDefault();

                if (assistanceInfo == null)
                {
                    _context.Assistances.Add(new Assistance
                    {
                        Id = newId,
                        Date = assistance.Date,
                        UserId = user.Id
                    });
                    _context.SaveChanges();

                }
                var finalFilterId = assistanceInfo == null ? newId : assistanceInfo.Id;
                var validate = _context.Assistances.Where(x => x.Id == finalFilterId).FirstOrDefault();

                if (validate == null) return NotFound();

                return Ok(new
                {
                    FullName = $"{validate.User.PaternalSurname} {validate.User.MaternalSurname}, {validate.User.Name}",
                    Email = validate.User.Email,
                    Dni = validate.User.DNI,
                    ParticipantType = validate.User.ParticipantType,
                    IsPreviousSave = assistanceInfo != null
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

        }

        // POST: api/Assistances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assistance>> PostAssistance(Assistance assistance)
        {
            _context.Assistances.Add(assistance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssistance", new { id = assistance.Id }, assistance);
        }

        // DELETE: api/Assistances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssistance(Guid id)
        {
            var assistance = await _context.Assistances.FindAsync(id);
            if (assistance == null)
            {
                return NotFound();
            }

            _context.Assistances.Remove(assistance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
    public class AssistanceParameter
    {

        public DateTime Date { get; set; }
        public Guid UserId { get; set; }

    }




}
