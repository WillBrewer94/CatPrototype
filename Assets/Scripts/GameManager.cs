using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //GUI Variables
    public Text uiText;
    public Text timer;
    public Text loveLabel;
    public Text statusLabel;
    public GameObject likeDislike;
    public Sprite like;
    public Sprite dislike;
    
    //GameManager Variables
    public float time;
    public string inputText;
    public int love = -1;
    public bool isAttention = false;
    public bool isTreat = false;
    string favPet = "";
    int petCount = 0;

    //Audio
    public AudioClip keySound;
    public AudioClip hiss;
    public AudioClip angry;
    public AudioClip purr;
    public AudioClip irritated;
    public AudioClip meow;
    public AudioSource keyboard;
    public AudioSource catAudio;

    public GameObject cat;
    public Animator catAnim;


	// Use this for initialization
	void Start () {
        inputText = "";
        uiText.text = "Type to Start";
        catAnim = cat.GetComponent<Animator>();
        love = -1;
        likeDislike.SetActive(false);

        //Generate random body parts
        string[] bodyParts = { "belly", "head", "back" };
        favPet = bodyParts[Random.Range(0, bodyParts.Length)];
        Debug.Log(favPet);

	}
	
	// Update is called once per frame
	void Update () {
        //Update Timer
        time += Time.fixedDeltaTime;
        timer.text = time.ToString("F2");

        //Update Love
        loveLabel.text = "Love: " + love + "/15";
        catAnim.SetInteger("Love", love);
        catAnim.SetBool("isAttention", isAttention);

        //Update Status
        UpdateStatus();

        //Take Keyboard Input
        foreach(char c in Input.inputString) {
            if(c == "\b"[0]) {
                if(inputText.Length != 0) {
                    inputText = inputText.Substring(0, inputText.Length - 1);
                }
            } else {
                inputText += c;
            }
            
            keyboard.Play();
        }

        uiText.text = inputText;

        if(Input.GetKeyDown(KeyCode.Return)) {
            ParseText(inputText.ToLower());
            inputText = "";
        }

        //Win Condition
        if(love >= 15) {

        }
    }

    void UpdateStatus() {
        string status = "";

        if(love < 0) {
            status = "literally \n ignoring you";
        } else if(love < 1) {
            status = "contemplating ways \n to murder you";
        } else if(love < 2) {
            status = "totally pissed";
        } else if(love < 4) {
            status = "totally pissed but \n less than before";
        } else if(love < 6) {
            status = "no longer \n contemplating \n murder";
        } else if(love < 8) {
            status = "ambivalent";
        } else if(love < 10) {
            status = "kinda \n likes you";
        } else if(love < 12) {
            status = "almost bros";
        } else if(love < 14) {
            status = "less than three \n (<3)";
        } else if(love < 15) {
            status = "brought you a \n dead bird \n (out of love)";
        } 

        statusLabel.text = status;
    }

    void ParseText(string text) {
        string[] split = text.Split(' ');

        for(int i = 0; i < split.Length; i++) {
            
            //This is a mess please don't look at it
            if(!isAttention) {
                if(split[i].Contains("call")) {
                    isAttention = true;
                    StartCoroutine(Reaction(true));
                    love++;
                    catAudio.clip = irritated;
                    catAudio.Play();
                } else {
                    StartCoroutine(Reaction(false));
                }
            } else {
                if(!isTreat) {
                    if(split[i].Contains("treat")) {
                        isTreat = true;
                        love += 3;
                        StartCoroutine(Reaction(true));
                    } else {
                        StartCoroutine(Reaction(false));
                    }
                } else {
                    if(split[i].Contains("pet")) {
                        if(split.Length > 1 && split[i + 1].Contains(favPet)) {
                            love += 5;
                            StartCoroutine(Reaction(true));
                        } else {
                            love += 2;
                            StartCoroutine(Reaction(true));
                        }

                    } else {
                        StartCoroutine(Reaction(false));
                    }
                }
            }
        }
    }

    public IEnumerator Reaction(bool isLike) {
        likeDislike.SetActive(true);

        if(isLike) {
            likeDislike.GetComponent<SpriteRenderer>().sprite = like;
            catAudio.clip = meow;
            catAudio.Play();

        } else {
            likeDislike.GetComponent<SpriteRenderer>().sprite = dislike;
            catAudio.clip = hiss;
            catAudio.Play();
        }

        yield return new WaitForSeconds(1);

        likeDislike.SetActive(false);
    }
}
