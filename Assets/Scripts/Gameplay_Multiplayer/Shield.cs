using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float invicibleTime = 3f;

    float timeElapsed = 0f;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= invicibleTime)
        {
            if(GetComponentInParent<PlayerStatusMultiplay>() != null)
            {
                GetComponentInParent<PlayerStatusMultiplay>().DeactivateInvicible();
            }
            Destroy(gameObject);
        }
    }
}
