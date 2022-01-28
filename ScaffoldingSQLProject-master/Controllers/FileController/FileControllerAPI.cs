using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ScaffoldingSQLProject.Controllers;

namespace ScaffoldingSQLProject.Controllers
{

    /// <summary>
    ///     This file controller: Handles File requests
    /// </summary>
    public partial class FileController : Controller
    {
        /// <summary>
        ///     Get the result from a file in the response header
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <param name="isValid"></param>
        /// <param name="getResult"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        private static JsonResult HandleMalformedForm(HttpRequest request, HttpResponse response, string key, Func<string, bool> isValid, Func<string, JsonResult> getResult, string errorMessage = "Bad Request/Syntax", int errorCode = 400) =>
            Handle404(
                response,
                () => isValid(request.Form[key]),
                () => getResult(request.Form[key]),
                errorMessage,
                errorCode,
                isValid: () => request.Form.ContainsKey(key),
                missing: errorCode,
                missingMessage: errorMessage
            );

        /// <summary>
        /// Handles if a file is missing or invalid
        /// </summary>
        /// <param name="response"></param>
        /// <param name="doesExist"></param>
        /// <param name="getResult"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <param name="init"></param>
        /// <param name="isValid"></param>
        /// <param name="missingMessage">The error message to return on a false doesExist</param>
        /// <param name="missing">The error code to fire off when the doesExist returns false</param>
        /// <returns></returns>
        private static JsonResult Handle404(
            HttpResponse response,
            Func<bool> doesExist,
            Func<JsonResult> getResult,
            string errorMessage = "This is not supposed to be reachable",
            int errorCode = 500,
            Action init = null,
            Func<bool> isValid = null,
            string missingMessage = "File does not exist.",
            int missing = 404)
        {
            // Try to run the init event
            init?.Invoke();
            // Give default isValid event if needed
            isValid ??= () => true;
            if (doesExist()) // If the doesExistEvent returns true
            {
                if (isValid()) // If the isValid event returns true
                {
                    return getResult(); // Return the result of the getResult event
                }
                else
                {
                    // Set status code and return error
                    response.StatusCode = errorCode;
                    return new JsonResult(new Dictionary<string, string>() { { "Error", errorMessage } });
                }
            }
            else
            {
                // doesExist returned false. Give a 404 by default.
                response.StatusCode = missing;
                return new JsonResult(new Dictionary<string, string> { { "Error", missingMessage } });
            }
        }


        /**
         * GET REQUESTS
         */


        // GET: FileController/Database/Bytes/{Filename}
        // Status Codes: 200, 404
        [HttpGet("FileController/Database/Bytes/{Filename}")]
        public JsonResult OnGetDatabaseBytes(string filename) =>
            Handle404(
                Response,
                () => System.IO.File.Exists(Path.Combine(P_DbPath, filename)),
                () => Json(System.IO.File.ReadAllBytes(Path.Combine(P_DbPath, filename)).ToList())
            );

        // Get: FileController/Database/List
        // Status Codes: 200
        [HttpGet("FileController/Database/List")]
        public JsonResult OnGetDatabaseList() => Json(DatabaseList());

        // Get: FileController/Database/ListSimple
        // Status Codes: 200
        [HttpGet("FileController/Database/ListSimple")]
        public JsonResult OnGetDatabaseListSimple() => Json(DatabaseListSimple());

        // Get: FileController/UnitNumbers/All/Questions
        // Status Codes: 200
        [HttpGet("FileController/UnitNumbers/All/Questions")]
        public JsonResult OnGetAllQestions()
        {
            var dict = new Dictionary<string, IEnumerable<string>>();
            foreach (var unitNumber in GetUnitNumbers())
            {
                dict.Add(unitNumber, GetQuestionsFromUnit(unitNumber));
            }
            return Json(dict);
        }

        // GET: FileController/FileList
        // Status Codes: 200
        [HttpGet("FileController/FileList")]
        public JsonResult OnGetFileList() => Json(FileList());

        // GET: FileController/FileListSimple
        // Status Codes: 200
        [HttpGet("FileController/FileListSimple")]
        public JsonResult OnGetFileListSimple() => Json(FileListSimple());

        // Get: FileController/GetQuestion/{q}
        // Status Codes: 200, 404
        [HttpGet("FileController/GetQuestion/{q}")]
        public JsonResult OnGetGetQuestionContents(string q) =>
            Handle404(
                Response,
                () => System.IO.File.Exists(GetQuestionPath(q)),
                () =>
                {
                    var Contents = GetFileContents(q);
                    var contents = GetFileContents(q);
                    contents.Remove("SecretWord");
                    contents.Remove("SecretWordParson");
                    return Json(contents);
                },
                "This is not supposed to be reachable",
                400
            );

        // Get: FileController/QuestionExists/{q}
        // Satus codes: 200
        [HttpGet("FileController/QuestionExists/{q}")]
        public JsonResult OnGetQuestionExists(string q)
        {
            string path = Path.Combine(P_QuestionDirectory, q);
            return Json(System.IO.File.Exists(path));
        }

        // Get: FileController/UnitNumbers/{unit}/Questions
        // Status Codes: 200,  404
        [HttpGet("FileController/UnitNumbers/{unit}/Questions")]
        public JsonResult OnGetQuestionsFromUnit(string unit) {
            IEnumerable<string> files = null;
            return Handle404(
                Response,
                () => files.Contains(unit),
                () => Json(GetQuestionsFromUnit(unit)),
                init: () => files = GetUnitNumbers()
            );
        }

        // Get: FileController/UnitNumbers
        // Status Codes: 200
        [HttpGet("FileController/UnitNumbers")]
        public JsonResult OnGetUnitNumbers() => Json(GetUnitNumbers());


        /**
         * POST REQUESTS
         */


        // Post: FileController/GetQuestion
        // Status Codes: 200, 400, 404 
        [Route("FileController/GetQuestion")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnPostGetQuestionContents() => 
            HandleMalformedForm(
                Request,
                Response,
                "File",
                value => System.IO.File.Exists(GetQuestionPath(value)),
                value => Json(GetFileContents(value))
            );


        // Post: FileController/SecretWord
        // Status Codes: 200, 400, 404 
        [Route("FileController/SecretWord")]
        [HttpPost]
      [IgnoreAntiforgeryToken]
      //[ValidateAntiForgeryToken]
        public JsonResult OnPostSecretWord() =>
            HandleMalformedForm(
                Request,
                Response,
                "File",
                value => System.IO.File.Exists(GetQuestionPath(value)),
                value => Json(GetFileContents(value)["SecretWord"])
            );

        // Post: FileController/SecretWord/Parsons
        // Status Codes: 200, 400, 404 
        [Route("FileController/SecretWord/Parsons")]
        [HttpPost]
      [IgnoreAntiforgeryToken]
      //[ValidateAntiForgeryToken]
        public JsonResult OnPostSecretWordParsons() =>
            HandleMalformedForm(
                Request,
                Response,
                "File",
                value => System.IO.File.Exists(GetQuestionPath(value)),
                value => Json(GetFileContents(value)["SecretWordParson"])
            );

    }
}
