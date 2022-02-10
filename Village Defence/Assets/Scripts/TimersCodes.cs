using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimersCodes : MonoBehaviour
{
    [SerializeField] private float timeForMinerals;
    [SerializeField] private float timeForFeeding;
    [SerializeField] private float timeForProbes;
    [SerializeField] private float timeForZealot;
    [SerializeField] private float timeForZergs;
    [Space]
    [SerializeField] private int howMuchZealotEat;
    [SerializeField] private int probesCnt;
    [SerializeField] private int mineralsPerCicle;
    [SerializeField] private int costOfProbe;
    [SerializeField] private int costOfZealot;
    [SerializeField] private int howManyZergsWillCome;
    [SerializeField] private int howZergsWillBeIncreased;
    [SerializeField] private int howManyProbesForWin;
    [SerializeField] private int howManyMineralsForWin;
    [SerializeField] private int howManyMineralsForGradeTime;
    [SerializeField] private int howManyMineralsForGradeDmg;
    [Space]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject losScreen;

    public Image mineralTimer;
    public Text mineralText;
    public Image feedTimer;
    public Image probeTimer;
    public Text probeText;
    public Image zealotTimer;
    public Text zealotText;
    public Image zergsTimer;
    public Text zergsText;
    public GameObject grade1icon;
    public GameObject grade2icon;
    [Space]
    public Text zealotStatTxtLos;
    public Text probeStatTxtLos;
    public Text wavesStatTxtLos;
    public Text zealotStatTxtWin;
    public Text probeStatTxtWin;
    public Text wavesStatTxtWin;

    private float currentTimerForMinerals;
    private float currentTimerForFeeding;
    private int cntMinerals;
    private float currentTimerForProbes;
    private float currentTimerForZealots;
    private int cntZealots;
    private float currentTimerForZergs;
    private int cntZergs;
    private int zealotStat;
    private int probeStat;
    private int waveStat;
    private float timeScaler = 1;
    private bool dmgIncreased = false;
    void Start()
    {
        Time.timeScale = 0f;
        currentTimerForMinerals = timeForMinerals;
        currentTimerForProbes = timeForProbes;
        currentTimerForZealots = timeForZealot;
        currentTimerForFeeding = timeForFeeding;
        currentTimerForZergs = timeForZergs;
        zergsText.text = $"{howManyZergsWillCome}";
        cntZergs = howManyZergsWillCome;
        probeText.text = $"{probesCnt}";
    }

    void Update()
    {
        // таймер минералов
        currentTimerForMinerals -= Time.deltaTime;
        mineralTimer.fillAmount = 1 - (currentTimerForMinerals / timeForMinerals);

        if (mineralTimer.fillAmount == 1)
        {
            mineralTimer.fillAmount = 0;
            cntMinerals += probesCnt * mineralsPerCicle;
            currentTimerForMinerals = timeForMinerals;
            mineralText.text = $"{cntMinerals}";
        }
        // таймер кормления
        if(cntZealots > 0)
        {
            feedTimer.gameObject.SetActive(true);
            currentTimerForFeeding -= Time.deltaTime;
            feedTimer.fillAmount = 1 - (currentTimerForFeeding / timeForFeeding);
            if (feedTimer.fillAmount == 1)
            {
                feedTimer.fillAmount = 0;
                for (int i = 0; i < cntZealots; i++)
                {
                    cntMinerals -= howMuchZealotEat;
                    if(cntMinerals < 0)
                    {
                        cntZealots -= 1;
                        cntMinerals = 0;
                    }
                }
                currentTimerForFeeding = timeForFeeding;
                zealotText.text = $"{cntZealots}";
                mineralText.text = $"{cntMinerals}";
            }
        } else if(cntZealots == 0)
        {
            feedTimer.gameObject.SetActive(false);
            feedTimer.fillAmount = 0;
            currentTimerForFeeding = timeForFeeding;
        }
        // таймер найма пробок
        if(probeTimer.IsActive())
        {
            currentTimerForProbes -= Time.deltaTime;
            probeTimer.fillAmount = 1 - (currentTimerForProbes / timeForProbes);
            if(probeTimer.fillAmount == 1)
            {
                probeTimer.fillAmount = 0;
                probesCnt += 1;
                probeStat++;
                currentTimerForProbes = timeForProbes;
                probeText.text = $"{probesCnt}";
                probeTimer.gameObject.SetActive(false);
            }
        }
        // таймер найма зилотов
        if (zealotTimer.IsActive())
        {
            currentTimerForZealots -= Time.deltaTime * timeScaler;
            zealotTimer.fillAmount = 1 - (currentTimerForZealots / timeForZealot);
            if (zealotTimer.fillAmount == 1)
            {
                zealotTimer.fillAmount = 0;
                zealotStat++;
                cntZealots += 1;
                currentTimerForZealots = timeForZealot;
                zealotText.text = $"{cntZealots}";
                zealotTimer.gameObject.SetActive(false);
            }
        }
        // таймер зергов
        currentTimerForZergs -= Time.deltaTime;
        zergsTimer.fillAmount = 1 - (currentTimerForZergs / timeForZergs);
        if (zergsTimer.fillAmount == 1)
        {
            zergsTimer.fillAmount = 0;
            for (int i = cntZergs; i > 0; i--)
            {
                if (cntZealots > 0)
                {
                    cntZealots -= 1;
                    if(dmgIncreased == true)
                    {
                        i--;
                    }
                }
                else if (cntZealots == 0 && probesCnt > 0)
                {
                    probesCnt -= 3;
                }
                else if (cntZealots == 0 && probesCnt <= 0)
                {
                    probesCnt = 0;
                    Time.timeScale = 0;
                    losScreen.SetActive(true);
                    zealotStatTxtLos.text = $"You created {zealotStat} zealots";
                    probeStatTxtLos.text = $"You created {probeStat} probes";
                    wavesStatTxtLos.text = $"You survived {waveStat} waves";
                    break;
                }
            }
            waveStat++;
            zealotText.text = $"{cntZealots}";
            probeText.text = $"{probesCnt}";
            cntZergs += howZergsWillBeIncreased;
            currentTimerForZergs = timeForZergs;
            zergsText.text = $"{cntZergs}";
        }

        if(howManyMineralsForWin <= cntMinerals && howManyProbesForWin <= probesCnt)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
            zealotStatTxtWin.text = $"You created {zealotStat} zealots";
            probeStatTxtWin.text = $"You created {probeStat} probes";
            wavesStatTxtWin.text = $"You survived {waveStat} waves";
        }
    }

    public void GetProbe()
    {
        if(costOfProbe <= cntMinerals && probeTimer.IsActive() == false)
        {
            probeTimer.gameObject.SetActive(true);
            cntMinerals -= costOfProbe;
            mineralText.text = $"{cntMinerals}";
        }
    }
    public void GetZealot()
    {
        if (costOfZealot <= cntMinerals && zealotTimer.IsActive() == false)
        {
            zealotTimer.gameObject.SetActive(true);
            cntMinerals -= costOfZealot;
            mineralText.text = $"{cntMinerals}";
            Debug.Log("Блять");
        }
    }

    public void GetGrade1()
    {
        if(cntMinerals >= howManyMineralsForGradeTime)
        {
            grade1icon.gameObject.SetActive(false);
            cntMinerals -= howManyMineralsForGradeTime;
            mineralText.text = $"{cntMinerals}";
            timeScaler = 4;
        }
    }
    public void GetGrade2()
    {
        if (cntMinerals >= howManyMineralsForGradeDmg)
        {
            grade2icon.gameObject.SetActive(false);
            cntMinerals -= howManyMineralsForGradeDmg;
            mineralText.text = $"{cntMinerals}";
            dmgIncreased = true;
        }
    }
}