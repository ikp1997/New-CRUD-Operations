using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operations
{
    public class CardModel
    {
        [Key]
        public int cardId { get; set; }
        [Column("Card Name")]
        public string? card_Name { get; set; }
        [Column("Image Path")]
        public string? imagePath { get; set; }
        [Column("Price")]
        public int price { get; set; }
    }
}