using System;

namespace FieldTool.ClipboardLookup.Logging
{
    public class MessageLog : Exception
    {
        public MessageLog()
            : base()
        { }

        public MessageLog(string message)
            : base(message)
        { }

        public MessageLog(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        { }
    }
}