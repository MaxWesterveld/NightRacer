using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIupdater : MonoBehaviour
{
    private RaceUI race;
    private CarScript car;
    private Checkpoint cp;

    [SerializeField] private string lapOneTime;
    [SerializeField] private string lapTwoTime;
    [SerializeField] private string lapThreeTime;

    [SerializeField] private GameObject[] checkpoints;

    private void Awake()
    {
        race = FindObjectOfType<RaceUI>();
        car = FindObjectOfType<CarScript>();
        cp = FindObjectOfType<Checkpoint>();
        race.Load();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checkpoints
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.gameObject.SetActive(false);
            race.currentCheckpoint++;
        }

        //Finish
        if (other.gameObject.CompareTag("Finish"))
        {
            float currentTime = race.time;
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            float ms = (currentTime % 1) * 100;

            if (race.currentLap == 1)
            {
                lapOneTime = "Lap 1: " + string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, ms);
            }
            else if (race.currentLap == 2)
            {
                lapTwoTime = "Lap 2: " + string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, ms);
            }
            else if (race.currentLap == 3)
            {
                lapThreeTime = "Lap 3: " + string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, ms);
            }

            if (race.currentLap != race.maxLap)
            {
                cp.PlaceCheckpoints();
                race.currentLap++;
                race.currentCheckpoint = 0;
            }

            if (race.currentLap == race.maxLap)
            {
                if (race.currentCheckpoint == race.maxCheckPoint)
                {
                    GameManager.instance.lapsScreen.SetActive(true);
                    GameManager.instance.lapsScreen.transform.Find("Lap1").GetComponent<TextMeshProUGUI>().text = lapOneTime;
                    GameManager.instance.lapsScreen.transform.Find("Lap2").GetComponent<TextMeshProUGUI>().text = lapTwoTime;
                    GameManager.instance.lapsScreen.transform.Find("Lap3").GetComponent<TextMeshProUGUI>().text = lapThreeTime;
                    Time.timeScale = 0f;
                }
            }

            if (race.time != 0)
            {
                race.bestTime.Add(race.time);
                if (other.gameObject.CompareTag("Finish"))
                {
                    race.time = 0;
                }
            }

            if (race.time != 0)
            {
                race.bestTime.Add(race.time);
            }

            if (race.bestTime.Count > 0)
            {
                race.bestTime.Sort();
            }

            if (race.currentLap > 1)
            {
                float bestTime = race.bestTime[0];

                float minuteS = Mathf.FloorToInt(bestTime / 60);
                float secondS = Mathf.FloorToInt(bestTime % 60);
                float mS = (bestTime % 1) * 100;

                race.bestTimeText.text = "Best: " + string.Format("{0:00}:{1:00}.{2:00}", minuteS, secondS, mS);
            }
        }
    }
}
