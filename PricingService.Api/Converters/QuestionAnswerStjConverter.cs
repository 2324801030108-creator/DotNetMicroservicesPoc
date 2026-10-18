using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using PricingService.Api.Commands.Dto;

namespace PricingService.Api.Converters;

/// <summary>
/// System.Text.Json counterpart of <see cref="QuestionAnswerConverter" /> (Newtonsoft.Json).
/// It keeps the same JSON shape (questionCode / questionType / answer) so polymorphic
/// question answers survive serialization with the default ASP.NET Core formatters
/// and with Alba's System.Text.Json serializer used by the integration tests.
/// </summary>
public class QuestionAnswerStjConverter : JsonConverter<QuestionAnswer>
{
    public override QuestionAnswer Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var json = document.RootElement;

        var questionCode = json.GetProperty("questionCode").GetString();
        var questionType = ReadQuestionType(json.GetProperty("questionType"));

        return questionType switch
        {
            QuestionType.Number => new NumericQuestionAnswer
            {
                QuestionCode = questionCode,
                Answer = json.GetProperty("answer").GetDecimal()
            },
            QuestionType.Choice => new ChoiceQuestionAnswer
            {
                QuestionCode = questionCode,
                Answer = json.GetProperty("answer").GetString() ?? string.Empty
            },
            _ => new TextQuestionAnswer
            {
                QuestionCode = questionCode,
                Answer = json.GetProperty("answer").GetString() ?? string.Empty
            }
        };
    }

    public override void Write(Utf8JsonWriter writer, QuestionAnswer value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("questionCode", value.QuestionCode);
        writer.WriteString("questionType", value.QuestionType.ToString());

        switch (value.GetAnswer())
        {
            case decimal number:
                writer.WriteNumber("answer", number);
                break;
            case string text:
                writer.WriteString("answer", text);
                break;
            case null:
                writer.WriteNull("answer");
                break;
            default:
                JsonSerializer.Serialize(writer, value.GetAnswer(), options);
                break;
        }

        writer.WriteEndObject();
    }

    private static QuestionType ReadQuestionType(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Number => (QuestionType)element.GetInt32(),
            JsonValueKind.String => Enum.Parse<QuestionType>(element.GetString() ?? string.Empty),
            _ => throw new JsonException($"Unexpected questionType value: {element}")
        };
    }
}
