using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioClip sceneTransitionSound, wakingSound;

    [SerializeField] AudioClip preDeathOST, postDeathOST;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ChangeOST();
    }

    public void ChangeOST()
    {
        if (PersistantManager.instance.deathCount > 0)
        {
            musicSource.volume = 0.2f;
            musicSource.clip = postDeathOST;
            musicSource.Play();
        }
        else
        {
            musicSource.clip = preDeathOST;
            musicSource.Play();
        }
    }

    public void Silence()
    {
        musicSource.Stop();
    }

    public void PlaySound(AudioClip sound)
    {
        source.pitch = Random.Range(0.8f, 1.2f);
        source.PlayOneShot(sound);
        source.pitch = 1;
    }

    public void PlayFootsteopSound()
    {
        source.PlayOneShot(wakingSound);
    }

    public void PlayScreenTransitionSound()
    {
        source.PlayOneShot(sceneTransitionSound);
    }
}
