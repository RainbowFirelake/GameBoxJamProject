using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioMenegers : MonoBehaviour
{
    public AudioMixer audioMixer; // ���������� ������� ��� ����������

    public void SetMasterVolume(float volume) // ������� ��� ���������� ������-����������
    {
        audioMixer.SetFloat("MasterVolume", volume);
        // MasterVolume - ��������� �������, ������� �� ���������
    }

}
