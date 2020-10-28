using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Created by: Hamza Herbou        (mobile games developer)
// email     : hamza95herbou@gmail.com

public class Toast : MonoBehaviour {
	float _counter = 0f;
	float _duration;
	bool _isToasting = false;
	bool _isToastShown = false;

	//public GameObject toastObject;
	public static Toast Instance;
	public TextMeshProUGUI toastText;
	[SerializeField] Animator anim = null;

	public enum ToastColor{Dark,Red,Green,Blue,Magenta,Pink}

	void Awake () {Instance = this;}

	void Start () {
		
	}

	void Update(){
		if (_isToasting){
			if (!_isToastShown){
				toastShow ();
				_isToastShown = true;
			}
			_counter += Time.deltaTime;
			if(_counter>=_duration){
				_counter = 0f;
				_isToasting = false;
				toastHide ();
				_isToastShown = false;
			}
		}
	}


	/// <summary>
	/// make an empty toast with text: "Hello ;)"
	/// </summary>
	public void Show(){
		toastText.text = "Hello ;)";
		_duration = 1f;
		_counter = 0f;
		if (!_isToasting) _isToasting = true;
	}

	/// <summary>
	/// make a toast with a message.
	/// </summary>
	/// <param name="text">(string), toast message.</param>
	public void Show(string text){
		toastText.text = text;
		_duration = 1f;
		_counter = 0f;
		if (!_isToasting) _isToasting = true;
	}

	/// <summary>
	/// make a toast with a message & duration.
	/// </summary>
	/// <param name="text">(string), toast message.</param>
	/// <param name="duration">(float), toast duration in seconds.</param>
	public void Show(string text, float duration){
		toastText.text = text;
		_duration = duration;
		_counter = 0f;
		if (!_isToasting) _isToasting = true;
	}

	public void goToNews(){
		SceneManager.LoadScene("News");
	}


	//show/hide Toast
	void toastShow(){ 
		anim.SetBool ("isToastUp",true);
	}
	void toastHide(){
		anim.SetBool ("isToastUp",false);
	}
}
