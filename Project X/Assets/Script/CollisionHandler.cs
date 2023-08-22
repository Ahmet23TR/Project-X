using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTrasitioning = false; 
    bool collisonDisabled = false;

     void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    } 
    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisonDisabled = !collisonDisabled; // toggle collison
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTrasitioning || collisonDisabled ) return;

        switch (other.gameObject.tag)
        {
        case "Friendly":
            break;
        case "Finish":
        startSuccesSequence ();       
            break;
        default:
        startCrashSequence();
            break;
        }
       
    }

    void startCrashSequence ()
    {
        isTrasitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        // todo particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        crashParticles.Play();
    }

    void startSuccesSequence ()
    {
        isTrasitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        // todo particle effect upon success
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        successParticles.Play();
    }
     void LoadNextLevel()
    {
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currenSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
    }

    void disabledCollision ()
    {
        if (Input.GetKey(KeyCode.C))
        {
        GetComponent<CollisionHandler>().enabled = false;
        }
    }
}
