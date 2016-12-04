using System.Security.AccessControl;

namespace Subscriber.Console
{
    internal class Program
    {
        private static void Main()
        {
            using (var subscriber = new MyMessageSubscriber())
            {
                subscriber.Start();

                System.Console.WriteLine("MyMessageSubscriber started");
                System.Console.ReadLine();
            }
        }
    }
}
