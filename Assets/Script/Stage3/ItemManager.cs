using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject shrinkItemPrefab; // 플레이어 크기 축소 아이템 프리팹을 저장하는 변수 선언
    public GameObject ignoreCollisionItemPrefab; // 적과의 충돌 무시 아이템 프리팹을 저장하는 변수 선언

    public float spawnInterval = 15f; // 아이템 생성 간격을 설정하는 변수 선언 및 초기화
    public float displayTime = 5f; // 아이템이 표시되는 시간을 설정하는 변수 선언 및 초기화

    private float nextSpawnTime = 0f; // 다음 아이템 생성 시간을 저장하는 변수 선언 및 초기화

    void Update()
    {
        // 현재 시간이 다음 아이템 생성 시간보다 크거나 같으면 아이템을 생성
        if (Time.time >= nextSpawnTime)
        {
            SpawnItem(); // 아이템을 생성하는 함수 호출
            nextSpawnTime = Time.time + spawnInterval; // 다음 아이템 생성 시간을 업데이트
        }
    }

    void SpawnItem()
    {
        // 랜덤하게 아이템을 선택하여 생성
        int randomItem = UnityEngine.Random.Range(0, 2); // UnityEngine.Random 사용하여 0 또는 1을 랜덤하게 선택
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-3f, 3f), 0f); // UnityEngine.Random 사용하여 아이템 생성 위치를 랜덤하게 설정

        GameObject itemPrefab = randomItem == 0 ? shrinkItemPrefab : ignoreCollisionItemPrefab; // 선택된 아이템 프리팹을 결정
        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity); // 선택된 아이템을 생성

        Destroy(item, displayTime); // 일정 시간 후에 아이템을 제거
    }
}
