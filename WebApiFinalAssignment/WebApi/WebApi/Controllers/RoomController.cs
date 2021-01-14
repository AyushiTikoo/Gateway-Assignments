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
    public class RoomController : ApiController
    {
        private HotelDBEntities db = new HotelDBEntities();

        // GET: api/Room
        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }

        [Route("api/Room/{hotelId}")]
        public IEnumerable<getrooms> GetRoomByHotelId(int hotelId) 
        {
            /*var room = db.Rooms.Where(p => p.HotelId == hotelId);
            return(room);*/
            return db.Rooms
                .OrderBy(p => p.RoomPrice)
         .Where(p => p.HotelId == hotelId)
         
        .ToList()
        .Select(p => new getrooms
        {
            RoomName = p.RoomName,
            RoomPrice = p.RoomPrice,
            RoomCategoryName = p.RoomCategory1.RoomCategoryName,
            Pincode = p.Hotel.Pincode,
            City = p.Hotel.City
        })
        .AsQueryable();
        }


        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookRoom(int id, Booking book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.RoomId)
            {
                return BadRequest();
            }

            db.Bookings.Add(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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
        
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            var room = db.Rooms.Find(id);
          
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Room/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.RoomId)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Room
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.RoomId }, room);
        }

        // DELETE: api/Room/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.RoomId == id) > 0;
        }
    }
}