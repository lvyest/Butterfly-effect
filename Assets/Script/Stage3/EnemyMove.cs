using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹을 저장하는 변수 선언
    public float speed = 3f; // 적의 이동 속도를 설정하는 변수 선언 및 초기화

    void Start()
    {
        SpawnNewEnemy(); // 게임 시작 시 새로운 적을 생성하는 함수 호출
    }

    void Update()
    {
        // 적을 왼쪽으로 이동시키는 코드
        // Vector3.left는 (-1, 0, 0)을 나타냄
        // speed는 적의 이동 속도
        // Time.deltaTime을 곱해서 프레임 독립적인 움직임을 만듦 (초당 이동 거리)
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void SpawnNewEnemy()
    {
        // Main Camera의 오른쪽 밖에서 적이 시작하도록 설정
        Vector3 cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 10));
        GameObject newEnemy = Instantiate(
            enemyPrefab,
            new Vector3(cameraRightEdge.x + UnityEngine.Random.Range(1f, 5f), transform.position.y, transform.position.z),
            Quaternion.identity
        );
        Destroy(newEnemy, 10f); // 새로 생성된 적은 일정 시간 후 파괴되도록 설정
    }

    void OnDestroy()
    {
        // 적이 파괴될 때마다 새로운 적을 생성하는 함수 호출
        SpawnNewEnemy();
    }
}
