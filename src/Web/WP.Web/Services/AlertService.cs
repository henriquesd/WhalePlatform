namespace WP.Web.Services;

public class AlertService
{
    public event Action<AlertMessage>? OnAlertAdded;

    public void ShowSuccess(string message)
    {
        AddMessage("success", message);
    }

    public void ShowError(string message, List<string>? details = null)
    {
        AddMessage("danger", message, details);
    }

    public void ShowWarning(string message)
    {
        AddMessage("warning", message);
    }

    public void ShowInfo(string message)
    {
        AddMessage("info", message);
    }

    private void AddMessage(string type, string text, List<string>? details = null)
    {
        var message = new AlertMessage
        {
            Id = Guid.NewGuid(),
            Type = type,
            Text = text,
            Details = details
        };

        OnAlertAdded?.Invoke(message);
    }
}

public class AlertMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public List<string>? Details { get; set; }
}
