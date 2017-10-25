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
        Manchester =1 , Plymouth =2, Brighton=3, Other = 4
    };

    public enum eCookingItems { Cake, Pasta, Steak}
    public enum eInterests { Environment, Engineering, Cooking, Sports, PublicSpeaking, Arts, Politics, OutwardBound, Gaming, Coding, Travel}
    public enum eEducationLevels { School, College, University}

    [Serializable]
    public class PathwaysProfile
    {
        [Prompt("Now you know my name, what would you like me to call you?")]
        public string Name;

        [Prompt("What is the best way to get in touch with you, email or phone? Promise I won't send you spam or share your email with anyone :)")]
        public string ContactType;

        [Prompt("Can I get your phone number for my records?")]
        public string Phone;

        [Prompt("Can I get your email address for my records?")]
        public string Email;

        [Prompt("Where are you looking for oppurtunities?{||}")]
        public eCities? City;

        [Prompt("What is the higest level of education you have completed? {||}")]
        public eEducationLevels? EducationLevel;

        [Prompt("Are you working at the moment?")]
        public bool IsWorking;

        [Prompt("What is your current role?")]
        public string Work;

        [Prompt("That is great! Are you ready to explore new oppurtunities?")]
        public bool IsReady;

        public static IForm<PathwaysProfile> BuildForm()
        {
            return new FormBuilder<PathwaysProfile>()
                    .Message("Hello there! My name is Sam. I can help you explore new oppurtunities. I am new to this, please excuse any hitches and glitches :)")
                    .Field(nameof(Name))
                    .Field(nameof(ContactType))
                    .Field(nameof(Email), c => c.ContactType == "email")
                    .Field(nameof(Phone), c => c.ContactType == "phone")
                    .Field(nameof(City))
                    .Message("Manchester! Great music scene!", c => c.City == eCities.Manchester)
                    .Message("Plymouth! A great spot for water sports!", c => c.City == eCities.Plymouth)
                    .Message("Brighton! The foody capital of the Britain.", c => c.City == eCities.Brighton)
                    .Message("To help me find the best oppurtunities for you, I will need to ask a few more questions.")
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
                    .Message("Let's get to know each other a little better. What are you interested in or inspired by? This will help me find the best oppurtunities for you.")
                    .Build();
        }
    }
}