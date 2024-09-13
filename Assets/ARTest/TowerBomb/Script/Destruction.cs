using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;

public class Destruction : MonoBehaviour
{
    public GameObject TowerSplit;
    public Collider coll;
    // public Button yourButton;
    private InputAction touchAction;
        [SerializeField]
    private float maxDistance;
    private int layerMask;

    void Awake(){
      touchAction = new InputAction(binding: "<Touchscreen>/primaryTouch/position");
        touchAction.Enable();


    }
    void Start(){
        // coll = GetComponent<Collider>();
        // Button btn = yourButton.GetComponent<Button>();
		// btn.onClick.AddListener(TaskOnClick);
    }

 private bool TryGetTouchPosition(out Vector2 touchPos)
    {
        // Här använder vi nu inputAction äntligen för att läsa av
        // vart på mobilskärmen vi har tryckt
        // Triggered håller reda på om detta stämmer eller ej och retunerar sant/falskt
        if (touchAction.triggered)
        {
            touchPos = touchAction.ReadValue<Vector2>();
            return true;
        }

        touchPos = default;
        return false;
    }
    public Component[] Debri;

    public float Force;
    public float UpForce;
    public void TaskOnClick(){
       // if(TryGetTouchPosition(out Vector2 touchPos)){}
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0,5f));    
        RaycastHit hit;
        
        
        if( Physics.Raycast(ray, out hit, maxDistance)){
            GameObject TowerWhole = hit.collider.gameObject;
            Destroy(TowerWhole);

            Instantiate(TowerSplit, hit.transform.position, hit.transform.rotation);
            Debri = TowerSplit.GetComponentsInChildren<Rigidbody>(); 
            foreach (Rigidbody rb in Debri){
                if(rb != null){
                    rb.AddExplosionForce(Force, rb.transform.position, 10.0f, UpForce);

                    StartCoroutine(CountDown());
                }

            }
        }
    }

    public IEnumerator CountDown(){
            Debug.Log("Stat");
            yield return new WaitForSeconds(10);
            Array.Resize(ref Debri, Debri.Length -1);
            Debug.Log("End");
    }

}
