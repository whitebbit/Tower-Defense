  

using TMPro;
using UnityEngine.UI;
using UnityEngine;
using YG;

public class IncomeMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyForKillDisplayText;
    [SerializeField] private TextMeshProUGUI[] priceDisplayText;
    [SerializeField] private GameObject adsBlockImage, priceWrapper;
    [SerializeField] private Sprite[] buttonSprite;
    [SerializeField] private Image button; 
    public static float MoneyForKill;
    private Wallet _wallet;
    private int _level;

    private void Awake()
    {
        if (YandexGame.HasObject("MoneyForKill"))
        {
            MoneyForKill = YandexGame.GetFloat("MoneyForKill");
        }
        else
        {
            MoneyForKill = .4f;
        }
        _level = YandexGame.GetInt("level_goldPerKill");
        _wallet = FindObjectOfType<Wallet>();


        CheckAdsImage();

        _wallet.MoneyChange.AddListener((level) =>
        {
            CheckAdsImage();
        });

        UpdateText();
    }

    public void Upgrade()
    {
        MoneyForKill += .3f;
        YandexGame.SetFloat("MoneyForKill", MoneyForKill);
        _level++;
        YandexGame.SetInt("level_goldPerKill", _level);
        UpdateText();
    }

    private void UpdateText()
    {
        var text = YandexGame.lang == "ru" ? " зол./уб." : " g/kill";

        _moneyForKillDisplayText.text = MoneyForKill.ToString("f1") + text;
        priceDisplayText[0].text = GetCurrentPrice().ToString();
        priceDisplayText[1].text = GetCurrentPrice().ToString();
    }

    private void CheckAdsImage()
    {
        if (_wallet.GetMoney() >= GetCurrentPrice())
        {
            adsBlockImage.SetActive(false);
            priceWrapper.SetActive(true);
            button.sprite = buttonSprite[0];
        }
        else
        {
            adsBlockImage.SetActive(true);
            priceWrapper.SetActive(false);
            button.sprite = buttonSprite[1];
        }
    }

    private int GetCurrentPrice()
    {
        return 250 * (_level + 1);
    }
}
