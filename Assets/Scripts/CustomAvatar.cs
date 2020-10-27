using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomAvatar : MonoBehaviour
{
    public Button customAvatarButtom;
    public GameObject avatarCanvas;
    public Button closeAvatarCustom;
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
