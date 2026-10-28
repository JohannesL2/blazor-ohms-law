namespace OhmsLaw.Services;

public class LanguageService
{
    public enum Language { English, Swedish }
    
    private Language _currentLanguage = Language.English; // Engelska som standard

    public Language CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            if (_currentLanguage != value)
            {
                _currentLanguage = value;
                OnLanguageChanged?.Invoke();
            }
        }
    }

    public event Action? OnLanguageChanged;
}