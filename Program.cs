namespace EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new Context.ContextSQL(); // open connection to the database
                                                          // using statment ensures that the context is disposed of properly after use, which closes the connection to the database.

         
        }
    }
}
