using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text uiText;
    public string inputText;

	// Use this for initialization
	void Start () {
        inputText = "";
        uiText.text = "Type to Start";
	}
	
	// Update is called once per frame
	void Update () {
        foreach(char c in Input.inputString) {
            inputText += c;
        }

        uiText.text = inputText;

        if(Input.GetKeyDown(KeyCode.Return)) {
            inputText = "";
        }
    }
}
