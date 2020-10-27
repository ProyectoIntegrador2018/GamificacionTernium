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

    public Image customAvatarStand_hat;
    public Image customAvatarStand_Glasses;
    public Sprite CustomAvatar;
    public Sprite CustomGlasses;
    public Sprite YellowCustomHelmet;
    public Sprite RedCustomHelmet;
    public Sprite GreenCustomHelmet;
    public Sprite Empty;


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
        if (!Database.getCustomAvatar())
        {
            if (Database.getAvatar() == "Default")
            {
                stand.sprite = stdr_av;
                customAvatarStand_hat.sprite = Empty;
                customAvatarStand_Glasses.sprite = Empty;

            }
            else if (Database.getAvatar() == "Female_av")
            {
                stand.sprite = female_av;
                customAvatarStand_hat.sprite = Empty;
                customAvatarStand_Glasses.sprite = Empty;
            }
            else if (Database.getAvatar() == "Glass_av")
            {
                stand.sprite = glass_av;
                customAvatarStand_hat.sprite = Empty;
                customAvatarStand_Glasses.sprite = Empty;
            }
        }
        else
        {
            stand.sprite = CustomAvatar;
            if(Database.getGlasses() == "SunGlasses")
            {
                customAvatarStand_Glasses.sprite = CustomGlasses;
            }
            else
            {
                customAvatarStand_Glasses.sprite = Empty;
            }

            if (Database.getHelmet() == "Yellow")
            {
                customAvatarStand_hat.sprite = YellowCustomHelmet;
            }
            else if (Database.getHelmet() == "Red")
            {
                customAvatarStand_hat.sprite = RedCustomHelmet;
            }
            else if (Database.getHelmet() == "Green")
            {
                customAvatarStand_hat.sprite = GreenCustomHelmet;
            }
            else
            {
                customAvatarStand_hat.sprite = Empty;
            }

            //Database.getHelmet();

        }
    }

}
