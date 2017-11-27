using EBMTodo.Lib.CIC;
using EBMTodo.Models;
using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Wei.Controllers
{
    public class MyCICBotController : ApiController
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
            string ChannelAccessToken = "zxQLMZeaoS3DVwBlZ4InMqFI/ngwJHFQnwllK6y3jktNENDkbnKfP8kVzgTZGMmGUcqrzOdpl/sXB6Lpn/sxztP3m57bjSddGZp9CPRMQwJW2rzNqClzrlVbhV2NTYZbRUIFZq/EU0dYBVrCBaS4EQdB04t89/1O/w1cDnyilFU=";
            var responseMsg = "";

            isRock.LineBot.Bot bot;
            bot = new isRock.LineBot.Bot(ChannelAccessToken);
            //取得 http Post RawData(should be JSON)
            string postData = Request.Content.ReadAsStringAsync().Result;
            //剖析JSON
            var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);

            try
            {
               


                var UserSays = ReceivedMessage.events[0].message.text;
                var ReplyToken = ReceivedMessage.events[0].replyToken;
                var userId = ReceivedMessage.events[0].source.userId;
                var userName = bot.GetUserInfo(ReceivedMessage.events[0].source.userId).displayName;
                var groupId = ReceivedMessage.events[0].source.groupId;
                var roomId = ReceivedMessage.events[0].source.roomId;
                var lineType = ReceivedMessage.events[0].source.type;

                bool initFlag = false;

                #region MyRegion
                //if (UserSays.ToUpper() == "/S")
                //{
                //    //定義資訊蒐集者
                //    isRock.LineBot.Conversation.InformationCollector<Schedule> ScheduleCIC =
                //        new isRock.LineBot.Conversation.InformationCollector<Schedule>(ChannelAccessToken);

                //    var projectList = db.EBMProject.Where(x => x.IsHode == false).Select(x => new { ProjectName = x.ProjectName, ProjectNo = x.ProjectNo }).ToList();

                //    ScheduleCIC.OnMessageTypeCheck += (s, e) =>
                //    {
                //        switch (e.CurrentPropertyName)
                //        {
                //            case "行程日期":
                //                if (e.ReceievedMessage.ToUpper() == "T")
                //                {
                //                    //ReceivedMessage.events[0].message.text = DateTime.Now.ToString("yyyy/MM/dd");
                //                }
                //                else
                //                {
                //                    try
                //                    {
                //                        var checkDateTime = Convert.ToDateTime(e.ReceievedMessage);
                //                    }
                //                    catch (Exception)
                //                    {
                //                        e.isMismatch = true;
                //                        e.ResponseMessage = "請填入日期 yyyy/MM/dd，今天請填t!";
                //                    }
                //                }
                //                break;

                //            case "行程類別":
                //                if (!(projectList.Select(x => x.ProjectName).Contains(e.ReceievedMessage.ToUpper())))
                //                {
                //                    e.isMismatch = true;
                //                    e.ResponseMessage = "目前支援類型: " + string.Join(",", Enum.GetValues(typeof(Models.Base.Enum.ScheduleType)).Cast<Models.Base.Enum.ScheduleType>().ToArray());
                //                }
                //                break;
                //            case "專案":
                //                if (!(projectList.Select(x => x.ProjectName).Contains(e.ReceievedMessage.ToUpper())))
                //                {
                //                    e.isMismatch = true;
                //                    e.ResponseMessage = "你的心只能有 " + string.Join(" , ", projectList.Select(x => x.ProjectName).ToArray());
                //                }
                //                break;
                //            default:
                //                break;
                //        }

                //    };
                //    //定義接收CIC結果的類別
                //    ProcessResult<Schedule> result;     
                //    if (ReceivedMessage.events[0].message.text == "/S")
                //    {
                //        //把訊息丟給CIC 
                //        result = ScheduleCIC.Process(ReceivedMessage.events[0], true);
                //        responseMsg = "開始紀錄 取消請輸入/b \n";
                //    }
                //    else
                //    {
                //        ReceivedMessage.events[0].message.text = ReceivedMessage.events[0].message.text == "/b" ? "break" : ReceivedMessage.events[0].message.text;
                //        //把訊息丟給CIC 
                //        result = ScheduleCIC.Process(ReceivedMessage.events[0]);
                //    }

                //    //處理 CIC回覆的結果
                //    switch (result.ProcessResultStatus)
                //    {
                //        case ProcessResultStatus.Processed:
                //            if (result.ResponseButtonsTemplateCandidate != null)
                //            {
                //                //如果有template Message，直接回覆，否則放到後面一起回覆
                //                isRock.LineBot.Utility.ReplyTemplateMessage(
                //                    ReceivedMessage.events[0].replyToken,
                //                    result.ResponseButtonsTemplateCandidate,
                //                    ChannelAccessToken);
                //                return Ok();
                //            }
                //            //取得候選訊息發送
                //            responseMsg += result.ResponseMessageCandidate;
                //            break;
                //        case ProcessResultStatus.Done:
                //            responseMsg += result.ResponseMessageCandidate;
                //            responseMsg += $"我記得了!!";
                //            try
                //            {
                //                var token = ReceivedMessage.events[0].replyToken;
                //                var lineUID = ReceivedMessage.events[0].source.userId;
                //                var getLineUser = db.LineUser.Where(x => x.UID == lineUID).FirstOrDefault();
                //                var pID = db.EBMProject.Where(x => x.ProjectName.ToUpper() == result.ConversationState.ConversationEntity.專案).FirstOrDefault().PID;

                //                if (getLineUser == null)
                //                {
                //                    db.LineUser.Add(new Models.Todo.LineUser() { UID = lineUID, Name = "未知" });
                //                    db.SaveChanges();
                //                }



                //                var newSchedule = new Models.Todo.EBMProjectSchedule();
                //                newSchedule.CreateDateTime = DateTime.Now;



                //                newSchedule.ScheduleDateTime = Convert.ToDateTime(result.ConversationState.ConversationEntity.行程日期);
                //                newSchedule.scheduleType = (Models.Base.Enum.ScheduleType)Enum.Parse(typeof(Models.Base.Enum.ScheduleType), result.ConversationState.ConversationEntity.行程類別, false);
                //                newSchedule.WokingHour = result.ConversationState.ConversationEntity.預期時數;
                //                newSchedule.Target = result.ConversationState.ConversationEntity.地點;
                //                newSchedule.Title = result.ConversationState.ConversationEntity.行程Title;
                //                newSchedule.Description = result.ConversationState.ConversationEntity.行程內容;

                //                newSchedule.LineUID = lineUID;
                //                newSchedule.PID = pID;

                //                var getUser = db.Users.Where(x => x.LineID == lineUID).FirstOrDefault();
                //                if (getUser == null)
                //                {
                //                    getUser = db.Users.Where(x => x.UserName == "admin").FirstOrDefault();
                //                }
                //                newSchedule.Id = getUser.Id;

                //                db.EBMProjectSchedule.Add(newSchedule);
                //                db.SaveChanges();
                //            }
                //            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                //            {
                //                foreach (var eve in ex.EntityValidationErrors)
                //                {
                //                    responseMsg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //                    foreach (var ve in eve.ValidationErrors)
                //                    {
                //                        responseMsg += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                //                            ve.PropertyName, ve.ErrorMessage);
                //                    }
                //                }
                //            }

                //            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ed)
                //            {
                //                responseMsg += ed.Message;
                //            }
                //            catch (Exception e)
                //            {
                //                responseMsg += e.Message;

                //            }

                //            //responseMsg += Newtonsoft.Json.JsonConvert.SerializeObject(result.ConversationState.ConversationEntity);
                //            break;
                //        case ProcessResultStatus.Pass:

                //            break;
                //        case ProcessResultStatus.Exception:
                //            //取得候選訊息發送
                //            responseMsg += result.ResponseMessageCandidate;
                //            break;
                //        case ProcessResultStatus.Break:
                //            //取得候選訊息發送
                //            responseMsg += result.ResponseMessageCandidate;
                //            break;
                //        case ProcessResultStatus.InputDataFitError:
                //            responseMsg += "\n資料型態不合\n";
                //            responseMsg += result.ResponseMessageCandidate;
                //            break;
                //        default:
                //            //取得候選訊息發送
                //            responseMsg += result.ResponseMessageCandidate;
                //            break;
                //    }

                //}
                //else if (UserSays.ToUpper() == "/M")
                //{
                #endregion

                //定義資訊蒐集者
                var getSData = db.EBMProjectSchedule.Where(x => !x.ProgressingFlag && x.LineUID == userId).FirstOrDefault() != null ? db.EBMProjectSchedule.Where(x => !x.ProgressingFlag && x.LineUID == userId).FirstOrDefault() : new Models.Todo.EBMProjectSchedule() { ProgressingFlag = true };

                var getMData = db.Memo.Where(x => !x.ProgressingFlag && x.LineUID == userId).FirstOrDefault() != null ? db.Memo.Where(x => !x.ProgressingFlag && x.LineUID == userId).FirstOrDefault() : new Models.Todo.Memo() { ProgressingFlag = true };

                var getWData = db.EBMProjectWorking.Where(x => !x.ProgressingFlag && x.LineUID == userId).FirstOrDefault() != null ? db.EBMProjectWorking.Where(x => !x.ProgressingFlag && x.LineUID == userId).FirstOrDefault() : new Models.Todo.EBMProjectWorking() { ProgressingFlag = true }; ;


                if (UserSays.ToUpper() == "/S" && getSData.ProgressingFlag && getMData.ProgressingFlag && getWData.ProgressingFlag)
                {
                    //開始  Schedule
                }
                else if (UserSays.ToUpper() == "/M" && getSData.ProgressingFlag && getMData.ProgressingFlag && getWData.ProgressingFlag)
                {
                    //把訊息丟給CIC 
                    isRock.LineBot.Conversation.InformationCollector<Memo> MemoCIC = new isRock.LineBot.Conversation.InformationCollector<Memo>(ChannelAccessToken);
                    //定義接收CIC結果的類別
                    ProcessResult<Memo> MemoResult;
                    //把訊息丟給CIC 
                    MemoResult = MemoCIC.Process(ReceivedMessage.events[0], true);
                    responseMsg = "開始紀錄 取消請輸入/b \n";

                    db.Memo.Add(new Models.Todo.Memo()
                    {
                        LineUID = userId,
                        CreateDateTime = DateTime.Now,
                        ProgressingFlag = false,
                        memoType = Models.Base.Enum.MemoType.ING
                    });
                    db.SaveChanges();

                    initFlag = true;
                    getMData.ProgressingFlag = false;
                }
                else if (UserSays.ToUpper() == "回報" && getSData.ProgressingFlag && getMData.ProgressingFlag && getWData.ProgressingFlag)
                {
                    // 定義資訊蒐集者
                    isRock.LineBot.Conversation.InformationCollector<LeaveRequestV2> WorkCIC =
                    new isRock.LineBot.Conversation.InformationCollector<LeaveRequestV2>(ChannelAccessToken);
                   
                    ProcessResult<LeaveRequestV2> result;
                    //把訊息丟給CIC 
                    result = WorkCIC.Process(ReceivedMessage.events[0], true);
                    responseMsg = "開始回報進度 取消請輸入/b \n";

                    var pID = db.EBMProject.FirstOrDefault().PID;
                    var adminID = db.Users.Where(x => x.UserName == "admin").FirstOrDefault().Id;
                    db.EBMProjectWorking.Add(new Models.Todo.EBMProjectWorking()
                    {
                        LineUID = userId,
                        CreateDateTime = DateTime.Now,
                        ProgressingFlag = false,
                        workingType = Models.Base.Enum.WorkingType.一般,
                        WokingHour = 0,
                        PID = pID,
                        Id = adminID,
                        RecordDateTime = DateTime.Now
                    });
                    db.SaveChanges();

                    initFlag = true;
                    getWData.ProgressingFlag = false;
                }
                else if (getSData.ProgressingFlag && getMData.ProgressingFlag && getWData.ProgressingFlag)
                {
                    responseMsg += "目前我的懂得指令有: \n  /S => 排程  \n  /M => 註記 \n  回報 => 回報工作進度";
                }


                //Memo CIC
                if (getSData.ProgressingFlag && !getMData.ProgressingFlag && getWData.ProgressingFlag)
                {
                    //把訊息丟給CIC 
                    isRock.LineBot.Conversation.InformationCollector<Memo> MemoCIC = new isRock.LineBot.Conversation.InformationCollector<Memo>(ChannelAccessToken);
                    MemoCIC.OnMessageTypeCheck += (s, e) =>
                    {
                        switch (e.CurrentPropertyName)
                        {
                            default:
                                break;
                        }

                    };
                    ProcessResult<Memo> MemoResult;
                    ReceivedMessage.events[0].message.text = ReceivedMessage.events[0].message.text == "/b" ? "break" : ReceivedMessage.events[0].message.text;
                    //把訊息丟給CIC 
                    if(initFlag)
                    {
                        MemoResult = MemoCIC.Process(ReceivedMessage.events[0],true);

                    }
                    else
                    {
                        MemoResult = MemoCIC.Process(ReceivedMessage.events[0]);

                    }
                    //處理 CIC回覆的結果
                    switch (MemoResult.ProcessResultStatus)
                    {
                        case ProcessResultStatus.Processed:
                            if (MemoResult.ResponseButtonsTemplateCandidate != null)
                            {
                                //如果有template Message，直接回覆，否則放到後面一起回覆
                                isRock.LineBot.Utility.ReplyTemplateMessage(
                                    ReceivedMessage.events[0].replyToken,
                                    MemoResult.ResponseButtonsTemplateCandidate,
                                    ChannelAccessToken);
                                return Ok();
                            }
                            //取得候選訊息發送
                            responseMsg += MemoResult.ResponseMessageCandidate;
                            break;
                        case ProcessResultStatus.Done:
                            responseMsg += MemoResult.ResponseMessageCandidate;
                            responseMsg += $"我記得了!!";
                            try
                            {
                                var token = ReceivedMessage.events[0].replyToken;
                                var lineUID = ReceivedMessage.events[0].source.userId;
                                var getUser = db.LineUser.Where(x => x.UID == lineUID).FirstOrDefault();
                                if (getUser == null)
                                {
                                    db.LineUser.Add(new Models.Todo.LineUser() { UID = lineUID, Name = "未知" });
                                    db.SaveChanges();
                                }

                                getMData.CreateDateTime = DateTime.Now;
                                getMData.Tag = MemoResult.ConversationState.ConversationEntity.Tag;
                                getMData.Content = MemoResult.ConversationState.ConversationEntity.內容;
                                getMData.LineUID = lineUID;
                                getMData.memoType = Models.Base.Enum.MemoType.ING;
                                getMData.ProgressingFlag = true;
                                db.Entry(getMData).State = System.Data.Entity.EntityState.Modified;
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
                            responseMsg += MemoResult.ResponseMessageCandidate;
                            break;
                        case ProcessResultStatus.Break:
                            //取得候選訊息發送
                            responseMsg += MemoResult.ResponseMessageCandidate;
                            break;
                        case ProcessResultStatus.InputDataFitError:
                            responseMsg += "\n資料型態不合\n";
                            responseMsg += MemoResult.ResponseMessageCandidate;
                            break;
                        default:
                            //取得候選訊息發送
                            responseMsg += MemoResult.ResponseMessageCandidate;
                            break;
                    }
                }
                //Schedule CIC
                else if (!getSData.ProgressingFlag && getMData.ProgressingFlag && getWData.ProgressingFlag)
                {
                    responseMsg += "Schedule";
                }
                //Working CIC
                else if (getSData.ProgressingFlag && getMData.ProgressingFlag && !getWData.ProgressingFlag)
                {
                    isRock.LineBot.Conversation.InformationCollector<LeaveRequestV2> WorkCIC =
                    new isRock.LineBot.Conversation.InformationCollector<LeaveRequestV2>(ChannelAccessToken);
                    var projectList = db.EBMProject.Where(x => x.IsHode == false).Select(x => new { ProjectName = x.ProjectName, ProjectNo = x.ProjectNo }).ToList();

                    WorkCIC.OnMessageTypeCheck += (s, e) =>
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

                                break;
                            default:
                                break;
                        }

                    };

                    ProcessResult<LeaveRequestV2> result;
                    if (initFlag)
                    {
                        //把訊息丟給CIC 
                        result = WorkCIC.Process(ReceivedMessage.events[0], true);
                        responseMsg = "開始回報進度 取消請輸入/b \n";
                    }
                    else
                    {
                        ReceivedMessage.events[0].message.text = ReceivedMessage.events[0].message.text == "/b" ? "break" : ReceivedMessage.events[0].message.text;
                        //把訊息丟給CIC 
                        result = WorkCIC.Process(ReceivedMessage.events[0]);
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
                            try
                            {
                                var pID = db.EBMProject.Where(x => x.ProjectName.ToUpper() == result.ConversationState.ConversationEntity.專案).FirstOrDefault().PID;
                                var token = ReceivedMessage.events[0].replyToken;
                                var lineUID = ReceivedMessage.events[0].source.userId;
                                var getLineUser = db.LineUser.Where(x => x.UID == lineUID).FirstOrDefault();
                                if (getLineUser == null)
                                {
                                    db.LineUser.Add(new Models.Todo.LineUser() { UID = lineUID, Name = userName });
                                    db.SaveChanges();
                                }




                                getWData.RecordDateTime = result.ConversationState.ConversationEntity.回報日期.ToUpper() == "T" ? DateTime.Now : Convert.ToDateTime(result.ConversationState.ConversationEntity.回報日期);

                                getWData.Target = result.ConversationState.ConversationEntity.您的工作項目;
                                getWData.Description = result.ConversationState.ConversationEntity.內容;
                                getWData.LineUID = lineUID;
                                getWData.PID = pID;
                                getWData.WokingHour = Convert.ToDecimal(result.ConversationState.ConversationEntity.花費時數);
                                getWData.workingType = Models.Base.Enum.WorkingType.一般;
                                var getUser = db.Users.Where(x => x.LineID == lineUID).FirstOrDefault();
                                if (getUser == null)
                                {
                                    getUser = db.Users.Where(x => x.UserName == "admin").FirstOrDefault();
                                }
                                getWData.Id = getUser.Id;
                                getWData.ProgressingFlag = true;
                                db.Entry(getWData).State = System.Data.Entity.EntityState.Modified;
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

                            responseMsg += Newtonsoft.Json.JsonConvert.SerializeObject(result.ConversationState.ConversationEntity);
                            responseMsg += $"\n辛苦你了 ， " + userName + "!!";

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
                else
                {
                    responseMsg += string.Format(" 異常，請通知李偉帆 => SFlag:{0},WFlag:{1},MFlag:{2}" ,getSData.ProgressingFlag.ToString(), getWData.ProgressingFlag.ToString(),getMData.ProgressingFlag.ToString());
                }
               
                //else
                //{
                //    responseMsg = "未預期的錯誤，請通知李偉帆";
                //}
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
                //throw ex;
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, ex.Message, ChannelAccessToken);
                return Ok();
            }
        }

    }
}
