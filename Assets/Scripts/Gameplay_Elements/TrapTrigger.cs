using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] AudioClip triggerSE;
    [SerializeField] Light consoleLight;
    [SerializeField] GameObject[] traps;
    [SerializeField] bool isActiviated;

    AudioSource audioSource;

    private void Awake()
    {
        isActiviated = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player") && !isActiviated)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(triggerSE);
            consoleLight.enabled = true;
            for(int i = 0; i < traps.Length; i++)
            {
                switch (traps[i].gameObject.tag)
                {
                    case "Robot":
                        traps[i].gameObject.GetComponent<Oscillator>().enabled = true;
                        traps[i].gameObject.gameObject.GetComponent<Robot>().StartWalking();
                        break;
                    case "MoveableWall":
                    case "Door":
                        traps[i].gameObject.GetComponent<Oscillator>().enabled = true;
                        break;
                    case "Tubes":
                        traps[i].gameObject.GetComponent<Tube>().EnableFreeFall();                     
                         break;
                    case "Fabrics":
                        traps[i].gameObject.GetComponent<FlyFabrics>().StartToFly();
                        break;
                }
            }
            isActiviated = !isActiviated;
        }
    }
}
