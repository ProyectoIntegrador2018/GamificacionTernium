using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    public static string avatarName = "Default";

    public Image avatarImage;
    public Image avatarAnimation;

    public Animator animator;
    public bool stdr_av_anim;
    public bool female_av_anim;
    public bool glass_av_anim;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
                  
            avatarAnimation.enabled = true;
            avatarImage.enabled = false;
            Database.setAvatar(avatarName);
            
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

            Database.saveData();


            // Debug.Log(Database.getAvatar());
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StandardAvatarAnim()
    {
        stdr_av_anim = true;
        female_av_anim = false;
        glass_av_anim = false;

        animator.SetBool("Default_Av", stdr_av_anim);
        animator.SetBool("Female_Av", female_av_anim);
        animator.SetBool("Glass_Av", glass_av_anim);

    }

    void FemaleAvatarAnim()
    {
        stdr_av_anim = false;
        female_av_anim = true;
        glass_av_anim = false;

        animator.SetBool("Default_Av", stdr_av_anim);
        animator.SetBool("Female_Av", female_av_anim);
        animator.SetBool("Glass_Av", glass_av_anim);
    }

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
