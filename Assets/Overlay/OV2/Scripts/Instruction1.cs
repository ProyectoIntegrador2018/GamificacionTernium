using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Instruction1 : MonoBehaviour {
    public Button button;
    public Button button1;
    public Button button2;
    public Button button3;
    public Image fog;
    public Text indicatorText1;
    public Text indicatorText2;
    public Text indicatorText3;
    public Image indicatorBox1;
    public Image indicatorBox2;
    public Image indicatorBox3;
    public Image indicatorLine1;
    public Image indicatorLine2;
    public Image indicatorLine3;
    public Text dialogueText;
    public Sprite[] LivesSprites;
    public Image LivesUI;
    public Text Score;

	private void Awake() {
		button.GetComponent<Button>().enabled = false;
    	indicatorText1.canvasRenderer.SetAlpha(0f);
    	indicatorText2.canvasRenderer.SetAlpha(0f);
    	indicatorText3.canvasRenderer.SetAlpha(0f);
    	indicatorBox1.canvasRenderer.SetAlpha(0f);
    	indicatorBox2.canvasRenderer.SetAlpha(0f);
    	indicatorBox3.canvasRenderer.SetAlpha(0f);
    	indicatorLine1.canvasRenderer.SetAlpha(0f);
    	indicatorLine2.canvasRenderer.SetAlpha(0f);
    	indicatorLine3.canvasRenderer.SetAlpha(0f);
    	Score.text = "0";
    }

    private void OnEnable() {
    	StartCoroutine(Appear());
    }

    public void Feedback(string option) {
    	if (option != "Blanco") {
    		dialogueText.text = "¡Incorrecto! Es color blanco.";
    		button1.GetComponent<Button>().enabled = false;
    		button2.GetComponent<Button>().enabled = false;
    		button3.GetComponent<Button>().enabled = false;
    		button.GetComponent<Button>().enabled = true;
    		Score.text = "-100";
    		LivesUI.sprite = LivesSprites[4];
    	} else {
    		dialogueText.text = "¡Correcto!";
    		button1.GetComponent<Button>().enabled = false;
    		button2.GetComponent<Button>().enabled = false;
    		button3.GetComponent<Button>().enabled = false;
    		button.GetComponent<Button>().enabled = true;
    		Score.text = "100";
    	}
    }

    IEnumerator Appear() {
    	indicatorText3.CrossFadeAlpha(1.0f, 0.5f, false);
    	indicatorBox3.CrossFadeAlpha(1.0f, 0.5f, false);
    	indicatorLine3.CrossFadeAlpha(1.0f, 0.5f, false);
    	yield return new WaitForSeconds(1);
    	indicatorText2.CrossFadeAlpha(1.0f, 0.5f, false);
    	indicatorBox2.CrossFadeAlpha(1.0f, 0.5f, false);
    	indicatorLine2.CrossFadeAlpha(1.0f, 0.5f, false);
    	yield return new WaitForSeconds(1);
    	indicatorText1.CrossFadeAlpha(1.0f, 0.5f, false);
    	indicatorBox1.CrossFadeAlpha(1.0f, 0.5f, false);
    	indicatorLine1.CrossFadeAlpha(1.0f, 0.5f, false);
    	yield return new WaitForSeconds(3);
    	indicatorText1.GetComponent<Text>().enabled = false;
    	indicatorText2.GetComponent<Text>().enabled = false;
    	indicatorText3.GetComponent<Text>().enabled = false;
    	indicatorBox1.GetComponent<Image>().enabled = false;
    	indicatorBox2.GetComponent<Image>().enabled = false;
    	indicatorBox3.GetComponent<Image>().enabled = false;
    	indicatorLine1.GetComponent<Image>().enabled = false;
    	indicatorLine2.GetComponent<Image>().enabled = false;
    	indicatorLine3.GetComponent<Image>().enabled = false;
    	fog.GetComponent<Image>().enabled = false;
    	yield return new WaitForEndOfFrame();
    }
}
