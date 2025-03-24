using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
			_configuration = configuration;
		}

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var conn = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                conn.Open();

                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText = $"INSERT INTO drinking_water(date, quantity) VALUES('{DrinkingWater.Date}', {DrinkingWater.Quantity})";
                tableCmd.ExecuteNonQuery();

                conn.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}
