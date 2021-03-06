using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSolo : MonoBehaviour
{
    [SerializeField] float maxSEVolume = 1f;
    [SerializeField] float thrustSpeed = 1500f, rotateThrust = 100;
    [SerializeField] float thrustSpeedUpFactor = 2f, thrustSppedNormalFactor = 1f;
    [SerializeField] float maxSpeed = 50f, slowDownSpeedFactor = 1.0005f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioSource movementAudioSource;

    [SerializeField] ParticleSystem boostParticles;

    int playerID;
    int seVolume;
    float speedFactor;
    Rigidbody rb;
    AudioSource audioSource;
    PlayerStatusSolo playerStatus;
    bool canControl, canResetRotation;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerStatus = GetComponent<PlayerStatusSolo>();
        Initialization();
    }

    private void Initialization()
    {
        canControl = true;
        canResetRotation = false;
        speedFactor = thrustSppedNormalFactor;
        playerID = GetComponent<PlayerStatusSolo>().GetPlayerID();
        if (PlayerPrefs.HasKey("SEVolume"))
        {
            seVolume = PlayerPrefs.GetInt("SEVolume");
        }
        else
        {
            seVolume = 100;
        }
        movementAudioSource.volume = maxSEVolume * seVolume / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            ProcessThrust();
            ProcessRotation();
            if (canResetRotation)
            {
                ProcessResetRotation();
            }
        }
    }

    private void ProcessThrust()
    {
        switch (ControlType.chosenControlType)
        {
            case "Type1":
                if ((Input.GetMouseButton(0) || Input.GetButton(playerID + "PBoost")) && playerStatus.GetCurrentBoost() > 0)
                {
                    if (Input.GetMouseButton(1))
                    {
                        SlowDownSpeed();
                    }
                    else
                    {
                        StartThursting();
                        LimitMaxmimumSpeed();
                    }
                    playerStatus.ReducePlayerBoost(speedFactor);
                }
                else if (Input.GetMouseButton(1))
                {
                    SlowDownSpeed();
                    playerStatus.ReducePlayerBoost(speedFactor);
                }
                else
                {
                    StopThursting();
                    //playerStatus.RecoverBoost();
                }
                break;
            case "Type2":
                if (Input.GetButton(playerID + "PBoost2") && playerStatus.GetCurrentBoost() > 0)
                {
                    if (Input.GetButton(playerID + "PSlowDown2"))
                    {
                        SlowDownSpeed();
                    }
                    else
                    {
                        StartThursting();
                        LimitMaxmimumSpeed();
                    }
                    playerStatus.ReducePlayerBoost(speedFactor);
                }
                else if (Input.GetButton(playerID + "PSlowDown2"))
                {
                    SlowDownSpeed();
                    playerStatus.ReducePlayerBoost(speedFactor);
                }
                else
                {
                    StopThursting();
                    //playerStatus.RecoverBoost();
                }
                break;
            default:
                break;

        }
        playerStatus.UpdatePlayerBoostSlider();
    }

    private void ProcessResetRotation()
    {
        switch (ControlType.chosenControlType)
        {
            case "Type1":
                if (Input.GetMouseButtonDown(2) || Input.GetButton(playerID + "PResetRotation"))
                {
                    ResetRotation();
                }
                break;
            case "Type2":
                if (Input.GetButton(playerID + "PResetRotation2"))
                {
                    ResetRotation();
                }
                break;
        }
    }

    private void ResetRotation()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rb.velocity = Vector3.zero;
    }

    private void ResponseToBoostUp()
    {
        speedFactor = thrustSpeedUpFactor;
    }

    private void ResetSpeedFactor()
    {
        speedFactor = thrustSppedNormalFactor;
    }

    private void StopThursting()
    {
        rb.velocity = rb.velocity / slowDownSpeedFactor;
        audioSource.Stop();
        if(boostParticles != null && boostParticles.isPlaying)
        {
            boostParticles.Stop();
        }
    }

    private void StartThursting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime * speedFactor);
        if (!audioSource.isPlaying && mainEngine != null)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (boostParticles != null && boostParticles.isStopped)
        {
            boostParticles.Play();
        }
    }
    private void LimitMaxmimumSpeed()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    public float GetLimitedMaxSpeed()
    {
        return maxSpeed;
    }

    private void SlowDownSpeed()
    {
        rb.velocity = rb.velocity / slowDownSpeedFactor;
        if (!audioSource.isPlaying && mainEngine != null)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (boostParticles != null && boostParticles.isStopped)
        {
            boostParticles.Play();
        }

    }

    private void ProcessRotation()
    {
        switch (ControlType.chosenControlType)
        {
            case "Type1":
                if (Input.GetButton(playerID + "PRotateLeft"))
                {
                    RotateLeft();
                }
                else if (Input.GetButton(playerID + "PRotateRight"))
                {
                    RotateRight();
                }
                break;
            case "Type2":
                if (Input.GetButton(playerID + "PRotateLeft2"))
                {
                    RotateLeft();
                }
                else if (Input.GetButton(playerID + "PRotateRight2"))
                {
                    RotateRight();
                }
                break;
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotateThrust);
    }
    private void RotateRight()
    {
        ApplyRotation(-rotateThrust);
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation so the physics system can take over
    }

    public void DisablePlayerControl()
    {
        canControl = false;
        rb.freezeRotation = true;
    }

    public void EnablePlayerControl()
    {
        canControl = true;
        /*
        rb.constraints = RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        */
        rb.freezeRotation = false;
    }

    public bool PlayerCanControl()
    {
        return canControl;
    }

    public void StopMovement()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = Vector3.zero;
    }

    public void EnableResetRotation()
    {
        canResetRotation = true;
    }

    public void DisableResetRotation()
    {
        canResetRotation = false;
    }
}