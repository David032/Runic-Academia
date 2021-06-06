using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    GameObject cameraGameObject;

    Vector3 panMove;
    Vector2 pointerPosition;

    CharacterManager CharacterManager;

    void Start()
    {
        cameraGameObject = Camera.main.gameObject;
        CharacterManager = CharacterManager.Instance;
    }

    private void LateUpdate()
    {
        cameraGameObject.transform.Translate(panMove, Space.World);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            //Roofs are layer 6
            if (hit.transform.gameObject.layer == 6) 
            {
                StartCoroutine
                    (RemoveRoof(hit.transform.gameObject));
            }
        }
    }

    IEnumerator RemoveRoof(GameObject roof) 
    {
        roof.SetActive(false);
        yield return new WaitForSecondsRealtime(2.5f);
        roof.SetActive(true);
    }

    public void PanCamera(InputAction.CallbackContext context) 
    {
        Vector2 panValue = context.ReadValue<Vector2>();
        panMove = new Vector3(panValue.x, 0, panValue.y);
    }
    public void MousePos(InputAction.CallbackContext context) 
    {
        pointerPosition = context.ReadValue<Vector2>();
    }

    public void Click(InputAction.CallbackContext context) 
    {
        if (!context.performed)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        if (Physics.Raycast(ray, out hit,100f))
        {
            GameObject objectHit = hit.transform.gameObject;
            print(objectHit);
            //Clicked on an entity that isn't currently selected
            if (objectHit.CompareTag("Entity") && 
                CharacterManager.SelectedEntity != objectHit)
            {
                CharacterManager.SelectedEntity = 
                    hit.transform.gameObject;
            }
            //Clicked on an entity that is currently selected
            else if (objectHit.CompareTag("Entity") &&
                CharacterManager.SelectedEntity == objectHit)
            {
                CharacterManager.SelectedEntity = null;
            }
            //Clicked on an interactable that isn't currently selected

            //Clicked on an interactable that is currently selected
        }
    }
}
