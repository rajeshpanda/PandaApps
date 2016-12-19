using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianPandaFBApps.Models
{
    public class ApplicationModels
    {
        string cookieValue;
    }

    public class SecretArmyMissionResponse
    {
        public string url, description;
    }

    public class SecretArmyMissionObject
    {
        public string Location;
        public int Kills, Headshots, Merit;
        public string Description;
        public string DisplayPictureLink;
    }

}