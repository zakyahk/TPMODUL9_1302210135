using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace TPMODUL9_1302210135
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahasiswaController : ControllerBase
    {
        // Data dummy untuk contoh
        private static List<Mahasiswa> _mahasiswaData = new List<Mahasiswa>()
        {
            new Mahasiswa() { Nama = "Zakkiya Hakeem", Nim = "1302210135", },
            new Mahasiswa() { Nama = "Muhammad Zaidan Rafif", Nim = "1302213072", },
            new Mahasiswa() { Nama = "Hafid Naoya", Nim = "1302210129", },
            new Mahasiswa() { Nama = "Aryasatya Pratama", Nim = "1302210082", },
            new Mahasiswa() { Nama = "Muhammad Alif Rasyid Ramdhani", Nim = "1302210098", },
        };

        // GET api/mahasiswa
        [HttpGet]
        public ActionResult<IEnumerable<Mahasiswa>> GetMahasiswa()
        {
            return _mahasiswaData;
        }

        // GET api/mahasiswa/1302210135
        [HttpGet("{nim}")]
        public ActionResult<Mahasiswa> GetMahasiswaByNim(string nim)
        {
            var mahasiswa = _mahasiswaData.FirstOrDefault(m => m.Nim == nim);
            if (mahasiswa == null)
            {
                return NotFound();
            }
            return mahasiswa;
        }

        // POST api/mahasiswa
        [HttpPost]
        public ActionResult<Mahasiswa> AddMahasiswa(Mahasiswa mahasiswa)
        {
            _mahasiswaData.Add(mahasiswa);
            return CreatedAtAction(nameof(GetMahasiswaByNim), new { nim = mahasiswa.Nim }, mahasiswa);
        }

        // PUT api/mahasiswa/1302210135
        [HttpPut("{nim}")]
        public ActionResult UpdateMahasiswa(string nim, Mahasiswa mahasiswa)
        {
            var existingMahasiswa = _mahasiswaData.FirstOrDefault(m => m.Nim == nim);
            if (existingMahasiswa == null)
            {
                return NotFound();
            }
            existingMahasiswa.Nama = mahasiswa.Nama;
            return NoContent();
        }

        // DELETE api/mahasiswa/1302210135
        [HttpDelete("{nim}")]
        public ActionResult DeleteMahasiswa(string nim)
        {
            var existingMahasiswa = _mahasiswaData.FirstOrDefault(m => m.Nim == nim);
            if (existingMahasiswa == null)
            {
                return NotFound();
            }
            _mahasiswaData.Remove(existingMahasiswa);
            return NoContent();
        }
    }

    public class Mahasiswa
    {
        public string Nama { get; set; }
        public string Nim { get; set; }
    }

}

