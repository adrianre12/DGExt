using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//namespace DevionGames
//{
    public class PlayAudioClips : MonoBehaviour
    {
/*        [SerializeField]
        private int m_ClipId;*/
        [SerializeField]
        private AudioClip[] m_AudioClips = new AudioClip[0];
        [SerializeField]
        private AudioMixerGroup m_AudioMixerGroup = null;
        [SerializeField]
        private float m_Volume = 1f;
        [SerializeField]
        private float m_Delay = 0f;
        private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
/*    public void Play(int clipId)
    {
        m_ClipId = clipId;
        StartCoroutine("PlayAudio");
    }*/

    IEnumerator PlayAudio(int clipId)
        {
            if (clipId < 0 || clipId >= m_AudioClips.Length || audioSource is null)
                yield return null;

            yield return new WaitForSeconds(this.m_Delay);

            audioSource.outputAudioMixerGroup = this.m_AudioMixerGroup;
            audioSource.volume = this.m_Volume;
            audioSource.PlayOneShot(this.m_AudioClips[clipId]);
        }
    }
//}