using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signalment : MonoBehaviour
{   
    [SerializeField] private float _maxLoudnes;
    [SerializeField] private float _minLoudnes;
    [SerializeField] private float _recoveryRate;
    [SerializeField] private float _volumeRampTime;
    [SerializeField] private bool _isIncluded = false;

    private AudioSource _audioSource;
    private float _targetTime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        _isIncluded = true;
        _targetTime = _maxLoudnes;
        StartCoroutine(ControlVolume());            
    }

    private void OnTriggerExit(Collider collider)
    {
        _isIncluded = false;
        _targetTime = _minLoudnes;
    }

    private IEnumerator ControlVolume() 
    {
        _audioSource.Play();

        while (_audioSource.volume != 0 || _isIncluded == true)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetTime, _recoveryRate);

            yield return new WaitForSeconds(_volumeRampTime);
        }

        _audioSource.Stop();
    }
}