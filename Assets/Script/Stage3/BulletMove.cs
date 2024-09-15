using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 5; // 총알의 이동 속도를 설정하는 변수 선언 및 초기화

    void Update()
    {
        // 총알을 오른쪽 방향으로 이동시키는 코드
        // transform.right는 오브젝트의 오른쪽 방향을 나타냄
        // speed는 총알의 이동 속도
        // Time.deltaTime을 곱해서 프레임 독립적인 움직임을 만듦 (초당 이동 거리)
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
