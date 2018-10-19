using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CharacterSelector.Scripts
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
    {//Clase bastante util que sirve en la seleccion de personaje para asegurar
        //que solamenrte existe una copia de la instancia en la pantalla
        //y se mantenga viva la instancia de CharacterManager
        static T instance;

        bool persist = false;

        public static T Instance
        {
            get
            {

                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;

                    if (instance == null)
                    {
                        return null;
                    }
                    instance.Init();
                }
                return instance;
            }
        }

        public bool Persist
        {
            get { return persist; }
            protected set { persist = value; }
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                instance.Init();
                if (persist)
                    DontDestroyOnLoad(gameObject);
            }
        }

        virtual protected void Init() { }

        void OnApplicationQuit()
        {
            instance = null;
        }
    }
}
