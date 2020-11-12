using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    private AudioSource soundEffect;


    void Awake()
    {
        if(instance != null)
        {
            Destroy(transform.gameObject);
           
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

     
    }

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        soundEffect.volume = Database.getVolumenMusica(GlobalVariables.usernameId);
    }

    private void Update()
    {
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
