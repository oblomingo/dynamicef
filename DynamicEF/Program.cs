using System;
using WebTest;

namespace DynamicEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppContext context = new AppContext())
            {
                var type = DynamicTypeBuilder.CreateType();
                var myObject = Activator.CreateInstance(type);
                Console.WriteLine("Hello World!");
            }

        }
    }
}
