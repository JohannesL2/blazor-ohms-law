using Microsoft.AspNetCore.Components;
using OhmsLaw.Resources;
using OhmsLaw.Services;

namespace OhmsLaw.Components.Pages;


public partial class OhmsLag : ComponentBase, IDisposable
{
    [Inject] private LanguageService LangService { get; set; } = default!;

    protected override void OnInitialized()
    {
        LangService.OnLanguageChanged += StateHasChanged;
    }

    public void Dispose()
    {
        LangService.OnLanguageChanged -= StateHasChanged;
    }
    private bool IsSwedish => LangService.CurrentLanguage == LanguageService.Language.Swedish;
private bool IsEnglish => LangService.CurrentLanguage == LanguageService.Language.English;

private void SetLanguage(LanguageService.Language lang)
{
    LangService.CurrentLanguage = lang;
}
    public enum Storhet
    {
        Spanning,
        Strom,
        Resistans,
        Effekt
    }

    private Storhet _valdStorhet = Storhet.Spanning;
    
    // Inmatningsfält (separata från resultat)
    private double? _spanning;
    private double? _strom;
    private double? _resistans;
    private double? _effekt;

    private string _aktivFormelId = "U-IR";
    private string _aktivFormel = "U = I × R";
    private double? _beräknatVärde = null;

    private Storhet ValdStorhet
    {
        get => _valdStorhet;
        set
        {
            if (_valdStorhet != value)
            {
                _valdStorhet = value;
                RensaInmatningar();
                UppdateraStandardFormelFörStorhet(value);
                Berakna();
            }
        }
    }

    private double? Spanning
    {
        get => _spanning;
        set { _spanning = value; Berakna(); }
    }

    private double? Strom
    {
        get => _strom;
        set { _strom = value; Berakna(); }
    }

    private double? Resistans
    {
        get => _resistans;
        set { _resistans = value; Berakna(); }
    }

    private double? Effekt
    {
        get => _effekt;
        set { _effekt = value; Berakna(); }
    }

    private string AktivFormel => _aktivFormel;
    
    private string Resultat => _beräknatVärde.HasValue 
        ? $"{_beräknatVärde.Value:0.####} {Enhet(ValdStorhet)}" 
        : Text.InitialResult(IsSwedish);

    private string Forklaring => _beräknatVärde.HasValue 
        ? (IsSwedish ? $"Beräknat med {AktivFormel}." : $"Calculated using {AktivFormel}.") 
        : (IsSwedish ? $"Aktiv formel: {AktivFormel}" : $"Active formula: {AktivFormel}");

    private string ResultatRubrik => IsSwedish 
        ? $"Beräknad {Namn(ValdStorhet)}" 
        : $"Calculated {Namn(ValdStorhet)}";

    private string HjulKlass(string formelId) =>
        formelId == _aktivFormelId ? "wheel-formula active" : "wheel-formula";

    public void ValjFormel(string formelId)
    {
        _aktivFormelId = formelId;
        
        switch (formelId)
        {
            case "U-IR": _valdStorhet = Storhet.Spanning; _aktivFormel = "U = I × R"; break;
            case "U-PI": _valdStorhet = Storhet.Spanning; _aktivFormel = "U = P / I"; break;
            case "U-PR": _valdStorhet = Storhet.Spanning; _aktivFormel = "U = √(P × R)"; break;

            case "R-UI": _valdStorhet = Storhet.Resistans; _aktivFormel = "R = U / I"; break;
            case "R-PI": _valdStorhet = Storhet.Resistans; _aktivFormel = "R = P / I²"; break;
            case "R-UP": _valdStorhet = Storhet.Resistans; _aktivFormel = "R = U² / P"; break;

            case "P-UI": _valdStorhet = Storhet.Effekt; _aktivFormel = "P = U × I"; break;
            case "P-RI": _valdStorhet = Storhet.Effekt; _aktivFormel = "P = R × I²"; break;
            case "P-UR": _valdStorhet = Storhet.Effekt; _aktivFormel = "P = U² / R"; break;

            case "I-UR": _valdStorhet = Storhet.Strom; _aktivFormel = "I = U / R"; break;
            case "I-PU": _valdStorhet = Storhet.Strom; _aktivFormel = "I = P / U"; break;
            case "I-PR": _valdStorhet = Storhet.Strom; _aktivFormel = "I = √(P / R)"; break;
        }

        RensaInmatningar();
        Berakna();
    }

    private void RensaInmatningar()
    {
        _spanning = null;
        _strom = null;
        _resistans = null;
        _effekt = null;
        _beräknatVärde = null;
    }

    public bool ÄrFältInaktiverat(Storhet fält)
    {
        if (fält == ValdStorhet) return true;

        return _aktivFormelId switch
        {
            "U-IR" or "I-UR" or "R-UI" or "P-UI" => fält == Storhet.Effekt,
            "U-PI" or "I-PU" or "R-PI" or "P-RI" => fält == Storhet.Resistans,
            "U-PR" or "I-PR" or "R-UP" or "P-UR" => fält == Storhet.Strom,
            _ => false
        };
    }

    private void UppdateraStandardFormelFörStorhet(Storhet storhet)
    {
        _aktivFormelId = storhet switch
        {
            Storhet.Spanning => "U-IR",
            Storhet.Strom => "I-UR",
            Storhet.Resistans => "R-UI",
            Storhet.Effekt => "P-UI",
            _ => "U-IR"
        };
    }

    private void Berakna()
    {
        if (HarOgiltigtVarde())
        {
            VisaFel(IsSwedish ? "Använd 0 eller positiva tal." : "Use 0 or positive numbers.");
            return;
        }

        double? svar = _aktivFormelId switch
        {
            "U-IR" => Strom.HasValue && Resistans.HasValue ? Strom.Value * Resistans.Value : null,
            "U-PI" => Effekt.HasValue && Strom is > 0 ? Effekt.Value / Strom.Value : null,
            "U-PR" => Effekt.HasValue && Resistans.HasValue ? Math.Sqrt(Effekt.Value * Resistans.Value) : null,

            "I-UR" => Spanning.HasValue && Resistans is > 0 ? Spanning.Value / Resistans.Value : null,
            "I-PU" => Effekt.HasValue && Spanning is > 0 ? Effekt.Value / Spanning.Value : null,
            "I-PR" => Effekt.HasValue && Resistans is > 0 ? Math.Sqrt(Effekt.Value / Resistans.Value) : null,

            "R-UI" => Spanning.HasValue && Strom is > 0 ? Spanning.Value / Strom.Value : null,
            "R-PI" => Effekt.HasValue && Strom is > 0 ? Effekt.Value / Math.Pow(Strom.Value, 2) : null,
            "R-UP" => Spanning.HasValue && Effekt is > 0 ? Math.Pow(Spanning.Value, 2) / Effekt.Value : null,

            "P-UI" => Spanning.HasValue && Strom.HasValue ? Spanning.Value * Strom.Value : null,
            "P-RI" => Resistans.HasValue && Strom.HasValue ? Resistans.Value * Math.Pow(Strom.Value, 2) : null,
            "P-UR" => Spanning.HasValue && Resistans is > 0 ? Math.Pow(Spanning.Value, 2) / Resistans.Value : null,
            _ => null
        };

        if (svar is null)
        {
            _beräknatVärde = null;
            return;
        }

        if (!double.IsFinite(svar.Value))
        {
            VisaFel(IsSwedish ? "Resultatet blev för stort." : "The result was too large.");
            return;
        }

        _beräknatVärde = svar.Value;
    }

    private bool HarOgiltigtVarde() =>
        new[] { Spanning, Strom, Resistans, Effekt }.Any(varde => varde is < 0);

    private void VisaFel(string meddelande)
    {
        _beräknatVärde = null;
    }

    private string Namn(Storhet storhet) => storhet switch
    {
        Storhet.Spanning => IsSwedish ? "spänning" : "voltage",
        Storhet.Strom => IsSwedish ? "ström" : "current",
        Storhet.Resistans => IsSwedish ? "resistans" : "resistance",
        Storhet.Effekt => IsSwedish ? "effekt" : "power",
        _ => IsSwedish ? "värde" : "value"
    };

    private static string Enhet(Storhet storhet) => storhet switch
    {
        Storhet.Spanning => "V",
        Storhet.Strom => "A",
        Storhet.Resistans => "Ω",
        Storhet.Effekt => "W",
        _ => ""
    };
}