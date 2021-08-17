using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSolo : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 1500f, rotateThrust = 100, thrustSpeedUpFactor = 2f, thrustSppedNormalFactor = 1f, maxVelocity = 30f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem boostParticles;

    int playerID;
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
            ProcessPause();
        }
    }

    private void ProcessPause()
    {
        if (Input.GetButton(playerID + "PPause"))
        {
            FindObjectOfType<GameManagerSolo>().PauseGame();
        }
    }

    private void ProcessThrust()
    {
        if (Input.GetButton(playerID + "PBoost") && playerStatus.GetCurrentBoost() > 0)
        {
            if (Input.GetButton(playerID + "PSlowDown"))
            {
                SlowDownSpeed();
            }
            else
            {
                if (Input.GetButton(playerID + "PSpeedUp"))
                {
                    ResponseToBoostUp();
                }
                else
                {
                    ResetSpeedFactor();
                }
                StartThursting();
                //LimitMaxmimumSpeed();
            }
            playerStatus.ReducePlayerBoost(speedFactor);
        }
        else if (Input.GetButton(playerID + "PSlowDown"))
        {
            SlowDownSpeed();
            playerStatus.ReducePlayerBoost(speedFactor);
        }
        else
        {
            StopThursting();
            //playerStatus.RecoverBoost();
        }
        playerStatus.UpdatePlayerBoostSlider();
    }

    private void ProcessResetRotation()
    {
        if (Input.GetButton(playerID + "PResetRotation"))
        {
            ResetRotation();
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
        audioSource.Stop();
        if(boostParticles != null && boostParticles.isPlaying)
        {
            boostParticles.Stop();
        }
    }

    private void StartThursting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime * speedFactor);
        Debug.Log("Speed: "+rb.velocity);
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
        if(rb.velocity.magnitude > maxVelocity)
        {
            float velocityX = Mathf.Min(Mathf.Abs(rb.velocity.x), maxVelocity) * Mathf.Sign(rb.velocity.x);
            float velocityY = Mathf.Min(Mathf.Abs(rb.velocity.y), maxVelocity) * Mathf.Sign(rb.velocity.y);
            float velocityZ = rb.velocity.z;

            rb.velocity = new Vector3(velocityX, velocityY, velocityZ);
        }
    }

    public float GetLimitedMaxVelocity()
    {
        return maxVelocity;
    }

    private void SlowDownSpeed()
    {
        rb.AddForce(Physics.gravity);
        Debug.Log("Speed: " + rb.velocity);
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
        if (Input.GetButton(playerID + "PRotateLeft"))
        {
            RotateLeft();
        }
        else if (Input.GetButton(playerID + "PRotateRight"))
        {
            RotateRight();
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