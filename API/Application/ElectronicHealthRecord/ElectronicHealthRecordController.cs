using API.Application;
using API.Domain;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("electronic-health-record")]
public class ElectronicHealthRecordController : ControllerBase
{
    private readonly IElectronicHealthRecordCommandHandler _commandHandler;
    private readonly IElectronicHealthRecordQueryHandler _queryHandler;
    private readonly ILogger<ElectronicHealthRecordController> _logger;

    public ElectronicHealthRecordController(
        IElectronicHealthRecordCommandHandler commandHandler,
        IElectronicHealthRecordQueryHandler queryHandler,
        ILogger<ElectronicHealthRecordController> logger)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Create([FromBody] ElectronicHealthRecordCreateDto dto)
    {
        return Ok(_commandHandler.Handle(ElectronicHealthRecordCommand.Create(dto)));
    }

    [HttpGet("{id}")]
    public IActionResult Find(long id) =>
        Ok(_queryHandler.Handle(ElectronicHealthRecordQuery.Find(id)));

    [HttpPost("{ehrId}/add-diagnosis")]
    public IActionResult AddDiagnosis(long ehrId, [FromBody] Diagnosis diagnosis) =>
        Ok(_commandHandler.Handle(ElectronicHealthRecordCommand.AddDiagnosis(ehrId, diagnosis)));
}
