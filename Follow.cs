using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; // 따라갈 물체의 Transform 컴포넌트

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position + new Vector3(0, 1, 0);
            // 물체 A의 위치를 따라가도록 설정
        }
    }
}
