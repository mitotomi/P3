using Microsoft.AspNetCore.Mvc;
using P3.Models.Requests;
using P3.Services.Contracts;

namespace P3.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class FolderController : ControllerBase
    {
        private readonly IFolderService folderService;

        public FolderController(IFolderService folderService)
        {
            this.folderService = folderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return new ObjectResult(folderService.GetAll());
            }
            catch (Exception ex)
            {

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFolderRequest request)
        {
            try
            {
                return new ObjectResult( await folderService.Create(request));
            } catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            try
            {
                folderService.Delete(id);
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch(ArgumentException ex)
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
