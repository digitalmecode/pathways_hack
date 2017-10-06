using Microsoft.Bot.Builder.FormFlow;
using System;

#pragma warning disable 649

// The SandwichOrder is the simple form you want to fill out.  It must be serializable so the bot can be stateless.
// The order of fields defines the default order in which questions will be asked.
// Enumerations shows the legal options for each field in the SandwichOrder and the order is the order values will be presented 
// in a conversation.
namespace Microsoft.Bot.Sample.FormBot
{
    public enum eCities
    {
        Manchester =1 , Plymouth =2, Brighton=3, Other=0
    };

    public enum eCookingItems { Cake, Pasta, Steak}
    public enum eInterests { Environment, Engineering, Cooking, Sports, PublicSpeaking, Arts, Politics, OutwardBound, Gaming, Coding, Travel}
    public enum eEducationLevels { School, College, University, Masters, PhD }

    [Serializable]
    public class PathwaysProfile
    {
        [Prompt("What would you like me call you?")]
        public string Name;

        [Prompt("Can we get your email address for our records? Promise I won't send you spam or share your email with anyone :)")]
        public string Email;

        [Prompt("Where do you live?{||}")]
        public eCities? City;

        [Prompt("What is the higest level of education you have completed? {||}")]
        public eEducationLevels? EducationLevel;

        [Prompt("Are you working at the moment?")]
        public bool IsWorking;

        [Prompt("What role do you work at?")]
        public string Work;

        [Prompt("That is great! Are you ready to explore new oppurtunities?")]
        public bool IsReady;

        public static IForm<PathwaysProfile> BuildForm()
        {
            return new FormBuilder<PathwaysProfile>()
                    .Message("Hello there! Welcome to your future")
                    .Field(nameof(Name))
                    .Field(nameof(Email))
                    .Field(nameof(City))
                    .Field(nameof(EducationLevel))
                    .Field(nameof(IsWorking))
                    .Field(nameof(Work), c => c.IsWorking)
                    .Field(nameof(IsReady),
                    validate: async (state, values) =>
                    {
                        var result = new ValidateResult { IsValid = false, Value = values };
                        if (values != null && (bool) values)
                        {
                            result.IsValid = true;
                        }
                        else
                        {
                            result.Feedback = "Take your time and I will be here when you are ready :)";
                        }
                        return result;
                    })
                    .AddRemainingFields()
                    //.Confirm("Are you sure? Here are your current selections: {*}")
                    .Message("Would you please tell me a little about your interests and inspirations? I will use this to customise your experience.")
                    .Build();
        }
    }
}