using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public GameObject Tower;
    public Collider coll;
    public Button yourButton;
    private InputAction touchAction;

    void Awake(){
      touchAction = new InputAction(binding: "<Touchscreen>/primaryTouch/position");
        touchAction.Enable();


    }
    void Start(){
        coll = GetComponent<Collider>();
        Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
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


    public void TaskOnClick(){
       // if(TryGetTouchPosition(out Vector2 touchPos)){}
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0,5f));    
        RaycastHit hit;

        if( Physics.Raycast(ray, out hit, 1000.0f)){
            Destroy(hit.collider.gameObject);
            Instantiate(Tower, hit.transform.position, hit.transform.rotation);
        }
    }

}
