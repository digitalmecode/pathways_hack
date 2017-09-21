using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

#pragma warning disable 649

// The SandwichOrder is the simple form you want to fill out.  It must be serializable so the bot can be stateless.
// The order of fields defines the default order in which questions will be asked.
// Enumerations shows the legal options for each field in the SandwichOrder and the order is the order values will be presented 
// in a conversation.
namespace Microsoft.Bot.Sample.FormBot
{
    public enum Cities
    {
        Manchester =1 , Plymouth =2, Brighton=3, Other=0
    };
    public enum Interests { Environment, Engineering, Sports, PublicSpeaking, Arts, Politics, OutwardBound, Gaming, Coding, Travel};
    public enum BreadOptions { NineGrainWheat, NineGrainHoneyOat, Italian, ItalianHerbsAndCheese, Flatbread };
    public enum EducationLevels { School, College, University };

    [Serializable]
    public class PathwaysProfile
    {
        [Prompt("What would you like me call you?")]
        public string Name;

        [Prompt("Can we get your email address for our records? Promise we won't send you spam or share your email with anyone :)")]
        public string Email;

        [Prompt("For the moment we have opputunities in these cities. Would you let us know which city you live in? Or just choose other.  {||}")]
        public Cities? City;

        [Prompt("Are you interested in any of these areas? We will use this to customise your experience. Just type in the numbers separated with commas. {||}")]
        public List<Interests> Interests;

        [Prompt("What is the higest level of education you have completed? {||}")]
        public EducationLevels? EducationLevel;

        [Prompt("Are you working at the moment?")]
        public bool IsWorking;

        [Prompt("What role do you work as?")]
        public string Work;

        [Prompt("What are most inpired by?")]
        public string Inspiration;

        public static IForm<PathwaysProfile> BuildForm()
        {
            return new FormBuilder<PathwaysProfile>()
                    .Message("Hello there! Welcome to your future")
                    .Field(nameof(Name))
                    .Field(nameof(Email))
                    .Field(nameof(City))
                    .Field(nameof(Interests))
                    .Field(nameof(EducationLevel))
                    .Field(nameof(IsWorking))
                    .Field(nameof(Work), (c) => c.IsWorking == true)
                    .AddRemainingFields()
                    .Confirm("Are you sure? Here are your current selections: {*}")
                    .Build();
        }
    }
}