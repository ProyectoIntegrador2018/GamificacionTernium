using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Clase para manejar los botones en P2 y P6 - escenas que tienen funcionalidad de drag and drop
public class BtnMangment : MonoBehaviour
{
    // Variables publicas
    public Button BotonRevisar;
    public string SigEscena;
    public Button continueButton;
    Transform canvasPosition;
    public Text DialogueText;
    GameObject[] ListaDItems;
    float[] PosX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] PosY = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int cuantosItems;
    string Item;
    string ItemBox;
    string ItemSlot;
    static int CH;

    public GameObject item_1;
    public GameObject item_2;
    public GameObject item_3;
    public GameObject item_4;

    private AudioSource soundEffect;
    public AudioClip correctAnwser;
    public AudioClip wrongAnswer;

    private void Awake()
    {
        ContarItems();
        FindItems();
        Shuffle();
    }

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        soundEffect.volume = Database.getVolumenSonidos(GlobalVariables.usernameId);
    }
    public bool giveAnswer()
    {
        if ((item_1.transform.localPosition.y == 111f) &&
            (item_2.transform.localPosition.y == 211f) &&
            (item_3.transform.localPosition.y == -371f))
            return true;

        else
            Debug.Log("Nel");
                return false;
        
    }
        
       

       
    
    private void OnEnable()
    {
        BotonRevisar.onClick.AddListener(delegate 
        {
            /*
            // Si la escena en juego es la P2
            if (SceneManager.GetActiveScene().name == "P2" || SceneManager.GetActiveScene().name == "ES4P2") {
                if (DragDrop.statusAnswer() == "Correct") {
                DialogueText.text = "Correcto! El guardia ahora tiene su equipo de seguridad puesto.";
                // Suma puntos
                GameMind.addPoints(100);

                    //StartCoroutine(WaitSeconds(5));
                    // * ChangeCurrentScene()  Cambio aqui;


                showContinueButton();


                }
                else if (DragDrop.statusAnswer() == "Incorrect") {
                    DialogueText.text = "Incorrecto! El guardia debe tener puesto su casco de seguridad con barbiquejo, lentes de seguridad, guantes combinados de carnaza y botines de seguridad con casquillo.";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);

                    // **   Solution(); cambio aqui
                    //StartCoroutine(WaitSeconds(5));
                    //*cambio aqui   ChangeCurrentScene();
                }
            }*/



            // Si la escena en juego es la P2
            if (SceneManager.GetActiveScene().name == "P2" || SceneManager.GetActiveScene().name == "ES4P2")
            {
                if (giveAnswer())
                {
                    displayCorrectAnswerAudio();
                    DialogueText.text = Database.getTextoCorrecto(0, 1);
                    Debug.Log("correcto");
                 
                    // Suma puntos
                    GameMind.addPoints(Database.getPuntosPregunta(0, 1));
                    //StartCoroutine(WaitSeconds(5));
                    //ChangeCurrentScene();

                    Solution();

                    showContinueButton();


                }
                else if (!giveAnswer())
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(0, 1)[0];
                    Debug.Log("incorrecto");
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(0, 1)[0]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();
                    //StartCoroutine(WaitSeconds(10));
                    //ChangeCurrentScene();
                }
            }





            // Si la escena en juego es la P6
            if (SceneManager.GetActiveScene().name == "P6") {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 7) {
                    displayCorrectAnswerAudio();
                DialogueText.text = Database.getTextoCorrecto(0, 5);
                    // Suma puntos
                    GameMind.addPoints(Database.getPuntosPregunta(0, 5));
                showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 7) {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(0, 5)[0];
                    // Quita vida y suma puntos
                   
                    GameMind.takeAwayLive(Database.getVidasPerdidas(0, 5)[0]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 7) {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(0, 5)[1];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(0, 5)[1]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 7) {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(0, 5)[2];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(0, 5)[1]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();
                }
            }
            // Si la escena en juego es la ES2P3
            if (SceneManager.GetActiveScene().name == "ES2P3")
            {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    displayCorrectAnswerAudio();
                    DialogueText.text = Database.getTextoCorrecto(1, 2);
                    // Suma puntos
                    GameMind.addPoints(Database.getPuntosPregunta(1, 2));
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = DialogueText.text = Database.getFalloTexto(1, 2)[0];
                    Debug.Log(GlobalVariables.pairAnswerSlot.Count);
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(1, 2)[0]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = DialogueText.text = Database.getFalloTexto(1, 2)[1];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(1, 2)[1]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = DialogueText.text = Database.getFalloTexto(1, 2)[2];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(1, 2)[2]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }

            }
            // Si la escena en juego es la ES2P5
            if (SceneManager.GetActiveScene().name == "ES2P5")
            {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    displayCorrectAnswerAudio();
                    DialogueText.text = Database.getTextoCorrecto(1, 4);
                    // Suma puntos
                    GameMind.addPoints(Database.getPuntosPregunta(1, 4));
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(1, 4)[0];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(1, 4)[0]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(1, 4)[1];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(1, 4)[1]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(1, 4)[2];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(1, 4)[2]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }

            }

             if (SceneManager.GetActiveScene().name == "ES3P3.1")
            {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    displayCorrectAnswerAudio();
                    DialogueText.text = Database.getTextoCorrecto(2, 2);
                    // Suma puntos
                    GameMind.addPoints(Database.getPuntosPregunta(2, 2));

                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect")
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(2, 2)[0];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(2, 2)[0]);
                   // GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(2, 2)[1];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(2, 2)[1]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }

            }

            // INICIO - PARTE DE FABIANA
            // Si la escena en juego es la ES4P4
            if (SceneManager.GetActiveScene().name == "ES4P4") {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    displayCorrectAnswerAudio();
                    DialogueText.text = Database.getTextoCorrecto(3, 3);
                    // Suma puntos
                    GameMind.addPoints(Database.getPuntosPregunta(3, 3));
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(3, 3)[0];
                    Debug.Log(GlobalVariables.pairAnswerSlot.Count);
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(3, 3)[0]);
                   // GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();


                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(3, 3)[1];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(3, 3)[1]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(3, 3)[2];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(3, 3)[2]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }

            }

            // Si la escena en juego es la ES4P5
            if (SceneManager.GetActiveScene().name == "ES4P5") {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    displayCorrectAnswerAudio();
                    DialogueText.text = Database.getTextoCorrecto(3, 4);
                    // Suma puntos
                    GameMind.addPoints(Database.getPuntosPregunta(3, 4));
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(3, 4)[0];
                    Debug.Log(GlobalVariables.pairAnswerSlot.Count);
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(3, 4)[0]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(3, 4)[1];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(3, 4)[1]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    displayWrongAnswerAudio();
                    DialogueText.text = Database.getFalloTexto(3, 4)[2];
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(Database.getVidasPerdidas(3, 4)[2]);
                    //GameMind.addPoints(-100);
                    Solution();
                    showContinueButton();

                }

            }
            // FIN - PARTE DE FABIANA
        });
    }

    // Funcion para hacer visible el boton para cambiar a la siguiente pregunta
    void showContinueButton(){
        GameObject castedBtn = BotonRevisar.gameObject;
        castedBtn.SetActive(false);
        continueButton.onClick.AddListener(ChangeCurrentScene);
    }
    
    void ChangeCurrentScene() {
        // Si las vidas es 0 o menos se cargara la escena de perder, sino la siguiente escena
        if (GlobalVariables.lives <= 0) {
            string Escena = SceneManager.GetActiveScene().name;

            if (Escena.Substring(0, 1) == "P") {
                SceneManager.LoadScene("Lose");
            }
            else {
                SceneManager.LoadScene(Escena.Substring(0, 3) + "Lose");
            }

            //SceneManager.LoadScene("Lose");
        }
        else {
            SceneManager.LoadScene(SigEscena);
        }
    }


    // Coroutine donde se espera 5 segundos para que el usuario pueda leer el feedback
    /*
    IEnumerator WaitSeconds(int seconds) {
        Boton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(seconds);
        // Si las vidas es 0 o menos se cargara la escena de perder, sino la siguiente escena
        if (GlobalVariables.lives <= 0)
        {
            string Escena = SceneManager.GetActiveScene().name;

            if (Escena.Substring(0, 1) == "P")
            {
                SceneManager.LoadScene("Lose");
            }
            else
            {
                SceneManager.LoadScene(Escena.Substring(0, 3) + "Lose");
            }

            //SceneManager.LoadScene("Lose");
        } else {
            SceneManager.LoadScene(SigEscena);
        }
    }*/
    

   


    //Ricky
    //Cambiar dew posicion las cosas
    public void Shuffle()
    {
        //CH = cuantosItems;
        int topRange = cuantosItems + 1;
        int Rand = Random.Range(1, topRange);
        for (int i = 1; i <= cuantosItems; i++)
        {
            while (PosX[Rand] == 0)
            {
                Rand = Random.Range(1, topRange);
            }
            ItemBox = "ItemBox" + i;
            GameObject Dummy = GameObject.Find(ItemBox);
            Dummy.transform.localPosition = new Vector2(PosX[Rand], PosY[Rand]);
            PosX[Rand] = 0;
            PosY[Rand] = 0;
        }
    }

    public void FindItems()
    { 
        for (int i = 1; i <= cuantosItems; i++)
        {
            ItemBox = "ItemBox" + i;
            GameObject Dummy = GameObject.Find(ItemBox) ;
            PosX[i] = Dummy.transform.localPosition.x;
            PosY[i] = Dummy.transform.localPosition.y;
        }
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


    public void Solution()
    {

        string Escena = SceneManager.GetActiveScene().name;

        if (Escena == "P2" || Escena == "ES4P2")
        {
            GameObject Slot1 = GameObject.Find("ItemSlot1");
            GameObject Slot2 = GameObject.Find("ItemSlot2");
            GameObject Slot3 = GameObject.Find("ItemSlot3");
            GameObject Slot4 = GameObject.Find("ItemSlot4");
            //Lentes
            GameObject Item1 = GameObject.Find("Item1");
            //Casco
            GameObject Item2 = GameObject.Find("Item2");
            //Boots
            GameObject Item3 = GameObject.Find("Item3");
            //Guantes
            GameObject Item4 = GameObject.Find("Item4");

            Item1.transform.localPosition = new Vector2(Slot2.transform.localPosition.x, Slot2.transform.localPosition.y);
            Item2.transform.localPosition = new Vector2(Slot1.transform.localPosition.x, Slot1.transform.localPosition.y);
            Item3.transform.localPosition = new Vector2(Slot4.transform.localPosition.x, Slot4.transform.localPosition.y);
            Item4.transform.localPosition = new Vector2(Slot3.transform.localPosition.x, Slot3.transform.localPosition.y);

        }
        else
        {
            for (int i = 1; i <= cuantosItems; i++)
            {
                //La cosa
                ItemBox = "ItemBox" + i;
                // Donde va
                ItemSlot = "ItemSlot" + i;

                GameObject Box = GameObject.Find(ItemBox);
                GameObject Slot = GameObject.Find(ItemSlot);

                if (Slot != null)
                {
                    Box.transform.localPosition = new Vector2(Slot.transform.localPosition.x, Slot.transform.localPosition.y);
                }
                else
                {
                    Box.transform.localPosition = new Vector2(-1000, -1000);
                }
            }
        }
    }




    public void ContarItems()
    {
        int i = 1;

        ItemBox = "ItemBox" + i;
        GameObject Box = GameObject.Find(ItemBox);

        while (Box != null)
        {
            i++;
            ItemBox = "ItemBox" + i;
            Box = GameObject.Find(ItemBox);
        }
        cuantosItems = i-1;
        CH = cuantosItems;
        //Debug.Log(cuantosItems);
    }

    public static void Help()
    {
        string Escena2 = SceneManager.GetActiveScene().name;

        if (Escena2 == "P2" || Escena2 == "ES4P2")
        {
            GameObject Slot = GameObject.Find("ItemSlot");
            //Lentes
            GameObject Item1 = GameObject.Find("Item1");
            //Casco
            GameObject Item2 = GameObject.Find("Item2");
            //Boots
            GameObject Item3 = GameObject.Find("Item3");
            //Guantes
            GameObject Item4 = GameObject.Find("Item4");

            if(Item1.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item1.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
            else if(Item2.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item2.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
            else if (Item3.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item3.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
            else if (Item4.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item4.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
  

        }
        else if(Escena2 == "P6")
        {
            for (int i = 1; i <= CH; i++)
            {
                //La cosa
                string ItemBox2 = "ItemBox" + i;
                // Donde va
                string ItemSlot2 = "ItemSlot" + i;

                GameObject Box = GameObject.Find(ItemBox2);
                GameObject Slot = GameObject.Find(ItemSlot2);

                if (Box.transform.localPosition.x != Slot.transform.localPosition.x)
                {
                    Box.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
                    Slot.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
                    i = CH;
                }
            }
        }
        else
        {
            for (int i = 1; i <= CH; i++)
            {
                //La cosa
                string ItemBox2 = "ItemBox" + i;
                // Donde va
                string ItemSlot2 = "ItemSlot" + i;

                GameObject Box = GameObject.Find(ItemBox2);
                GameObject Slot = GameObject.Find(ItemSlot2);

                if (Slot == null)
                {
                    Box.SetActive(false);
                    i = CH;
                }
            }
        }
    }

}


