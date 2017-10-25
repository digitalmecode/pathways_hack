using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Sample.FormBot;
using Newtonsoft.Json;

namespace FormBot.Dialogs
{
    [Serializable]
    public class CookingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            var cookingGreeting = context.MakeMessage();
            cookingGreeting.Text = "I love cooking! My lasagna rocks :) what kind of cooking do you like?";
            await context.PostAsync(cookingGreeting);

            context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
            var badgeSearch = await new BadgeSearch {Category = "cooking"}.PostAsJsonToApi("GetInterestMatchApi");
            var results = JsonConvert.DeserializeObject<BadgeSearchResults>(badgeSearch);
            var message = context.MakeMessage();
            ThumbnailCard card = new ThumbnailCard()
            {
                Title = results.BadgeDetail[0].Name,
                Images = new List<CardImage> {new CardImage(url: results.BadgeDetail[0].ImageUrl)},
                Buttons = new List<CardAction>
                {
                    new CardAction(
                        "openUrl",
                        "Click to get started", null, results.BadgeDetail[0].Url)
                }
            };
            message.Attachments.Add(card.ToAttachment());
            message.Text = "Here is an oppurtunity to up your cooking game. So by completing this badge you will stand out from the crowd. Oh, and it is backed by a big culinary organisation!";
            await context.PostAsync(message);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            await Respond(context);
            context.Done(message);
        }
    }
}
 