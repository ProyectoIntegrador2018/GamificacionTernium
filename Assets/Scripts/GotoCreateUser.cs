using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoCreateUser : MonoBehaviour
{
    public GameObject userButton;
    void Start()
    {
        // si el usuario no es de tipo administrador el boton se mantiene escondido
        if(!Database.isAdmin(GlobalVariables.usernameId))
        {
            userButton.SetActive(false);
        }
        // se crea un listener para mandar a la escena de la creacion de usuario
        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("UserCreation");
        });
    }
}
