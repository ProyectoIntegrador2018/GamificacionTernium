using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomAvatar : MonoBehaviour
{
    public Button customAvatarButtom; // boton para seleccionar un avatar personalizado
    public GameObject avatarCanvas; // canvas que contiene el menu de un avatar personalizado
    public Button closeAvatarCustom; // boton para cerrar el menu del avatar personalizado
    // Start is called before the first frame update
    void Start()
    {
        customAvatarButtom.onClick.AddListener(delegate {
            Database.setCustomAvatar(true);
            avatarCanvas.SetActive(true);
     

        });
        /*
        closeAvatarCustom.onClick.AddListener(delegate {
            avatarCanvas.SetActive(false);


        });*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
