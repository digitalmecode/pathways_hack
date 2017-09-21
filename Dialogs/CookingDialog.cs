using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Sample.FormBot;
using Newtonsoft.Json;

namespace FormBot.Dialogs
{
    [Serializable]
	public class CookingDialog:IDialog
	{
		public async Task StartAsync(IDialogContext context)
		{
			context.Wait(MessageReceivedAsync);
		}
		private static async Task Respond(IDialogContext context)
		{
            var badgeSearch = await new BadgeSearch { Category = "cooking" }.PostAsJsonToApi("GetInterestMatchApi");
            var results = JsonConvert.DeserializeObject<BadgeSearchResults>(badgeSearch);
            			var message =  context.MakeMessage();
			ThumbnailCard card = new ThumbnailCard()
			{
				Title = results.BadgeDetail[0].Name,
				Images = new List<CardImage> { new CardImage(url: results.BadgeDetail[0].ImageUrl) },
				Buttons=new List<CardAction> { new CardAction(
					"openUrl",
					"Start this badge",null,results.BadgeDetail[0].Url)}
			};
			message.Attachments.Add(card.ToAttachment());
            message.Text = "We found this badge for you";
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
 