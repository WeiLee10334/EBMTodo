using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Lib.CIC
{
    public class LeaveRequestV2 : ConversationEntity
    {
        [Question("請問您要回報的工作進度日期? (yyyy/MM/dd) 今天請填t")]
        [Order(1)]
        public DateTime 回報日期 { get; set; }

        [Question("請問您要回報的專案為?")]
        [Order(2)]
        public string 專案 { get; set; }

        [Question("請輸入回報內容")]
        [Order(3)]
        public string 回報內容 { get; set; }

        [Question("花費時數?")]
        [Order(4)]
        public int 花費時數 { get; set; }

        [Question("有任何 重要註記或提醒?")]
        [Order(5)]
        public string 註記或提醒 { get; set; }
    }

}