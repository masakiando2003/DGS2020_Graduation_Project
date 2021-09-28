using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlerMultiplay : MonoBehaviour
{
    [SerializeField] float respawnTime = 1f;
    [SerializeField] float playerCanControlTime = 1f;
    [SerializeField] AudioClip successSE;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    MovementMultiplay playerMovement;
    PlayerStatusMultiplay playerStatus;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<MovementMultiplay>();
        playerStatus = GetComponent<PlayerStatusMultiplay>();
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
                if (other.gameObject.GetComponentInChildren<CheckPointFlagMultiplay>() != null &&
                    !other.gameObject.GetComponentInChildren<CheckPointFlagMultiplay>().IsChecked())
                {
                    other.gameObject.GetComponentInChildren<CheckPointFlagMultiplay>().SetToChecked();
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
            case "Player":
                break;
            default:
                if(GetComponent<PlayerStatusMultiplay>() != null)
                {
                    if (!GetComponent<PlayerStatusMultiplay>().IsInvicible())
                    {
                        StartFailedSequence(other.gameObject.tag);
                    }
                }
                else
                {
                    StartFailedSequence(other.gameObject.tag);
                }
                break;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Player":
                break;
            case "CheckPoint":
            case "SafeZone":
            case "StartPoint":
                GetComponent<MovementMultiplay>().EnableResetRotation();
                break;
            default:
                if (GetComponent<PlayerStatusMultiplay>() != null)
                {
                    if (!GetComponent<PlayerStatusMultiplay>().IsInvicible())
                    {
                        StartFailedSequence(other.gameObject.tag);
                    }
                }
                else
                {
                    StartFailedSequence(other.gameObject.tag);
                }
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<MovementMultiplay>().DisableResetRotation();
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Fuel":
                playerStatus.ResetBoostToFull();
                //other.gameObject.GetComponent<Fuel>().DeactivateFuelObject();
                break;
        }
    }

    private void StartSaveLatestCheckPointSequence(GameObject checkPoint)
    {
        Transform checkPointPos = (checkPoint.GetComponentInChildren<Transform>() != null) ? checkPoint.GetComponentInChildren<Transform>().transform : checkPoint.transform;
        if (checkPoint.GetComponentInChildren<CheckPointFlagMultiplay>() != null)
        {
            checkPoint.GetComponentInChildren<CheckPointFlagMultiplay>().PassCheckPoint();
        }
        int playerID = GetComponent<PlayerStatusMultiplay>().GetPlayerID();
        FindObjectOfType<GameManagerMultiplay>().SaveLatestCheckPoint(playerID, checkPointPos);
        playerStatus.ResetBoostToFull();
    }

    private void StartSuccessSequence(GameObject goalPoint)
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSE);
        playerMovement.enabled = false;
        if (MultiplayPlayerMode.gameMode == "BattleRoyale")
        {
            int playerID = GetComponent<PlayerStatusMultiplay>().GetPlayerID();
            FindObjectOfType<GameManagerMultiplay>().FinishBattleRoyale(playerID);
        }
        else
        {
            FindObjectOfType<GameManagerMultiplay>().FinishTeamPlay();
        }
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
        playerStatus.ResetBoostToFull();
        StartCoroutine(ResetPlayerPositionAndActivateFuelObjects(gameObject));
    }

    private IEnumerator ResetPlayerPositionAndActivateFuelObjects(GameObject playerObject)
    {
        yield return new WaitForSeconds(respawnTime);
        playerStatus.SetPlayerColorToNormal();
        int playerID = GetComponent<PlayerStatusMultiplay>().GetPlayerID();
        FindObjectOfType<GameManagerMultiplay>().ResetPlayerToStartPosition(playerID, playerObject);
        FindObjectOfType<GameManagerMultiplay>().ActiviateAllFuelObjects();
        playerStatus.UpdatePlayerBoostSlider();
        yield return new WaitForSeconds(playerCanControlTime);
        playerMovement.EnablePlayerControl();
        isTransitioning = false;
    }
}
