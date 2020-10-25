using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameLivesTimer : MonoBehaviour
{

    Text timer;
    float timerAmount = 5400;
    // cuanto tiempo se tarda en regenerar una vida en segundos
    float timeRegenerateLive = 1800;
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

    private void Awake() {
        //obtenido de la base de datos
        DateTime lastDate = new DateTime(2020, 10, 25, 11, 41, 0);
        TimeSpan difference;

        difference = System.DateTime.Now - lastDate;
        //diferencia en segundos
        print(difference.TotalSeconds);
        //las cantidad de vidas a regenerar 
        print(Mathf.FloorToInt((float)difference.TotalSeconds / timeRegenerateLive));
        //el tiempo actual del reloj
        timerAmount -= (float)difference.TotalSeconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Text>();
        if (timerAmount > 0) {
            isRunning = true;
        }
        displayCurrentTime();
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
                this.transform.parent.gameObject.SetActive(false);
                isRunning = false;
            }
        }
    }
}
