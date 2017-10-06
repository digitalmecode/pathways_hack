using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Web.Http.Description;
using System.Diagnostics;
using Microsoft.Bot.Sample.FormBot.Dialogs;

namespace Microsoft.Bot.Sample.FormBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        internal static IDialog<PathwaysProfile> MakeRootDialog()
        {
            return Chain.From(() => new LUISDialog(PathwaysProfile.BuildForm))
                .Do(OnProfileComplete);
        }


        private async static Task<IDialog<string>> AfterLuisContinuation(IBotContext context, IAwaitable<object> res)
        {
            var token = await res;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return Chain.Return($"Thank you for using the Pathway bot, {name}");
        }

        private static async Task OnProfileComplete(IBotContext context, IAwaitable<PathwaysProfile> profileDialog)
        {
            var profile = await profileDialog;
            var response = await profile.PostAsJsonToApi("ProfileCompleteApi");
            await context.PostAsync("Logic App returned: " + response);
        }

        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>
        /// <param name="activity"></param>
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null)
            {
                // one of these will have an interface and process it
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                        await Conversation.SendAsync(activity, MakeRootDialog);
                        break;

                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}