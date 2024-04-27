  

using UnityEngine;
using UnityEngine.Events;
using YG;

public class PlayerData : MonoBehaviour
{
    public DataEvent LevelChange;
    private int level;
    private int recordLevel;

    private void Awake()
    {
        if (LevelChange == null)
            LevelChange = new DataEvent();

       // level = PlayerPrefs.GetInt("level");
        recordLevel = YandexGame.GetInt("recordLevel");

        if (YandexGame.HasObject("tempLevel"))
        {
            level = YandexGame.GetInt("tempLevel");
            YandexGame.DeleteObject("tempLevel");
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetRecordLevel()
    {
        return recordLevel;

    }
    public void SetLevel()
    {
        level++;
        LevelChange.Invoke(level);
       // PlayerPrefs.SetInt("level", level);

        if(level> recordLevel)
        {
            SetRecordLevel();
        }
    }

    private void SetRecordLevel()
    {
        recordLevel = level;
        YandexGame.SetInt("recordLevel", recordLevel);
    }
}
