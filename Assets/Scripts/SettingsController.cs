using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public Slider volumenMusica;
    public Slider volumenSonidos;
    public Toggle apagarEventos;
    public Toggle apagarTutorial;
    public Button guardarCambios;

    void Start(){

        int id = GlobalVariables.usernameId;

        volumenMusica.value = Database.getVolumenMusica(id);
        volumenSonidos.value = Database.getVolumenSonidos(id);
        apagarEventos.isOn = Database.getEventosActivos(id);
        apagarTutorial.isOn = Database.getTutorial();

        //listener para el boton de guardar cambios, al presionarlo guarda los valores actuales dentro de la base de datos
        guardarCambios.onClick.AddListener(() => {
            Database.setVolumenMusica(id, Mathf.Round(volumenMusica.value * 100.0f) / 100.0f);
            Database.setVolumenSonidos(id, Mathf.Round(volumenSonidos.value * 100.0f) / 100.0f);
            Database.setEventosActivos(id, apagarEventos.isOn);
            Database.setTutorial(apagarTutorial.isOn);
            Database.saveData();
            BackgroundMusic.instance.GetComponent<AudioSource>().volume = Mathf.Round(volumenMusica.value * 100.0f) / 100.0f;
        });

    }

}
