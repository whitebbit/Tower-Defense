  

using TMPro;
using UnityEngine;
using YG;

public class TapDamage : MonoBehaviour
{
    public static int TapDamageValue;
    [SerializeField] private TextMeshProUGUI TapDamageValueDisplayText;

    private void Start()
    {
        if (YandexGame.HasObject("TapDamageValue"))
        {
            TapDamageValue = YandexGame.GetInt("TapDamageValue");
        }
        else
        {
            TapDamageValue = 2;
            YandexGame.SetInt("TapDamageValue", TapDamageValue);
        }
        UpdateText();
    }


    public void Upgrade()
    {
        TapDamageValue +=5;
        YandexGame.SetInt("TapDamageValue", TapDamageValue);
        UpdateText();
    }

    private void UpdateText()
    {
        TapDamageValueDisplayText.text = TapDamageValue.ToString();
    }
}
