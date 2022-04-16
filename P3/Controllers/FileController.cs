using Microsoft.AspNetCore.Mvc;
using P3.Models.Filtering;
using P3.Models.Requests;
using P3.Services.Contracts;

namespace P3.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FileController: ControllerBase
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return new ObjectResult(fileService.GetAll());
            }
            catch (Exception ex)
            {

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("/search")]
        public IActionResult Search(FilterRequest request)
        {
            try
            {
                return new ObjectResult(fileService.Search(request));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFileRequest request)
        {
            try
            {
                return new ObjectResult(await fileService.Create(request));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            try
            {
                fileService.Delete(id);
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (ArgumentException ex)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
