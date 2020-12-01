using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq; // Muy util esta libreria

// Clase para crear los objetos
public class Team{
    public string name {get; set;}
    public int score {get; set;}
}

// Clase para hacer una lista para decidir cual es el mejor equipo
// Uno de los PBI es que se muestre toda la lista de las cuadrillas de mayor a menor, como un ranking
// Este codigo podria funcionar, solo faltaria hacer la UI y mostrar la lista ya ordenada
// Por ahora solo se hace un query para sacar la mejor, la mas alta, la mas grande
// Creada por el equipo 2

public class Top3 : MonoBehaviour
{
    // Variable para obtener la lista de usuarios
    static private User[] users;
    //Necesita refactor, o podria hacerse de otra forma en lugar de poner imagenes individuales, o cargarlas en un array, no se
    //public Image teamImage;
    //public Sprite greenTeam;
    //public Sprite pinkTeam;
    //public Sprite blueTeam;

    public TMP_Text teamText;
    // Lista de la clase de arriba
    List<Team> teamList = new List<Team>();

    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/

    // Funcion para crear una lista
    public void getScores(){
        // Lista auxiliar de los usuarios, se obtiene en array y se transforma en List
        List<User> aux = users.ToList();
        // Agrupar por el field "equipo"
        var groupedResult = from s in aux
                            group s by s.equipo;

        // Iterar en cada lista para sumar las puntuaciones de cada una
        foreach (var teamGroup in groupedResult)
        {
            var currGroup = teamGroup.Key; //Cada grupo tiene una key
            var currSum = 0; // Variable para el acumulado
            foreach(User u in teamGroup){ // Cada grupo tiene una lista de usuarios
                currSum += u.niveles.Sum(); // Sumar el array de puntuaciones y agregarlo al acumulado del equipo
            }
            // Hacer otra lista con el nombre del equipo y el acumulado obtenido
            teamList.Add(new Team {name = currGroup, score= currSum});
        }
        // Para debuggear la lista si esta mostrando cosas raras
        /*foreach(Team t in teamList){
            Debug.Log(t.name + " : " + t.score);
        }*/
    }

    // Funcion para mostrar el mejor equipo

    public void showScores(){
        
        // Construir la lista de equipos con puntuaciones
        getScores();

        // Decidir cual es el score mas alto
        int maxScore = teamList.Max(s => s.score);

        // Query para encontrar el nombre del score mas alto
        // Creo que hay otra forma de hacerlo, no estoy seguro
        var best = from s in teamList
            where s.score == maxScore
            select s;
        
        // Mostrar el mejor equipo en el txtField deseado
        foreach(var scr in best){
            teamText.text = scr.name;
        }
          
    }

    


    // Start is called before the first frame update
    void Start()
    {
        // Obtener todos los usuarios no admin
        users = Database.GetNonAdminUsers();
        // Correr la funcion para realizar todo el procedimiento
        showScores();
    }
    


}
