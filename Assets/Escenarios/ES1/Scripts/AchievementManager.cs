using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AchievementManager : MonoBehaviour
{
    public const int NUMCASES = 10;

    public const int  NUMACHIVEMENTS = NUMCASES + 2; //TODO: Change to dinamic value

    public GameObject AchievementPrefab;
    public GameObject AchievementPrefabLose;

    public GameObject AchivementDescription;

    public class Achievement
    {
        //TODO: Agregar la capacidad de cambiar el sprite del achivement
        public Achievement()
        {
            Title = "";
            Description = "";
            DescriptionMala = "";
            Achieved = false;
        }

        public Achievement(string title, string description, string descripcionMala, bool achived)
        {
            Title = title;
            Description = description;
            DescriptionMala = descripcionMala;
            Achieved = achived;
        }

        public string Title { get; }
        public string Description { get; }
        public string DescriptionMala { get; }
        public bool Achieved;

    }

    //Todos los achievements son declarados aquí
    public static Achievement[] achievementsArr = new Achievement[NUMACHIVEMENTS]
    {
    new Achievement("Veo mis logros, con otros ojos.","Por revisar tu progreso. ¡El primer paso siempre cuenta!", "", true),
    new Achievement("Yo mero, con el rodillo.", "Por pasar el caso, \"¡Avería en el Rodillo!\" con 5 estrellas. \n ¡El primer paso siembre cuenta!", "Termina el caso \"¡Avería en el Rodillo!\" con 5 estrellas.", false),
    new Achievement("El acoplo que no acopla.", "Por pasar el caso, \"¡Avería en Acoplamiento!\" con 5 estrellas.", "Termina el caso \"¡Avería en Acoplamiento!\" con 5 estrellas.", false),
    new Achievement("¿Hace calor aquí?", "Por pasar el caso, \"El Sobrecalentamiento en Bomba.\" con 5 estrellas.", "Termina el caso \"El Sobrecalentamiento en Bomba.\" con 5 estrellas.", false),
    new Achievement("El sensor de proximidad se demora.", "Por pasar el caso, \"El Sensor de Proximidad genera demoras.\" con 5 estrellas.", "Termina el caso \"El Sensor de Proximidad genera demoras.\" con 5 estrellas.", false),
    new Achievement("¡Relajaté, motor!", "Por pasar el caso, \"¡Sobrecarga de Motor hace demoras!\" con 5 estrellas.", "Termina el caso \"¡Sobrecarga de Motor hace demoras!\" con 5 estrellas.", false),
    new Achievement("No mucho aceite.", "Por pasar el caso, \"¡Avería: Nivel de Aceite!\" con 5 estrellas.", "Termina el caso \"¡Avería: Nivel de Aceite!\" con 5 estrellas.", false),
    new Achievement("El PM10", "Por pasar el caso, \"Correctivo de emergencia: PM10\" con 5 estrellas.", "Termina el caso \"Correctivo de emergencia: PM10\" con 5 estrellas.", false),
    new Achievement("El PM11", "Por pasar el caso, \"Correctivo programado PM11, Inspector\" con 5 estrellas.", "Termina el caso \"Correctivo programado PM11, Inspector\" con 5 estrellas.", false),
    new Achievement("Un Aviso M3", "Por pasar el caso, \"Aviso M3, Inspector\" con 5 estrellas.", "Termina el caso \"Aviso M3, Inspector\" con 5 estrellas.", false),
    new Achievement("Un Aviso M6.", "Por pasar el caso, \"Aviso M3, Inspector\" con 5 estrellas.", "Termina el caso \"Aviso M6, Inspector\" con 5 estrellas.", false),
    new Achievement("Un gran entrenamiento.", "¡Ahora puedes llamarte capacitado! ¡Gracias por jugar!", "Termina TODO caso con 5 estrellas. ¿Podrás hacerlo?", false),
    };

    // Start is called before the first frame update
    void Start()
    {
        bool temp = true;

        CreateAchivement("Achivement Container", achievementsArr[0].Title, achievementsArr[0].Description, achievementsArr[0].DescriptionMala, temp);

        for (int i = 0; i < NUMCASES; i++)
        {
            achievementsArr[i+1].Achieved = Database.getAchivement(i);
            CreateAchivement("Achivement Container", achievementsArr[i+1].Title, achievementsArr[i+1].Description, achievementsArr[i+1].DescriptionMala, achievementsArr[i+1].Achieved);
            temp = temp && achievementsArr[i+1].Achieved;
        }

        CreateAchivement("Achivement Container", achievementsArr[NUMACHIVEMENTS-1].Title, achievementsArr[NUMACHIVEMENTS-1].Description, achievementsArr[NUMACHIVEMENTS-1].DescriptionMala, temp);

    }

    // Update is called once per frame
    void Update()
    {
        SetDescription();
    }

    public void CreateAchivement(string category, string title, string description, string descripcionMala, bool achived)
    {

        GameObject achivement;
        //Render either a resolved or not resolved (black Achivement)
        if (achived)
        {
           achivement = (GameObject)Instantiate(AchievementPrefab);
        } else {
           achivement = (GameObject)Instantiate(AchievementPrefabLose);
        }


        SetAchivementInfo(category, achivement, title, description, descripcionMala, achived);
    }

    public void SetAchivementInfo(string category, GameObject achivement, string title, string description, string descripcionMala, bool achieved)
    {
        achivement.transform.SetParent(GameObject.Find(category).transform);
        //Transformation values chose by hand / experimentation
        achivement.transform.localScale = new Vector3( (float)0.73, (float)1.006147, 1);
        achivement.transform.GetChild(1).GetComponent<Text>().text = title;
        if (achieved)
        {
            achivement.transform.GetChild(2).GetComponent<Text>().text = description;
        } else
        {
            achivement.transform.GetChild(2).GetComponent<Text>().text = descripcionMala;
        }
        
    }

    public static void GainAchievement(int AchievementIndex)
    {
        GameMind.setAchivement(AchievementIndex);
    }

    public void SetDescription()
    {
        AchivementDescription.transform.GetChild(0).GetComponent<Text>().text = AchivementFunction.Description;
    }



    public void GoToMainMenu()
    {
        GlobalVariables.lives = 5;
        // TODO : Cuando tengamos la escena del menu
        GlobalVariables.score = 0;
        GlobalVariables.pairAnswerSlot.Clear();
        GlobalVariables.items.Clear();
        SceneManager.LoadScene("Menu"); //TODO: Add an option to go back
    }

}
