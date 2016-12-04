using System;
using System.Threading.Tasks;
using Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;
using Rebus.NewtonsoftJson;
using Rebus.Persistence.FileSystem;
using Rebus.Routing.TypeBased;
using Rebus.SqlServer.Transport;
using Rebus.Transport.FileSystem;

namespace Publisher
{
    public class Publisher : IDisposable
    {
        private readonly BuiltinHandlerActivator _activator;

        public Publisher()
        {
            _activator = new BuiltinHandlerActivator();
        }

        public void Start()
        {
            // Pub/sub
            //Configure
            //    .With(_activator)
            //    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Debug))
            //    //.Serialization(s => s.UseNewtonsoftJson())
            //    .Transport(t => t.UseSqlServerAsOneWayClient(@"Server=.\sql;Database=queuet;Trusted_Connection=True;", "messages"))
            //    //.Transport(t => t.UseFileSystem(@"C:\bus", "publisher"))
            //    //.Subscriptions(s => s.UseJsonFile(@"C:\bus\subscriptions.json")) 
            //    .Subscriptions(s => s.StoreInSqlServer(@"Server=.\sql;Database=queuet;Trusted_Connection=True;", "subscribers"))
            //    //.Routing(r => r.TypeBased().MapAssemblyOf<MyMessage>("subscriber1"))
            //    .Start();


            // Send/Receive
            Configure
                .With(_activator)
                .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                //.Serialization(s => s.UseNewtonsoftJson())
                .Transport(t => t.UseSqlServerAsOneWayClient(@"Server=.\sql;Database=queuet;Trusted_Connection=True;", "messages"))
                //.Transport(t => t.UseFileSystem(@"C:\bus", "publisher"))
                //.Subscriptions(s => s.UseJsonFile(@"C:\bus\subscriptions.json")) 
                .Subscriptions(s => s.StoreInSqlServer(@"Server=.\sql;Database=queuet;Trusted_Connection=True;", "subscribers"))
                .Routing(r => r.TypeBased().MapAssemblyOf<MyMessage>("subscriber1"))
                .Start();
        }

        public Task PublishAsync(MyMessage message)
        {
            return _activator.Bus.Send(message);
        }

        public void Publish(MyMessage message)
        {
            PublishAsync(message).Wait();
        }

        public void Dispose()
        {
            _activator.Dispose();
        }
    }
}
