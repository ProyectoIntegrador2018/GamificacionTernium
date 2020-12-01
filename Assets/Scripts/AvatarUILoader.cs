using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AvatarUILoader : MonoBehaviour
{
    public Image stand; // Stand en donde aparecera el avatar en el UI
    public Sprite stdr_av; // sprite del avatar estandar
    public Sprite female_av; // sprite del avatar femenino
    public Sprite glass_av; // sprite del avatar con lentes

    public Image customAvatarStand_hat; // imagen donde aparecera el casco del avatar
    public Image customAvatarStand_Glasses; // imagen donde apareceran los lentes
    public Sprite CustomAvatar; // sprite del avatar personalizado
    public Sprite CustomGlasses; // sprite de los lentes en la UI
    public Sprite YellowCustomHelmet; // casco amarillo en la UI
    public Sprite RedCustomHelmet; // casco rojo en la UI
    public Sprite GreenCustomHelmet; // casco verde en la UI
    public Sprite Empty; // imagen vacia que aparecera cuando no se tenga ningun item seleccionado


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAvatarImage();

    }

    // Funcion para desplegar la imagen del avatar segun los campos guardados en la base de datos
    void DisplayAvatarImage()
    {
        if (!Database.getCustomAvatar()) // si no se tiene un avatar personalizado entonces se usara uno de los 3 predeterminados
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
        else // en caso de tener un avatar personalizado 
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
