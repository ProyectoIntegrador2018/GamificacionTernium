using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    public static string avatarName = "Default"; // nombre del avatar que se escribe en la base de datos
    public static string avatarHelmet = "None"; // casco que contiene el avatar
    public static string avatarGlasses = "None"; // lentes que contiene el avatar

    public Image avatarImage; // imagen del avatar
    public Image avatarAnimation; // animacion del avatar para activar o desactivar

    public Animator animator; // controlador de animaciones del avatar 
    public bool stdr_av_anim; // activar la animacion del avatar estandar
    public bool female_av_anim; // activar la animacion del avatar femenino
    public bool glass_av_anim; // activar la animacion del avatar con lentes

    // Start is called before the first frame update
    void Start()
    {
        // segun el avatar seleccionado se iniciara la animacion correscomndiente
        GetComponent<Button>().onClick.AddListener(() => {
                  
            avatarAnimation.enabled = true;
            avatarImage.enabled = false;
            Database.setAvatar(avatarName);
            Database.setHelmet(avatarHelmet);
            Database.setGlasses(avatarGlasses);
            
            if (Database.getAvatar() == "Default")
            {           
                StandardAvatarAnim();
            }
            else if (Database.getAvatar() == "Female_av")
            {
              
                FemaleAvatarAnim();
            }
            else if (Database.getAvatar() == "Glass_av")
            {          
                GlassAvatarAnim();
            }
            else
            {
                return;
            }

            Database.saveData();


            // Debug.Log(Database.getAvatar());
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //iniciar animacion del avatar estandar
    void StandardAvatarAnim()
    {
        stdr_av_anim = true;
        female_av_anim = false;
        glass_av_anim = false;

        animator.SetBool("Default_Av", stdr_av_anim);
        animator.SetBool("Female_Av", female_av_anim);
        animator.SetBool("Glass_Av", glass_av_anim);

    }
    // iniciar animacion del avatar femenino
    void FemaleAvatarAnim()
    {
        stdr_av_anim = false;
        female_av_anim = true;
        glass_av_anim = false;

        animator.SetBool("Default_Av", stdr_av_anim);
        animator.SetBool("Female_Av", female_av_anim);
        animator.SetBool("Glass_Av", glass_av_anim);
    }
    // iniciar animacion del avatar con lentes
    void GlassAvatarAnim()
    {
        stdr_av_anim = false;
        female_av_anim = false;
        glass_av_anim = true;

        animator.SetBool("Default_Av", stdr_av_anim);
        animator.SetBool("Female_Av", female_av_anim);
        animator.SetBool("Glass_Av", glass_av_anim);
    }

}
