using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logof : MonoBehaviour
{
    public Button LgBtn;

    void Start() {
        LgBtn.onClick.AddListener(delegate {GameMind.logOff();});
        SceneManager.LoadScene("login");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
