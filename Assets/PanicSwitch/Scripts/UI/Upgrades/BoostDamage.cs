  

using TMPro;
using UnityEngine;
using YG;

public class BoostDamage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _DamageDisplayText;
    public static int damage;
    

    private void Start()
    {
        if (YandexGame.HasObject("boostDamage"))
        {
            damage = YandexGame.GetInt("boostDamage");
        }
        else
        {
            damage = 1;
            YandexGame.SetInt("boostDamage", damage);
        }
        UpdateText();
    }


    public void Upgrade()
    {
        damage+=10;
        YandexGame.SetInt("boostDamage", damage);
        UpdateText();
    }

    private void UpdateText()
    {
        _DamageDisplayText.text = damage.ToString();
    }
}
