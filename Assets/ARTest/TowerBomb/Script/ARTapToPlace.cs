using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class ARTapToPlace : MonoBehaviour
{
    [SerializeField]
    private GameObject refToPrefab;

    [SerializeField]
    private ARRaycastManager raycastManager;

    private static List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    private GameObject spawnedObject;

    private Camera mainCamera;

    private InputAction touchAction;

    private Vector2 touchPosition;
    private float timeStamp;

    private void Awake()
    {
        timeStamp = Time.time + 5f;
        
        mainCamera = Camera.main;

        touchAction = new InputAction(binding: "<Touchscreen>/primaryTouch/position");
        touchAction.Enable();

        // V�g 1: En v�g att g�!!
        //touchAction.started += TouchAction_started;
    }

    // Tillh�r v�g nr 1
    /*
    private void TouchAction_started(InputAction.CallbackContext obj)
    {
        // ..... skriv resten av koden h�r
    }
    */

    private void OnDestroy()
    {
        // V�g 1: Tillh�r v�g nr 1
        //touchAction.started -= TouchAction_started;

        touchAction.Disable();

        if (touchAction != null)
        {
            touchAction = null;
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPos)
    {
        // H�r anv�nder vi nu inputAction �ntligen f�r att l�sa av
        // vart p� mobilsk�rmen vi har tryckt
        // Triggered h�ller reda p� om detta st�mmer eller ej och retunerar sant/falskt
        if (touchAction.triggered)
        {
            touchPos = touchAction.ReadValue<Vector2>();
            return true;
        }

        touchPos = default;
        return false;
    }

    // V�g 2: 
    private void Update()
    {
        if(timeStamp <= Time.time){

            // H�r anropar vi TryGetTouchPosition metoden
            // och f�rhoppningsvis f�r vi tillbaka vart vi har tryckt p� sk�rmen

            if (!TryGetTouchPosition(out Vector2 touchPos))
            {
                return;
            }

            // Detta omvandlar tv� punkten p� sk�rmen till bokstavligen en
            // raycast str�le som tr�ffar eventuella planes i AR
            if (raycastManager.Raycast(touchPos, hitResults, TrackableType.Planes))
            {
                // Ok vi har verkligen tr�ffat en punkt p� ett plan
                // h�r s� kan vi g�ra vad vi vill nu med prefabobjektet
                // och ta vara p� positionen vi tryckte p�
                Pose hitPose = hitResults[0].pose;

                Instantiate(refToPrefab, hitPose.position, hitPose.rotation);

            }
        }
    }


}
