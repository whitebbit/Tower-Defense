using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WarriorsCoreUpgrade : MonoBehaviour
{
    public int id;

    [Header("INFORMATION UI")]
    //[SerializeField] private Image borderColor;
    [SerializeField]
    private GameObject adsBlockImage, priceWrapper;

    [SerializeField] private TextMeshProUGUI[] priceDisplayText;
    [SerializeField] private TextMeshProUGUI levelDisplayText, levelColorText;
    [SerializeField] private Image researchProgressDisplay;
    [SerializeField] private Sprite[] _avatarCycleImages;
    [SerializeField] private ButtonInfo _buttonInfo;

    [SerializeField] private GameObject[] progressObjects;
    [HideInInspector] public int currentLevel;
    [SerializeField] private int counter = 1;
    [SerializeField] private int counterWarriorEnable = 1;
    private int cycleCounter;

    [Header("DATA")] [SerializeField] private warriorLevel[] _warriorLevel;

    //public W_UpgradesDetail[] details;
    [SerializeField] private WarriorAttack_1[] _warriors;
    [SerializeField] private Sprite[] buttonSprite;
    [SerializeField] private Image button;
    private float _timer;
    private int _isprocess;
    private float tempTimer;
    private Wallet _wallet;
    private int _warriorLenght;
    private WindowsUI _windowsUI;
    [SerializeField] private AudioSource _clickAudios;
    private bool isActive;
    private AddWarriors _addWarriors;

    private int GetCurrentPrice()
    {
        return 100 * (currentLevel + 1);
    }

    private void Awake()
    {
        currentLevel = YandexGame.GetInt("currentLevel" + id);
        cycleCounter = YandexGame.GetInt("cycleCounter" + id);
    }

    public void Start()
    {
        _warriorLenght = _warriors.Length;
        _wallet = FindObjectOfType<Wallet>();
        _windowsUI = FindObjectOfType<WindowsUI>();
        _addWarriors = FindObjectOfType<AddWarriors>();

        if (YandexGame.HasObject("counter" + id))
            counter = YandexGame.GetInt("counter" + id);

        if (YandexGame.HasObject("counterWarriorEnable" + id))
            counterWarriorEnable = YandexGame.GetInt("counterWarriorEnable" + id);

        _isprocess = YandexGame.GetInt("_isprocessWarrior" + id);

        CheckAdsImage();

        _wallet.MoneyChange.AddListener((level) => { CheckAdsImage(); });

        CheckWarriors(1);
        CheckProcess(_isprocess);
        UpdateText();
    }

    private void UpdateButtonInfo()
    {
        _buttonInfo.price = GetCurrentPrice();
        _buttonInfo.CheckStatus();
    }


    private void UpdateLevel()
    {
        currentLevel++;
        YandexGame.SetInt("currentLevel" + id, currentLevel);

        foreach (var t in _warriors)
        {
            if (t.groupId == counter)
                t.UpgradeWarriors();

            CheckWarriors(1);
        }

        counter++;

        if (counter > 5)
        {
            cycleCounter++;
            YandexGame.SetInt("cycleCounter" + id, cycleCounter);
            counter = 0;
        }

        YandexGame.SetInt("counter" + id, counter);
    }


    //public void AddUnits()
    //{
    //    if (counterWarriorEnable < 6)
    //    {
    //        for (int i = 0; i < _warriors.Length; i++)
    //        {
    //            if (_warriors[i].groupId == counterWarriorEnable)
    //                _warriors[i].AddUnit();

    //            CheckWarriors(1);
    //        }

    //        counterWarriorEnable++;
    //        PlayerPrefs.SetInt("counterWarriorEnable" + id, counterWarriorEnable);
    //    }
    //}

    public void CheckWarriors(int type)
    {
        for (int i = 0; i < _warriorLenght; i++)
        {
            if (_warriors[i].warriorEnable <= 0) continue;
            var warriorLevel = currentLevel >= _warriorLevel.Length
                ? _warriorLevel[^1]
                : _warriorLevel[currentLevel];
            _warriors[i].CheckStats(warriorLevel.Health, warriorLevel.Attack, warriorLevel.AttackTimer, type);
        }
    }

    public void HealSingleWarrior(bool check)
    {
        for (var i = 0; i < _warriorLenght; i++)
        {
            if (!check) continue;
            if (_warriors[i].warriorLevel <= 0 || !_warriors[i].isdead) continue;
            var warriorLevel = currentLevel >= _warriorLevel.Length
                ? _warriorLevel[^1]
                : _warriorLevel[currentLevel];
            _warriors[i].CheckStats(warriorLevel.Health, warriorLevel.Attack,
                warriorLevel.AttackTimer, 1);
            check = false;
        }
    }

    public void Upgrade()
    {
        if (_wallet.GetMoney() >= GetCurrentPrice())
        {
            _wallet.SetMoney(-GetCurrentPrice());

            UseUpgrade();
            CheckAdsImage();
        }
        else
        {
            _windowsUI.GemShopOpen(0);
        }
    }

    public void UseUpgrade()
    {
        _addWarriors.AddUnit(id);
        UpdateLevel();
        UpdateText();
        _clickAudios.Play();
    }

    private void CheckProcess(int cheker)
    {
        if (cheker > 0)
        {
            progressObjects[0].SetActive(false);
            progressObjects[1].SetActive(true);
        }
        else
        {
            progressObjects[0].SetActive(true);
            progressObjects[1].SetActive(false);
        }
    }

    public void UpdateText()
    {
        priceDisplayText[0].text = GetCurrentPrice().ToString();
        priceDisplayText[1].text = GetCurrentPrice().ToString();
        var text = YandexGame.lang == "ru" ? "Уровень " : "Level ";

        levelDisplayText.text = text + (currentLevel + 1).ToString();

        UpdateButtonInfo();
    }

    public void UseRampage(float cycle)
    {
        //_audiosKlich.Play();

        for (int i = 0; i < _warriorLenght; i++)
        {
            if (_warriors[i].warriorLevel > 0)
                _warriors[i].Rampage(cycle);
        }

        _clickAudios.Play();
    }

    public void StopRampage()
    {
        CheckWarriors(0);
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
}