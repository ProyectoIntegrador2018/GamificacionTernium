using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    static string[] LosQueAyudaron = new string[100];
    static string[] LosVatos = new string[100];
    static int CuantosHay = 0;
    static int Vatos = 0;
    public static string path_personas;
    public static string path_escenarios;
    public TextAsset Persons;
    public TextAsset Cases;
    public static string PE;
    public static string PP;


    ///public static TextAsset

    //Nombre de Escena
    string NamingConv;

    GameObject BtnAyuda ;
    GameObject BA;


    // Start is called before the first frame update
    void Start()
    {
        string Caso = GlobalVariables.Caso.ToString();
        string CurrentScene = SceneManager.GetActiveScene().name;

        BtnAyuda = Resources.Load<GameObject>("Prefabs/Pregunta");
        //LoadInfo(Caso[0]);

        //Debug.Log(Caso);

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public static void ExisteAyuda( string Escena)
    {
        Vatos = 0;
        CuantosHay = 0;
        LoadInfo(Escena[0]);
    }

    public static string Ask4Help()
    {
        if(CuantosHay == 0)
        {
            return "Nadie";
        }
        else
        {
            int Rand = Random.Range(0, CuantosHay+1);

            return LosQueAyudaron[Rand];
        }
    }

    //Literal solo traer la info
    static void LoadInfo(char Escena)
    {
        path_personas = Application.persistentDataPath + "/Personas.txt";
        path_escenarios = Application.persistentDataPath + "/Escenarios.txt";
        //string PathEscenarios;


        //Debug.Log(path);
        if (File.Exists(path_personas))
        {
            PE = File.ReadAllText(Application.persistentDataPath + "/Escenarios.txt");
            PP = File.ReadAllText(Application.persistentDataPath + "/Personas.txt");
            //Debug.Log("hola" + myTextAsset);
            //userBase = JsonUtility.FromJson<Users>(myTextAsset);
        }
        else
        {
            //userBase = JsonUtility.FromJson<Users>(jsonFile.text);
            var PathEscenarios = Resources.Load<TextAsset>("TextFiles/Escenarios");
            var PathPersonas = Resources.Load<TextAsset>("TextFiles/Personas");
            PE = PathEscenarios.text;
            PP = PathPersonas.text;
        }

            int i = 0;
            string Nombre = "";
            char Vacio = ' ';

            while (i < PP.Length)
            {
                //Debug.Log(i);
                if (PP[i] != Vacio)
                {
                    Nombre = Nombre + PP[i];
                    i++;
                }
                else if (i + 3 < PP.Length)
                {
                    LosVatos[Vatos] = Nombre;
                    Vatos++;
                    //Debug.Log(Nombre);
                    Nombre = "";
                    i = i + 3;

                }
                else
                {
                    LosVatos[Vatos] = Nombre;
                    Vatos++;
                    Nombre = "";
                    i++;
                }
            }


            for (i = 0; i < Vatos; i++)
            {
                //Debug.Log(Escena);
                if(i*3 < PE.Length)
                {
                    if (PE[i * 3] == Escena)
                    {
                        //Debug.Log(LosVatos[i]);
                        LosQueAyudaron[CuantosHay] = LosVatos[i];
                        CuantosHay++;
                        GlobalVariables.ExisteAyuda = true;
                    }
                }
                

            }
            //Debug.Log(CuantosHay);

        
    }

    //Liteal solo escribir la info
    static void WriteInfo()
    {
        int CuantasPers = PP.Length;
        int CuantosCasos = PE.Length;

        PP = PP + LosQueAyudaron[CuantosHay - 1] + " \n";
        PE = PE + GlobalVariables.Caso.ToString()[0]+"\n";

        File.WriteAllText(path_personas, PP);
        File.WriteAllText(path_escenarios, PE);
        
        /*
        string escenarios_path = "Assets/M2/TextFile/Escenarios.txt";
        string personas_path = "Assets/M2/TextFile/Personas.txt";
        //Write some text to the test.txt file
        StreamWriter Escenarios = new StreamWriter(escenarios_path, true);
        StreamWriter Personas = new StreamWriter(personas_path, true);

        //Preguntas.WriteLine("");
        //Personas.WriteLine("");
        Escenarios.WriteLine(GlobalVariables.Caso.ToString());
        Personas.WriteLine(LosQueAyudaron[CuantosHay-1]);
        
        
        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(escenarios_path);
        //TextAsset asset = Resources.Load("test");
        //Print the text from the file
        //Debug.Log(asset.text);
        //UnityEditor.AssetDatabase.ImportAsset(escenarios_path);
        //UnityEditor.AssetDatabase.ImportAsset(personas_path);

        Personas.Close();
        Escenarios.Close();
        */
    }
    
    static public void AgregarAyuda(string Nombre, string Caso)
    {
        LosQueAyudaron[CuantosHay] = Nombre;
        CuantosHay++;
        WriteInfo();
    }
}
