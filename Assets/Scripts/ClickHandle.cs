using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickHandle: MonoBehaviour
{

    [SerializeField] private Camera mainCamera;


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var hit = PerformRaycast();

            if (hit.collider && IsInteractableTag(hit.collider.tag)) {

                var picture = hit.collider.gameObject.GetComponent<Picture>();
                picture.PlayMusic();
            }

        }
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
