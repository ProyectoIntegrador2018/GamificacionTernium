﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToSettings : MonoBehaviour {

    // Start is called before the first frame update
    void Start(){

        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("Settings");
        });

    }

}
