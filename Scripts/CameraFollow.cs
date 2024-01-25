using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    /// <summary>
    /// Update the translation and rotation of the camera
    /// </summary>
    private void FixedUpdate() 
    {
        HandleTranslation();
        HandleRotation();
    }

    /// <summary>
    /// Handles the translation of the camera,
    /// so how it follows the player 
    /// </summary>
    private void HandleTranslation()
    {
        var targetPos = target.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, translateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Handles the rotation of the camera,
    /// so it turns towards the player while following
    /// </summary>
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}