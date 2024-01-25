using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private RaceUI race;

    [SerializeField] public GameObject[] checkpoints;

    private void Start()
    {
        race = FindObjectOfType<RaceUI>();
    }

    public void PlaceCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].SetActive(true);
        }
    }

}
