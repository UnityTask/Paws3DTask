using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCaculator : MonoBehaviour {
    public InputField Num1Field;
    public InputField Num2Field;
    public Dropdown OperatorDropdown;
    public Text ResultText;

    public void Calculate() {
        string operation = OperatorDropdown.options[OperatorDropdown.value].text;
        switch (operation) {
            case "+":
                ResultText.text = (float.Parse(Num1Field.text) + float.Parse(Num2Field.text)).ToString();
                break;
            case "-":
                ResultText.text = (float.Parse(Num1Field.text) - float.Parse(Num2Field.text)).ToString();
                break;
            case "*":
                ResultText.text = (float.Parse(Num1Field.text) * float.Parse(Num2Field.text)).ToString();
                break;
            case "/":
                ResultText.text = float.Parse(Num2Field.text) == 0 ? "undefined" :
                    (float.Parse(Num1Field.text) / float.Parse(Num2Field.text)).ToString();
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
