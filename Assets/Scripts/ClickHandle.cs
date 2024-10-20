using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandle: MonoBehaviour
{
    [SerializeField] private GameObject audioMsg;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject ClickToReadMessage;
    [SerializeField] private float distance = 10;
    private bool isSoundEnded = false;
    Picture picture;
    private string _audioName = string.Empty;
    private void Start()
    {
    }

    private void Update()
    {
        SetAudioMessage();
        CheckIfSoundEnded();

        if (Input.GetKeyUp(KeyCode.E) && picture != null)
        {
            if (picture.IsSoundPlaying)
                picture.PauseMusic();
            else
                picture.ResumMusic();
        }
        else if (Input.GetKeyUp(KeyCode.Q) && picture != null)
        {
            picture.StopMusic();
            picture = null;
        }

        var hit = PerformRaycast();
        if (hit.collider && IsInteractableTag(hit.collider.tag) && hit.distance <= distance)
        {
            //if(picture != null)
            //    Debug.Log("Sound Playing: " + picture.IsSoundPlaying);

            if (picture == null)
            {
                ShowMessage();
            }
            else
            {
                CloseMessage();
            }


            if (Input.GetMouseButtonUp(0) && picture == null)
            {
                picture = hit.collider.gameObject.GetComponent<Picture>();
                _audioName = "Sound";
                picture.PlayMusic();
            }
        }
        else
        {
            CloseMessage();
        }
        
    }

    private void CheckIfSoundEnded()
    {
        if(picture == null)
        {
            isSoundEnded = false;
            return;
        }
        var state = picture.GetPictureState();
        if (state.soundTime == 0 || (state.soundTime >= state.clipLength))
            isSoundEnded = true;
        else
            isSoundEnded = false;

        Debug.Log("Clip length: " + state.clipLength + "\n Clip time: " + state.soundTime);
    }

    private void SetAudioMessage()
    {
        if (picture == null)
        {
            audioMsg.SetActive(false);
            return;
        }

        string msg = "Press [Q] to quit \n";

        if (isSoundEnded)
        {
            msg += "Press [E] to replay \n";
            msg += $"{_audioName} is end";
        }
        else
        {
            msg += (picture.IsSoundPlaying ? "Press [E] to pause" : "Press [E] to continue") + "\n";
            msg += picture.IsSoundPlaying ? $"{_audioName} is playing" : $"{_audioName} is paused";
        }


        var text = audioMsg.GetComponent<Text>();
        text.text = msg;
        audioMsg.SetActive(true);
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
