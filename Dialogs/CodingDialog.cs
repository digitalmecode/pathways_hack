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
    public class CodingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            var codingGreeting = context.MakeMessage();
            codingGreeting.Text = "I am a bit of a geek as well :) what kind of coding do you like?";
            await context.PostAsync(codingGreeting);

            context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
            var badgeSearch = await new BadgeSearch { Category = "coding" }.PostAsJsonToApi("GetInterestMatchApi");
            var results = JsonConvert.DeserializeObject<BadgeSearchResults>(badgeSearch);
            var message = context.MakeMessage();
            ThumbnailCard card = new ThumbnailCard()
            {
                Title = results.BadgeDetail[0].Name,
                Images = new List<CardImage> { new CardImage(url: results.BadgeDetail[0].ImageUrl) },
                Buttons = new List<CardAction>
                {
                    new CardAction(
                        "openUrl",
                        "Click to get started", null, results.BadgeDetail[0].Url)
                }
            };
            message.Attachments.Add(card.ToAttachment());
            message.Text = "Here is an oppurtunity for you. It will help you stay up to date and learn new coding skills.";
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
