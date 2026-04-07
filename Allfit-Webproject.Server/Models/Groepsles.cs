namespace Allfit_Webproject.Server.Models
{
    public class Groepsles : Aanbod
    {

        public string niveau { get; set; }
        public string duur { get; set; }
        //public Trainer trainer { get; set; }

        public Groepsles(string niveau, string duur) {
            this.niveau = niveau;
            this.duur = duur;

        }


    }
}
