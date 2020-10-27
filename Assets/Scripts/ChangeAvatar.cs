using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangeAvatar : MonoBehaviour
{
    public Image stand;
    public Sprite stdr_av;
    public Sprite female_av;
    public Sprite glass_av;

    public Image customAvatarStand_hat;
    public Image customAvatarStand_Glasses;
    public Sprite customAvatarHat_yellow;
    public Sprite customAvatarHat_red;
    public Sprite customAvatarHat_green;
    public Sprite glasses;
    public Sprite empty;

    public Image avatarAnimation;
    public Image avatar;
    public static bool flag;




    // Start is called before the first frame update
    void Start()
    {
        flag = false;

        GetComponent<Button>().onClick.AddListener(() => {
         // 
          avatar.enabled = true;
          avatarAnimation.enabled = false;

          if (this.name == "Avatar1_Stand")
          {
              stand.sprite = stdr_av;       
              AvatarController.avatarName = "Default";
              flag = true;
              Database.setCustomAvatar(false);
           
            }
          else if (this.name == "Avatar2_Stand ")
          {
              stand.sprite = female_av;       
              AvatarController.avatarName = "Female_av";
              flag = true;
              Database.setCustomAvatar(false);
                
            }
          else if (this.name == "Avatar3_Stand ")
          {
              stand.sprite = glass_av;
              AvatarController.avatarName = "Glass_av";
              flag = true;
              Database.setCustomAvatar(false);
               
            }

          else if (this.name == "ItemStand_1")
          {
                flag = true;
                customAvatarStand_hat.sprite = customAvatarHat_yellow;         
                AvatarController.avatarHelmet = "Yellow";


          }
          else if (this.name == "ItemStand_2")
            {
                customAvatarStand_hat.sprite = customAvatarHat_red;
                flag = true;
                AvatarController.avatarHelmet = "Red";


            }
            else if (this.name == "ItemStand_3")
            {
                customAvatarStand_hat.sprite = customAvatarHat_green;
                flag = true;
                AvatarController.avatarHelmet = "Green";


            }
            else if (this.name == "ItemStand_4")
            {
             
                customAvatarStand_Glasses.sprite = glasses;
                flag = true;
                AvatarController.avatarGlasses = "SunGlasses";
                


            }
            else if(this.name == "ItemStand_Reset")
            {
                customAvatarStand_hat.sprite = empty;
                customAvatarStand_Glasses.sprite = empty;
                flag = true;
                AvatarController.avatarGlasses = "None";
                AvatarController.avatarHelmet = "None";
            }


            Database.saveData();


        });
        if (Database.getAvatar() == "Default")
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

    // Update is called once per frame
    void Update()
    {
        if (Database.getCustomAvatar())
        {
            if (Database.getHelmet() == "Yellow" && !flag)
            {
                customAvatarStand_hat.sprite = customAvatarHat_yellow;
            }
            else if (Database.getHelmet() == "Red" && !flag)
            {
                customAvatarStand_hat.sprite = customAvatarHat_red;
            }
            else if (Database.getHelmet() == "Green" && !flag)
            {
                customAvatarStand_hat.sprite = customAvatarHat_green;
            }


            if (Database.getGlasses() == "SunGlasses" && !flag)
            {
                customAvatarStand_Glasses.sprite = glasses;
            }
        }
      


    }



}
