using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameLives : MonoBehaviour
{
    public static int maxLives = 3;
    int currentLives;

    public Button btn;

    //la funcion se encarga de deshabilitar el boton que se le asigna al sript en caso de que no haya vidas
    void toggleBtn(bool toggle) {
        if (currentLives == 0) {
            btn.interactable = toggle;
        }
    }

    //pone todas las sprites de vidas de manera correcta
    void restartLives() {
        for (int i = 0; i < maxLives; i++) {
            this.transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    //se encarga de modificar las sprites de vidas para representar el valor actual
    void setCurrentLivesSprites() {
        for(int i = maxLives; i > currentLives; i--) {
            this.transform.GetChild(i - 1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    //se encarga de regenerar una vida
    void regenerateLive() {
        toggleBtn(true);
        this.transform.GetChild(currentLives).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        currentLives++;
        Database.setCurrentLives(GlobalVariables.usernameId, currentLives);
        Database.removeTimeLastLiveLost(GlobalVariables.usernameId, 1);
        if (currentLives != maxLives) {
            Database.setFirstTime(GlobalVariables.usernameId);
        }
        GameMind.saveData();
        GlobalVariables.currentQuickGameLives = currentLives;
    }

    void OnTimeframeReached() {
        regenerateLive();
    }

    //se encarga de revisar si se puede regenerar una vida al reactivar el objeto
    void OnEnableCheck() {
        restartLives();
        currentLives = GlobalVariables.currentQuickGameLives;
        if(currentLives == 0) {
            btn.interactable = false;
        }
        else {
            btn.interactable = true;
        }
        setCurrentLivesSprites();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLives = GlobalVariables.currentQuickGameLives;
        toggleBtn(false);
        setCurrentLivesSprites();
        QuickGameEventSystem.current.onTimeframeReached += OnTimeframeReached;
        QuickGameEventSystem.current.onEnableCheck += OnEnableCheck;
    }

    private void Update() {
        toggleBtn(false);
    }

    private void OnDestroy() {
        QuickGameEventSystem.current.onTimeframeReached -= OnTimeframeReached;
        QuickGameEventSystem.current.onEnableCheck -= OnEnableCheck;
    }
}
