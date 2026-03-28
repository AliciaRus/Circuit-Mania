using UnityEngine;

public class Resistor : MonoBehaviour
{
    [Header("Band Colors")]
    public ResistorColor band1 = ResistorColor.Brown;
    public ResistorColor band2 = ResistorColor.Black;
    public ResistorColor band3 = ResistorColor.Red;   // multiplier
    public ResistorColor band4 = ResistorColor.Gold;  // tolerance (optional)

    [Header("Band Renderers")]
    public SpriteRenderer band1Renderer;
    public SpriteRenderer band2Renderer;
    public SpriteRenderer band3Renderer;
    public SpriteRenderer band4Renderer;   // optional tolerance band
    public LightBulb connectedBulb;


    [Header("Calculated")]
public float resistance;
public string toleranceLabel;

    // Maps each ResistorColor to its Unity Color
    private static readonly Color[] bandColors = new Color[]
    {
        Color.black,                            // 0 - Black
        new Color(0.4f, 0.2f, 0f),             // 1 - Brown
        Color.red,                              // 2 - Red
        new Color(1f, 0.5f, 0f),               // 3 - Orange
        Color.yellow,                           // 4 - Yellow
        new Color(0f, 0.5f, 0f),               // 5 - Green
        Color.blue,                             // 6 - Blue
        new Color(0.56f, 0f, 1f),              // 7 - Violet
        Color.grey,                             // 8 - Grey
        Color.white,                            // 9 - White
        new Color(1f, 0.84f, 0f),              // 10 - Gold
        new Color(0.75f, 0.75f, 0.75f),        // 11 - Silver
    };

    void OnValidate()
    {
        // Called in Editor whenever you change a value in the Inspector
        UpdateBandColors();
        CalculateResistance();
    }

    void Start()
    {
        UpdateBandColors();
        CalculateResistance();
    }

    public void UpdateBandColors()
    {
        SetBandColor(band1Renderer, band1);
        SetBandColor(band2Renderer, band2);
        SetBandColor(band3Renderer, band3);
        SetBandColor(band4Renderer, band4);
    }

    private void SetBandColor(SpriteRenderer renderer, ResistorColor color)
    {
        if (renderer == null) return;
        int index = (int)color;
        if (index >= 0 && index < bandColors.Length)
            renderer.color = bandColors[index];
    }

    public void CalculateResistance()
    {
        int digit1     = (int)band1;
        int digit2     = (int)band2;
        int multiplier = (int)band3;

        // Two significant digits + multiplier
        resistance = (digit1 * 10 + digit2) * Mathf.Pow(10, multiplier);
        toleranceLabel = GetTolerance(band4);

        Debug.Log($"Resistance: {FormatResistance(resistance)} ±{toleranceLabel}");
        if (connectedBulb != null)
            connectedBulb.SetResistance(resistance);
    }

    private string GetTolerance(ResistorColor color)
    {
        return color switch
        {
            ResistorColor.Brown  => "1%",
            ResistorColor.Red    => "2%",
            ResistorColor.Green  => "0.5%",
            ResistorColor.Blue   => "0.25%",
            ResistorColor.Violet => "0.1%",
            ResistorColor.Grey   => "0.05%",
            ResistorColor.Gold   => "5%",
            ResistorColor.Silver => "10%",
            _                    => "20%"
        };
    }

    public string FormatResistance(float ohms)
    {
        if (ohms >= 1_000_000) return $"{ohms / 1_000_000f:0.##}MΩ";
        if (ohms >= 1_000)     return $"{ohms / 1_000f:0.##}kΩ";
        return $"{ohms:0.##}Ω";
    }
    
}