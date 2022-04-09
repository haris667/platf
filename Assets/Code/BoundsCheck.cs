using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// предотвращает выход объекта за границы экрана
/// ВАЖНО: работает только с ортоганальной камерой
///</summary>
public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float            radius = 1f;
    public bool             keepOnScreen = true;

    [Header("Set Dynamically")]
    public bool             isOnScreen = true;
    public float            camWidth;
    public float            camHeight;
    [HideInInspector]
    public bool             offRight, offLeft, offUp, offDown;
    private void Awake() {
        Screen.orientation = ScreenOrientation.Portrait;
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    private void LateUpdate() {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offDown = offUp = false;

        if (pos.x > camWidth - radius){
            pos.x = camWidth - radius;
            offRight = true;
        }
        if (pos.x < -camWidth + radius){
            pos.x = -camWidth + radius;
            offLeft = true;
        }

        if (pos.y > camHeight - radius){
            pos.y = camHeight - radius;
            offUp = true;
        }
        if (pos.y < -camHeight + radius){
            pos.y = -camHeight + radius;
            offDown = true;
        }
        isOnScreen = !(offRight || offLeft || offDown || offUp);
        if (keepOnScreen && !isOnScreen){
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offDown = offUp = false;
        }
    }

    private void OnDrawGizmos() {
        if (!Application.isPlaying) return;

        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
