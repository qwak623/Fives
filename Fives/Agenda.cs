using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fives
{
    public class AgendaDb
    {
        [Key]
        public string Cards { get; set; }
        public List<BuyMenuItem> BuyMenu { get; set; }
        public int Provinces { get; set; }
        public int Duchies { get; set; }
        public int Estates { get; set; }
    }

    public class BuyMenuItem
    {
        [Key]
        public int Id { get; set; }
        public int CardId { get; set; }
        public int Number { get; set; }
    }
}