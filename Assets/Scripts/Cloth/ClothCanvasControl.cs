using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClothCanvasControl : MonoBehaviour {

    public VariableControl varcontrol;

    public Slider springConstantSlider;
    public Slider dampingFactorSlider;
    public Slider restLengthSlider;
    public Slider windSlider;
    public Slider gravitySlider;
    public Slider breakFactorSlider;


    // Use this for initialization
    void Start () {
        springConstantSlider.value = varcontrol.springConst;
        dampingFactorSlider.value = varcontrol.dampingFactor;
        restLengthSlider.value = varcontrol.restLength;

        windSlider.value = varcontrol.windStrength;

        gravitySlider.value = varcontrol.gravity;
        breakFactorSlider.value = varcontrol.breakFactor;
    }
	
	// Update is called once per frame
	void Update () {
        varcontrol.springConst = springConstantSlider.value;
        varcontrol.dampingFactor = dampingFactorSlider.value;
        varcontrol.restLength = restLengthSlider.value;

        if (windSlider.value > 0)
        {
            varcontrol.wind = true;
            varcontrol.windStrength = windSlider.value;
        }
        else
            varcontrol.wind = false;

        varcontrol.gravity = gravitySlider.value;
        varcontrol.breakFactor = breakFactorSlider.value;
    }
}
