using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace EventSourceSampleInheritance
{
    public class SampleEventSource : EventSource 
    {
        private static readonly Lazy<SampleEventSource> instance = new Lazy<SampleEventSource>(() => new SampleEventSource());
        private SampleEventSource() : base("roka-event-source") { }

        public static SampleEventSource Logger => instance.Value;

        public void Startup() => WriteEvent(1);

        public void CallService(string url) => WriteEvent(2, url);

        public void CalledService(string url, int length) => WriteEvent(3, url, length);

        public void ServiceError(string message, int error) => WriteEvent(4, message, error);
    }
}
