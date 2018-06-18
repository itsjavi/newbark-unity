using UnityEngine;

public enum COLLISION_TYPE
{
    UNKNOWN,
    DEFAULT,
    CAMERA_BOUNDS
}

public class CollisionData : MonoBehaviour
{

    public COLLISION_TYPE type = COLLISION_TYPE.DEFAULT;

    public AudioSource PlaySound()
    {
        var audioSource = GetComponent<AudioSource>();

        if (!audioSource)
        {
            return null;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        return audioSource;
    }
}
