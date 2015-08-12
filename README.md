# ConsoleTest
Tests the console ouptut in a C# console project 

This projet generates a C# dynamic library. 
The ConsoleStream class contains methods to test the output text in a C# console project.

How to use it:  
1) Build the library.  
2) Create a C# console project.  
3) Display a message on the console.  
Example:
```c#
static void Main(string[] args)
        {
         
            Exercice11_FirstName();
          
            Console.ReadLine();
        }


        /// <summary>
        /// Display Hello and firstname
        /// </summary>
        public static void Exercice11_FirstName()
        {
            
            Console.WriteLine("Enter your firstname: ");
            string name  = Console.ReadLine();
            Console.WriteLine("Hello " + name);
           

        }
```
4) Create an unit test project.  
5) Add the consoleTest DLL to the test project.  
6) Create the unit test method.   

Example : 
```c#
 [TestMethod]
        public void TestExercice11_FirstName()
        {

            string text;
            ConsoleStream cs = new ConsoleStream();
            
            //The data needed by the console.
            //if you have several Console.ReadLine, put all the input strings separated by a comma.
            cs.SetConsoleIn("John");
            
            //The method to test
            cs.SetConsoleOut(Program.Exercice11_FirstName);

            text = cs.GetLastLine();
            
            Assert.IsTrue(text.Contains("hello john"));
            cs.EndTest();
           
        }
        
 ```       

The main isssue is the use of Console.Write that doesn't generate an end of line.   
We need to find the string with the Contains method.


