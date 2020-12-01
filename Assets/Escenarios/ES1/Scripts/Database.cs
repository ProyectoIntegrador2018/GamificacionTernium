using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

[System.Serializable]
public struct ListaMisiones
{
    public List<Misiones> misiones;
}

[System.Serializable]
public struct Misiones {
    public int id;
    public string nombre;
    public string descripcion;
    public int puntuacionMax;
    public List<Preguntas> preguntas;
}

[System.Serializable]
public struct Preguntas {
    public double id;
    public string tipo;
    public string pregunta;
    public string[] opciones;
    public int[] vidasPerdidas;
    public string correctoTexto;
    public string[] falloTexto;
    public int respuestaCorrecta;
    public int puntos;
    public string sigEscenaCorrecto;
    public string[] sigEscenasError;
}

[System.Serializable]
public class User
{
    public int id;
    public string tipo;
    public string username;
    public string password;
    public bool tutorial;
    public string equipo;
    public int expMax;
    public int expMin;
    public int expCurrent;
    public int quickGameLives;
    public List<string> timeOfLastLiveLost;
    public string avatarImg;
    public bool customAvatar;
    public string avatarHelmet;
    public string avatarGlasses;
    public int nivelJugador;
    public float volumenMusica;
    public float volumenSonidos;
    public bool eventosActivos;
    public int[] niveles;
    public bool[] achivements;
    public bool[] started;

}

[System.Serializable]
public class Users
{
    //employees is case sensitive and must match the string "employees" in the JSON.
    public User[] users;
    
    public void Push(User x) {
        int len = users.Length;
        User[] newUsers = new User[len+1];
        for(int i=0; i<len;i++){
            newUsers[i] = users[i];
        }
        newUsers[len] = x;
        users = newUsers;
    }
}

// Clase para manejar toda la info de la base de datos local
// Creada por el equipo 1, modificada por el equipo 2

public class Database : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextAsset jsonMisiones;
    public static Users userBase;
    public static ListaMisiones missionList;
    public static string path;

    // Start is called before the first frame update
    void Start()
    {
        // userBase = JsonUtility.FromJson<Users>(jsonFile.text);

        //Asumire que es este
        path = Application.persistentDataPath + "/database.json";
        //Debug.Log("Fabi Aqui");
        //Debug.Log(path);

        //Este estara mal


        //Debug.Log(path);
        // Si el archivo existe, crear la estructura de datos desde sus contents
        if (File.Exists(path)) {
            var myTextAsset = File.ReadAllText(Application.persistentDataPath + "/database.json"); 
            //Debug.Log("hola" + myTextAsset);
            // Deserializando el archivo anterior con la libreria que ya trae Unity
            // Consulta https://docs.unity3d.com/ScriptReference/JsonUtility.html para mas detalles
            // Esta pagina tambien me ayudo bastante https://json2csharp.com/    
            userBase = JsonUtility.FromJson<Users>(myTextAsset);
        }
        // Si el archivo no existe, mandar llamar la funcion saveData para crearlo
        else
        {
            // Deserializando el archivo anterior con la libreria que ya trae Unity
            // Consulta https://docs.unity3d.com/ScriptReference/JsonUtility.html para mas detalles
            // Esta pagina tambien me ayudo bastante https://json2csharp.com/   
            userBase = JsonUtility.FromJson<Users>(jsonFile.text);
            //Si no existe se crea en local para siempre accesar desde el path 
            saveData();
        }

        missionList = JsonUtility.FromJson<ListaMisiones>(jsonMisiones.text);

    }

    // GETTERS y demas funciones para manipular la info de la DB local

    public static float getVolumenMusica(int userId) {
        return userBase.users[userId].volumenMusica;
    }
    public static void setVolumenMusica(int userId, float value) {
        userBase.users[userId].volumenMusica = value;
    }
    public static float getVolumenSonidos(int userId) {
        return userBase.users[userId].volumenSonidos;
    }
    public static void setVolumenSonidos(int userId, float value) {
        userBase.users[userId].volumenSonidos = value;
    }
    public static bool getEventosActivos(int userId) {
        return userBase.users[userId].eventosActivos;
    }
    public static void setEventosActivos(int userId, bool value) {
        userBase.users[userId].eventosActivos = value;
    }
    public static void setFirstTime(int userId) {
        userBase.users[userId].timeOfLastLiveLost[0] = DateTime.Now.ToString();
    }
    public static void setCurrentLives(int userId, int lives) {
        userBase.users[userId].quickGameLives = lives;
    }
    public static void clearTimeLastLiveLost(int userId) {
        userBase.users[userId].timeOfLastLiveLost.Clear();
        
    }
    public static void removeTimeLastLiveLost(int userId, int howMany) {
        for (int i = 0; i < howMany; i++) {
            userBase.users[userId].timeOfLastLiveLost.RemoveAt(0);
        }
    }
    public static void addTimeLastLiveLost(int userId) {
        userBase.users[userId].timeOfLastLiveLost.Add(DateTime.Now.ToString());
    }
    public static int getCurrentLives(int userId) {
        return userBase.users[userId].quickGameLives;
    }
    public static List<string> getTimeOfLastLiveLost(int userId) {
        return userBase.users[userId].timeOfLastLiveLost;
    }
    public static bool isAdmin(int userId) {
        if (userBase.users[userId].tipo == "admin") {
            return true;
        }
        return false;
    }
    public static string getTextoCorrecto(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].correctoTexto;
    }

    public static string[] getSigEscenasError(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].sigEscenasError;
    }

    public static string getSigEscenaCorrecto(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].sigEscenaCorrecto;
    }

    public static int getPuntosPregunta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].puntos;
    }

    public static int getOpcionCorrecta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].respuestaCorrecta;
    }

    public static string[] getFalloTexto(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].falloTexto;
    }

    public static int[] getVidasPerdidas(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].vidasPerdidas;
    }

    public static string getPregunta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].pregunta;
    }

    public static string[] getOpcionesPregunta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].opciones;
    }

    public static string[] getAllMissionNames() {
        int n = missionList.misiones.Count;
        string[] namesList = new string[n];

        for (int i = 0; i < n; i++) {
            namesList[i] = missionList.misiones[i].nombre;
        }

        return namesList;
    }

    public static string[] getAllMissionDescriptions() {
        int n = missionList.misiones.Count;
        string[] descriptionsList = new string[n];

        for (int i = 0; i < n; i++) {
            descriptionsList[i] = missionList.misiones[i].descripcion;
        }

        return descriptionsList;
    }

    public static string getMissionName(int index) {
        return missionList.misiones[index].nombre;
    }

    public static int getAmountOfMissions() {
        return missionList.misiones.Count;
    }

    public static int login(string username, string pass) {
        // Debug.Log("aver si encontramos algo " + userBase.users[1].username);
        foreach (User user in userBase.users) {
            if(user.username == username && user.password == pass) {
                // Debug.Log("aver si encontramos algo ");
                Login.flag = true;
                return user.id;
            }
        }
        Login.flag = false;
        return -1;
    }

    public static User[] GetNonAdminUsers() {

        List<User> aux = new List<User>();

        for(int i = 0; i < userBase.users.Length; i++) {
            if (!isAdmin(i)) {
                aux.Add(userBase.users[i]);
            }
        }

        return aux.ToArray();
    }

    public static User[] GetUsers() {
        return userBase.users;
    }

    public static int[] getExpBarData() {
        int[] aux = {userBase.users[GlobalVariables.usernameId].expMax, userBase.users[GlobalVariables.usernameId].expMin, userBase.users[GlobalVariables.usernameId].expCurrent};
        return aux;
    }

    public static void setExpBarData(int expMax, int expMin, int expCurrent) {
        userBase.users[GlobalVariables.usernameId].expMax = expMax;
        userBase.users[GlobalVariables.usernameId].expMin = expMin;
        userBase.users[GlobalVariables.usernameId].expCurrent = expCurrent;
    }

    public static void setAvatar(string avatarName)
    {
        userBase.users[GlobalVariables.usernameId].avatarImg = avatarName;
    }

    public static int getNivelJugador() {
        return userBase.users[GlobalVariables.usernameId].nivelJugador;
    }

    public static void setNivelJugador(int nivelJugador) {
        userBase.users[GlobalVariables.usernameId].nivelJugador = nivelJugador;
    }

    // Check if the user has an achivmenet on "i" scene
    public static bool getAchivement(int i) {
        return userBase.users[GlobalVariables.usernameId].achivements[i];
    }
    
    public static void setAchivement(int i) {
        userBase.users[GlobalVariables.usernameId].achivements[i-1] = true;
    }

    // Checks if the user has STARTED the "i" scene
    public static bool getStarted(int i)
    {
        return userBase.users[GlobalVariables.usernameId].started[i];
    }

    public static void setStarted(int i)
    {
        //Debug.Log("1Numero out of range: " + i);
        userBase.users[GlobalVariables.usernameId].started[i-1] = true;
    }

    public static int getScore(int i) {
        //Debug.Log("2Numero out of range: " + i);
        return userBase.users[GlobalVariables.usernameId].niveles[i];
    }
    
    public static void setScore(int i, int s) {
        //Debug.Log("Set Score");
        //Debug.Log("Caso " + i + " Score " + s);
        //Debug.Log("Antes");
        //Debug.Log(userBase.users[GlobalVariables.usernameId].niveles[i - 1]);
        userBase.users[GlobalVariables.usernameId].niveles[i-1] = s;
        //Debug.Log("Despues");
        //Debug.Log(userBase.users[GlobalVariables.usernameId].niveles[i - 1]);
    }

    public static string getEquipo(){
        return userBase.users[GlobalVariables.usernameId].equipo; 
    }

    public static bool getTutorial() {
        return userBase.users[GlobalVariables.usernameId].tutorial;
    }

    public static void setTutorial() {
        userBase.users[GlobalVariables.usernameId].tutorial = false;
    }
    public static void setTutorial(bool value) {
        userBase.users[GlobalVariables.usernameId].tutorial = value;
    }
    
    public static void makeUser(string name, string password,string team,string isAdmin) {
        foreach (User user in userBase.users) {
            if(user.username == name) {
                CreacionDeUsuario.available = false;
                Debug.Log("Este usuario ya existe en la base de datos");
                return;
            }
        }
        CreacionDeUsuario.available = true;
        createUser(name, password,team,isAdmin);
        Debug.Log("Usuario creado y guardado correctamente");

    }

    public static void createUser(string name, string password, string team, string isAdmin) {
        User nUser = new User();
        nUser.id = userBase.users[userBase.users.Length - 1].id + 1;
        nUser.tipo = isAdmin;
        nUser.username = name;
        nUser.password = password;
        nUser.tutorial = true;
        nUser.equipo = team;
        nUser.expMax = 500;
        nUser.expMin = 0;
        nUser.avatarImg = "Default";
        nUser.customAvatar = false;
        nUser.avatarHelmet = "None";
        nUser.avatarGlasses = "None";
        nUser.expCurrent = 0;
        nUser.quickGameLives = 3;
        nUser.timeOfLastLiveLost = new List<string>();
        nUser.nivelJugador = 1;
        nUser.volumenMusica = 1.0f;
        nUser.volumenSonidos = 1.0f;
        nUser.eventosActivos = true;
        int[] niv = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        nUser.niveles = niv;
        bool[] ach = {false, false, false, false, false, false, false, false, false, false, false };
        nUser.achivements = ach;
        nUser.started = ach;
        userBase.Push(nUser);
    }

    public static string getAvatar()
    {
        return userBase.users[GlobalVariables.usernameId].avatarImg;
    }

    public static void setHelmet(string helmet)
    {
        userBase.users[GlobalVariables.usernameId].avatarHelmet = helmet;
    }

    public static string getHelmet()
    {
        return userBase.users[GlobalVariables.usernameId].avatarHelmet;
    }

    public static void setGlasses(string glasses)
    {
        userBase.users[GlobalVariables.usernameId].avatarGlasses = glasses;
    }

    public static string getGlasses()
    {
        return userBase.users[GlobalVariables.usernameId].avatarGlasses;
    }
    public static bool getCustomAvatar()
    {
        return userBase.users[GlobalVariables.usernameId].customAvatar;
    }

    public static void setCustomAvatar(bool customAvatar)
    {
        userBase.users[GlobalVariables.usernameId].customAvatar = customAvatar;
    }

    public static int getCurrentAchivements(){
        int current=0;
        for (int ach=0; ach<userBase.users[GlobalVariables.usernameId].achivements.Length; ach++) {
            if(userBase.users[GlobalVariables.usernameId].achivements[ach]){
                current++;
            }
        }
        return current;
    }

    // Serializa la estructura de datos y la convierte en un archivo json
    public static void saveData(){
        string jsonData = JsonUtility.ToJson (userBase, true);
        File.WriteAllText(path, jsonData);
        //File.WriteAllText(path2, jsonData);
    }

    
}