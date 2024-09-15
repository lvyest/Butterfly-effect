using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4; // 적의 이동 속도를 설정하는 변수 선언 및 초기화

    void Update()
    {
        Vector3 dir = Vector3.left; // 적의 이동 방향을 왼쪽으로 설정

        // 적을 왼쪽 방향으로 이동시키는 코드
        // dir는 Vector3.left이므로 (-1, 0, 0)의 벡터를 의미
        // speed는 적의 이동 속도
        // Time.deltaTime을 곱해서 프레임 독립적인 움직임을 만듦 (초당 이동 거리)
        transform.position += dir * speed * Time.deltaTime;
    }
}
