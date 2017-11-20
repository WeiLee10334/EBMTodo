using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Line.Controllers
{
    public class ChartController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GET()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult POST()
        {
            //設定你的Channel Access Token
            string ChannelAccessToken = "SaYtf1wVmTpk6VwGjioV9ky4Juynt042ihrA89odHmHsFzh/Uy1ACu4GGKaXTbkHx/hHwACVB+Y4gZNaaAFkEymTbgDJAhRExQZtzG0ZQorj3t6pmOSB69SGCRBHZr/MndaCYpA8xhJOdeXsEYkVtwdB04t89/1O/w1cDnyilFU=";
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
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                var UserSays = ReceivedMessage.events[0].message.text;
                var ReplyToken = ReceivedMessage.events[0].replyToken;

                var userId = ReceivedMessage.events[0].source.userId;
                var userName = bot.GetUserInfo(ReceivedMessage.events[0].source.userId).displayName;
                var groupId = ReceivedMessage.events[0].source.groupId;
                var roomId = ReceivedMessage.events[0].source.roomId;
                var lineType = ReceivedMessage.events[0].source.type;


                var testMessage = String.Format("Hi {0} : your Id is :{1} , roomId is : {2} , groupId is : {3} ,LineType is :{4}", userName, userId, roomId, groupId, lineType);
                //依照用戶說的特定關鍵字來回應
                switch (UserSays.ToLower())
                {
                    case "/u":
                        var carouselTemplate = new isRock.LineBot.CarouselTemplate();

                        //var col1 = new isRock.LineBot.Column() { title = "TEST",actions=};
                        //carouselTemplate.columns.Add;
                        //carouselTemplate.altText = "confirm!";

                        //bot.PushMessage(userId, confirmTemplate);

                        break;
                    case "/c":
                        var confirmTemplate = new isRock.LineBot.ConfirmTemplate();
                        confirmTemplate.text = "text";
                        confirmTemplate.altText = "confirm!";

                        bot.PushMessage(userId, confirmTemplate);

                        break;
                    case "/p":
                        try
                        {
                            //建立actions，作為ButtonTemplate的用戶回覆行為
                            var actions = new List<isRock.LineBot.TemplateActionBase>();
                            actions.Add(new isRock.LineBot.MessageActon()
                            { label = "點選這邊等同用戶直接輸入某訊息", text = "/例如這樣" });
                            actions.Add(new isRock.LineBot.UriActon()
                            { label = "點這邊開啟網頁", uri = new Uri("http://www.google.com") });
                            actions.Add(new isRock.LineBot.PostbackActon()
                            { label = "點這邊發生postack", data = "abc=aaa&def=111" });

                            //單一Button Template Message
                            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
                            {
                                altText = "替代文字(在無法顯示Button Template的時候顯示)",
                                text = "測試TEXT",
                                title = "測試",
                                //設定圖片
                                thumbnailImageUrl = new Uri("https://upload.wikimedia.org/wikipedia/commons/d/da/Internet2.jpg"),
                                actions = actions //設定回覆動作
                            };

                            //bot.ReplyMessage(ReplyToken, "TEST");
                            ////發送
                            //bot.PushMessage(userId, "TEST2");
                            bot.PushMessage(userId, ButtonTemplate);
                        }
                        catch (Exception e)
                        {

                            bot.ReplyMessage(ReplyToken, e.Message);

                        }
                        break;


                    case "/t":
                        //回覆貼圖
                        bot.ReplyMessage(ReplyToken, 1, 1);
                        break;
                    case "/i":
                        //回覆圖片
                        bot.ReplyMessage(ReplyToken, new Uri("https://upload.wikimedia.org/wikipedia/commons/d/da/Internet2.jpg"));
                        break;
                    default:
                        //回覆訊息
                        string Message = testMessage;
                        //回覆用戶
                        bot.ReplyMessage(ReplyToken, Message);
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
