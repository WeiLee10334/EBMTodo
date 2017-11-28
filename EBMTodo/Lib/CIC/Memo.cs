using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Lib.CIC
{
    public class Memo : ConversationEntity
    {
        [Question("分類標籤 (多種可用,隔開)")]
        [Order(1)]
        public string Tag { get; set; }

        [Question("註記內容?")]
        [Order(2)]
        public string 內容 { get; set; }

        [Question("重要等級")]
        [Order(3)]
        public int 重要等級 { get; set; }
    }



}