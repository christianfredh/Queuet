using System;
using System.Threading.Tasks;
using Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using Rebus.NewtonsoftJson;
using Rebus.Routing.TypeBased;
using Rebus.SqlServer.Transport;
using Rebus.Transport.FileSystem;

namespace Subscriber
{
    public class MyMessageSubscriber : IDisposable
    {
        private readonly BuiltinHandlerActivator _activator;

        public MyMessageSubscriber()
        {
            _activator = new BuiltinHandlerActivator();
        }

        public void Start()
        {


            // Pub/sub
            // _activator.Register(() => new Handler());
            //Configure
            //    .With(_activator)
            //    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Debug))
            //    //.Serialization(s => s.UseNewtonsoftJson())
            //    //.Transport(t => t.UseFileSystem(@"C:\bus", "subscriber1"))
            //    .Transport(t => t.UseSqlServer(@"Server=.\sql;Database=queuet;Trusted_Connection=True;", "messages", "subscriber1"))
            //    .Routing(r => r.TypeBased().MapAssemblyOf<MyMessage>("subscriber1"))
            //    .Start();

            // Send/Revceive

            _activator.Handle<MyMessage>(async message =>
            {
                Console.WriteLine("Got string: {0}", message.Text);

                await Task.Delay(TimeSpan.FromMilliseconds(300));
            });
            Configure
                .With(_activator)
                .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                //.Serialization(s => s.UseNewtonsoftJson())
                //.Transport(t => t.UseFileSystem(@"C:\bus", "subscriber1"))
                .Transport(t => t.UseSqlServer(@"Server=.\sql;Database=queuet;Trusted_Connection=True;", "messages", "subscriber1"))
                //.Routing(r => r.TypeBased().MapAssemblyOf<MyMessage>("subscriber1"))
                .Start();

            //_activator.Bus.Subscribe<MyMessage>();
        }

        private class Handler : IHandleMessages<MyMessage>
        {
            public async Task Handle(MyMessage message)
            {
                Console.WriteLine("Got string: {0}", message.Text);
            }
        }

        public void Dispose()
        {
            _activator.Dispose();
        }
    }
}
