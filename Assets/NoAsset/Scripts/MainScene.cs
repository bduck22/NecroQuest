using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public Transform Fade;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GameStart()
    {
        StartCoroutine(G());
    }
    public void Credit()
    {
        StartCoroutine(C());
    }
    public void Init()
    {
        Data.Delete();
        Application.Quit();
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator G()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
    IEnumerator C()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
    }
}
