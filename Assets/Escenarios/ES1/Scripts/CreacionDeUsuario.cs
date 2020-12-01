using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// Clase para recibir la info de los inputs para crear usuario
// Solo accesible para el admin
// Creada por el equipo 2

public class CreacionDeUsuario : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Login;
    public Button Cancel;
    public InputField UsernameText;
    public InputField PasswordText;
    public Dropdown teamDropdown;
    public GameObject popUp;
    public GameObject popUp_2;
    public Toggle adminToggle;
    public string adminString;
    public static bool available;

    void Start() {
        Login.onClick.AddListener(delegate {createUser();});
      
        
    }

    // Función para agregar al usuario
    public void createUser() {
        if (adminToggle.isOn == true)
        {
            Database.makeUser(UsernameText.text, PasswordText.text, teamDropdown.options[teamDropdown.value].text, "admin");
            if (available)
            {              
                Database.saveData();
                popUp.SetActive(true);
                cleanInputs();
                adminToggle.isOn = false;
            }
            else
            {
                popUp_2.SetActive(true);
            }

        }
        else
        {
            Database.makeUser(UsernameText.text, PasswordText.text, teamDropdown.options[teamDropdown.value].text, "usuario");
            if (available)
            {    
                Database.saveData();
                popUp.SetActive(true);
                cleanInputs();
            }
            else
            {
                popUp_2.SetActive(true);
            }

        }
    
    }

    //Regresar a login
    public void returnLogin(){
        SceneManager.LoadScene("Login");
    }

    // Limpiar inputs
    void cleanInputs(){
        UsernameText.text = "";
        PasswordText.text = "";
    }


    /*
    public void testText()
    {
        Debug.Log("Username: " + UsernameText.text + " Password: " + PasswordText.text + " Turno: " + teamDropdown.options[teamDropdown.value].text);
    }
    */
}
