using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickHandle: MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject ClickToReadMessage;
    [SerializeField] private float distance = 10;
    Picture picture;
    private void Start()
    {
    }

    private void Update()
    {
        var hit = PerformRaycast();
        if (hit.collider && IsInteractableTag(hit.collider.tag) && hit.distance <= distance)
        {
            if(picture != null)
                Debug.Log("Sound Playing: " + picture.IsSoundPlaying);

            if (picture == null || !picture.IsSoundPlaying)
            {
                ShowMessage();
            }
            else
            {
                CloseMessage();
            }


            if (Input.GetMouseButtonUp(0))
            {
                picture = hit.collider.gameObject.GetComponent<Picture>();
                picture.PlayMusic();
            }
        }
        else
        {
            CloseMessage();
        }
        
    }

    private void ShowMessage()
    {
        if (ClickToReadMessage != null && !ClickToReadMessage.activeSelf)
            ClickToReadMessage.SetActive(true);
    }

    private void CloseMessage()
    {
        if (ClickToReadMessage != null && ClickToReadMessage.activeSelf)
            ClickToReadMessage.SetActive(false);
    }

    private bool IsInteractableTag(string tag)
    {
        return tag == "picture";
    }

    private RaycastHit PerformRaycast()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hit, Mathf.Infinity);
        return hit;
    }



    public static bool IsPointerOverUIObject()
    {
        var results = new List<RaycastResult>();

        var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
