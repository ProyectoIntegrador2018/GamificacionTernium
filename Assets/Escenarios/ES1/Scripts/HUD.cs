using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Clase para los elementos graficos relacionados a variables
public class HUD : MonoBehaviour {
    
    // Variables publicas
    public Sprite[] LivesSprites;
    public Image LivesUI;
    public Text Score;

    void Start() {
        Score.text = "0";
    }

    void Update() {
        // Variable de vidas
        if (GlobalVariables.lives > 0) {
            LivesUI.sprite = LivesSprites[GlobalVariables.lives];
        } else {
            LivesUI.sprite = LivesSprites[0];
        }

        // Variable de puntaje
        Score.text = GlobalVariables.score.ToString();
    }

    // Reiniciar el juego
    public void ResetGame() {
        GlobalVariables.lives = 5;
        GlobalVariables.score = 0;
        SceneManager.LoadScene("P1");
    }

}
