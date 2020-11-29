using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToTop10 : MonoBehaviour
{
    // En inicio se crea un listener para el boton al que esta asociado el listener para que mande al usuario a la pantalla del top 10
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("Top10");
        });
    }
}
