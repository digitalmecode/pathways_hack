using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Internals.Fibers;

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
			var message =  context.MakeMessage();
			ThumbnailCard card = new ThumbnailCard()
			{
				Title = "Professional Chef",
				Images = new List<CardImage> { new CardImage(url: "https://obaprod.s3-eu-west-1.amazonaws.com/fw/cm/image/44/1035/o_1b3pi4nsl11ve11bajngj3d15jma_180__o.PNG") },
				Buttons=new List<CardAction> { new CardAction(

					"openUrl",
					"Start this badge",null,"https://www.openbadgeacademy.com/badge/1035")}


			};
			message.Attachments.Add(card.ToAttachment());
			message.Text = "[Professional Chef](https://www.openbadgeacademy.com/badge/1035)";
			message.TextFormat = "markdown";
			//message.Locale = "en-us";

			//var message = "Test";
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
 