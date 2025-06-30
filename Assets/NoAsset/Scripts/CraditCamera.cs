using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CraditCamera : MonoBehaviour
{
    public Transform target;
    public float Speed;
    float time;
    public Transform fade;
    AudioSource source;
    public Transform Setting;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.anyKey&& !Setting.gameObject.activeSelf)
        {
            if(!Input.GetKey(KeyCode.Escape))
            {
                source.pitch = 3;
                Speed = 1.25f * 3f;
            }
            
        }
        else
        {
            source.pitch = 1;
            Speed = 1.25f;
        }

        if (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
        else
        {
            time += Time.deltaTime;
            if (time >= 3f)
            {
                if (!fade.gameObject.activeSelf)
                {
                    fade.gameObject.SetActive(true);
                }
                if (time >= 4f)
                {
                    if (Input.anyKeyDown)
                    {
                        if (File.Exists(Data.path))
                        {
                            Data.Save();
                        }
                        fade.root.GetComponent<AudioSource>().Play();
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fade.root.GetComponent<AudioSource>().Play();
            Setting.gameObject.SetActive(!Setting.gameObject.activeSelf);
        }
    }

    public void Quit()
    {
        fade.root.GetComponent<AudioSource>().Play();
        Application.Quit();
    }
    public void Main()
    {
        fade.gameObject.SetActive(true);
        fade.root.GetComponent<AudioSource>().Play();
        StartCoroutine(main());
    }
    IEnumerator main()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    private void OnApplicationQuit()
    {
        if (File.Exists(Data.path))
        {
            Data.Save();
        }
    }
}
