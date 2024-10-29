using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Gắn cho obj PauseMenu
//Quản lý sự kiện nhấn F1 để mở menu tạm dừng tham quan
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    
    public GameObject pauseMenuUI; // Tham chiếu đến Panel
    
    public bool isPaused = false; //Biến check xem game có đang tạm dừng hay không

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục trò chơi
        Cursor.lockState = CursorLockMode.Locked; // Hiện con trỏ chuột
        Cursor.visible = false; // Đảm bảo con trỏ chuột hiển thị

        if (ClickHandle.instance.isSoundEnded) return;

        AudioManager.instance.ResumePictureMusic(); // Tiếp tục thuyết minh
        
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Dừng trò chơi
        Cursor.lockState = CursorLockMode.None; // Hiện con trỏ chuột
        Cursor.visible = true; // Đảm bảo con trỏ chuột hiển thị

        if (ClickHandle.instance.isSoundEnded) return;

        AudioManager.instance.PausePictureMusic(); //Tạm dừng thuyết minh
    }

    public void Quit()
    {
        Application.Quit(); // Thoát trò chơi
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng chơi trong Editor
#endif
    }
}
