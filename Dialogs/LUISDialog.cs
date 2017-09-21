using FormBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Microsoft.Bot.Sample.FormBot.Dialogs
{
	[LuisModel("dbd843d1-5885-4356-95a1-2126684fa8e7", "11854a84ab2c4d75a17b26665238613c")]
	[Serializable]
	public class LUISDialog : LuisDialog<PathwaysProfile>
	{
		private readonly BuildFormDelegate<PathwaysProfile> Profile;

		public LUISDialog(BuildFormDelegate<PathwaysProfile> profile)
		{
			this.Profile = profile;
		}


		[LuisIntent("")]
		public async Task None(IDialogContext context, LuisResult result)
		{
			await context.PostAsync("I'm sorry I don't know what you mean.");
			context.Wait(MessageReceived);
		}

		[LuisIntent("Greeting")]
		public async Task Greeting(IDialogContext context, LuisResult result)
		{
			var enrollmentForm = new FormDialog<PathwaysProfile>(new PathwaysProfile(), this.Profile, FormOptions.PromptInStart);
			context.Call<PathwaysProfile>(enrollmentForm, Callback);
			//context.Call(new CookingDialog(), Callback);

			//context.Call(new GreetingDialog(), Callback);
		}

		private async Task Callback(IDialogContext context, IAwaitable<object> result)
		{
			context.Wait(MessageReceived);
		}

		[LuisIntent("Cooking")]
		public async Task Cooking(IDialogContext context, LuisResult result)
		{
			//var enrollmentForm = new FormDialog<RoomReservation>(new RoomReservation(), this.ReserveRoom, FormOptions.PromptInStart);
			//context.Call<RoomReservation>(enrollmentForm, Callback);
			context.Call(new CookingDialog(), Callback);

			
		}

		//[LuisIntent("QueryAmenities")]
		//public async Task QueryAmenities(IDialogContext context, LuisResult result)
		//{
		//	foreach (var entity in result.Entities.Where(Entity => Entity.Type == "Amenity"))
		//	{
		//		var value = entity.Entity.ToLower();
		//		if (value == "pool" || value == "gym" || value == "wifi" || value == "towels")
		//		{
		//			await context.PostAsync("Yes we have that!");
		//			context.Wait(MessageReceived);
		//			return;
		//		}
		//		else
		//		{
		//			await context.PostAsync("I'm sorry we don't have that.");
		//			context.Wait(MessageReceived);
		//			return;
		//		}
		//	}
		//	await context.PostAsync("I'm sorry we don't have that.");
		//	context.Wait(MessageReceived);
		//	return;
		//}

	}
}