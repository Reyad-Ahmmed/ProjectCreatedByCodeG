using System;

namespace CDO
{
    internal class Message
    {
        internal string From;

        public string Sender { get; internal set; }
        public string To { get; internal set; }
        public string HTMLBody { get; internal set; }
        public string Subject { get; internal set; }
        public string CC { get; internal set; }
        public string BCC { get; internal set; }

        internal void Send()
        {
            throw new NotImplementedException();
        }
    }
}