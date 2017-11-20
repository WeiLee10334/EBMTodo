using EBMTodo.Lib.CIC;
using EBMTodo.Models;
using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Line.Controllers
{
    public class RDCICBotController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public IHttpActionResult GET()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult POST()
        {
            string ChannelAccessToken = "JezMOCW65vjOAMH43Hr/lNI/bFUt9gEhXIF1pk4U+X9NknvRG7Fu0LRS9NaJo2uyucCdn+JeJ+a9l/IgrvGXU3eLGOHcwn5ivn5H9ZqZC5qkq4Ek5FbZNwlpB6TMHhVM/vE2TzWfzwg4IOd5UrSN7QdB04t89/1O/w1cDnyilFU=";
            var responseMsg = "";

            try
            {
                //定義資訊蒐集者
                isRock.LineBot.Conversation.InformationCollector<LeaveRequestV2> CIC =
                    new isRock.LineBot.Conversation.InformationCollector<LeaveRequestV2>(ChannelAccessToken);
                var projectList = db.EBMProject.Select(x => x.ProjectName).ToList();
                CIC.OnMessageTypeCheck += (s, e) => {
                    switch (e.CurrentPropertyName)
                    {
                        
                        case "回報日期":
                            if (e.ReceievedMessage == "t")
                            {
                                e.ReceievedMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                            break;
                        case "專案":
                            if(!projectList.Contains(e.ReceievedMessage.ToUpper()))
                            {
                                e.ReceievedMessage ="你的心只有 "+ string.Join(",", projectList.ToString());
                            }

                            break;
                        default:
                            break;
                    }

                };

                //取得 http Post RawData(should be JSO
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //定義接收CIC結果的類別
                ProcessResult<LeaveRequestV2> result;
                if (ReceivedMessage.events[0].message.text == "回報進度")
                {
                    //把訊息丟給CIC 
                    result = CIC.Process(ReceivedMessage.events[0], true);
                    responseMsg = "開始回報進度 取消請輸入/b \n";
                }
                else
                {
                    ReceivedMessage.events[0].message.text = ReceivedMessage.events[0].message.text == "/b" ? "break" : ReceivedMessage.events[0].message.text;
                    //把訊息丟給CIC 
                    result = CIC.Process(ReceivedMessage.events[0]);
                }

                //處理 CIC回覆的結果
                switch (result.ProcessResultStatus)
                {
                    case ProcessResultStatus.Processed:
                        if (result.ResponseButtonsTemplateCandidate != null)
                        {
                            //如果有template Message，直接回覆，否則放到後面一起回覆
                            isRock.LineBot.Utility.ReplyTemplateMessage(
                                ReceivedMessage.events[0].replyToken,
                                result.ResponseButtonsTemplateCandidate,
                                ChannelAccessToken);
                            return Ok();
                        }
                        //取得候選訊息發送
                        responseMsg += result.ResponseMessageCandidate;
                        break;
                    case ProcessResultStatus.Done:
                        responseMsg += result.ResponseMessageCandidate;
                        responseMsg += $"蒐集到的資料有...\n";
                        responseMsg += Newtonsoft.Json.JsonConvert.SerializeObject(result.ConversationState.ConversationEntity);
                        break;
                    case ProcessResultStatus.Pass:
                        responseMsg = $"你說的 '{ReceivedMessage.events[0].message.text}' 我看不懂，如果想要回報工作進度，請跟我說 : 『回報進度』";
                        break;
                    case ProcessResultStatus.Exception:
                        //取得候選訊息發送
                        responseMsg += result.ResponseMessageCandidate;
                        break;
                    case ProcessResultStatus.Break:
                        //取得候選訊息發送
                        responseMsg += result.ResponseMessageCandidate;
                        break;
                    case ProcessResultStatus.InputDataFitError:
                        responseMsg += "\n資料型態不合\n";
                        responseMsg += result.ResponseMessageCandidate;
                        break;
                    default:
                        //取得候選訊息發送
                        responseMsg += result.ResponseMessageCandidate;
                        break;
                }
                //回覆用戶訊息
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, responseMsg, ChannelAccessToken);
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                //如果你要偵錯的話
                //isRock.LineBot.Utility.PushMessage("!!!!!!!!!!!!   換成你自己的 Admin Line UserId !!!!!!!!!!!!!", ex.Message, ChannelAccessToken);
                //return Ok();
                throw ex;
            }
        }

        }
}
