using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FirebaseWebGL.Examples.Database
{
    public class newsCreator : MonoBehaviour
    {
        public InputField titleInput;
        public InputField descInput;
        public GameObject errorObject;
        string dateString;

        // Start is called before the first frame update
        void Start()
        {
            dateString = DateTime.Now.ToString("g");
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                    Debug.Log("The code is not running on a WebGL build; as such, the Javascript functions will not be recognized.");
        }

        public void pushEvent() => FirebaseDatabase.PushEvent(titleInput.text, descInput.text, dateString, gameObject.name, 
        "created", "failed");

        public void created(){
            titleInput.text = "";
            descInput.text = "";
        }

        public void failed(){
            errorObject.SetActive(true);
            StartCoroutine(RemoveAfterSeconds(5, errorObject));
        }

        IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
        {
                yield return new WaitForSeconds(seconds);
                obj.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void returnToNewsScene(){
            SceneManager.LoadScene("News");
        }
    }
}
