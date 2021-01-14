using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HotelController : ApiController
    {
        private HotelDBEntities db = new HotelDBEntities();

        // GET: api/Hotel
        public IQueryable<getHotel> GetHotels()
        {
            //return db.Hotels.OrderBy(p => p.HotelName);
         return db.Hotels
         .OrderBy(p => p.HotelName)
        .ToList()
        .Select(p => new getHotel
        {
            HotelName = p.HotelName,
            Address = p.Address,
            City = p.City,
            Pincode = p.Pincode,
            ContactNumber = p.ContactNumber,
            ContactPerson = p.ContactPerson,
            Website = p.Website,
            Facebook = p.Facebook,
            Twitter = p.Twitter
        })
        .AsQueryable();
        }

        // GET: api/Hotel/5
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult GetHotel(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // PUT: api/Hotel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest();
            }

            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Hotel
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult PostHotel([FromBody] Hotel hotel)
        { 
            db.Hotels.Add(hotel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotel.HotelId }, hotel);
        }

        // DELETE: api/Hotel/5
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult DeleteHotel(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hotels.Remove(hotel);
            db.SaveChanges();

            return Ok(hotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelExists(int id)
        {
            return db.Hotels.Count(e => e.HotelId == id) > 0;
        }
    }
}