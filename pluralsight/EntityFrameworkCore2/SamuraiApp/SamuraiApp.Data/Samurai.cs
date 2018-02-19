using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public int BattleId { get; set; }

        public Samurai()
        {
            Quotes = new List<Quote>();
        }
    }
}
