using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        Vector3 playerPosition = target.position;
        playerPosition.y = transform.position.y;
        transform.position = playerPosition;
        transform.eulerAngles = new Vector3(90, 0, -target.transform.eulerAngles.y);
    }
}