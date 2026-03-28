using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResistorUI : MonoBehaviour
{
    [Header("Resistor")]
    public Resistor resistor;

    [Header("Band Dropdowns")]
    public TMP_Dropdown band1Dropdown;
    public TMP_Dropdown band2Dropdown;
    public TMP_Dropdown band3Dropdown;
    public TMP_Dropdown band4Dropdown;

    [Header("Resistance Display")]
    public TMP_Text resistanceText;

    // The colors available for each band
    private static readonly string[] colorNames = new string[]
    {
        "Black", "Brown", "Red", "Orange", "Yellow",
        "Green", "Blue", "Violet", "Grey", "White"
    };

    // Band 4 (tolerance) has different options
    private static readonly string[] toleranceColorNames = new string[]
    {
        "Brown", "Red", "Green", "Blue", "Violet",
        "Grey", "Gold", "Silver"
    };

    private static readonly ResistorColor[] toleranceColors = new ResistorColor[]
    {
        ResistorColor.Brown, ResistorColor.Red,   ResistorColor.Green,
        ResistorColor.Blue,  ResistorColor.Violet, ResistorColor.Grey,
        ResistorColor.Gold,  ResistorColor.Silver
    };

    void Start()
    {
        PopulateDropdown(band1Dropdown, colorNames);
        PopulateDropdown(band2Dropdown, colorNames);
        PopulateDropdown(band3Dropdown, colorNames);
        PopulateDropdown(band4Dropdown, toleranceColorNames);

        // Set dropdowns to match the resistor's current values
        band1Dropdown.value = (int)resistor.band1;
        band2Dropdown.value = (int)resistor.band2;
        band3Dropdown.value = (int)resistor.band3;
        band4Dropdown.value = System.Array.IndexOf(toleranceColors, resistor.band4);

        // Listen for changes
        band1Dropdown.onValueChanged.AddListener(OnBand1Changed);
        band2Dropdown.onValueChanged.AddListener(OnBand2Changed);
        band3Dropdown.onValueChanged.AddListener(OnBand3Changed);
        band4Dropdown.onValueChanged.AddListener(OnBand4Changed);

        UpdateResistanceText();
    }

    private void PopulateDropdown(TMP_Dropdown dropdown, string[] options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(new System.Collections.Generic.List<string>(options));
    }

    private void OnBand1Changed(int index)
    {
        resistor.band1 = (ResistorColor)index;
        resistor.UpdateBandColors();
        resistor.CalculateResistance();
        UpdateResistanceText();
    }

    private void OnBand2Changed(int index)
    {
        resistor.band2 = (ResistorColor)index;
        resistor.UpdateBandColors();
        resistor.CalculateResistance();
        UpdateResistanceText();
    }

    private void OnBand3Changed(int index)
    {
        resistor.band3 = (ResistorColor)index;
        resistor.UpdateBandColors();
        resistor.CalculateResistance();
        UpdateResistanceText();
    }

    private void OnBand4Changed(int index)
    {
        resistor.band4 = toleranceColors[index];
        resistor.UpdateBandColors();
        resistor.CalculateResistance();
        UpdateResistanceText();
    }

    private void UpdateResistanceText()
    {
        if (resistanceText != null)
            resistanceText.text = $"Resistance: {resistor.FormatResistance(resistor.resistance)} ±{resistor.toleranceLabel}";
    }
}