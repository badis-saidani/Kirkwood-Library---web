using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    class Message
    {
        public int MessageID { get; set; }
        public string Text { get; set; }
        public int ConversationID { get; set; }
        public string DateOfMessage { get; set; }
        public string Sender { get; set; }
        
    }
}
