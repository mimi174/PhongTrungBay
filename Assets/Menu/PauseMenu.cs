using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Gắn cho obj PauseMenu
//Quản lý sự kiện nhấn F1 để mở menu tạm dừng tham quan
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Tham chiếu đến Panel

    private AudioSource[] audioSources; // Danh sách AudioSource

    private void Start()
    {
        // Lấy tất cả AudioSource trong scene
        audioSources = FindObjectsOfType<AudioSource>();
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục trò chơi
        Cursor.lockState = CursorLockMode.Locked; // Hiện con trỏ chuột
        Cursor.visible = false; // Đảm bảo con trỏ chuột hiển thị

        foreach (var audio in audioSources)
        {
            audio.UnPause();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Dừng trò chơi
        Cursor.lockState = CursorLockMode.None; // Hiện con trỏ chuột
        Cursor.visible = true; // Đảm bảo con trỏ chuột hiển thị

        foreach (var audio in audioSources)
        {
            audio.Pause();
        }
    }

    public void Quit()
    {
        Application.Quit(); // Thoát trò chơi
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng chơi trong Editor
#endif
    }
}
