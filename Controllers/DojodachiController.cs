using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dojodachi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace dojodachi.Controllers
{
    public class Dojodachi : Controller
    {
        DojodachiModel petname = new DojodachiModel();
        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname")==null)
            {
                HttpContext.Session.SetObjectAsJson("petname", petname);
            }
            ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
            ViewBag.Message = "";
            return View("Index", petname);
        }
        
        Random rand = new Random();
        
        //feed
        // [HttpGet("dojodachi")]
        public IActionResult Feed()
        {
            
            if(HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname")==null)
            {
                HttpContext.Session.SetObjectAsJson("petname", petname);
                DojodachiModel petTemp = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                if(petTemp.meals > 0)
                {
                    petTemp.meals -= 1;
                    petTemp.fullness += rand.Next(5, 11);
                    HttpContext.Session.SetObjectAsJson("petname", petTemp);
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                    return RedirectToAction("Index", petTemp);
                }
                else
                {
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                    HttpContext.Session.SetString("Message", "Not enough meals to feed the dojodachi");
                    ViewBag.Message = HttpContext.Session.GetString("Message");
                }
            }
            else 
            {
                DojodachiModel petTemp = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                if(petTemp.meals > 0)
                {
                    petTemp.meals -= 1;
                    petTemp.fullness += rand.Next(5, 11);
                    HttpContext.Session.SetObjectAsJson("petname", petTemp);
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                }
                else
                {
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                    HttpContext.Session.SetString("Message", "Not enough meals to feed the dojodachi");
                    ViewBag.Message = HttpContext.Session.GetString("Message");
                }
            }
            return View("Index");
        }

        //play 
        public IActionResult Play()
        {
            
            if(HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname")==null)
            {
                HttpContext.Session.SetObjectAsJson("petname", petname);
                DojodachiModel petTemp = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                // if(petTemp.meals > 0)
                {
                    petTemp.energy -= 5;
                    petTemp.happiness += rand.Next(5, 11);
                    HttpContext.Session.SetObjectAsJson("petname", petTemp);
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                    return RedirectToAction("Index", petTemp);
                }
                else
                {
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                    HttpContext.Session.SetString("Message", "Not enough meals to feed the dojodachi");
                    ViewBag.Message = HttpContext.Session.GetString("Message");
                }
            }
            else 
            {
                DojodachiModel petTemp = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                if(petTemp.meals > 0)
                {
                    petTemp.meals -= 1;
                    petTemp.fullness += rand.Next(5, 11);
                    HttpContext.Session.SetObjectAsJson("petname", petTemp);
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                }
                else
                {
                    ViewBag.pet = HttpContext.Session.GetObjectFromJson<DojodachiModel>("petname");
                    HttpContext.Session.SetString("Message", "Not enough meals to feed the dojodachi");
                    ViewBag.Message = HttpContext.Session.GetString("Message");
                }
            }
            return View("Index");
        }
    }

    public static class SessionExtensions
    {
            public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        // This helper function simply serializes theobject to JSON and stores it as a string in session
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
       
    // generic type T is a stand-in indicating that we need to specify the type on retrieval
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        string value = session.GetString(key);
        // Upon retrieval the object is deserialized based on the type we specified
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
    }
}