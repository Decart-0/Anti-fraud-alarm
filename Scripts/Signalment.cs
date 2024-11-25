using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signalment : MonoBehaviour
{   
    [SerializeField] private float _maxLoudnes;
    [SerializeField] private float _minLoudnes;
    [SerializeField] private float _recoveryRate;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void Update()
    {
        if (_audioSource.volume == 0)   
        {
            _audioSource.Stop();
            StopCoroutine();
        }          
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>()) 
        {
            _audioSource.Play();
            StopCoroutine();
            _coroutine = StartCoroutine(ControlVolume(_maxLoudnes));
        }           
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Player>()) 
        {
            StopCoroutine();
            _coroutine = StartCoroutine(ControlVolume(_minLoudnes));
        }          
    }

    private IEnumerator ControlVolume(float targetTime) 
    {
        float volumeRampTime = 1;

        while (_audioSource.volume != targetTime)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetTime, _recoveryRate);

            yield return new WaitForSeconds(volumeRampTime);
        }
    }

    private void StopCoroutine()
    {
        if (_coroutine != null) 
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }         
    }
}