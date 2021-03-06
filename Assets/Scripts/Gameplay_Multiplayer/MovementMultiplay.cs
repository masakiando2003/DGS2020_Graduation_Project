using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementMultiplay : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 1500f, rotateThrust = 100;
    [SerializeField] float thrustSpeedUpFactor = 2f, thrustSppedNormalFactor = 1f;
    [SerializeField] float maxSpeed = 50f, slowDownSpeedFactor = 1.0005f;
    [SerializeField] Image speedDownImage;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem boostParticles;

    int playerID;
    float speedFactor, currentMaxSpeed;
    Rigidbody rb;
    AudioSource audioSource;
    PlayerStatusMultiplay playerStatus;
    bool canControl, canResetRotation;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerStatus = GetComponent<PlayerStatusMultiplay>();
        Initialization();
    }

    private void Initialization()
    {
        canControl = true;
        canResetRotation = false;
        speedFactor = thrustSppedNormalFactor;
        playerID = GetComponent<PlayerStatusMultiplay>().GetPlayerID();
        currentMaxSpeed = maxSpeed;
        speedDownImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameManagerMultiplay.GameState currentGameState = FindObjectOfType<GameManagerMultiplay>().GetCurrentGameState();
        if(currentGameState == GameManagerMultiplay.GameState.Finish)
        {
            return;
        }

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
        // Disable keyboard Input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || 
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) ||
            Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.V) ||
            Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Space) ||
            Input.GetKey(KeyCode.Escape))
        {
            return;
        }
        if (Input.GetButton(playerID + "PBoost") && playerStatus.GetCurrentBoost() > 0)
        {
            if (Input.GetButton(playerID + "PSlowDown"))
            {
                SlowDownSpeed();
            }
            else
            {
                StartThursting();
                LimitMaxmimumSpeed();
            }
            playerStatus.ReducePlayerBoostContinously(speedFactor);
        }
        else if (Input.GetButton(playerID + "PSlowDown"))
        {
            SlowDownSpeed();
            playerStatus.ReducePlayerBoostContinously(speedFactor);
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
        // Disable keyboard Input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) ||
            Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.V) ||
            Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Space) ||
            Input.GetKey(KeyCode.Escape))
        {
            return;
        }
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
        if (boostParticles != null && boostParticles.isPlaying)
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
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, currentMaxSpeed);
    }

    public void ReduceMaximumSpeed(float reduceMaximumSpeedFactor)
    {
        currentMaxSpeed = maxSpeed - reduceMaximumSpeedFactor;
        speedDownImage.enabled = true;
    }

    public void RestoreOrginalMaximumSpeed()
    {
        currentMaxSpeed = maxSpeed;
        speedDownImage.enabled = false;
    }

    public float GetLimitedMaxVelocity()
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
        // Disable keyboard Input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) ||
            Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.V) ||
            Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Space) ||
            Input.GetKey(KeyCode.Escape))
        {
            return;
        }
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
