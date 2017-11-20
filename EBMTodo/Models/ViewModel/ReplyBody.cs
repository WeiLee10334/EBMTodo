using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Models.Line
{
    public class ReplyBody
    {
        public string replyToken { get; set; }
        public List<SendMessage> messages { get; set; }
    }
}