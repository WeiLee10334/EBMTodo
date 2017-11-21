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
        public string 回報日期 { get; set; }

        [Question("請問您要回報的專案為?")]
        [Order(2)]
        public string 專案 { get; set; }

        [Question("請填入您的工作項目(專案程式名稱)")]
        [Order(3)]
        public string 您的工作項目 { get; set; }

        [Question("請輸入回報工作內容")]
        [Order(4)]
        public string 內容 { get; set; }

        [Question("花費時數?")]
        [Order(5)]
        public int 花費時數 { get; set; }

        [Question("有任何 重要註記或提醒? 無請填入N")]
        [Order(6)]
        public string 註記或提醒 { get; set; }
    }

}