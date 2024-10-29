using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Gắn cho obj ClickHandle
//Quản lý sự kiện click chuột để nghe thuyết minh

public class ClickHandle : MonoBehaviour
{
    public static ClickHandle instance;

    [SerializeField] private GameObject audioMsg;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject ClickToReadMessage;
    [SerializeField] private float distance = 10;
    public bool isSoundEnded = false;

    public Picture picture;
    private string _audioName = string.Empty;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    private void Update()
    { 
        if (PauseMenu.instance.isPaused) return;
        
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
            if (picture == null)
            {
                ShowMessage();
            }
            else
            {
                CloseMessage();
            }
            
            if (Input.GetMouseButtonDown(0) && picture == null)
            {
                if (!IsPointerOverUIObject()) return;
                
                picture = hit.collider.gameObject.GetComponent<Picture>();
                _audioName = picture.Name;
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
        if (picture == null)
        {
            isSoundEnded = true;
            return;
        }
        var state = picture.GetPictureState();
        if (state.soundTime == 0 || (state.soundTime >= state.clipLength))
            isSoundEnded = true;
        else
            isSoundEnded = false;
    }

    private void SetAudioMessage()
    {
        if (picture == null)
        {
            audioMsg.SetActive(false);
            return;
        }

        string msg = "Nhấn [Q] để thoát \n";

        if (isSoundEnded)
        {
            msg += "Nhấn [E] để tiếp tục \n";
            msg += $"{_audioName} is end";
        }
        else
        {
            msg += (picture.IsSoundPlaying ? "Nhấn [E] để tạm dừng " : "Nhấn [E] để tiếp tục") + "\n";
            msg += picture.IsSoundPlaying ? $"{_audioName} đang được phát" : $"{_audioName} đã được tạm dừng";
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
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                results.RemoveAt(i);
                i--;
            }
        }
        
        return results.Count > 0;
    }
}
