using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using StaffDirectoryAPI.Models;

namespace StaffDirectoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private static readonly string StaffFilePath = "assets/StaffList.xml";
        private static readonly string DepartmentsFilePath = "assets/Departments.xml";

        [HttpGet("staff")]
        public IActionResult GetStaff()
        {
            if (!System.IO.File.Exists(StaffFilePath))
            {
                return NotFound("File not found.");
            }

            var doc = XDocument.Load(StaffFilePath);
            var staffList = doc.Root.Elements("Staff").Select(x => new Staff
            {
                ID = (int)x.Element("ID"),
                Name = (string)x.Element("Name"),
                Phone = (string)x.Element("Phone"),
                Department = (int)x.Element("Department"),
                Street = (string)x.Element("Street"),
                City = (string)x.Element("City"),
                State = (string)x.Element("State"),
                ZIP = (string)x.Element("ZIP"),
                Country = (string)x.Element("Country")
            }).ToList();

            return Ok(staffList);
        }

        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            if (!System.IO.File.Exists(DepartmentsFilePath))
            {
                return NotFound("File not found.");
            }

            var doc = XDocument.Load(DepartmentsFilePath);
            var departments = doc.Root.Elements("Department").Select(x => new Department
            {
                ID = (int)x.Element("ID"),
                Name = (string)x.Element("Name")
            }).ToList();

            return Ok(departments);
        }

        [HttpPost("savestaff")]
        public IActionResult SaveStaff(List<Staff> staffList)
        {
            var doc = new XDocument(new XElement("StaffList",
                staffList.Select(s => new XElement("Staff",
                    new XElement("ID", s.ID),
                    new XElement("Name", s.Name),
                    new XElement("Phone", s.Phone),
                    new XElement("Department", s.Department),
                    new XElement("Street", s.Street),
                    new XElement("City", s.City),
                    new XElement("State", s.State),
                    new XElement("ZIP", s.ZIP),
                    new XElement("Country", s.Country)
                ))
            ));

            doc.Save(StaffFilePath);
            return Ok("File saved successfully.");
        }
    }
}
