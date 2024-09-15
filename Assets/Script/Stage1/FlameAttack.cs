using UnityEngine;
using System.Collections; 

public class FlameAttack : MonoBehaviour 
{
    public GameObject fireballPrefab; // 불씨 프리팹
    public Transform fireballSpawnPoint; // 불씨 생성 위치
    public float fireballSpeed = 5f; // 불씨 속도
    public float fireballSpawnInterval = 2f; // 불씨 생성 간격
    public Transform player; // 플레이어 위치 참조

    void Start() // 게임 오브젝트가 활성화될 때 호출되는 메서드
    {
        StartCoroutine(SpawnFireball()); // 불씨 생성 코루틴 시작
    }

    IEnumerator SpawnFireball() // 불씨를 주기적으로 생성하는 코루틴 메서드
    {
        while (true) // 무한 루프
        {
            yield return new WaitForSeconds(fireballSpawnInterval); // 지정된 간격만큼 대기

            // 불씨 생성
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity); // 불씨 프리팹 생성

            // 플레이어의 현재 위치로 향하도록 방향 설정
            Vector2 direction = (player.position - fireballSpawnPoint.position).normalized; // 방향 벡터 계산 및 정규화
            fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed; // 불씨의 속도 설정
        }
    }
}
