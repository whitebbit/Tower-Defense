  

using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using YG;

public class RandomSpell : MonoBehaviour
{
    [SerializeField] private Animator randomSpellPopup;
    [SerializeField] private GameObject adsImage;
    [SerializeField] private Image progress;
    [SerializeField] private GameObject[] spellImages;
    [SerializeField] private int[] spellID;
    private SpellsWrapper spellsWrapper;
    private int progressCounter;
    private bool isActive;
    private int _randomSpell;
    private float _spinTimer;
    private MAXReward _maxReward;

    // Start is called before the first frame update
    void Start()
    {
        spellsWrapper = FindObjectOfType<SpellsWrapper>();
        _maxReward = FindObjectOfType<MAXReward>();
    }

    public void AdsUpdateSpell()
    {
        ClosePopup();
        progressCounter = 250;
        RandomSpellProgess();
    }

    public void RandomSpellProgess()
    {
        if (isActive) return;

        progressCounter++;

        if (progressCounter >= 250)
        {
            isActive = true;
            StartCoroutine(ActivatorSpell());
        }

        progress.fillAmount = (float)progressCounter / 250;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (_spinTimer <= .3f)
            {
                _randomSpell = Random.Range(0, spellID.Length);
                _spinTimer = 0;
                SpinImage(_randomSpell);
            }
            else
                _spinTimer -= Time.deltaTime;
        }
    }

    IEnumerator ActivatorSpell()
    {
        yield return new WaitForSeconds(3f);
        progressCounter = 0;
        isActive = false;
        SpinImage(_randomSpell);
        spellsWrapper.AdsSpells(spellID[_randomSpell]);
        yield return new WaitForSeconds(3f);
        SpinImage(7);
    }

    private void SpinImage(int _id)
    {
        for (int i = 0; i < spellImages.Length; i++)
            spellImages[i].SetActive(false);

        spellImages[_id].SetActive(true);
    }

    public void OpenPopup()
    {
        CheckAdsImage();
        randomSpellPopup.SetTrigger("show");
    }

    public void AdsUseRandomSpell()
    {
        if (YandexGame.HasObject("freeRandomSpell"))
        {
            _maxReward.AdsGetReward(33);
        }
        else
        {
            AdsUpdateSpell();
            YandexGame.SetInt("freeRandomSpell", 1);
        }
    }

    public void ClosePopup()
    {
        randomSpellPopup.SetTrigger("hide");
    }

    private void CheckAdsImage()
    {
        adsImage.SetActive(YandexGame.HasObject("freeRandomSpell"));
    }

}
