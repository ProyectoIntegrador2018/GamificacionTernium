using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// [System.Serializable]
// public class Niveles
// {

// }

[System.Serializable]
public class User
{
    public int id;
    public string username;
    public string password;
    public bool tutorial;
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


public class Database : MonoBehaviour
{
    public TextAsset jsonFile;
    public static Users userBase;
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
        if (File.Exists(path)) {
            var myTextAsset = File.ReadAllText(Application.persistentDataPath + "/database.json"); 
            //Debug.Log("hola" + myTextAsset);
            userBase = JsonUtility.FromJson<Users>(myTextAsset);
        }
        else
        {
            userBase = JsonUtility.FromJson<Users>(jsonFile.text);
        }
    }

    public static int login(string username, string pass) {
        // Debug.Log("aver si encontramos algo " + userBase.users[1].username);
        foreach (User user in userBase.users) {
            if(user.username == username && user.password == pass) {
                // Debug.Log("aver si encontramos algo ");
                return user.id;
            }
        }
        return -1;
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

    public static bool getTutorial() {
        return userBase.users[GlobalVariables.usernameId].tutorial;
    }

    public static void setTutorial() {
        userBase.users[GlobalVariables.usernameId].tutorial = false;
    }
    
    public static void makeUser(string name, string password) {
        foreach (User user in userBase.users) {
            if(user.username == name) {
                Debug.Log("Este usuario ya existe en la base de datos");
                return;
            }
        }
        createUser(name, password);
        Debug.Log("Usuario creado y guardado correctamente");

    }

    public static void createUser(string name, string password) {
        User nUser = new User();
        nUser.id = userBase.users[userBase.users.Length - 1].id + 1;
        nUser.username = name;
        nUser.password = password;
        nUser.tutorial = true;
        int[] niv = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        nUser.niveles = niv;
        bool[] ach = {false, false, false, false, false, false, false, false, false, false, false };
        nUser.achivements = ach;
        nUser.started = ach;
        userBase.Push(nUser);
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

    public static void saveData(){
        string jsonData = JsonUtility.ToJson (userBase, true);
        File.WriteAllText(path,jsonData);
        //File.WriteAllText(path2, jsonData);
    }
    
}
