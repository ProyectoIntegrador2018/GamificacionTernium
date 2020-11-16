using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{

    private Text playerLevelTxt;
    private int aux;
    private AudioSource levelUpSfx;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInfoEventSystem.current.onExpBarFill += OnExpBarFill;
        PlayerInfoEventSystem.current.onFinishExpGain += OnFinishExpGain;
        playerLevelTxt = transform.GetChild(0).GetComponent<Text>();
        levelUpSfx = GetComponent<AudioSource>();
        levelUpSfx.volume = Database.getVolumenSonidos(GlobalVariables.usernameId);
        playerLevelTxt.text = Database.getNivelJugador().ToString();
        aux = Int32.Parse(playerLevelTxt.text);
    }

    private void IncreaseLevel() {
        aux++;
        playerLevelTxt.text = aux.ToString();
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 0), 0.1f).setOnComplete(() => {
            PlayerInfoEventSystem.current.FinishLevelUpAnimation();
            //LeanTween.move(transform.parent.gameObject.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 1f).setDelay(0.5f).setEase(LeanTweenType.easeInOutBack).setOnComplete(() => {
            //    PlayerInfoEventSystem.current.FinishLevelUpAnimation();
            //});
        });
    }

    private void OnExpBarFill() {
        PlayerInfoEventSystem.current.StartLevelUpAnimation();
        levelUpSfx.Play();
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 0), 0.1f).setOnComplete(IncreaseLevel);
        //LeanTween.move(transform.parent.gameObject.GetComponent<RectTransform>(), new Vector3(109f, 435f, 0), 1f).setEase(LeanTweenType.easeInOutBack).setOnComplete(() => {
        //    LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 0), 0.1f).setOnComplete(IncreaseLevel);
        //});

    }

    private void OnFinishExpGain() {
        Database.setNivelJugador(aux);
        Database.saveData();
        WinCase.enableButtons();
    }

    private void OnDestroy() {
        PlayerInfoEventSystem.current.onExpBarFill -= OnExpBarFill;
        PlayerInfoEventSystem.current.onFinishExpGain -= OnFinishExpGain;
    }
}
