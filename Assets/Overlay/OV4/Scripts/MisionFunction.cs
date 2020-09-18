using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MisionFunction : MonoBehaviour
{
    public static string Description = "Aqui aparecerá la información de una mision selecionada.";
    public static string Score = "0";
    public static int Escenario = -1;
    public GameObject self;
    //Function transported from AchivementManager in order to be dinamically called.
    public void changeInfo()
    {
        Description = self.transform.GetChild(2).GetComponent<Text>().text;
        Score = self.transform.GetChild(3).GetComponent<Text>().text;
        Escenario = int.Parse(self.transform.GetChild(4).GetComponent<Text>().text);
    }

}
