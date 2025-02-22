using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int BakeId { get; set; }

        public string User { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedDate { get; set; }

        // virtual is for the entity framework to link the Review table to the Bake table 
        // default in case not initialised and so will be set as default instead of null
        //default of type Bake
        // ! is null-forgiving operator
        // BakeId is from above and set as a foreign key from the Bake table 
        [ForeignKey("BakeId")]
        public virtual Bake Bake { get; set; } = default!;
    }
}
