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
    public class NotInterestedDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            var notInterestedGreeting = context.MakeMessage();
            notInterestedGreeting.Text = "That is ok :/ I will be here whenever you need me";
            await context.PostAsync(notInterestedGreeting);

            context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            await Respond(context);
            context.Done(message);
        }
    }
}
