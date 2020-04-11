using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMN2B1.Models
{
    [Table("cattle")]
    public class Cattle
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }        
        public const string Type = "Animal";
        public string Identifier { get; set; }
        public Specie Specie { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }

        //public double Weight { get; set; }

        public override string ToString()
        {
            return Identifier;
        }
    }

    public enum Sex
    {
        Fêmea,
        Macho        
    }

    public enum Specie
    {
        Leiteiro,
        Corte
    }
}
