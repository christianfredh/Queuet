using System.Security.Cryptography.X509Certificates;
using Messages;

namespace Publisher.Console
{
    internal static class Program
    {
        private static void Main()
        {
            using (var publisher = new Publisher())
            {
                publisher.Start();

                System.Console.WriteLine("Publisher started");

                publisher.Publish(new MyMessage("Luke,"));
                publisher.Publish(new MyMessage("I"));
                publisher.Publish(new MyMessage("am"));
                publisher.Publish(new MyMessage("your"));
                publisher.Publish(new MyMessage("father"));


                string message = null;
                while (message != "q")
                {
                    message = System.Console.ReadLine();

                    publisher.Publish(new MyMessage(message));
                }
            }
        }
    }
}
