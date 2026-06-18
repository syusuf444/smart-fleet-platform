using FleetService.Application.DTOs;
using FleetService.Application.Features.Vehicles.Commands;
using FleetService.Application.Features.Vehicles.Queries;
using FleetService.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace FleetService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehiclesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllVehiclesQuery());

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Vehicles fetched successfully",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var vehicles = await _mediator.Send(
            new GetAllVehiclesQuery());

        var vehicle = vehicles.FirstOrDefault(x => x.Id == id);

        if (vehicle == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Vehicle not found"
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Vehicle fetched successfully",
            Data = vehicle
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVehicleDto dto)
    {
        var result = await _mediator.Send(
            new CreateVehicleCommand(dto));

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Vehicle created successfully",
            Data = result
        });
    }

    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("Test Exception");
    }
}