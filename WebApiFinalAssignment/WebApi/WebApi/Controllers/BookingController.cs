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
    public class BookingController : ApiController
    {
        private HotelDBEntities db = new HotelDBEntities();

        // GET: api/Booking
        public IQueryable<Booking> GetBookings()
        {
            return db.Bookings;
        }
        /*te}/{RoomId}/{StatusOfBooking}")]
        public IHttpActionResult PostBooking(string BookingDate, int RoomId, string StatusOfBooking)
        {
            Booking b = new Booking();
            db.Bookings.Add(new Booking()
            {
                BookingId = b.BookingId,
                BookingDate = b.BookingDate,
                RoomId = b.RoomId,
                StatusOfBooking = b.StatusOfBooking
            });
            db.SaveChanges();
            return Ok();
        }*/
        
        public IHttpActionResult PostBooking([FromBody]Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*var room = from r in db.Rooms
                           select r;
            room = room.Where(r => r.RoomId.Equals(booking.RoomId);
            var ActiveStatus = db.Rooms.Where(r => r.RoomId == booking.RoomId)
                                       .Where(r => r.Booking.BookingDate == booking.BookingDate)
                                       .Select(r => r.IsActive);
            */
            db.Bookings.Add(booking);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = booking.BookingId }, booking);
        }
        // GET: api/Booking/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBooking(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/Booking/5
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutBooking(int id,Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*if (id != booking.BookingId)
            {
                return BadRequest();
            }*/
            db.Bookings.Attach(booking);
            db.Entry(booking).Property(x => x.StatusOfBooking).IsModified = true;
            //db.SaveChanges();
            //db.Entry(booking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // DELETE: api/Booking/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult DeleteBooking(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(booking);
            db.SaveChanges();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(int id)
        {
            return db.Bookings.Count(e => e.BookingId == id) > 0;
        }
    }
}