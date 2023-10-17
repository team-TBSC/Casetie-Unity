using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : MonoBehaviour
{
    public Transform headTransform; // 캐릭터의 머리 위치를 가리키는 Transform 컴포넌트
    public Vector3 offset = new Vector3(0f, 1f, 0f); // 이름 태그의 머리 위치로부터의 오프셋

    void LateUpdate()
    {
        // UI 요소의 위치 계산
        Vector3 nameTagPosition = headTransform.position + offset;

        // UI 요소의 위치 설정
        transform.position = nameTagPosition;

        // UI 요소를 항상 카메라를 향해 회전하도록 설정
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
