using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.SideView; //Define���� �����س��� ī�޶���(���̵��) �ʿ��� �� �ٸ��䵵 ���� ���

    [SerializeField]
    Vector3 _delta = new Vector3(10.0f, 2.0f, 0.0f); //ī�޶� ��ġ ����
    
    //TODO : Y�����

    [SerializeField]
    GameObject _player = null; //�÷��̾��(�ӽ�)

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {

    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.SideView)
        {
            //�÷��̾� ����ÿ��� ī�޶� �����̰� �ؾ��� -> �����ʿ�
            if (_player.IsValid() == false) //Extension�� IsVaild �����ؼ� �÷��̾ ������ ī�޶� �������� ���� ����
            {
                return;
            }

            RaycastHit hit; //Raycast�� �浹 ����
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);       //ī�޶� �Ѿư��� ����
            }
        }
    }

    public void SetSideView(Vector3 delta)
    {
        _mode = Define.CameraMode.SideView;
        _delta = delta;
    }
}