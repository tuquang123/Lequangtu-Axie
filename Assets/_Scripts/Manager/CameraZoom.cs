using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomIn;
    [SerializeField] private float _zoomOut;
    [SerializeField] private float _zoomInSpeed;
    [SerializeField] private float _moveInSpeed;
    [SerializeField] private float _zoomOutSpeed;
    [SerializeField] private float _moveOutSpeed;

    private Vector3 _originalCameraPosition;
    private float _originalCameraSize;


    private void Start()
    {
        _originalCameraPosition = _camera.transform.position;
        _originalCameraSize = _camera.orthographicSize;
    }


    private void Update()
    {
        //ZoomIn();
        
        if (Input.GetKeyDown("q"))
        {
            ZoomIn();
        }
        if (Input.GetKeyDown("w"))
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        ZoomInAsync();
    }

    async void ZoomInAsync()
    {
        Vector3 target = new Vector3(_player.transform.position.x, _player.transform.position.y, _camera.transform.position.z);
        
        while (_camera.orthographicSize > _zoomIn || (_camera.transform.position.x != target.x ||
                                                      _camera.transform.position.y != target.y) )
        {
            if (_camera.orthographicSize > _zoomIn)
            {
                _camera.orthographicSize -= _zoomInSpeed;
            }

            target = new Vector3(_player.transform.position.x, _player.transform.position.y, _camera.transform.position.z);
            if (_camera.transform.position.x != target.x || _camera.transform.position.y != target.y)
            {
                _camera.transform.position = Vector3.MoveTowards(this.transform.position, target, _moveInSpeed);
            }
            
            await Task.Yield();
        }

    }

    public void ZoomOut()
    {
        ZoomOutAsync();
    }
    
    async void ZoomOutAsync()
    {
        Vector3 target = _originalCameraPosition;
        
        while (_camera.orthographicSize < _originalCameraSize || (_camera.transform.position.x != target.x ||
                                                                  _camera.transform.position.y != target.y) )
        {
            if (_camera.orthographicSize < _originalCameraSize)
            {
                _camera.orthographicSize += _zoomOutSpeed;
            }

            target = _originalCameraPosition;
            if (_camera.transform.position.x != target.x || _camera.transform.position.y != target.y)
            {
                _camera.transform.position = Vector3.MoveTowards(this.transform.position, target, _moveOutSpeed);
            }
            
            await Task.Yield();
        }

    }
}
