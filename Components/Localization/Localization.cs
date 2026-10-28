namespace OhmsLaw.Resources;

public static class Text
{
    public static string Title(bool isSwedish) => isSwedish ? "Ohm-labbet" : "Ohm's Law Lab";
    public static string Subtitle(bool isSwedish) => isSwedish ? "Räkna • förstå • öva" : "Calculate • Understand • Practice";
    public static string IntroText(bool isSwedish) => isSwedish 
        ? "Välj vad som är okänt i menyn, eller klicka direkt på en formel i hjulet." 
        : "Select what you want to calculate in the menu, or click directly on a formula in the wheel.";

    public static string SelectTarget(bool isSwedish) => isSwedish ? "Jag vill räkna ut" : "I want to calculate";
    
    public static string OptVoltage(bool isSwedish) => isSwedish ? "Spänning (U)" : "Voltage (U)";
    public static string OptCurrent(bool isSwedish) => isSwedish ? "Ström (I)" : "Current (I)";
    public static string OptResistance(bool isSwedish) => isSwedish ? "Resistans (R)" : "Resistance (R)";
    public static string OptPower(bool isSwedish) => isSwedish ? "Effekt (P)" : "Power (P)";

    public static string VoltageLabel(bool isSwedish) => isSwedish ? "Spänning (U)" : "Voltage (U)";
    public static string UnitVolts(bool isSwedish) => isSwedish ? "volt (V)" : "volts (V)";

    public static string CurrentLabel(bool isSwedish) => isSwedish ? "Ström (I)" : "Current (I)";
    public static string UnitAmps(bool isSwedish) => isSwedish ? "ampere (A)" : "amperes (A)";

    public static string ResistanceLabel(bool isSwedish) => isSwedish ? "Resistans (R)" : "Resistance (R)";
    public static string UnitOhms(bool isSwedish) => isSwedish ? "ohm (Ω)" : "ohms (Ω)";

    public static string PowerLabel(bool isSwedish) => isSwedish ? "Effekt (P)" : "Power (P)";
    public static string UnitWatts(bool isSwedish) => isSwedish ? "watt (W)" : "watts (W)";
    
    public static string InitialResult(bool isSwedish) => isSwedish ? "Fyll i två värden för att börja." : "Enter two values to begin.";
    public static string WheelActiveText(bool isSwedish) => isSwedish ? "Den blå formeln är den som används just nu." : "The blue formula is currently active.";
    public static string WheelHint(bool isSwedish) => isSwedish ? "Tips: Du kan även klicka direkt på valfri formel i hjulet." : "Tip: You can also click directly on any formula in the wheel.";
}