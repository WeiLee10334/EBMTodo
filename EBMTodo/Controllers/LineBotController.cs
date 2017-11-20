using EBMTodo.Lib;
using EBMTodo.Models.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EBMTodo.Controllers
{
    public class LineBotController : ApiController
    {
        //public async Task<IHttpActionResult> PostLineBot([FromBody] LineWebhookModels data)
        //{
        //    if (data == null) return BadRequest();
        //    if (data.events == null) return BadRequest();

        //    foreach (Event e in data.events)
        //    {
        //        if (e.type == EventType.message)
        //        {
        //            string senderID = "";
        //            switch (e.source.type)
        //            {
        //                case SourceType.user:
        //                    senderID = e.source.userId;
        //                    break;
        //                case SourceType.room:
        //                    senderID = e.source.roomId;
        //                    break;
        //                case SourceType.group:
        //                    senderID = e.source.groupId;
        //                    break;
        //            }

        //        }
        //    }
        //    return Ok();
        //}

        public IHttpActionResult PostLineBot([FromBody] LineWebhookModels data)
        {
            if (data == null) return BadRequest();
            if (data.events == null) return BadRequest();

            foreach (Event e in data.events)
            {
                if (e.type == EventType.message)
                {
                    ReplyBody rb = new ReplyBody()
                    {
                        replyToken = e.replyToken,
                        messages = procMessage(e.message)
                    };
                    Reply reply = new Reply(rb);
                    reply.send();

                }
            }
            return Ok(data);
        }
        private List<SendMessage> procMessage(ReceiveMessage m)
        {
            List<SendMessage> msgs = new List<SendMessage>();
            SendMessage sm = new SendMessage()
            {
                type = Enum.GetName(typeof(MessageType), m.type)
            };
            switch (m.type)
            {
                case MessageType.sticker:
                    sm.packageId = m.packageId;
                    sm.stickerId = m.stickerId;
                    break;
                case MessageType.text:
                    sm.text = m.text;
                    break;
                default:
                    sm.type = Enum.GetName(typeof(MessageType), MessageType.text);
                    sm.text = "很抱歉，我只是一隻回音機器人，目前只能回覆基本貼圖與文字訊息喔！";
                    break;
            }
            msgs.Add(sm);
            return msgs;
        }

    }
}
