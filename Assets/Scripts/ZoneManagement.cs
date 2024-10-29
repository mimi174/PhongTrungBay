using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Gắn cho obj Floor
//Xử lý vùng phát ra âm thanh
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

    }

    private void OnTriggerExit(Collider other)
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

}
