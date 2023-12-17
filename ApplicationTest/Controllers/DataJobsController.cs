using ApplicationTest.Application.Dtos;
using ApplicationTest.Application.Services;
using ApplicationTest.Domain.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataJobsController : ControllerBase
    {
        private readonly IDataProcessorService _service;
        private readonly IMapper _mapper;

        public DataJobsController(IDataProcessorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;            
        }

        /// <summary>
        /// Retrieves all DataJobs.
        /// </summary>
        /// <returns>A list of DataJobDTO objects.</returns>
        /// <response code="200">Returns the list of DataJobs.</response>
        /// <response code="404">If no DataJobs are found.</response>
        [HttpGet]
        public ActionResult<IEnumerable<DataJobDTO>> GetAllDataJobs()
        {
            var jobs = _service.GetAllDataJobs();

            if(jobs == null)
            {
                return NotFound("-> No results");                
            }

            var jobDtos = _mapper.Map<IEnumerable<DataJobDTO>>(jobs);
            return Ok(jobDtos);
        }

        /// <summary>
        /// Retrieves a specific DataJob by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the DataJob.</param>
        /// <returns>A DataJobDTO object.</returns>
        /// <response code="200">Returns the requested DataJob.</response>
        /// <response code="404">If the DataJob with the specified ID is not found.</response>
        [HttpGet("{id}")]
        public ActionResult<DataJobDTO> GetDataJob(Guid id)
        {
            var job = _service.GetDataJob(id);
            if(job == null)
            {
                return NotFound("DataJob Not Found");
            }

            var jobDto = _mapper.Map<DataJobDTO>(job);
            return Ok(jobDto);
        }

        /// <summary>
        /// Retrieves DataJobs by their status.
        /// </summary>
        /// <param name="status">The status to filter DataJobs.</param>
        /// <returns>A list of DataJobDTO objects.</returns>
        /// <response code="200">Returns the list of DataJobs with the specified status.</response>
        /// <response code="404">If no DataJobs with the specified status are found.</response>
        [HttpGet("status/{status}")]
        public ActionResult<IEnumerable<DataJobDTO>> GetDataJobsByStatus(DataJobStatus status)
        {
            var jobs = _service.GetDataJobsByStatus(status);

            if (jobs == null)
            {
                return NotFound("DataJob with Status not found");
            }

            var jobDtos = _mapper.Map<IEnumerable<DataJobDTO>>(jobs);
            return Ok(jobDtos);
        }


        /// <summary>
        /// Creates a new DataJob.
        /// </summary>
        /// <param name="dataJob">The DataJob information to create.</param>
        /// <returns>The created DataJobDTO.</returns>
        /// <response code="201">Returns the newly created DataJob.</response>
        /// <response code="400">If the item is null or invalid.</response>
        [HttpPost]
        public ActionResult<DataJobDTO> Create(DataJobCreateDTO dataJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdDataJob = _service.Create(dataJob);            
            return CreatedAtRoute(nameof(GetDataJob), new { id = createdDataJob.Id }, createdDataJob);
        }

        /// <summary>
        /// Updates an existing DataJob.
        /// </summary>
        /// <param name="id">The unique identifier of the DataJob to update.</param>
        /// <param name="dataJob">The updated DataJob information.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the DataJob is successfully updated.</response>
        /// <response code="400">If the item is null or invalid.</response>
        /// <response code="404">If the DataJob with the specified ID is not found.</response>
        [HttpPut("{id}")]
        public ActionResult<DataJobDTO> Update(Guid id, DataJobUpdateDTO dataJob)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDataJob = _service.GetDataJob(id);
            if( existingDataJob == null)
            {
                return NotFound("DataJob doesnt exists");
            }

            _service.Update(id, dataJob);
            return NoContent();
        }


        /// <summary>
        /// Deletes a specific DataJob.
        /// </summary>
        /// <param name="id">The unique identifier of the DataJob to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the DataJob is successfully deleted.</response>
        /// <response code="404">If the DataJob with the specified ID is not found.</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var job = _service.GetDataJob(id);
            
            if(job == null )
            {
                return NotFound("DataJob doesnt exists");
            }

            _service.Delete(id);
            return NoContent();
        }


        //from here I didnt get if you wanted this background processes to be accesible from the API
        //or if it should be something handedled by the application when creating the datajobs. 
        //I exposed this endpoints in case we wanted the user of the api to handle this functionality from their side


        /// <summary>
        /// Starts a background process for a DataJob.
        /// </summary>
        /// <param name="id">The unique identifier of the DataJob for which to start the process.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">If the background process is successfully started.</response>
        /// <response code="400">If the process could not be started.</response>
        /// <response code="404">If the DataJob with the specified ID is not found.</response>
        [HttpPost("startProcess/{id}")]
        public ActionResult StartBackgroundProcess(Guid id)
        {
            var existingJob = _service.GetDataJob(id);
            if(existingJob == null)
            {
                return NotFound("DataJob doesnt exists");
            }

            var started = _service.StartBackgroundProcess(id);
            
            if (!started)
            {
                return BadRequest("Could not start the background process.");
            }
            
            return Ok("Background process started.");
        }

        /// <summary>
        /// Gets the status of a background process for a DataJob.
        /// </summary>
        /// <param name="id">The unique identifier of the DataJob to check the status for.</param>
        /// <returns>The status of the background process.</returns>
        /// <response code="200">Returns the current status of the background process.</response>
        /// <response code="404">If the DataJob with the specified ID is not found.</response>
        [HttpGet("status/process/{id}")]
        public ActionResult<DataJobStatus> GetBackgroundProcessStatus(Guid id)
        {
            var existingJob = _service.GetDataJob(id);
            if (existingJob == null)
            {
                return NotFound("DataJob doesnt exists");
            }

            var jobStatus = _service.GetBackgroundProcessStatus(id);
            return Ok(jobStatus);
        }


        /// <summary>
        /// Retrieves the results of a background process for a DataJob.
        /// </summary>
        /// <param name="id">The unique identifier of the DataJob to retrieve results for.</param>
        /// <returns>A list of results strings.</returns>
        /// <response code="200">Returns the list of results from the background process.</response>
        /// <response code="404">If the DataJob with the specified ID is not found.</response>
        [HttpGet("results/{id}")]
        public ActionResult<IEnumerable<string>> GetBackgroundProcessResults(Guid id)
        {
            var existingJob = _service.GetDataJob(id);
            if (existingJob == null)
            {
                return NotFound("DataJob doesnt exists");
            }

            var jobResults = _service.GetBackgroundProcessResults(id);
            return Ok(jobResults);
        }
    }
}
