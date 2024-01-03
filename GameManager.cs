using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton
{
    public class Singleton <T>:MonoBehaviour where T: Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if(_instance == null ) 
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        public virtual void Awake ()
        {
            if(_instance == null ) {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public class GameManager : Singleton<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;
        void Start()
        {
            // TODO:
            // -�÷��̾� ���̺� �ε�
            // -���̺갡 ������ �÷��̾ ��� ������ �����̷����Ѵ�.
            // -�鿣�带 ȣ���ϰ� ���� ç������ ������ ��´�.

            _sessionStartTime = DateTime.Now;
            Debug.Log("Game session start @:" + DateTime.Now);
        }

        void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            TimeSpan timeDifference = _sessionEndTime.Subtract( _sessionStartTime);

            Debug.Log("Game session end @:" + DateTime.Now);
            Debug.Log("Game session lasted @:" + timeDifference);
        }

        void OnGUI()
        {
            if(GUILayout.Button("Next Scene"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

}




