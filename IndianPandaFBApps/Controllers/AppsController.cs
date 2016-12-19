
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Facebook;
using IndianPandaFBApps.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace IndianPandaFBApps.Controllers
{
    public class AppsController : Controller
    {
        Random rnd;
        String[] location = {"Mumbai","Delhi","Hyderabad","Uri", "Punchh",
            "Pathankot", "Leh", "Ladakh","Lahore", "Karachi", "Paris", "London", "PoK", "Islamabad" };
        public AppsController()
        {
           rnd = new Random();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SecretArmyMission()
        { 
            return View();
        }

        [HttpGet]
        public JsonResult GetSecretArmyMissionData()
        {
            FacebookClient fb = new FacebookClient();
            try
            {
                var cookie = Request.Cookies.Get("panda-apps-data");
                fb.AccessToken = cookie.Value;
                dynamic pic = fb.Get("me/picture?width=400&height=400&type=large&redirect=false");
                var resp = new SecretArmyMissionObject();
                resp.DisplayPictureLink = Convert.ToString(pic.data.url);
                if (string.IsNullOrEmpty(resp.DisplayPictureLink))
                {
                    return null;
                }
                dynamic me = fb.Get("me?fields=friends");
                int friends =Convert.ToInt32(me.friends.summary.total_count);
                if(friends > 50)
                {
                    var digits = NumbersIn(friends);
                    int locn = rnd.Next(14);
                    resp.Headshots = rnd.Next(10);
                    resp.Kills = ((friends + 99) / 100) + resp.Headshots;
                    resp.Location = location[locn];
                    resp.Merit = 0;
                    if(resp.Kills > 20)
                    {
                        resp.Merit = resp.Merit + 3;
                    }
                    else if(resp.Kills > 10)
                    {
                        resp.Merit = resp.Merit + 2;
                    }
                    else
                    {
                        resp.Merit = resp.Merit + 1;
                    }
                    if(resp.Headshots > 5)
                    {
                        resp.Merit = resp.Merit + 2;
                    }
                    else
                    {
                        resp.Merit = resp.Merit + 1;
                    }

                    if(resp.Merit == 5)
                    {
                        resp.Description = "PERFECT MISSION! Your efforts to eliminate the enemies of the nation has again set a new benchmark."
                            + " You killed " + resp.Kills + " including " + resp.Headshots + " headshots in a recent combat at " + resp.Location 
                            + ". You used your guile, strength and modern warfare to make this nation safe again.";
                    }
                    else if(resp.Merit == 4)
                    {
                        resp.Description = "AWESOME MISSION! Your efforts to eliminate the enemies of the nation has again set a new benchmark."
                            + " You killed " + resp.Kills + " including " + resp.Headshots + " headshots in a recent combat at " + resp.Location
                            + ". You used your guile, strength and modern warfare to make this nation safe again.";
                    }
                    else if (resp.Merit == 3)
                    {
                        resp.Description = "CRITICAL MISSION! Your efforts to eliminate the enemies and gather maximum information has again set a new benchmark."
                            + " You killed " + resp.Kills + " including " + resp.Headshots + " headshots in a recent combat at " + resp.Location
                            + ". You used your guile, strength and modern warfare to make this nation safe again.";
                    }

                    else if (resp.Merit < 3)
                    {
                        resp.Description = "CRITICAL MISSION! Your efforts to eliminate the enemies and gather maximum information has again set a new benchmark."
                            + " You killed " + resp.Kills + " including " + resp.Headshots + " headshots in a recent combat at " + resp.Location
                            + ". You used your guile and stealth to make this nation safe again.";
                    }
                    
                }
                else
                {
                    resp.Kills = 0;
                    resp.Headshots = 0;
                    resp.Location = "Antarctica";
                    resp.Merit = 2;
                    resp.Description = "CRITICAL MISSION! Your efforts to gather maximum information has again set a new benchmark."
                            + " You gathered critical information from " + resp.Location
                            + ". You used your guile, strength and modern warfare to make this nation safe again.";
                }
                fb = null;



                var returnObj = new SecretArmyMissionResponse
                {
                    url = GetUrlString(ImageOverlay(resp)),
                    description = resp.Description
                };

                return Json(returnObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                fb = null;
            }
            return null;
        }

        public int[] NumbersIn(int value)
        {
            var numbers = new Stack<int>();

            for (; value > 0; value /= 10)
                numbers.Push(value % 10);

            return numbers.ToArray();
        }

        internal string ImageOverlay(SecretArmyMissionObject request)
        {
            System.Net.WebRequest request0 = System.Net.WebRequest.Create("https://s27.postimg.org/t13najtj7/terrorists_Kill.jpg");
            System.Net.WebResponse response0 = request0.GetResponse();
            System.IO.Stream responseStream0 = response0.GetResponseStream();
            var baseImage = new Bitmap(responseStream0);

            System.Net.WebRequest request1 = System.Net.WebRequest.Create(request.DisplayPictureLink);
            System.Net.WebResponse response1 = request1.GetResponse();
            System.IO.Stream responseStream1 =  response1.GetResponseStream();
            var dp = new Bitmap(responseStream1);

            System.Net.WebRequest request2 = System.Net.WebRequest.Create("https://s28.postimg.org/h1ksbe0n1/classified_logo.png");
            System.Net.WebResponse response2 = request2.GetResponse();
            System.IO.Stream responseStream2 = response2.GetResponseStream();
            var classified = new Bitmap(responseStream2);

            var finalImage = new Bitmap(baseImage.Width, baseImage.Height, baseImage.PixelFormat);
            
            string header = "\u2605 Secret Army Mission Report \u2605";

            string tagkills = "Kills : ";
            string tagheadShots = "Headshots : ";
            string taglocation = "Location : ";
            string tagmerit = "Merit : ";

            string kills = request.Kills.ToString();
            string headShots = request.Headshots.ToString();
            string location = request.Location;
            string merit = new String('\u2605',request.Merit);
            using (Graphics graphics = Graphics.FromImage(finalImage))
            {
                graphics.CompositingMode = CompositingMode.SourceOver;

                graphics.DrawImage(baseImage, 0, 0, baseImage.Width, baseImage.Height);
                graphics.DrawImage(dp, 170, 160, 150, 150);
                graphics.DrawImage(classified, 640, 410, 450, 160);

                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(Server.MapPath("~/fonts/HVD_Peace.ttf"));

                PointF tagkillLocation = new PointF(640f, 200f);
                PointF tagheadshotLocation = new PointF(640f, 260f);
                PointF taglocationLocation = new PointF(640f, 320f);
                PointF tagmeritLocation = new PointF(640f, 380f);

                PointF headerLocation = new PointF(210f, 11f);
                PointF killLocation = new PointF(834f, 200f);
                PointF headshotLocation = new PointF(984f,260f);
                PointF locationLocation = new PointF(940f,320f);
                PointF meritLocation = new PointF(854f, 380f);

                using (Font heavyFont = new Font(pfc.Families[0], 34, FontStyle.Regular))
                {
                    graphics.DrawString(header, heavyFont, Brushes.GreenYellow, headerLocation);
                    graphics.DrawString(kills, heavyFont, Brushes.Gold, killLocation);
                    graphics.DrawString(headShots, heavyFont, Brushes.Gold, headshotLocation);
                    graphics.DrawString(location, heavyFont, Brushes.Gold, locationLocation);
                    graphics.DrawString(merit, heavyFont, Brushes.Gold, meritLocation);

                    graphics.DrawString(tagkills, heavyFont, Brushes.DarkBlue, tagkillLocation);
                    graphics.DrawString(tagheadShots, heavyFont, Brushes.DarkBlue, tagheadshotLocation);
                    graphics.DrawString(taglocation, heavyFont, Brushes.DarkBlue, taglocationLocation);
                    graphics.DrawString(tagmerit, heavyFont, Brushes.DarkBlue, tagmeritLocation);

                }
            }
            using(var ms = new MemoryStream())
            {
                finalImage.Save(ms, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.ToArray());
            }
        }


        internal string GetUrlString(string base64Image)
        {
            try
            {
                Account account = new Account(
                  "rajesh-panda",
                  "871889588653835",
                  "jF6nbm9mfUS5B38sXnKGl-sKFnI"
                  );

                Cloudinary cloudinary = new Cloudinary(account);

                var uploadParams = new ImageUploadParams()
                {

                    File = new FileDescription("data:image/png;base64," + base64Image)

                };
                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUri.ToString();
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}