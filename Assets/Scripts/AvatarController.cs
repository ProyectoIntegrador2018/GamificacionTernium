using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    public static string avatarName = "Default";
  



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Database.setAvatar(avatarName);
            Database.saveData();
           // Debug.Log(Database.getAvatar());
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
