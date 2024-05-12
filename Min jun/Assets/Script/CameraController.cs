using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.SideView; //Define으로 정의해놓은 카메라모드(사이드뷰) 필요할 시 다른뷰도 만들어서 사용

    [SerializeField]
    Vector3 _delta = new Vector3(10.0f, 2.0f, 0.0f); //카메라 위치 조정
    
    //TODO : Y축고정

    [SerializeField]
    GameObject _player = null; //플레이어설정(임시)

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {

    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.SideView)
        {
            //플레이어 존재시에만 카메라 움직이게 해야함 -> 구현필요
            if (_player.IsValid() == false) //Extension에 IsVaild 구현해서 플레이어가 없으면 카메라 움직임이 없게 만듦
            {
                return;
            }

            RaycastHit hit; //Raycast로 충돌 감지
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);       //카메라가 쫓아가게 설정
            }
        }
    }

    public void SetSideView(Vector3 delta)
    {
        _mode = Define.CameraMode.SideView;
        _delta = delta;
    }
}