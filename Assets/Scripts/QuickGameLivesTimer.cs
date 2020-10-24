using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameLivesTimer : MonoBehaviour
{

    Text timer;
    float timerAmount = 15;
    float timeRegenerateLive = 5;//1800;
    float hours;
    float minutes;
    float seconds;
    int lastWhole;
    bool isRunning;

    string formatTime(float time) {

        string aux = time.ToString();

        if (time < 10) {
            aux = "0" + aux;
        }

        return aux;
    }

    void displayCurrentTime() {
        hours = Mathf.FloorToInt(timerAmount / 3600);
        minutes = Mathf.FloorToInt(timerAmount % 3600) / 60;
        seconds = Mathf.FloorToInt(timerAmount % 60);
        if (hours != 0) {
            timer.text = formatTime(hours) + ":" + formatTime(minutes) + ":" + formatTime(seconds);
        }
        else {
            timer.text = formatTime(minutes) + ":" + formatTime(seconds);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Text>();
        //timerAmount += 1;
        if (timerAmount > 1) {
            isRunning = true;
        }
        displayCurrentTime();
    }

    IEnumerator waitBeforeDisappearing() {
        yield return new WaitForSeconds(0f);
        this.transform.parent.gameObject.SetActive(false);
    }

    void canRegenerateLive() {
        if (Mathf.FloorToInt(timerAmount) % timeRegenerateLive == 0 && lastWhole != Mathf.FloorToInt(timerAmount)) {
            QuickGameEventSystem.current.OnTimeframeReached();
            lastWhole = Mathf.FloorToInt(timerAmount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) {
            if (Mathf.FloorToInt(timerAmount) > 0) {
                //print(timerAmount);
                timerAmount -= Time.deltaTime;
                canRegenerateLive();
                displayCurrentTime();
            }
            else {
                //StartCoroutine(waitBeforeDisappearing());
                this.transform.parent.gameObject.SetActive(false);
                isRunning = false;
            }
        }
    }
}
