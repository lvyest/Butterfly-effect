using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject prefab; // 총알의 프리팹을 담을 변수를 선언

    public static int maxBulletCount = 10; // 최대 총알 발사 횟수를 10으로 설정
    public int bulletCount = 0; // 현재 발사한 총알 횟수를 0으로 초기화
    public int bulletuseable; // 사용 가능한 총알 개수를 담는 변수를 선언 (UI에 표시용)

    private void OnTriggerEnter2D(Collider2D other) // 2D 충돌이 발생했을 때 호출되는 메서드를 정의
    {
        if (other.CompareTag("Enemy")) // 충돌한 객체의 태그가 "Enemy"인 경우를 확인
        {
            Destroy(other.gameObject); // 적 객체를 파괴
        }
    }

    private void Start() // 게임 시작 시 호출되는 메서드를 정의
    {
        bulletuseable = maxBulletCount; // 사용 가능한 총알 개수를 최대 총알 발사 횟수로 초기화
    }

    void Update() // 매 프레임마다 호출되는 메서드를 정의
    {
        if (Input.GetKeyDown(KeyCode.Space) && bulletCount < maxBulletCount) // 스페이스바가 눌리고 발사할 수 있는 총알이 남아 있는지 확인
        {
            GameObject obj = Instantiate(prefab, transform.position, transform.rotation); // 총알 프리팹을 현재 위치와 회전으로 생성
            Destroy(obj, 5); // 생성된 총알을 5초 후에 파괴

            bulletCount++; // 발사한 총알 횟수를 증가
            bulletuseable = maxBulletCount - bulletCount; // 남은 사용 가능한 총알 개수를 계산
        }
    }
}
