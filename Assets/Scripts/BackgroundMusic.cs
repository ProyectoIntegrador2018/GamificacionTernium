using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance; // instancia que mantendra la musica de fondo por todas las escenas
    public AudioSource soundEffect; // cancion con la musica de fondo


    void Awake()
    {
       
        if(instance != null) // si esta instancia ya existe en la escena, entonces se debe destruir para evitar bugs de sonido
        {
            Destroy(transform.gameObject);
           
        }
        else // mantener la instancia cuando se cambie de escena
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

     
    }

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>(); // acceder al componente de audio
        soundEffect.volume = Database.getVolumenMusica(GlobalVariables.usernameId); // volumen del efecto de sonido
    }

    private void Update()
    {
        //si la escena es la de log in entonces se debe silenciar el juego
        if(SceneManager.GetActiveScene().name == "login")
        {
            soundEffect.mute = true;
        }
        else
        {
            soundEffect.mute = false;
        }
    }
}
