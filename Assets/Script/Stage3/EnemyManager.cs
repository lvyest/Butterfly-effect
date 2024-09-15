using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime; // 현재 시간을 저장하는 변수 선언

    public float createTime = 1; // 적 생성 주기를 설정하는 변수 선언 및 초기화
    public GameObject[] enemyFactories; // 여러 적 팩토리를 저장하는 배열 선언

    public float minTime = 2f; // 적 생성 시간의 최소값을 설정하는 변수 선언 및 초기화
    public float maxTime = 6f; // 적 생성 시간의 최대값을 설정하는 변수 선언 및 초기화

    float minScale = 0.1f; // 적의 최소 크기를 설정하는 변수 선언 및 초기화
    float maxScale = 3.0f; // 적의 최대 크기를 설정하는 변수 선언 및 초기화

    private PlayerMovev1 playerMove; // PlayerMovev1 스크립트를 참조하는 변수 선언

    void Start()
    {
        // PlayerMove 스크립트에 접근
        playerMove = GameObject.FindObjectOfType<PlayerMovev1>(); // 게임 내 PlayerMovev1 객체를 찾고, 참조를 저장

        // 초기 Enemy 생성 시간 설정
        createTime = UnityEngine.Random.Range(minTime, maxTime); // 초기 적 생성 시간을 minTime과 maxTime 사이에서 랜덤으로 설정
    }

    void Update()
    {
        currentTime += Time.deltaTime; // currentTime을 매 프레임마다 Time.deltaTime 만큼 증가

        if (currentTime > createTime) // 현재 시간이 설정된 생성 시간보다 큰 경우
        {
            // 적 팩토리를 랜덤으로 선택
            int randomIndex = UnityEngine.Random.Range(0, enemyFactories.Length); // 배열에서 랜덤 인덱스를 선택
            GameObject selectedFactory = enemyFactories[randomIndex]; // 선택된 인덱스에 해당하는 적 팩토리를 선택
            GameObject enemy = Instantiate(selectedFactory); // 선택된 적 팩토리를 인스턴스화 (생성)

            enemy.transform.position = transform.position; // 생성된 적의 위치를 EnemyManager의 위치로 설정

            // Player의 크기에 따라 Enemy의 크기 범위 조정
            float playerSize = playerMove.GetPlayerSize(); // Player의 크기를 가져옴
            if (playerSize <= 1.5) // 플레이어의 크기가 1.5보다 작거나 같은 경우
            {
                minScale = 0.3f; // 적의 최소 크기를 0.3으로 설정
                maxScale = 1.8f; // 적의 최대 크기를 1.8로 설정
            }
            else if (playerSize <= 2.0) // 플레이어의 크기가 2.0보다 작거나 같은 경우
            {
                minScale = 0.5f; // 적의 최소 크기를 0.5으로 설정
                maxScale = 2.5f; // 적의 최대 크기를 2.5로 설정
            }
            else // 플레이어의 크기가 2.0보다 큰 경우
            {
                minScale = 1.5f; // 적의 최소 크기를 1.5으로 설정
                maxScale = 3.0f; // 적의 최대 크기를 3.0로 설정
            }

            float randomSize = UnityEngine.Random.Range(minScale, maxScale); // minScale과 maxScale 사이에서 랜덤 크기 값을 선택
            Vector3 randomScale = new Vector3(randomSize, randomSize, 1f); // 랜덤 크기를 사용하여 적의 스케일을 설정
            enemy.transform.localScale = randomScale; // 생성된 적의 크기를 랜덤 크기로 설정

            // 다음 Enemy 생성 시간 및 크기 범위 설정
            currentTime = 0; // currentTime을 0으로 초기화
            createTime = UnityEngine.Random.Range(minTime, maxTime); // 다음 적 생성 시간을 minTime과 maxTime 사이에서 랜덤으로 설정
        }
    }
}
