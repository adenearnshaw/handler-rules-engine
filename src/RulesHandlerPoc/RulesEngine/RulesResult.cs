namespace RulesHandlerPoc.RulesEngine;

public class RulesResult<T> where T : new()
{
    public T? State { get; set; } = default;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
