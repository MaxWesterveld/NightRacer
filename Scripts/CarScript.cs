using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarScript : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private float accelerationSpeed = 0.25f;
    [SerializeField] private float noInputStoppingSpeed = 1f;
    [SerializeField] private float reverseSpeed = 1f;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float health;
    [SerializeField] private float hitDamage;

    [Header("Information")]
    public float currentSpeed = 0;
    private float steerAmount;
    private float realSpeed;
    public float UISpeed;
    public bool canRace;
    private float moveControl;
    private float steerControl;

    [Header("Tires")]
    public Transform frontLeftTire;
    public Transform frontRightTire;
    public Transform backLeftTire;
    public Transform backRightTire;

    [Header("Gravity")]
    [SerializeField] private float gravityScale = 1f;
    public static float globalGravity = -9.81f;

    private Rigidbody rb;

    /// <summary>
    /// Get RigidBody component
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update the UIspeed and the steer/move controls
    /// </summary>
    private void Update()
    {
        if (canRace)
        {
            UISpeed = realSpeed * 3;
            steerControl = Input.GetAxisRaw("Horizontal");
            moveControl = Input.GetAxisRaw("Vertical");
        }
    }

    /// <summary>
    /// Update the gravity and the other functions
    /// </summary>
    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
        Move();
        TireSteer();
        Steer();
        GroundNormalRotation();
    }

    /// <summary>
    /// Move the vehicle through the moveControl variable, speed and rb velocity
    /// </summary>
    private void Move()
    {
        realSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (moveControl > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, MaxSpeed, accelerationSpeed * Time.deltaTime);
        }
        else if (moveControl < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -MaxSpeed / 1.75f, reverseSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, noInputStoppingSpeed * Time.deltaTime);
        }

        Vector3 vel = transform.forward * currentSpeed;
        vel.y = rb.velocity.y * 0.6f;
        rb.velocity = vel;
    }

    /// <summary>
    /// Steer the vehicle through realSpeed, steerAmount and steerControl
    /// </summary>
    private void Steer()
    {
        Vector3 steerDirVect;
        transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, 0f, 0), 8f * Time.deltaTime);

        if (realSpeed > 0 && realSpeed < 16 || realSpeed < 0 && realSpeed > -16)
        {
            steerAmount = realSpeed / 0.25f * steerControl;
        }
        else if (realSpeed > 15 && realSpeed < 31 || realSpeed < -15 && realSpeed > -31)
        {
            steerAmount = realSpeed / 1f * steerControl;
        }
        else if (realSpeed > 30 && realSpeed < 46 || realSpeed < -30 && realSpeed > -46)
        {
            steerAmount = realSpeed / 1.75f * steerControl;
        }
        else if (realSpeed > 45 || realSpeed < -45)
        {
            steerAmount = realSpeed / 2.5f * steerControl;
        }

        steerDirVect = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirVect, 3 * Time.deltaTime);

    }

    /// <summary>
    /// Makes sure the rotation to the ground is normal
    /// </summary>
    private void GroundNormalRotation()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.75f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.deltaTime);
        }   
    }

    /// <summary>
    /// Update the tired so they turn and spin
    /// </summary>
    private void TireSteer()
    {
        if (steerControl < 0)
        {
            frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
            frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
        }
        else if (steerControl > 0)
        {
            frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 205, 0), 5 * Time.deltaTime);
            frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 205, 0), 5 * Time.deltaTime);
        }
        else
        {
            frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
            frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
        }

        if (currentSpeed > 30)
        {
            frontLeftTire.GetChild(0).Rotate(-90 * Time.deltaTime * currentSpeed * 0.5f, 0, 0);
            frontRightTire.GetChild(0).Rotate(-90 * Time.deltaTime * currentSpeed * 0.5f, 0, 0);
            backLeftTire.Rotate(90 * Time.deltaTime * currentSpeed * 0.5f, 0, 0);
            backRightTire.Rotate(90 * Time.deltaTime * currentSpeed * 0.5f, 0, 0);
        }
        else
        {
            frontLeftTire.GetChild(0).Rotate(-90 * Time.deltaTime * realSpeed * 0.5f, 0, 0);
            frontRightTire.GetChild(0).Rotate(-90 * Time.deltaTime * realSpeed * 0.5f, 0, 0);
            backLeftTire.Rotate(90 * Time.deltaTime * realSpeed * 0.5f, 0, 0);
            backRightTire.Rotate(90 * Time.deltaTime * realSpeed * 0.5f, 0, 0);
        }
    }

    /// <summary>
    /// Lose a life when hitting a ...
    /// </summary>
    /// <param name="collision">The collision of the other object</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.UpdateHealth(-hitDamage);
        }
    }
}