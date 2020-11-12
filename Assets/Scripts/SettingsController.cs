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

    // Start is called before the first frame update
    void Start(){

        int id = GlobalVariables.usernameId;

        volumenMusica.value = Database.getVolumenMusica(id);
        volumenSonidos.value = Database.getVolumenSonidos(id);
        apagarEventos.isOn = Database.getEventosActivos(id);
        apagarTutorial.isOn = Database.getTutorial();

        guardarCambios.onClick.AddListener( () => {
            Database.setVolumenMusica(id, Mathf.Round(volumenMusica.value * 100.0f) / 100.0f);
            Database.setVolumenSonidos(id, Mathf.Round(volumenSonidos.value * 100.0f) / 100.0f);
            Database.setEventosActivos(id, apagarEventos.isOn);
            Database.setTutorial(apagarTutorial.isOn);
            Database.saveData();
        });

    }

}
