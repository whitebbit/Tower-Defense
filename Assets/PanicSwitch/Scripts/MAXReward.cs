using System;
using TMPro;
using UnityEngine;
using YG;

public class MAXReward : MonoBehaviour
{
#if UNITY_IOS
     string rewardedAdUnitId = "6153b41caf275f07"; // IOS
#else
    string rewardedAdUnitId = "d54086bf10c91a64"; // Android
#endif


    int retryAttempt;
    private int _idRewardButton;

    private int _internetConnection = 1;
    [SerializeField] private AdsButton _boosts;
    private SpellsWrapper _spellWrapper;
    private Rampage _rampage;
    private QuestManager _questManager;
    private EarnBuild _earnOffline;
    [SerializeField] private ButtonInfo[] _buttonInfo;
    [SerializeField] private TowerWrapper _towerWrapper;
    [SerializeField] private WarriorsCoreUpgrade[] _warriorsUpgrades;
    [SerializeField] private UpgradeDetails[] _upgradesDetails;
    [SerializeField] private TextMeshProUGUI _rewardMoneyDisplayText;
    [SerializeField] private AddWarriors _addWarriors;
    [SerializeField] private RandomSpell _randomSpell;
    private GameUI _gameUI;
    private Wallet _wallet;
    private static int rewardMoneyCounter;
    [HideInInspector] public int RandomChestOfGold;

    private void Awake()
    {
        SetRandomMoney();
    }

    private void Start()
    {
        _wallet = FindObjectOfType<Wallet>();
        _spellWrapper = FindObjectOfType<SpellsWrapper>();
        _rampage = FindObjectOfType<Rampage>();
        _questManager = FindObjectOfType<QuestManager>();
        _earnOffline = FindObjectOfType<EarnBuild>();
        _gameUI = FindObjectOfType<GameUI>();
    }


    private void SetRandomMoney()
    {
        RandomChestOfGold = UnityEngine.Random.Range(800, 2500);
    }


    private void OnDisable()
    {
    }


    public void AdsGetReward(int id)
    {
        _idRewardButton = id;
        YandexGame.RewardVideoEvent += OnRewardedAdReceivedRewardEvent;
        YandexGame.RewVideoShow(id);
    }

    private void OnRewardedAdReceivedRewardEvent(int adUnitId)
    {
        switch (adUnitId)
        {
            //Boost Timer X2
            case 1:
                _boosts.UseAdsSpell(0);
                break;

            //Boost Gold X3
            case 2:
                _boosts.UseAdsSpell(1);
                break;

            // 1K gold
            case 3:
                _wallet.SetMoney(1000 * (rewardMoneyCounter + 1));

                if (rewardMoneyCounter < 10)
                    rewardMoneyCounter++;

                _rewardMoneyDisplayText.text = "+" + (rewardMoneyCounter + 1) + "K";
                break;

            // 10 gems
            case 4:
                _wallet.SetGems(10);
                break;

            case 5:
                _spellWrapper.AdsSpells(0);
                break;

            case 6:
                _buttonInfo[0].AdsButtonChecker(false);
                _spellWrapper.AdsSpells(1);
                break;

            case 7:
                _spellWrapper.AdsSpells(2);
                break;

            case 8:
                _spellWrapper.AdsSpells(4);
                break;

            case 9:
                _spellWrapper.AdsSpells(6);
                break;

            case 10:
                _buttonInfo[1].AdsButtonChecker(false);
                _spellWrapper.AdsSpells(7);
                break;

            case 11:
                _spellWrapper.AdsSpells(8);
                break;

            case 12:
                _rampage.AdsRampage(0);
                break;

            case 13:
                _rampage.AdsRampage(1);
                break;

            case 14:
                //_gameUI.AddGameCounter(-1);
                _questManager.AdsPlayerContinue();
                break;

            case 15:
                _warriorsUpgrades[0].UseUpgrade();
                break;

            case 16:
                _warriorsUpgrades[1].UseUpgrade();
                break;

            case 17:
                _upgradesDetails[0].UseUpgrade();
                break;

            case 18:
                _upgradesDetails[0].AdsUpgradeProcess();
                break;

            case 19:
                _upgradesDetails[1].UseUpgrade();
                break;

            case 20:
                _upgradesDetails[1].AdsUpgradeProcess();
                break;

            case 21:
                _upgradesDetails[2].UseUpgrade();
                break;

            case 22:
                _upgradesDetails[2].AdsUpgradeProcess();
                break;

            case 23:
                _upgradesDetails[3].UseUpgrade();
                break;

            case 24:
                _upgradesDetails[3].AdsUpgradeProcess();
                break;

            case 25:
                _upgradesDetails[4].UseUpgrade();
                break;

            case 26:
                _upgradesDetails[4].AdsUpgradeProcess();
                break;

            case 27:
                _earnOffline.AdsCollectX2();
                break;

            case 28:
                _towerWrapper.UseTower(0);
                break;

            case 29:
                _towerWrapper.UseTower(1);
                break;

            case 30:
                _towerWrapper.UseTower(2);
                break;

            case 31:
                _addWarriors.AdsAddUnity();
                break;

            case 32:
                _wallet.SetMoney(RandomChestOfGold);
                SetRandomMoney();
                break;

            case 33:
                _randomSpell.AdsUpdateSpell();
                break;

            default:
                break;
        }

        _idRewardButton = 0;
        YandexGame.RewardVideoEvent -= OnRewardedAdReceivedRewardEvent;
    }
}