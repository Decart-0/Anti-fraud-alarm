using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signalment : MonoBehaviour
{   
    [SerializeField] private float _maxLoudnes;
    [SerializeField] private float _recoveryRate;
    [SerializeField] private float _volumeRampTime;
    [SerializeField] private bool _isIncluded = false;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        _isIncluded = true;
        StartCoroutine(ControlVolume());            
    }

    private void OnTriggerExit(Collider collider)
    {
         _isIncluded = false;
    }

    private IEnumerator ControlVolume() 
    {
        _audioSource.Play();

        while (_audioSource.volume != 0 || _isIncluded == true)
        {
            if (_isIncluded)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxLoudnes, _recoveryRate);
            }
            else 
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0f, _recoveryRate);       
            }

            yield return new WaitForSeconds(_volumeRampTime);
        }

        _audioSource.Stop();
    }
}