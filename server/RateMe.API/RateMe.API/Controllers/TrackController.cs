using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateMe.API.Contracts.Tracks;
using RateMe.Application.Interfaces.Services;

namespace RateMe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TrackController : ControllerBase
{
   private readonly ITrackService _trackService;

   public TrackController(ITrackService trackService)
   {
      _trackService = trackService;
   }

   [HttpPost]
   public async Task<IActionResult> Create([FromBody] TrackCreationRequest request)
   {
      await _trackService.CreateTrack(request.TrackName, request.ArtistName, request.Duration, request.TrackLogoUrl,
         request.Genre);

      return Ok();
   }
}