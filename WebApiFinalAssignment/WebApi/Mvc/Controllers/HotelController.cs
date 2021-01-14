using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class HotelController : Controller
    {
        // GET: Hotel
        public ActionResult Index()
        {
            IEnumerable<mvcHotelModel> hotelList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Hotel").Result;
            hotelList = response.Content.ReadAsAsync<IEnumerable<mvcHotelModel>>().Result;
            return View(hotelList);
        }
        public ActionResult SeeRoom(int id)
        {
            IEnumerable<mvcRoomModel> roomList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Room/"+id.ToString()).Result;
            roomList = response.Content.ReadAsAsync<IEnumerable<mvcRoomModel>>().Result;
            return View(roomList);
        }
        public ActionResult BookRoom(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("BookRoom/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<mvcBookingModel>().Result);
        }
    }
}