using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceUI : MonoBehaviour
{
    private CarScript car;
    private Checkpoint cp;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] public TextMeshProUGUI bestTimeText;
    [SerializeField] public TextMeshProUGUI lapsText;
    [SerializeField] private TextMeshProUGUI speedOmeterText;
    [SerializeField] private TextMeshProUGUI checkpointText;

    [Header("Timers")]
    [SerializeField] public float time;
    public List<float> bestTime = new List<float>();

    [Header("Laps")]
    [SerializeField] public float currentLap;
    [SerializeField] public float maxLap;

    [SerializeField] public float currentCheckpoint;
    [SerializeField] public float maxCheckPoint;

    [Header("Countdown")]
    [SerializeField] private Image greenSprite;
    [SerializeField] private Image orangeSprite;
    [SerializeField] private Image redSprite;

    private void Start()
    {
        car = FindObjectOfType<CarScript>();
        cp = FindObjectOfType<Checkpoint>();

        greenSprite.enabled = false;
        orangeSprite.enabled = false;
        redSprite.enabled = false;

        maxCheckPoint = cp.checkpoints.Length;


        if (bestTime.Count != 0)
        {
            float best_Time = bestTime[0];

            float minutes = Mathf.FloorToInt(best_Time / 60);
            float seconds = Mathf.FloorToInt(best_Time % 60);
            float ms = (best_Time % 1) * 100;
            bestTimeText.text = "Best: " + string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, ms);
        }
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        if (currentLap > 0)
        {
            time += Time.deltaTime;
            UpdateTime();
        }

        UpdateLaps();
        UpdateCheckpoints();
        UpdateSpeedOMeter();
        Save();
    }

    private void UpdateTime()
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float ms = (time % 1) * 100;


        timeText.text = "Time: " + string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, ms);
    }
    private void UpdateLaps()
    {
        lapsText.text = "Lap " + string.Format("{0} / {1}", currentLap, maxLap);
    }

    private void UpdateCheckpoints()
    {
        checkpointText.text = "Checkpoint " + string.Format("{0} / {1}", currentCheckpoint, maxCheckPoint);
    }

    private void UpdateSpeedOMeter()
    {
        speedOmeterText.text = Mathf.Abs(car.UISpeed).ToString("F0");
    }


    public void Save()
    {
        if (bestTime.Count > 0)
        {
            bestTime.Sort();
            PlayerPrefs.SetFloat("BestTime", bestTime[0]);
            PlayerPrefs.Save();
        }
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime.Add(PlayerPrefs.GetFloat("BestTime"));
        }
    }


    private IEnumerator Countdown()
    {
        yield return new WaitForSecondsRealtime(1);
        greenSprite.enabled = true;
        yield return new WaitForSecondsRealtime(1);
        orangeSprite.enabled = true;
        yield return new WaitForSecondsRealtime(1);
        redSprite.enabled = true;
        yield return new WaitForSecondsRealtime(Random.Range(1, 3));
        greenSprite.enabled = false;
        orangeSprite.enabled = false;
        redSprite.enabled = false;
        car.canRace = true;
    }
}
