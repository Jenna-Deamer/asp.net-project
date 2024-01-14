using System.ComponentModel.DataAnnotations;
//This attached the dataAnnotation library. allowing us to create validations 
//and constraints on fields going into the DB

namespace newAspProject.Models
{
    //this will create a table called Department
    //it will have a id, name and desc column
    public class Department
    {
        [Key] //Data annotation (specifies this is a primary key)
        //this represents a function, the next augments is the next line under it

        //all props for DB have to be public. We access them from everywhere
        public int Id { get; set; } = 0; //set the value to 0. When we add a new record to DB it will auto increment
        [Required, StringLength(300)] //makes this field required prevents it from being null
        public string Name { get; set; } = String.Empty; //default value is empty string 

        //max 1000 characters allowed for this field
        [StringLength(1000)]
        //string? Allows the field to be optional. it is best practice to pre set it. 
        public string? Description { get; set; } = String.Empty;

        //ICollection is like an array
          //this field is meant to store the products related to that department
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
        //creates a relationship to the table and says product table is the child will allow us to auto
        //load that department record up with products

      
    }
}