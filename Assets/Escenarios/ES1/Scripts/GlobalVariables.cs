using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para las variables globales
public class GlobalVariables : MonoBehaviour {

	// Variables publicas tipo entero
    public static int sumPos = -20;
    public static int currentTagItem = 0;
    public static int lives = 5;
    public static int score = 0;
    public static string username;
    public static int usernameId;
    public static bool instructions = false;
    //Estos son para lo colaborativo
    public static string Escenario = "0";
    public static int Caso = 0;
    public static int VecesAyuda = 1;
    public static bool ExisteAyuda = false; 
    public static bool ElFinal = false;

    // Variables publicas tipo lista de objetos
    public static List<GameObject> items = new List<GameObject>();

    // Variables publicas tipo diccionario de enteros
    public static Dictionary<int, int> pairAnswerSlot = new Dictionary<int, int>();
}
