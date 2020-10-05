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

    public Image avatarAnimation;
    public Image avatar;

    // Start is called before the first frame update
    void Start()
    {


        GetComponent<Button>().onClick.AddListener(() => {
         // 
          avatar.enabled = true;
          avatarAnimation.enabled = false;

          if (this.name == "Avatar1_Stand")
          {
              stand.sprite = stdr_av;       
              AvatarController.avatarName = "Default";
          }
          else if (this.name == "Avatar2_Stand ")
          {
              stand.sprite = female_av;       
              AvatarController.avatarName = "Female_av";
          }
          else if (this.name == "Avatar3_Stand ")
          {
              stand.sprite = glass_av;
              AvatarController.avatarName = "Glass_av";
          }




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
        
    }



}
