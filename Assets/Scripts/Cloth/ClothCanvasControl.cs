using UnityEngine;
using UnityEngine.UI;

public class ClothCanvasControl : MonoBehaviour {

    public VariableControl Varcontrol;

    public Slider SpringConstantSlider;
    public Slider DampingFactorSlider;
    public Slider RestLengthSlider;
    public Slider WindSlider;
    public Slider GravitySlider;
    public Slider BreakFactorSlider;


    // Use this for initialization
    private void Start () {
        SpringConstantSlider.value = Varcontrol.SpringConst;
        DampingFactorSlider.value = Varcontrol.DampingFactor;
        RestLengthSlider.value = Varcontrol.RestLength;

        WindSlider.value = Varcontrol.WindStrength;

        GravitySlider.value = Varcontrol.Gravity;
        BreakFactorSlider.value = Varcontrol.BreakFactor;
    }
	
    // Update is called once per frame
    private void Update () {
        Varcontrol.SpringConst = SpringConstantSlider.value;
        Varcontrol.DampingFactor = DampingFactorSlider.value;
        Varcontrol.RestLength = RestLengthSlider.value;

        if (WindSlider.value > 0)
        {
            Varcontrol.Wind = true;
            Varcontrol.WindStrength = WindSlider.value;
        }
        else
            Varcontrol.Wind = false;

        Varcontrol.Gravity = GravitySlider.value;
        Varcontrol.BreakFactor = BreakFactorSlider.value;
    }
}

