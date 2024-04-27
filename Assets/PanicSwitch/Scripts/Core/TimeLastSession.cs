  

using System;
using UnityEngine;
using YG;

public class TimeLastSession : MonoBehaviour
{
   public static TimeSpan ts;

    private void Awake()
    {
        CheckTime();
    }    
    
    void CheckTime()
    {
        if (YandexGame.HasObject("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(YandexGame.GetString("LastSession"));            
        }
    }

    private void OnApplicationQuit()
    {
        YandexGame.SetString("LastSession", DateTime.Now.ToString());
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            YandexGame.SetString("LastSession", DateTime.Now.ToString());
        }
        
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            YandexGame.SetString("LastSession", DateTime.Now.ToString());
        }
    }

    public void SaveTime()
    {
        YandexGame.SetString("LastSession", DateTime.Now.ToString());
    }
}
