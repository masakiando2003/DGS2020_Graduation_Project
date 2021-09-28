using System;
using System.Collections;
using UnityEngine;

public class CollisionHandlerSolo : MonoBehaviour
{
    [SerializeField] float respawnTime = 1f;
    [SerializeField] float playerCanControlTime = 1f;
    [SerializeField] AudioClip successSE;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    MovementSolo playerMovement;
    PlayerStatusSolo playerStatus;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<MovementSolo>();
        playerStatus = GetComponent<PlayerStatusSolo>();
    }

    private void Update()
    {
        //RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "CheckPoint":
                playerMovement.StopMovement();
                if (other.gameObject.GetComponentInChildren<CheckPointFlagSolo>() != null &&
                    !other.gameObject.GetComponentInChildren<CheckPointFlagSolo>().IsChecked())
                {
                    other.gameObject.GetComponentInChildren<CheckPointFlagSolo>().SetToChecked();
                    playerStatus.RecoverPlayerLife(other.gameObject.GetComponentInChildren<CheckPointFlagSolo>().GetRecoverPlayerLifes());
                    FindObjectOfType<GameManagerSolo>().RecoverRemainingTime(other.gameObject.GetComponentInChildren<CheckPointFlagSolo>().GetRecoverRemainingTime());
                }
                StartSaveLatestCheckPointSequence(other.gameObject);
                break;
            case "SafeZone":
                playerMovement.StopMovement();
                break;
            case "StartPoint":
                playerMovement.StopMovement();
                playerStatus.ResetBoostToFull();
                break;
            case "Finish":
                StartSuccessSequence(other.gameObject);
                break;
            default:
                StartFailedSequence(other.gameObject.tag);
                break;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "CheckPoint":
            case "SafeZone":
            case "StartPoint":
                GetComponent<MovementSolo>().EnableResetRotation();
                break;
            default:
                StartFailedSequence(other.gameObject.tag);
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<MovementSolo>().DisableResetRotation();
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Fuel":
                playerStatus.ResetBoostToFull();
                other.gameObject.GetComponent<Fuel>().DeactivateFuelObject();
                break;
        }
    }

    private void StartSaveLatestCheckPointSequence(GameObject checkPoint)
    {
        Transform checkPointPos = (checkPoint.GetComponentInChildren<Transform>() != null) ? checkPoint.GetComponentInChildren<Transform>().transform : checkPoint.transform;
        if(checkPoint.GetComponentInChildren<CheckPointFlagSolo>() != null)
        {
            checkPoint.GetComponentInChildren<CheckPointFlagSolo>().PassCheckPoint();
        }
        FindObjectOfType<GameManagerSolo>().SaveLatestCheckPoint(checkPointPos);
        playerStatus.ResetBoostToFull();
    }

    private void StartSuccessSequence(GameObject goalPoint)
    {
        goalPoint.GetComponentInChildren<CheckPointFlagSolo>().PassCheckPoint();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSE);
        playerMovement.enabled = false;
        FindObjectOfType<GameManagerSolo>().Clear();
    }

    private void StartFailedSequence(string gameObecjtTag)
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        playerStatus.PlayExplosionVFX();
        playerMovement.DisablePlayerControl();
        playerMovement.StopMovement();
        playerStatus.SetPlayerColorToExploded();
        playerStatus.ReducePlayerLife();
        if (playerStatus.GetCurrentLife() > 0)
        {
            playerStatus.ResetBoostToFull();
            StartCoroutine(ResetPlayerPositionAndActivateFuelObjects(gameObject));
        }
        else
        {
            FindObjectOfType<GameManagerSolo>().GameOver();
        }
    }

    private IEnumerator ResetPlayerPositionAndActivateFuelObjects(GameObject playerObject)
    {
        yield return new WaitForSeconds(respawnTime);
        playerStatus.SetPlayerColorToNormal();
        FindObjectOfType<GameManagerSolo>().ResetPlayerToStartPosition(playerObject);
        FindObjectOfType<GameManagerSolo>().ActiviateAllFuelObjects();
        playerStatus.UpdatePlayerBoostSlider();
        yield return new WaitForSeconds(playerCanControlTime);
        playerMovement.EnablePlayerControl();
        isTransitioning = false;
    }
}