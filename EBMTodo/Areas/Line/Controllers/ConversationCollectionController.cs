﻿using EBMTodo.Models;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Line.Controllers
{
    public class ConversationCollectionController : ApiController
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
            //設定你的Channel Access Token
            string ChannelAccessToken = "oGbKvhmLIr4tx55QeUIxRFCbrQ8csvyobDTD7AfwSqIcx4wXCJFyhZbZRnfpSDzPyQVa/9NzhHo0Jr+wXTkfP5EQyXqAKa7dsy+0ZscZCvH6ncQ+RjHhBZP23X/j1UB9e4aQDBdixKfOQNBjVLUoFgdB04t89/1O/w1cDnyilFU=";
            isRock.LineBot.Bot bot;
            //如果有Web.Config app setting，以此優先4
            //if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("LineChannelAccessToken"))
            //{
            //    ChannelAccessToken = System.Configuration.ConfigurationManager.AppSettings["LineChannelAccessToken"];
            //}

            //create bot instance
            bot = new isRock.LineBot.Bot(ChannelAccessToken);

            try
            {
                LineCommand commandCollection = new LineCommand();
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);

                commandCollection.Message = ReceivedMessage.events[0].message.text;

                var ReplyToken = ReceivedMessage.events[0].replyToken;
                var lineType = ReceivedMessage.events[0].source.type;

                commandCollection.UserID = ReceivedMessage.events[0].source.userId;
                commandCollection.UserName = bot.GetUserInfo(ReceivedMessage.events[0].source.userId).displayName;
                commandCollection.GroupID = ReceivedMessage.events[0].source.groupId;
                commandCollection.RoomId = ReceivedMessage.events[0].source.roomId;

                var getCommand = commandCollection.Message.ToLower().Substring(0, 2);

                switch (getCommand)
                {
                    case "/i":
                        commandCollection.Message = commandCollection.Message.Substring(2,commandCollection.Message.Length-2);
                        commandCollection.Command = "Issue";
                        commandCollection.CommandStatus = Models.Base.Enum.CommandStatus.進行中;
                        commandCollection.CommandType = Models.Base.Enum.CommandType.Command;
                        db.LineCommand.Add(commandCollection);
                        db.SaveChanges();

                        bot.ReplyMessage(ReplyToken, "已接收"+commandCollection.UserName + "回報Issue，項目號:#" + commandCollection.IssueNo);

                        break;
                    case "/b":
                        commandCollection.Message = commandCollection.Message.Substring(2, commandCollection.Message.Length - 2);
                        commandCollection.Command = "Bug";
                        commandCollection.CommandStatus = Models.Base.Enum.CommandStatus.進行中;
                        commandCollection.CommandType = Models.Base.Enum.CommandType.Command;
                        db.LineCommand.Add(commandCollection);
                        db.SaveChanges();

                        bot.ReplyMessage(ReplyToken, "已接收" + commandCollection.UserName + "回報BUG，項目號:#" + commandCollection.IssueNo);
                        break;
                    case "/r":
                        commandCollection.Message = commandCollection.Message.Substring(2, commandCollection.Message.Length - 2);
                        commandCollection.Command = "Record";
                        commandCollection.CommandStatus = Models.Base.Enum.CommandStatus.進行中;
                        commandCollection.CommandType = Models.Base.Enum.CommandType.Command;
                        db.LineCommand.Add(commandCollection);
                        db.SaveChanges();

                        bot.ReplyMessage(ReplyToken, "已紀錄" + commandCollection.UserName + "，追殺代碼:#" + commandCollection.IssueNo);

                        break;
                    default:
                        commandCollection.CommandStatus = Models.Base.Enum.CommandStatus.一般對話;
                        commandCollection.CommandType = Models.Base.Enum.CommandType.Text;
                        if(commandCollection.Message.Length>20)
                        {
                            db.LineCommand.Add(commandCollection);
                            db.SaveChangesAsync();
                        }
                     
                        //db.L
                        //回覆訊息
                        //string Message = testMessage;
                        //回覆用戶
                        //bot.ReplyMessage(ReplyToken, Message);
                        break;
                }
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}
