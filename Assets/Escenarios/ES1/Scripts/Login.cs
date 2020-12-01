using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Clase para obtener la info de inicio de sesion y validarla con la DB local
// Creada por el equipo 1

public class Login : MonoBehaviour
{
    public Button LgBtn;
    public Text UsernameText;
    public InputField PasswordText;
    public static bool flag;
    public Text adviseText;

    void Start() {
        LgBtn.onClick.AddListener(delegate { GameMind.logOn(UsernameText.text, PasswordText.text); ShowText();});
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText()
    {
        if (!flag) adviseText.text = "Usuario y/o contraseña inválido";
        else adviseText.text = "";

    }




}
