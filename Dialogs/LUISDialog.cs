using FormBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

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
			await context.PostAsync("Sorry, we could not find the suggestions for you :( I will take note of this and work hard to find something that would excite you :)");
			context.Wait(MessageReceived);
		}

		[LuisIntent("Greeting")]
		public async Task Greeting(IDialogContext context, LuisResult result)
		{
			var pathwaysForm = new FormDialog<PathwaysProfile>(new PathwaysProfile(), this.Profile, FormOptions.PromptInStart);
			context.Call<PathwaysProfile>(pathwaysForm, Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
		{
            //This starts each time an intent can not be found
		    context.Wait(MessageReceived);
        }

        [LuisIntent("Cooking")]
		public async Task Cooking(IDialogContext context, LuisResult result)
		{
			context.Call(new CookingDialog(), Callback);
		}

        [LuisIntent("Coding")]
		public async Task Coding(IDialogContext context, LuisResult result)
		{
			context.Call(new CodingDialog(), Callback);
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