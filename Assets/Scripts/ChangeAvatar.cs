using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangeAvatar : MonoBehaviour
{
    public Image stand; // estand donde aparecera la imagen del avatar seleccionado en el menu
    public Sprite stdr_av; // sprite del avatar estandard
    public Sprite female_av; // sprite del avatar femenino
    public Sprite glass_av; // sprite del avatar con lentes

    public Image customAvatarStand_hat; // ubicacion en donde apareceran los cascos en caso de tener un avatar personalizado
    public Image customAvatarStand_Glasses; // ubicacon donde se tendran los lentes en caso de tener un avatar personalizado
    public Sprite customAvatarHat_yellow; // sprite del casco amarillo
    public Sprite customAvatarHat_red; // sprite del casco rojo
    public Sprite customAvatarHat_green; // sprite del casco verde
    public Sprite glasses; // sprite de lentes
    public Sprite empty; // sprite vacio

    public Image avatarAnimation; // animacion que realizara el avatar
    public Image avatar; // imagen del avatar para aparecer
    public static bool flag;




    // Start is called before the first frame update
    void Start()
    {
        flag = false; // flag usada para detectar un avatar estandar

        GetComponent<Button>().onClick.AddListener(() => {
         // 
          avatar.enabled = true;
          avatarAnimation.enabled = false;

            //Escribe en la base de datos la seleccion del usuario

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
        // adquiere de la base de datos la seleccion del usuario y en el estand aparece el avatar seleccionado
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
