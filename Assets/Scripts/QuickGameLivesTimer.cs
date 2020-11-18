using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameLivesTimer : MonoBehaviour
{

    Text timer;
    float timerAmount;
    // cuanto tiempo se tarda en regenerar una vida en segundos
    float timeRegenerateLive = 120; //1800;
    float hours;
    float minutes;
    float seconds;
    int lastWhole = -1;
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

    void clearSavedTime() {
        Database.clearTimeLastLiveLost(GlobalVariables.usernameId);
        GameMind.saveData();
    }

    private void Awake() {

        List<string> savedDate = Database.getTimeOfLastLiveLost(GlobalVariables.usernameId);
        int maxLives = QuickGameLives.maxLives;
        int livesToRegen;

        if (savedDate.Count != 0) {

            //obtenido de la base de datos
            DateTime lastDate = DateTime.Parse(savedDate.First());
            TimeSpan difference;

            timerAmount = (maxLives - GlobalVariables.currentQuickGameLives) * timeRegenerateLive;
            difference = System.DateTime.Now - lastDate;

            //el tiempo actual del reloj
            timerAmount -= (float)difference.TotalSeconds;
            //print("Test: " + TimeSpan.FromSeconds(timerAmount));

            if (timerAmount <= 0) {
                Database.setCurrentLives(GlobalVariables.usernameId, maxLives);
                clearSavedTime();
                this.transform.parent.gameObject.SetActive(false);
                GlobalVariables.currentQuickGameLives = maxLives;
            }
            else {
                int aux = Mathf.FloorToInt((float)difference.TotalSeconds / timeRegenerateLive);
                //las cantidad de vidas a regenerar
                //print("Vidas a recuperar: " + aux);
                livesToRegen = Database.getCurrentLives(GlobalVariables.usernameId) + aux;
                //print("Vidas totales deben ser: " + livesToRegen);
                
                if (livesToRegen != Database.getCurrentLives(GlobalVariables.usernameId)) {
                    Database.removeTimeLastLiveLost(GlobalVariables.usernameId, aux);
                    //if(Database.getTimeOfLastLiveLost(GlobalVariables.usernameId).Count != 0) {
                        Database.setFirstTime(GlobalVariables.usernameId, difference.TotalSeconds);
                    //print(DateTime.Parse(Database.getTimeOfLastLiveLost(GlobalVariables.usernameId).First()).Subtract(TimeSpan.FromSeconds(timerAmount)));
                    //}
                }
                Database.setCurrentLives(GlobalVariables.usernameId, livesToRegen);
                GlobalVariables.currentQuickGameLives = livesToRegen;
                Database.saveData();
            }

            
        }
        else {
            this.transform.parent.gameObject.SetActive(false);
        }

    }

    private void OnEnable() {
        Awake();
        QuickGameEventSystem.current.OnEnableCheck();
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
                clearSavedTime();
                this.transform.parent.gameObject.SetActive(false);
                isRunning = false;
            }
        }
    }
}
