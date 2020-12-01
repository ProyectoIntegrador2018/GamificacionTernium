using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToAvatarCustomizaiton : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        // se crea un listener para el boton asociado para ir a la escena de avatares
        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("AvatarCustomization");
        });
    }
}
