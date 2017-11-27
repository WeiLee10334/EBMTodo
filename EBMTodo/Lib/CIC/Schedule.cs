using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Lib.CIC
{
    public class Schedule : ConversationEntity
    {
        [Question("行程類別")]
        [Order(1)]
        public string 行程類別 { get; set; }

        [Question("請簡略填入行程項目")]
        [Order(2)]
        public string 行程Title { get; set; }

        [Question("行程地點")]
        [Order(3)]
        public string 地點 { get; set; }

        [Question("請問您專案為?")]
        [Order(4)]
        public string 專案 { get; set; }

        [Question("請填入行程說明")]
        [Order(5)]
        public string 行程內容 { get; set; }

        [Question("請輸入行程日期? (yyyy/MM/dd)")]
        [Order(6)]
        public string 行程日期 { get; set; }

        [Question("預期花費時數?")]
        [Order(7)]
        public int 預期時數 { get; set; }

    }

}