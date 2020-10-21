using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreacionDeUsuario : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Login;
    public Button Cancel;
    public Text UsernameText;
    public InputField PasswordText;
    public Dropdown turnDropDown;
    public GameObject popUp;

    void Start() {
        Login.onClick.AddListener(delegate {createUser();});
        Cancel.onClick.AddListener(delegate {returnLogin();});
    }

    // Función para agregar al usuario
    public void createUser() {
        Database.makeUser(UsernameText.text,PasswordText.text, turnDropDown.options[turnDropDown.value].text);
        Database.saveData();
        popUp.SetActive(true);

    
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
