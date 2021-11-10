using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif

public class FolllowPlayerDirection : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = GameObject.FindWithTag("CinemachineTarget").transform.position;
        //transform.rotation = GameObject.FindWithTag("CinemachineTarget").transform.rotation;


    }
}
}

