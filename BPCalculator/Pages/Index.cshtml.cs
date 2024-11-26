using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// page model

namespace BPCalculator.Pages
{
    public class BloodPressureModel : PageModel
    {
        [BindProperty]                              // bound on POST
        public BloodPressure BP { get; set; }

        // setup initial data
        public void OnGet()
        {
            BP = new BloodPressure() { Systolic = 100, Diastolic = 60 };
        }

        // POST, validate
        public IActionResult OnPost()
        {
            // extra validation
            if (!(BP.Systolic > BP.Diastolic))
            {
                ModelState.AddModelError("", "Systolic must be greater than Diastolic");
            }
            return Page();
        }

        public string GetCatgoryColor(BPCategory category)
        {
            switch (category)
            {
                case BPCategory.Low:
                    return "blueviolet";
                case BPCategory.Ideal:
                    return "green";
                case BPCategory.PreHigh:
                    return "greenyellow";
                case BPCategory.High:
                    return "red";
                default:
                    return "white";
            }
        }
    }
}