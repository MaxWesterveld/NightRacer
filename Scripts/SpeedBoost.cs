using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [Tooltip("The power of the speedboost")]
    [SerializeField] private float speedPower = 100f;

    /// <summary>
    /// If the other collider the player is, then give the player a speedboost
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<CarScript>().currentSpeed = speedPower;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        var player = other.GetComponent<NewNewCarScript>();
    //        var playerRb = other.GetComponent<Rigidbody>();
    //        if (/*player.moveControl > 0*/ playerRb.velocity.magnitude > 0)
    //        {
    //            other.GetComponent<NewNewCarScript>().CurrentSpeed = speedPower;
    //        }
    //        else if (/*player.moveControl < 0 &&*/ playerRb.velocity.magnitude < 0)
    //        {
    //            other.GetComponent<NewNewCarScript>().CurrentSpeed = -speedPower;
    //        }
    //        //StartCoroutine("Boost", other.GetComponent<NewNewCarScript>());
    //    }
    //}

    //private IEnumerator Boost(NewNewCarScript player)
    //{
    //    var firstSpeed = player.CurrentSpeed;
    //    player.CurrentSpeed = speedPower;
    //    yield return new WaitForSeconds(1f);
    //    player.CurrentSpeed = firstSpeed;
    //    yield return null;
    //}
}
