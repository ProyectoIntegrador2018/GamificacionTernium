using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoCreateUser : MonoBehaviour
{
    public GameObject userButton;
    void Start()
    {

        if(!Database.isAdmin(GlobalVariables.usernameId))
        {
            userButton.SetActive(false);
        }
        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("UserCreation");
        });
    }
}
