using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AvatarUILoader : MonoBehaviour
{
    public Image stand;
    public Sprite stdr_av;
    public Sprite female_av;
    public Sprite glass_av;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAvatarImage();
    }


    void DisplayAvatarImage()
    {
        if(Database.getAvatar() == "Default")
        {
            stand.sprite = stdr_av;
        }
        else if (Database.getAvatar() == "Female_av")
        {
            stand.sprite = female_av;
        }
        else if (Database.getAvatar() == "Glass_av")
        {
            stand.sprite = glass_av;
        }

    }

}
