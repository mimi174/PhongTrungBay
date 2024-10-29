using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

//Chạy khi chạy sence 1
//Xử lý khi click nút tham quan

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    
    [SerializeField] private Transform button;
    
    [SerializeField] private Transform loadingScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        button.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        LoadScene("SampleScene");
    }
    
    public async void LoadScene(string sceneName)
    {
        loadingScreen.gameObject.SetActive(true);
        
        var scene = SceneManager.LoadSceneAsync(sceneName);
        if (scene != null)
        { 
            scene.allowSceneActivation = false;

            do
            {
                await Task.Delay(3000);
            } while (scene.progress < 0.9f);
            
            await Task.Delay(1000);

            scene.allowSceneActivation = true;
            
            await Task.Delay(30);
        }
            
    }
}
