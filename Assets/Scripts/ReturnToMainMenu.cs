using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMainMenu : MonoBehaviour
{
    // En inicio se crea un listener para el boton al que esta asociado el listener para que mande al usuario a la pantalla del menu principal
    void Start(){
        GetComponent<Button>().onClick.AddListener( () => {
            SceneManager.LoadScene("Menu");
        });
    }
}
