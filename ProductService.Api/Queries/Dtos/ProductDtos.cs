namespace ProductService.Api.Queries.Dtos;

public record ChoiceDto(string Code, string Label);
public record CoverDto(string Code, string Name, decimal SumInsured);
public abstract record QuestionDtoBase(string QuestionCode, string Text);
public record ChoiceQuestionDto(string QuestionCode, string Text, IList<ChoiceDto> Choices) : QuestionDtoBase(QuestionCode, Text);
public record NumericQuestionDto(string QuestionCode, string Text) : QuestionDtoBase(QuestionCode, Text);
public class ProductDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<CoverDto> Covers { get; set; } = new();
    public List<QuestionDtoBase> Questions { get; set; } = new();
}
