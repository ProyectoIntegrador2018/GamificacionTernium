using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreacionDeUsuario : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Login;
   
    public Text UsernameText;
    public InputField PasswordText;
    public Dropdown turnDropDown;
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
            Database.makeUser(UsernameText.text, PasswordText.text, turnDropDown.options[turnDropDown.value].text, "admin");
            if (available)
            {              
                Database.saveData();
                popUp.SetActive(true);
            }
            else
            {
                popUp_2.SetActive(true);
            }

        }
        else
        {
            Database.makeUser(UsernameText.text, PasswordText.text, turnDropDown.options[turnDropDown.value].text, "usuario");
            if (available)
            {    
                Database.saveData();
                popUp.SetActive(true);
            }
            else
            {
                popUp_2.SetActive(true);
            }

        }
    
    }

    public void returnLogin(){
        SceneManager.LoadScene("Login");
    }


    /*
    public void testText()
    {
        Debug.Log("Username: " + UsernameText.text + " Password: " + PasswordText.text + " Turno: " + turnDropDown.options[turnDropDown.value].text);
    }
    */
}
