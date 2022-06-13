using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void setMusicVolume(float val)
    {
        if (val == -20f) val = -80f;
        mixer.SetFloat("Music", val);
    }
    public void setSoundsVolume(float val)
    {
        if (val == -20f) val = -80f;
        mixer.SetFloat("Sound", val);
    }
}
