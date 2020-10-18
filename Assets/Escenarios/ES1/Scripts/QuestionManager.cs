using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

// Clase para el manejo de preguntas tipo de decision
public class QuestionManager : MonoBehaviour {

    // Para manipular el Texto que se despliega
    public Text CanvasText;

    // Para manipular los botones
    public Button Btn1;
    public Button Btn2;
    public Button Btn3;
    Transform canvasPosition;
    public Button continueButton;
    private AudioSource soundEffect;
    public AudioClip correctAnwser;
    public AudioClip wrongAnswer;
    public string Escena;

    //public int SegundosEspera;

    // Las opciones de los botones, esto es para control mas abajo
    bool Opt1;
    bool Opt2;
    bool Opt3;

    // Vidas que se van a quitar dependiendo del error
    public int vidasFallo1;
    public int vidasFallo2;

    // Esto es para que podamos definir a que pregunta brincamos, gracias al index, solo tenemos que ponerlas en el orden correcto
    // Esta es la escena que sigue, mas abajo se define cual sera
    string SigEscena;

    // Este es el camino correcto
    private string SigEscenaCorrecto;

    // Este es el camino por Arriba, checar Moqup
    private string SigEscenaError1;

    // Este es el camino por Abajo, checar Moqup
    private string SigEscenaError2;

    // Esto es para cargar la pregunta correcta
    //public int PreguntaActual;

    // Esta es clase Preguntas, esta aqui porque importarla tiene que ver con Visual Studio, entonces para facilitarlo la puse aqui 
    public class Preguntas {

        // Constructor that takes no arguments:
        public Preguntas() {
            Pregunta = "Aqui va la pregunta";
            Opc1 = "a";
            Opc2 = "";
            Vidas1 = 0;
            Vidas2 = 0;
            Opc3 = "";
            Fail1 = "";
            Fail2 = "";
            Correct = 0;
            Points = 0;


        }

    
        // Constructor with arguments
        public Preguntas(string pregunta, string opc1, string opc2, int vidas1, int vidas2, string opc3, string fail1, string fail2, int correct, int points) {
            Pregunta = pregunta;
            Opc1 = opc1;
            Opc2 = opc2;
            Vidas1 = vidas1;
            Vidas2 = vidas2;
            Opc3 = opc3;
            Fail1 = fail1;
            Fail2 = fail2;
            Correct = correct;
            Points = points;
        }

        // Auto-implemented readonly property:
        public string Pregunta { get; }
        public string Opc1 { get; }
        public string Opc2 { get; }
        public int Vidas1 { get; }
        public int Vidas2 { get; }
        public string Opc3 { get; }
        public string Fail1 { get; }
        public string Fail2 { get; }
        public int Correct { get; }
        public int Points { get; }
    }

    // Los estados del juego, esto lo aprendi del video que estaba en la carta
    private enum States {
        Questions, trueState, falseState, falseState2
    };
    
    //Template
    //Preguntas tempD3 = new Preguntas("Pre", "Opc1", "Opc2", 0, 0, "Correcta", "Fail1", "Fail2", 3, 0);
    //Preguntas tempD2 = new Preguntas("P1", "None", "Opc1", 0, 0, "Correcta", "None", "Fail1", 2, 0);
    // Pregunta actual
    Preguntas QA = new Preguntas();

    // Pregunta Error
    Preguntas QE = new Preguntas("ERROR", "ERROR", "ERROR", 0, 0, "ERROR", "ERROR", "ERROR", 3, 0);

    // El estado actual
    private States myState;
  
    // Start is called before the first frame update
    void Start() {
        // Cargar el estado principal

        myState = States.Questions;

        Escena = SceneManager.GetActiveScene().name;

        //Debug.Log(GlobalVariables.Caso);

        soundEffect = GetComponent<AudioSource>();

        int questionIndex;
        int missionIndex = 0;
        string[] escenasError;

        if (Escena.Substring(0, 1) != "P") {

            if (Escena.Substring(2, 2) == "10") {
                questionIndex = Int32.Parse(Escena.Substring(5)) - 1;
                missionIndex = 9;
            }
            else {
                questionIndex = Int32.Parse(Escena.Substring(4)) - 1;
                missionIndex = Int32.Parse(Escena.Substring(2, 1)) - 1;
            }
        }
        else {
            if (Escena.Substring(1) == "6.5") {
                questionIndex = 6;
            }
            else if (Int32.Parse(Escena.Substring(1)) > 6) {
                questionIndex = Int32.Parse(Escena.Substring(1));
            }
            else {
                questionIndex = Int32.Parse(Escena.Substring(1)) - 1;
            }
        }

        escenasError = Database.getSigEscenasError(missionIndex, questionIndex);
        SigEscenaCorrecto = Database.getSigEscenaCorrecto(missionIndex, questionIndex);
        SigEscenaError1 = escenasError[0];
        SigEscenaError2 = escenasError[1];

        string[] opciones = Database.getOpcionesPregunta(missionIndex, questionIndex);
        int[] vidas = Database.getVidasPerdidas(missionIndex, questionIndex);
        string[] falloTexto = Database.getFalloTexto(missionIndex, questionIndex);

        QA = new Preguntas(
            Database.getPregunta(missionIndex, questionIndex),
            opciones[0],
            opciones[1],
            vidas[0],
            vidas[1],
            opciones[2],
            falloTexto[0],
            falloTexto[1],
            Database.getOpcionCorrecta(missionIndex, questionIndex),
            Database.getPuntosPregunta(missionIndex, questionIndex)
        );

        /*
        print(QA.Pregunta);
        print(QA.Opc1);
        print(QA.Opc2);
        print(QA.Vidas1);
        print(QA.Vidas2);
        print(QA.Opc3);
        print(QA.Fail1);
        print(QA.Fail2);
        print(QA.Correct);
        print(QA.Points);
        */

        // Revolver los cuadros de respuesta
        Revolver(QA.Correct); 
    }

    // Update is called once per frame
    void Update() {
        // Como siempre esta corriendo esto, hay que mantenerlo simple, asi que solo cambiara states
        if (myState == States.Questions){ Question(); }
        /*
        else if (myState == States.trueState){ trueState(); }
        else if (myState == States.falseState){ falseState(); }
        else if (myState == States.falseState2) { falseState2(); }
        */
    }

    // Cuando el objeto donde esta el script, es "enabled" comienza a checar esto
    private void OnEnable() {
        // El checar que los botones sean presionados, y que pasa si lo son
        Btn1.onClick.AddListener(delegate {Opt1 = true; Question(); showContinueButton(); });
        Btn2.onClick.AddListener(delegate {Opt2 = true; Question(); showContinueButton(); });
        Btn3.onClick.AddListener(delegate {Opt3 = true; Question(); showContinueButton(); });

    }

    // Desplegar la pregunta actual
    void Question() {
        //Cambiar el texto de la pregunta
        CanvasText.text = QA.Pregunta;

        Btn1.GetComponentInChildren<Text>().text = QA.Opc1;
        Btn2.GetComponentInChildren<Text>().text = QA.Opc2;
        Btn3.GetComponentInChildren<Text>().text = QA.Opc3;

        //Se agrego que se corriera la funcion que lee el estado actual para determinar la siguiente escena porque de lo contrario no se podria cambiar de escena instantaneamente
        if (Opt1 == true) { 
            GameMind.takeAwayLive(QA.Vidas1);
            //GameMind.addPoints(-QA.Points);
            myState = States.falseState;
            falseState();
        } else if (Opt2 == true){
            GameMind.takeAwayLive(QA.Vidas2);
            //GameMind.addPoints(-QA.Points);
            myState = States.falseState2;
            falseState2();
        } else if (Opt3 == true){
            GameMind.addPoints(QA.Points);
            myState = States.trueState;
            trueState();
        }
    }

    // El estado cuando le atina
    void trueState() {
        CanvasText.text = "Correcto!";
        displayCorrectAnswerAudio();
        SigEscena = SigEscenaCorrecto;  
    }

    // El estado de error 1
    void falseState() {
        CanvasText.text = "Incorrecto! " +'\n'+ QA.Fail1;
        displayWrongAnswerAudio();
        SigEscena = SigEscenaError1;
    }

    // El estado de error 2
    void falseState2() {
        CanvasText.text = "Incorrecto! " + '\n' + QA.Fail2;
        displayWrongAnswerAudio();
        SigEscena = SigEscenaError2;
    }


    // Funcion para hacer visible el boton para cambiar a la siguiente pregunta
    void showContinueButton(){
        Btn1.interactable = false;
        Btn2.interactable = false;
        Btn3.interactable = false;
        canvasPosition = GetComponentInParent<Canvas>().transform;
        Button newButton = Instantiate(continueButton, new Vector3(375,300,0), transform.rotation);
        newButton.transform.SetParent(canvasPosition);
        newButton.onClick.AddListener(ChangeCurrentScene);
    }

    //Funcion para sonar audio de respuestas correctas
    void displayCorrectAnswerAudio()
    {
        soundEffect.PlayOneShot(correctAnwser);
    }

    //Funcion para sonar audio de respuestas correctas
    void displayWrongAnswerAudio()
    {
        soundEffect.PlayOneShot(wrongAnswer);
    }


    void ChangeCurrentScene() {
        //Debug.Log("Aprentando boton");
        //print(SigEscena);
        ColorBlock colors = Btn3.colors;
        colors.normalColor = new Color32(0, 179, 81, 255);
        //colors.highlightedColor = new Color32(0, 179, 81, 255);
        Btn3.colors = colors;

        //Btn3.colors.normalColor = new Color(225, 225, 225, 255); ;
        if (GlobalVariables.lives <= 0) {

            //string Escena = SceneManager.GetActiveScene().name;

            if (Escena.Substring(0, 1) == "P") {
                SceneManager.LoadScene("Lose");
            }
            else {
                if (Escena.Substring(2, 2) == "10") {
                    SceneManager.LoadScene(Escena.Substring(0, 4) + "Lose");
                }
                else {
                    SceneManager.LoadScene(Escena.Substring(0, 3) + "Lose");
                }
            }
            //SceneManager.LoadScene("Lose");
        }
        else {
            SceneManager.LoadScene(SigEscena);
        }
    }

    // Dado que seria mucho problema randomizar la respuesta y el texto 
    // de fallo especifico sera mas facil mover las posiciones de los botones
    void Revolver(int PA) {

        int Rand;
        //New Measures
        // x  = 805
        // y1 = 112
        // y2 = -111
        // y3 = -335

        if (PA == 2) {
            Rand = UnityEngine.Random.Range(20, 22);
            Btn1.enabled = false;
            Btn1.gameObject.SetActive(false);
        } else {
            Rand = UnityEngine.Random.Range(1, 7);
        }
        
        switch (Rand) {
            case 1:
                Btn1.transform.localPosition = new Vector2(805, 112);
                Btn2.transform.localPosition = new Vector2(805, -111);
                Btn3.transform.localPosition = new Vector2(805, -335);
                break;

            case 3:
                Btn2.transform.localPosition = new Vector2(805, 112);
                Btn1.transform.localPosition = new Vector2(805, -111);
                Btn3.transform.localPosition = new Vector2(805, -335);
                break;

            case 4:
                Btn2.transform.localPosition = new Vector2(805, 112);
                Btn3.transform.localPosition = new Vector2(805, -111);
                Btn1.transform.localPosition = new Vector2(805, -335);
                break;

            case 5:
                Btn3.transform.localPosition = new Vector2(805, 112);
                Btn1.transform.localPosition = new Vector2(805, -111);
                Btn2.transform.localPosition = new Vector2(805, -335);
                break;

            // Estos son para cuando solo hay 2 respuestas
            case 20:
                Btn3.transform.localPosition = new Vector2(805, 50);
                Btn2.transform.localPosition = new Vector2(805, -170);
                break;

            case 21:
                Btn2.transform.localPosition = new Vector2(805, 50);
                Btn3.transform.localPosition = new Vector2(805, -170);
                break;

            default:
                break;
        }
    }

    public static void Help()
    {
        GameObject.Find("Button-2").SetActive(false);//Button-2
    }
}
