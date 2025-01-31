using Microsoft.AspNetCore.Mvc;
using TaskAPIOne;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private static List<Trip> _trips = new List<Trip>
    {
        new Trip { Id = 1, Destination = "Paris", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) },
        new Trip { Id = 2, Destination = "London", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(3) }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Trip>> GetTrips()
    {
        return Ok(_trips);
    }

    [HttpGet("{id}")]
    public ActionResult<Trip> GetTrip(int id)
    {
        var trip = _trips.FirstOrDefault(t => t.Id == id);
        if (trip == null)
            return NotFound();

        return Ok(trip);
    }

    [HttpPost]
    public ActionResult<Trip> CreateTrip(Trip trip)
    {
        trip.Id = _trips.Any() ? _trips.Max(t => t.Id) + 1 : 1;
        _trips.Add(trip);
        return CreatedAtAction(nameof(GetTrip), new { id = trip.Id }, trip);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateTrip(int id, Trip updatedTrip)
    {
        var trip = _trips.FirstOrDefault(t => t.Id == id);
        if (trip == null)
            return NotFound();

        trip.Destination = updatedTrip.Destination ?? trip.Destination;
        trip.StartDate = updatedTrip.StartDate != default ? updatedTrip.StartDate : trip.StartDate;
        trip.EndDate = updatedTrip.EndDate != default ? updatedTrip.EndDate : trip.EndDate;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTrip(int id)
    {
        var trip = _trips.FirstOrDefault(t => t.Id == id);
        if (trip == null)
            return NotFound();

        _trips.Remove(trip);
        return NoContent();
    }
}
