﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Top10UserScores : MonoBehaviour
{

    public Text userscoreTextPrefab;
    private User[] users;
    private Userscore[][] userscores;
    private Userscore[] totalScores;
    private int numberOfMissions;
    private bool totalScoresLoaded = false;
    

    //struct para esctructurar las puntuaciones de los jugadores
    private struct Userscore {
        public string username;
        public int score;

        public Userscore(string username, int score) {
            this.username = username;
            this.score = score;
        }

    }

    //la funcion carga las puntuaciones de los jugadores por mision
    Userscore[] loadMissionScores(int missionIndex) {
        Userscore[] aux = new Userscore[users.Length];
        int i = 0;
        foreach (User user in users) {
            aux[i] = new Userscore(user.username, user.niveles[missionIndex]);
            i++;
        }

        return aux;
    }

    //la funcion limpia el tablero de las puntuaciones
    void clearScoreboard() {
        foreach (Transform topscore in GameObject.Find("TopPlayers").transform) {
            Destroy(topscore.gameObject);
        }
    }

    //la funcion despliega en la tabla las puntuaciones de los jugadores junto con un titulo para la tabla actual
    void listTopScores(string missionName, Userscore[] userscores) {

        Text userscoreText = Instantiate(userscoreTextPrefab);
        userscoreText.transform.SetParent(GameObject.Find("TopPlayers").transform);
        userscoreText.transform.localScale = new Vector2(1, 1);
        userscoreText.transform.localPosition = new Vector2(0, 345);
        userscoreText.fontStyle = FontStyle.BoldAndItalic;
        userscoreText.text = missionName;
        for (int i = 0; i < 10 && i < userscores.Length; i++) {
            userscoreText = Instantiate(userscoreTextPrefab);
            userscoreText.transform.SetParent(GameObject.Find("TopPlayers").transform);
            userscoreText.transform.localScale = new Vector2(1, 1);
            //Offset para cada vez colocar mas abajo los jugadores con su puntuacion
            userscoreText.transform.localPosition = new Vector2(0, 345 - 70 * (i + 1));
            userscoreText.text = userscores[i].username + " - " + userscores[i].score;
        }
    }

    //la funcion manda a cargar las puntuaciones de los jugadores de una mision y despues los despliega en la tabla
    void displayMissionTopScores(string missionName, int missionIndex) {

        Userscore[] aux;
        if (userscores[missionIndex] == null) {
            userscores[missionIndex] = loadMissionScores(missionIndex);
        }

        clearScoreboard();

        aux = userscores[missionIndex].OrderByDescending(x => x.score).ToArray();
        listTopScores(missionName, aux);
    }

    //la funcion manda a cargar las puntuaciones de los jugadores por el total y despues los despliega en la tabla
    void displayTotalTopScores() {

        Userscore[] aux;

        for (int i = 0; i < numberOfMissions; i++) {
            if (userscores[i] == null) {
                userscores[i] = loadMissionScores(i);
            }
        }

        clearScoreboard();

        if (!totalScoresLoaded) {

            for (int i = 0; i < users.Length; i++) {
                totalScores[i].score = 0;
                totalScores[i].username = users[i].username;
                for (int j = 0; j < numberOfMissions; j++) {
                    totalScores[i].score += userscores[j][i].score;
                }
            }
            totalScoresLoaded = true;
        }

        
        aux = totalScores.OrderByDescending(x => x.score).ToArray();
        listTopScores("Puntuaciones Totales", aux);

    }

    void Start()
    {
        Top10EventSystem.current.onMissionClick += OnMissionClick;
        Top10EventSystem.current.onTotalClick += OnTotalClick;
        users = Database.GetNonAdminUsers();
        //El numero de niveles es igual al numero de misiones
        numberOfMissions = users[0].niveles.Length;
        userscores = new Userscore[numberOfMissions][];
        totalScores = new Userscore[users.Length];
    }

    private void OnMissionClick(string missionName, int missionIndex) {
        displayMissionTopScores(missionName, missionIndex);
    }

    private void OnTotalClick() {
        displayTotalTopScores();        
    }

    private void OnDestroy() {
        Top10EventSystem.current.onMissionClick -= OnMissionClick;
        Top10EventSystem.current.onTotalClick -= OnTotalClick;
    }
}
