using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
  
    static public MySceneManager instance;
    [SerializeField] private GameObject youLostText;
    void Start()
    {
        instance = this;

        if(SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("DeathScene").buildIndex)
        {
            Cursor.visible = true;
        }
        else if(SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("SampleScene").buildIndex)
        {
            Cursor.visible = false;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator OpenDeathScene()
    {
        Time.timeScale = 0.5f;
        youLostText.SetActive(true);

        yield return new WaitForSeconds(1);

        Time.timeScale = 1;
        youLostText.SetActive(false); 

        SceneManager.LoadScene("DeathScene");   
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");  
    }
    public void Quit()
    {
        Application.Quit();
    }
}
