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
    [SerializeField] private int howMuchZealotEat;
    [SerializeField] private int probesCnt;
    [SerializeField] private int mineralsPerCicle;
    [SerializeField] private int costOfProbe;
    [SerializeField] private int costOfZealot;
    [SerializeField] private int howManyZergsWillCome;
    [SerializeField] private int howZergsWillBeIncreased;

    public Image mineralTimer;
    public Text mineralText;
    public Image feedTimer;
    public Image probeTimer;
    public Text probeText;
    public Image zealotTimer;
    public Text zealotText;
    public Image zergsTimer;
    public Text zergsText;

    private float currentTimerForMinerals;
    private float currentTimerForFeeding;
    private int cntMinerals;
    private float currentTimerForProbes;
    private float currentTimerForZealots;
    private int cntZealots;
    private float currentTimerForZergs;
    private int cntZergs;
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
                currentTimerForProbes = timeForProbes;
                probeText.text = $"{probesCnt}";
                probeTimer.gameObject.SetActive(false);
            }
        }
        // таймер найма зилотов
        if (zealotTimer.IsActive())
        {
            currentTimerForZealots -= Time.deltaTime;
            zealotTimer.fillAmount = 1 - (currentTimerForZealots / timeForZealot);
            if (zealotTimer.fillAmount == 1)
            {
                zealotTimer.fillAmount = 0;
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
                }
                else if (cntZealots == 0 && probesCnt > 0)
                {
                    probesCnt -= 3;
                }
                else if (cntZealots == 0 && probesCnt <= 0)
                {
                    probesCnt = 0;
                    Time.timeScale = 0;
                    Debug.Log("Ты умер долбаёб");
                    break;
                }
            }
            zealotText.text = $"{cntZealots}";
            probeText.text = $"{probesCnt}";
            cntZergs += howZergsWillBeIncreased;
            currentTimerForZergs = timeForZergs;
            zergsText.text = $"{cntZergs}";
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
        }
    }
}