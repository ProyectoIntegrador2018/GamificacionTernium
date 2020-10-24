using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameLives : MonoBehaviour
{
    int maxLives = 3;
    int currentLives;

    void setCurrentLivesSprites() {
        for(int i = maxLives; i > currentLives; i--) {
            this.transform.GetChild(i - 1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    void regenerateLive() {
        this.transform.GetChild(currentLives).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        currentLives++;
    }

    void OnTimeframeReached() {
        regenerateLive();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLives = 0;
        setCurrentLivesSprites();
        QuickGameEventSystem.current.onTimeframeReached += OnTimeframeReached;
    }

    private void OnDestroy() {
        QuickGameEventSystem.current.onTimeframeReached -= OnTimeframeReached;
    }
}
