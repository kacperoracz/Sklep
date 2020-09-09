using System;
using System.ComponentModel.DataAnnotations;

namespace Sklep.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Podanie nazwy przedmiotu jest wymagane")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Podanie ceny jest wymagane")]
        [Range(0.01, Double.PositiveInfinity, ErrorMessage = "Cena musi być większa od 0")]
        public float? Price { get; set; }
        public string Descryption { get; set; }
        [Required(ErrorMessage = "Wymagana jest liczba dostępnych przedmiotów")]
        [Range(0, Double.PositiveInfinity, ErrorMessage="Ilość przedmiotów mósi być przynajmniej równa 0")]
        public int? Number { get; set; }
    }
}
