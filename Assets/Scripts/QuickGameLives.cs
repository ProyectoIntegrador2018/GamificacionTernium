using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameLives : MonoBehaviour
{
    public static int maxLives = 3;
    int currentLives;

    public Button btn;

    void toggleBtn(bool toggle) {
        if (currentLives == 0) {
            btn.interactable = toggle;
        }
    }

    void setCurrentLivesSprites() {
        for(int i = maxLives; i > currentLives; i--) {
            this.transform.GetChild(i - 1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    void regenerateLive() {
        toggleBtn(true);
        this.transform.GetChild(currentLives).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        currentLives++;
        Database.setCurrentLives(GlobalVariables.usernameId, currentLives);
        Database.removeTimeLastLiveLost(GlobalVariables.usernameId, 1);
        Database.setFirstTime(GlobalVariables.usernameId);
        GameMind.saveData();
        GlobalVariables.currentQuickGameLives = currentLives;
    }

    void OnTimeframeReached() {
        regenerateLive();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLives = GlobalVariables.currentQuickGameLives;
        toggleBtn(false);
        setCurrentLivesSprites();
        QuickGameEventSystem.current.onTimeframeReached += OnTimeframeReached;
    }

    private void Update() {
        toggleBtn(false);
    }

    private void OnDestroy() {
        QuickGameEventSystem.current.onTimeframeReached -= OnTimeframeReached;
    }
}
