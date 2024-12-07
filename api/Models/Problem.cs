using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("status")]
        public required string Status { get; set; }

        [JsonPropertyName("result")]
        public required List<ResultItem> Result { get; set; }
    }

    public class ResultItem
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("contestId")]
        public int ContestId { get; set; }

        [JsonPropertyName("creationTimeSeconds")]
        public long CreationTimeSeconds { get; set; }

        [JsonPropertyName("relativeTimeSeconds")]
        public int RelativeTimeSeconds { get; set; }

        [JsonPropertyName("problem")]
        public required ProblemDetails Problem { get; set; }

        [JsonPropertyName("author")]
        public required AuthorDetails Author { get; set; }

        [JsonPropertyName("programmingLanguage")]
        public required string ProgrammingLanguage { get; set; }

        [JsonPropertyName("verdict")]
        public required string Verdict { get; set; }

        [JsonPropertyName("testset")]
        public required string Testset { get; set; }

        [JsonPropertyName("passedTestCount")]
        public int PassedTestCount { get; set; }

        [JsonPropertyName("timeConsumedMillis")]
        public int TimeConsumedMillis { get; set; }

        [JsonPropertyName("memoryConsumedBytes")]
        public int MemoryConsumedBytes { get; set; }
    }

    public class ProblemDetails
    {
        [JsonPropertyName("contestId")]
        public int ContestId { get; set; }

        [JsonPropertyName("index")]
        public required string Index { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("rating")]
        public int? Rating { get; set; }

        [JsonPropertyName("tags")]
        public required List<string> Tags { get; set; }
    }

    public class AuthorDetails
    {
        [JsonPropertyName("contestId")]
        public int ContestId { get; set; }

        [JsonPropertyName("members")]
        public required List<MemberDetails> Members { get; set; }

        [JsonPropertyName("participantType")]
        public required string ParticipantType { get; set; }

        [JsonPropertyName("ghost")]
        public bool Ghost { get; set; }

        [JsonPropertyName("startTimeSeconds")]
        public long StartTimeSeconds { get; set; }
    }

    public class MemberDetails
    {
        [JsonPropertyName("handle")]
        [Required]
        public string Handle { get; set; } = "";
    }
}