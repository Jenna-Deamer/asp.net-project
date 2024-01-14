using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newAspProject.Models
{
    public enum ProductWeightUnit
    {
        //Capital letters  for enums like constants
        //never add to the middle or top. Only add new to the bottom of the list
        //grams is 0, kilo is 1 so on...
        //if you add new items in. it will malign the data in the db
        GRAMS,
        KILOGRAMS,
        POUNDS,
        OUNCES,
        LITERS,
        UNITS
    }
    public class Product
    {
        //will auto setup as a primary key with autoIncr during migration
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; } = 0;

        [Required, StringLength(300)]
        public string Name { get; set; } = String.Empty;

        [StringLength(1000)]
        public string? Description { get; set; } = String.Empty;

        [StringLength(250)]
        //storing the image path. Optional field for now.
        public string? Image { get; set; } = String.Empty;

        [Required]
        //value must be between 2 points
        [Range(0.01, 99999.99)]
        //Make datatype currency. Ensures it is stored as currency
        [DataType(DataType.Currency)]
        public decimal MSRP { get; set; } = 0.01M;

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Weight {get; set;} = 0.01M; //post prefix it with M


        [Required]
        public ProductWeightUnit WeightUnit {get; set;} = ProductWeightUnit.UNITS;

        //no product can exist without a parent (Department)
        [ForeignKey("DepartmentId")]
        //has to be optional, since this wont exist when product modal is first created
        //it has to get it later
        public virtual Department? Department {get; set;} //creates association to departments
        //allows a department to be stored in an instance of a product
        //virtual field. Allow us to store department record. without assuming its another prop going into our db table 
        //its in the doc. use it even though it seems to work without it.

    }
}