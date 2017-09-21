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
        Manchester, Plymouth, Birghton
    };
    public enum Interests { Environment, Engineering, Sports, PublicSpeaking, Arts, Politics, OutwardBound, Gaming, Coding, Travel};
    public enum BreadOptions { NineGrainWheat, NineGrainHoneyOat, Italian, ItalianHerbsAndCheese, Flatbread };
    public enum EducationLevels { School, College, University };

    [Serializable]
    public class PathwaysProfile
    {
        public string Name;
        public string Email;
        public Cities? City;
        public List<Interests> Interests;
        public EducationLevels? EducationLevel;
        public bool IsWorking;
        public string Work;
        public string Inspiration;

        public static IForm<PathwaysProfile> BuildForm()
        {
            return new FormBuilder<PathwaysProfile>()
                    .Message("Hello there! Welcome to your future")
                    .Build();
        }
    }
}