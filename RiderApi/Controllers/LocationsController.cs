//using DataLayer.Data;
//using DomainLayer.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using RepositoryLayer.IRepository;
//using ServiceLayer.DriverService;
//using ServiceLayer.ICustomServices;
//using ServiceLayer.IDriverService;

//namespace RiderApi.AddControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LocationsController : ControllerBase
//    {
//        private readonly ICustomService <Location> _locationService;
//        private readonly IDriverService driverService;

//        private readonly ApplicationDbContext _context;

//        public LocationsController(ICustomService <Location> locationService,DriverService driverService)
//        {
//            //_context = context;
//            driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
//            _locationService = locationService;
//        }
//        [HttpGet]
//        public IActionResult GetDrivers()
//        {
//           return this.driverService.GetDrivers();
//        }
//        [HttpGet(nameof(GetLocationById))]
//        public IActionResult GetLocationById(Guid Id) {
//                var obj = _locationService.Get(Id);
//                if (obj == null) {
//                    return NotFound();
//                } else {
//                    return Ok(obj);
//                }
//            }
//        [HttpGet(nameof(GetAllLocation))]
//        public IActionResult GetAllLocation() {
//                var obj = _locationService.GetAll();
//                if (obj == null) {
//                    return NotFound();
//                } else {
//                    return Ok(obj);
//                }
//            }
//        [HttpPost(nameof(CreateLocation))]
//        public IActionResult CreateLocation(Location location) {
//                if (location != null) {
//                    _locationService.Insert(location);
//                    return Ok("Created Successfully");
//                } else {
//                    return BadRequest("Somethingwent wrong");
//                }
//            }
//        [HttpPost(nameof(UpdateLocation))]
//        public IActionResult UpdateLocation(Location location) {
//                if (location != null) {
//                    _locationService.Update(location);
//                    return Ok("Updated SuccessFully");
//                } else {
//                    return BadRequest();
//                }
//            }
//            [HttpDelete(nameof(DeleteLocation))]
//        public IActionResult DeleteLocation(Location location) {
//            if (location != null) {
//                _locationService.Delete(location);
//                return Ok("Deleted Successfully");
//            } else {
//                return BadRequest("Something went wrong");
//            }
//        }
//    }
//}