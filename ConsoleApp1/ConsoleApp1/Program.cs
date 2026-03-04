using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            #region Part1Q1
            //A) public fields and the function does not check the business rules
            //B) make the fields private, create properties for them and modifiy the function to validate the business rules 
            //C) because that would make it possible to change the vales of the fields with no validation or applying for the business
            #endregion

            #region Part1Q2
            /*
             the properties are used for setting and getting the values of the fields but the methods are supposed to be used to aplly logic
             yes the property can contain logic like validating the value before we assign it to the field
             Example: total price => quantity * unitPrice 
             */
            #endregion

            #region Part1Q3
            //A) it is called an indexer and it is used to access an object inside the class without the need to use functions 
            //B) this will cause the code to crash, to make it safer we should validate the index parameter before we assign it  
            //C) yes, we can have an indexer which uses the index of the list and we can build another one which usees the id or another property (like the name) of the object 
            #endregion

            #region Part1Q4
            //A) it means that it is a static field , that makes it shared across all the object because it belongs to the class itself
            //B) no, because the static method does not know which object’s field it should use
            #endregion
        }

    }
}