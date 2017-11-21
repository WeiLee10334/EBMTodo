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

                //取得 http Post RawData(should be JSO
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);


                var projectList = db.EBMProject.Where(x => x.IsHode == false).Select(x => new { ProjectName = x.ProjectName, ProjectNo = x.ProjectNo }).ToList();
                CIC.OnMessageTypeCheck += (s, e) =>
                {
                    switch (e.CurrentPropertyName)
                    {

                        case "回報日期":
                            if (e.ReceievedMessage.ToUpper() == "T")
                            {
                                //ReceivedMessage.events[0].message.text = DateTime.Now.ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                try
                                {
                                    var checkDateTime = Convert.ToDateTime(e.ReceievedMessage);
                                }
                                catch (Exception)
                                {
                                    e.isMismatch = true;
                                    e.ResponseMessage = "請填入日期 yyyy/MM/dd，今天請填t!";
                                }
                            }
                            break;
                        case "專案":
                            if (!(projectList.Select(x => x.ProjectName).Contains(e.ReceievedMessage.ToUpper())))
                            {
                                e.isMismatch = true;
                                e.ResponseMessage = "你的心只能有 " + string.Join(",", projectList.Select(x => x.ProjectName).ToArray());
                            }
                            //case "您的工作項目":

                            //    var getUserTask = db.EBMProjectTodoList.Where(x => x.EBMProjectMember.ApplicationUser.LineID == e.SourceEvent.replyToken);

                            //    //getUserTask.Count()
                            //    if (!(projectList.Select(x => x.ProjectNo).Contains(e.ReceievedMessage.ToUpper()) || projectList.Select(x => x.ProjectName).Contains(e.ReceievedMessage.ToUpper())))
                            //    {
                            //        e.isMismatch = true;
                            //        e.ReceievedMessage = "你的心只能有 " + string.Join(",", projectList.Select(x => x.ProjectName).ToArray());
                            //    }

                            break;
                        default:
                            break;
                    }

                };


                //定義接收CIC結果的類別
                ProcessResult<LeaveRequestV2> result;
                if (ReceivedMessage.events[0].message.text == "回報")
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
                        responseMsg += $"辛苦你了!!";
                        try
                        {
                            var pID = db.EBMProject.Where(x => x.ProjectName.ToUpper() == result.ConversationState.ConversationEntity.專案).FirstOrDefault().PID;
                            var token = ReceivedMessage.events[0].replyToken;
                            var getUser = db.Users.Where(x => x.LineID == token).FirstOrDefault();

                            var newWork = new Models.Todo.EBMProjectWorking();
                            newWork.RecordDateTime = result.ConversationState.ConversationEntity.回報日期.ToUpper() == "T" ? DateTime.Now : Convert.ToDateTime(result.ConversationState.ConversationEntity.回報日期);

                            newWork.Target = result.ConversationState.ConversationEntity.您的工作項目;
                            newWork.Description = result.ConversationState.ConversationEntity.內容;
                            newWork.LineUID = token;
                            newWork.PID = pID;
                            newWork.WokingHour = Convert.ToDecimal(result.ConversationState.ConversationEntity.花費時數);
                            newWork.workingType = Models.Base.Enum.WorkingType.一般;
                            if (getUser == null)
                            {
                                getUser = db.Users.Where(x => x.UserName == "admin").FirstOrDefault();
                            }
                            newWork.Id = getUser.Id;
                            db.EBMProjectWorking.Add(newWork);
                            db.SaveChanges();
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                        {
                            foreach (var eve in ex.EntityValidationErrors)
                            {
                                responseMsg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    responseMsg += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                        }

                        catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ed)
                        {
                            responseMsg += ed.Message;
                        }
                        catch (Exception e)
                        {
                            responseMsg += e.Message;

                        }

                        //responseMsg += Newtonsoft.Json.JsonConvert.SerializeObject(result.ConversationState.ConversationEntity);
                        break;
                    case ProcessResultStatus.Pass:
                        responseMsg = $"你說的 '{ReceivedMessage.events[0].message.text}' 我看不懂，如果想要回報工作進度，請跟我說 : 『回報』";
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
