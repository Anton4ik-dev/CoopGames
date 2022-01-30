using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimersCodes : MonoBehaviour
{
    [SerializeField] private float timeForMinerals;
    [SerializeField] private int probesCnt;
    [SerializeField] private int mineralsPerCicle;
    [SerializeField] private float timeForProbes;
    [SerializeField] private float timeForZealot;
    [SerializeField] private int costOfProbe;
    [SerializeField] private int costOfZealot;

    public Image mineralTimer;
    public Text mineralText;
    public Image probeTimer;
    public Text probeText;
    public Image zealotTimer;
    public Text zealotText;

    private float currentTimerForMinerals;
    private int cntMinerals;
    private float currentTimerForProbes;
    private float currentTimerForZealots;
    private int cntZealots;
    void Start()
    {
        currentTimerForMinerals = timeForMinerals;
        currentTimerForProbes = timeForProbes;
        currentTimerForZealots = timeForZealot;
        probeText.text = $"{probesCnt}";
    }

    void Update()
    {
        currentTimerForMinerals -= Time.deltaTime;
        mineralTimer.fillAmount = 1 - (currentTimerForMinerals / timeForMinerals);

        if (mineralTimer.fillAmount == 1)
        {
            mineralTimer.fillAmount = 0;
            cntMinerals += probesCnt * mineralsPerCicle;
            currentTimerForMinerals = timeForMinerals;
            mineralText.text = $"{cntMinerals}";
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