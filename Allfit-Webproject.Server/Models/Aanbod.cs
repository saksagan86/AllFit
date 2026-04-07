using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace Allfit_Webproject.Server.Models
{

    // Hier vertellen we de JSON vertaler welke subklassen er zijn en wordt er een type discriminator/labeltje toegevoegd
    [JsonDerivedType(typeof(Groepsles), typeDiscriminator: "groepsles")]
    [JsonDerivedType(typeof(Fitness), typeDiscriminator: "fitness")]
    [JsonDerivedType(typeof(Kickboks), typeDiscriminator: "kickboks")]

    public abstract class Aanbod
    {
        public int id { get; set; }
        public string naam { get; set; }
        //public Sportschool sportschool;




    }
}
