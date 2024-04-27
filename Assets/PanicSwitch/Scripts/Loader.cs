using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace CompleteProject
{
    public class Loader: MonoBehaviour
    {
        private void OnEnable()
        {
            YandexGame.GetDataEvent += Load;
        }
        
        
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Load;
        }
        
        private void Load()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}