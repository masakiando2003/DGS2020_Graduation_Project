using System;
using System.Collections;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float respawnTime = 1f;
    [SerializeField] float adjustPlayerRotationTime = 0.5f;
    [SerializeField] float playerCanControlTime = 1f;
    //[SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successSE;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    Movement playerMovement;
    PlayerStatus playerStatus;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<Movement>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
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
                if (other.gameObject.GetComponentInChildren<CheckPointFlag>() != null &&
                    !other.gameObject.GetComponentInChildren<CheckPointFlag>().IsChecked())
                {
                    other.gameObject.GetComponentInChildren<CheckPointFlag>().SetToChecked();
                    playerStatus.RecoverPlayerLife(other.gameObject.GetComponentInChildren<CheckPointFlag>().GetRecoverPlayerLifes());
                    FindObjectOfType<GameManagerSolo>().RecoverRemainingTime(other.gameObject.GetComponentInChildren<CheckPointFlag>().GetRecoverRemainingTime());
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
                GetComponent<Movement>().EnableResetRotation();
                break;
            default:
                StartFailedSequence(other.gameObject.tag);
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Movement>().DisableResetRotation();
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
        if(checkPoint.GetComponentInChildren<CheckPointFlag>() != null)
        {
            checkPoint.GetComponentInChildren<CheckPointFlag>().PassCheckPoint();
        }
        FindObjectOfType<GameManagerSolo>().SaveLatestCheckPoint(checkPointPos);
        playerStatus.ResetBoostToFull();
        //StartCoroutine(AdjustPlayerRotation(this.gameObject));
    }

    private void StartSuccessSequence(GameObject goalPoint)
    {
        goalPoint.GetComponentInChildren<CheckPointFlag>().PassCheckPoint();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSE);
        playerMovement.enabled = false;
        //Invoke("LoadNextLevel", levelLoadDelay);
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
        //Debug.Log("Player Remaining Life: "+ playerStatus.GetCurrentLife());
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

    private void LoadNextLevel()
    {

    }

    private IEnumerator AdjustPlayerRotation(GameObject playerObject)
    {
        yield return new WaitForSeconds(adjustPlayerRotationTime);
        FindObjectOfType<GameManagerSolo>().ResetPlayerRotation(playerObject);
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