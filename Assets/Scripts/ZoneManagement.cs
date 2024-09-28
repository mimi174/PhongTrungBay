using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManagement : MonoBehaviour
{
    private BoxCollider _boxCollider, _parentBoxColiider;
    public float WidthRangeExtend = 10f, HeightRangeExtend = 10f;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _parentBoxColiider = transform.parent.GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();

        Debug.Log("Parent center:" + _parentBoxColiider.center);
        Debug.Log("Parent size:" + _parentBoxColiider.size);

        DrawCollider();
    }

    private void DrawCollider()
    {
        if (_boxCollider == null || _parentBoxColiider == null)
            return;

        _boxCollider.isTrigger = true;
        _boxCollider.size = new Vector3(_parentBoxColiider.size.x + WidthRangeExtend, _parentBoxColiider.size.y + HeightRangeExtend, _parentBoxColiider.size.z + WidthRangeExtend);
        _boxCollider.center = _parentBoxColiider.center;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_audioSource == null || _audioSource.clip == null || _audioSource.isPlaying)
            return;

        _audioSource.loop = true;
        _audioSource.Play();
        Debug.Log("Audio is playing");
    }

    private void OnTriggerExit(Collider other)
    {
        if (_audioSource == null || _audioSource.clip == null || !_audioSource.isPlaying)
            return;

        _audioSource.Stop();
        Debug.Log("Audio stopped");
    }

    // Update is called once per frame
    void Update()
    {
    }

}
