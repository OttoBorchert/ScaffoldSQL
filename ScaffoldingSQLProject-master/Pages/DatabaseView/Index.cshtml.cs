using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ScaffoldingSQLProject.Pages.Database_Page.IndexModel
{
    public class DatabaseViewModel : PageModel
    {
        [BindProperty]
        public IFormFile DatabaseFile { get; set; }

        public IActionResult OnGet()
        {
#if ADMIN
            return Page();
#else
            return Redirect("/QuestionPage");
#endif

        }

        public void OnPost()
        {
            if(CheckIfDatabaseFile(DatabaseFile) == true)
            {
                Controllers.FileController.WriteDatabase(DatabaseFile);
            }else {
                Console.WriteLine("Not Of type .db .sqli");
            }
        }

        private bool CheckIfDatabaseFile(IFormFile file)
        {
            if(file != null)
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                return (extension == ".db" || extension == ".sqli");
            }
            else
            {
                return false;
            }
        }
        
        //private async Task<bool> WriteFile(IFormFile file)
        //{
        //    bool isSaveSuccess = false;
        //    string fileName;
        //    try
        //    {
        //        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        //        fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

        //        var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

        //        if (!Directory.Exists(pathBuilt))
        //        {
        //            Directory.CreateDirectory(pathBuilt);
        //        }

        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
        //           fileName);

        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        isSaveSuccess = true;
        //    }
        //    catch (Exception e)
        //    {
        //        //log error
        //    }

        //    return isSaveSuccess;
        //}
        //public async Task<IActionResult> UploadFile(
        //  IFormFile file)
        //{
        //    Console.WriteLine("INSIDE FUNCTION");
        //    if (CheckIfDatabaseFile(file))
        //    {
        //        await WriteFile(file);
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = "Invalid file extension" });
        //    }

        //    return Ok();
        //}

        //private IActionResult Ok()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
