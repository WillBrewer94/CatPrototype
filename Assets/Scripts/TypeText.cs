using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeText : MonoBehaviour {
    public Text textBox;
    string[] inputText = { "this is steve" };
    string[] text2 = { "steve hates your goddamn guts" };
    string[] text3 = { "try to make him not hate you" };
    string[] text4 = { "type actions with the keyboard" };
    string[] text5 = { "examples: pet <bodypart>, call name, give treat" };
    string[] text6 = { "try to get the lowest time" };

    public float textSpeed = 0.05f;
    int pressed = 0;
    string currText;
    int currentlyDisplayingText = 0;
    public AudioSource keyboardSound;

    void Awake() {
        StartCoroutine(AnimateText(inputText));
        currText = "initialResponse";
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            if(pressed < 1) {
                StartCoroutine(AnimateText(text2));
            } else if(pressed < 2) {
                StartCoroutine(AnimateText(text3));
            } else if(pressed < 3) {
                StartCoroutine(AnimateText(text4));
            } else if(pressed < 4) {
                StartCoroutine(AnimateText(text5));
            } else if(pressed < 5) {
                StartCoroutine(AnimateText(text6));
            } else if(pressed < 6) {
                SceneManager.LoadScene("CatScene");
            }

            pressed++;
        }
    }

    //Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every 0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
    IEnumerator AnimateText(string[] text) {
        for(int i = 0; i < (text[currentlyDisplayingText].Length + 1); i++) {
            textBox.text = text[currentlyDisplayingText].Substring(0, i);
            keyboardSound.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
