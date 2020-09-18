using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementFunction : MonoBehaviour
{
    public static string Description = "Se muestra aquí la descripción del un trofeo seleccionado, \n ¡Adelante, presiona uno!";
    public GameObject self;
    //Function transported from AchivementManager in order to be dinamically called.
    public void changeDescription()
    {
        //AchievementManager.SetDescription( self.transform.GetChild(2).GetComponent<Text>().text );
        Description = self.transform.GetChild(2).GetComponent<Text>().text;
    }
}
