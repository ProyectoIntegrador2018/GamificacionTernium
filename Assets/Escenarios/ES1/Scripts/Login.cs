using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Button LgBtn;
    public Text UsernameText;
    public InputField PasswordText;

    void Start() {
        LgBtn.onClick.AddListener(delegate {GameMind.logOn(UsernameText.text,PasswordText.text);});
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
