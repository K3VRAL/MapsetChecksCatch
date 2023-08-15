using System;
using System.Collections.Generic;
using System.Linq;
using MapsetChecksCatch.Checks.General;
using MapsetChecksCatch.Helper;
using MapsetParser.objects;
using MapsetParser.statics;
using MapsetVerifierFramework.objects;
using MapsetVerifierFramework.objects.attributes;
using MapsetVerifierFramework.objects.metadata;

namespace MapsetChecksCatch.Checks.Compose
{
    [Check]
    public class CheckHasImpossibleTinyDroplet : BeatmapCheck
    {
        public override CheckMetadata GetMetadata() => new BeatmapCheckMetadata
        {
            Category = "Compose",
            Message = "Contains an uncatchable tiny droplet.",
            Modes = new[] { Beatmap.Mode.Catch },
            Author = "K 3 V R A L",

            Documentation = new Dictionary<string, string>
            {
                {
                    "Purpose",
                    @"
                    The Tiny Droplet generated by the Juice Stream is uncatchable."
                },
                {
                    "Reasoning",
                    @"
                    Bruh, like, shit happens and that's not ok, lmao." // TODO
                }
            }
        };
        
        public override Dictionary<string, IssueTemplate> GetTemplates()
        {
            return new Dictionary<string, IssueTemplate>
            {
                { "ImpossibleTinyDroplet",
                    new IssueTemplate(Issue.Level.Problem,
//                            "{0} {1} is an impossible tiny droplet.",
                            "{0} {1} IMPOSSIBLE TODO.",
                            "timestamp - ", "object")
                        .WithCause(
                            "Distance between the two objects is too high, making the distance impossible for the catcher to reach")
                }
            };
        }

        public override IEnumerable<Issue> GetIssues(Beatmap beatmap)
        {
            var catchObjects = CheckBeatmapSetDistanceCalculation.GetBeatmapDistances(beatmap);

            foreach (var catchObject in catchObjects)
            {
                yield return new Issue(
                    GetTemplate("ImpossibleTinyDroplet"),
                    beatmap,
                    catchObject.ActualTime,
                    catchObject.GetNoteTypeName()
                );
            }
        }
    }
}
