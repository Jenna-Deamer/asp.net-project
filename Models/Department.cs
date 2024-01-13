using System.ComponentModel.DataAnnotations;
//This attached the dataAnnotation library. allowing us to create validations 
//and constraints on fields going into the DB

namespace newAspProject.Models
{
    public class Department
    {
        [Key] //Data annotation (specifies this is a primary key)

        //all props for DB have to be public. We access them from everywhere
        public int Id { get; set; } = 0; //set the value to 0. When we add a new record to DB it will auto increment
        [Required, StringLength(300)] //makes this field required prevents it from being null
        public string Name { get; set; } = String.Empty; //default value is empty string 

        //max 1000 characters allowed for this field
        [StringLength(1000)]
        //string? Allows the field to be optional. it is best practice to pre set it. 
        public string? Description { get; set; } = String.Empty;

        //Relationship with Products and place to store products in the Department instance
        //ICollection is like an array
        public virtual ICollection<Product>? Products { get; set; }
        //creates a relationship to the table and says product table is the child will allow us to auto
        //load that department record up with products

    }
}