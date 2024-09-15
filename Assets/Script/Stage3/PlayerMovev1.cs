using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovev1 : MonoBehaviour
{
    public float speed = 2; // 플레이어의 이동 속도를 설정하는 변수 선언 및 초기화
    Vector3 moveL = new Vector3(-1, 0, 0); // 왼쪽 방향을 나타내는 벡터 정의
    Vector3 moveU = new Vector3(0, 1, 0); // 위쪽 방향을 나타내는 벡터 정의

    private Coroutine speedIncreaseCoroutine; // 플레이어 속도 증가 코루틴을 저장하는 변수 선언
    private float playerSize; // 플레이어의 크기를 저장하는 변수 선언

    private UIManager uiManager; // UIManager 스크립트를 참조하기 위한 변수 선언

    // Animator 컴포넌트를 참조하기 위한 필드 선언
    private Animator animator;

    private void Start()
    {
        // 플레이어의 초기 크기 설정
        playerSize = transform.localScale.y; // 플레이어의 현재 크기를 저장
        uiManager = FindObjectOfType<UIManager>(); // UIManager 스크립트를 찾아서 참조를 저장

        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>(); // 현재 게임 오브젝트에서 Animator 컴포넌트를 가져옴
        if (animator == null)
        {
            UnityEngine.Debug.LogError("Animator component is not found on the object."); // Animator가 없으면 오류 메시지 출력
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // 충돌한 객체가 적인 경우를 확인
        {
            Vector3 enemySize = other.transform.localScale; // 충돌한 적의 크기를 가져옴

            // 플레이어와 적의 크기를 비교하여 게임 오버 여부 확인
            if (enemySize.y > transform.localScale.y) // 적의 크기가 플레이어보다 큰 경우
            {
                UnityEngine.Debug.Log("Game Over"); // 게임 오버 메시지 출력
                Destroy(gameObject); // 플레이어를 파괴

                // 게임 오버 처리를 UI에 알림
                uiManager.ShowGameOverUI(); // 게임 오버 UI 표시 메서드 호출
            }

            // 플레이어 사이즈가 10 이상일 때 게임 클리어
            if (playerSize >= 10) // 플레이어의 크기가 10 이상인 경우
            {
                UnityEngine.Debug.Log("Game Clear"); // 게임 클리어 메시지 출력
                transform.position -= moveL * speed * Time.deltaTime; // 플레이어를 왼쪽으로 이동

                // 게임 클리어 처리를 UI에 알림
                uiManager.ShowGameClearUI(); // 게임 클리어 UI 표시 메서드 호출
            }
            else
            {
                // 플레이어의 크기 증가률 설정
                float growthRate = playerSize <= 1.5f ? 0.5f : 0.2f; // 플레이어의 크기에 따라 증가율 설정

                // 플레이어의 크기를 충돌한 적의 증가율만큼 증가시킴
                Vector3 newPlayerSize = transform.localScale + new Vector3(enemySize.y * growthRate, enemySize.y * growthRate, 0f);

                // 새로운 플레이어 크기를 적용할 때, 기존 플레이어 크기보다 클 때만 적용
                if (newPlayerSize.y > transform.localScale.y)
                {
                    transform.localScale = newPlayerSize; // 플레이어의 크기를 증가된 크기로 설정

                    // 충돌한 적을 제거
                    Destroy(other.gameObject); // 충돌한 적 객체를 파괴

                    // 플레이어의 크기 업데이트
                    playerSize = transform.localScale.y; // 업데이트된 플레이어 크기를 저장
                }
            }
        }
    }

    void Update()
    {
        // 플레이어 이동
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += moveU * speed * Time.deltaTime; // 위로 이동
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.position -= moveU * speed * Time.deltaTime; // 아래로 이동

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position += moveL * speed * Time.deltaTime; // 왼쪽으로 이동
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.position -= moveL * speed * Time.deltaTime; // 오른쪽으로 이동

        // 스페이스바를 눌렀을 때 애니메이션 트리거
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetTrigger("flap"); // "flap" 애니메이션 트리거 설정
            }
        }
    }

    // 플레이어의 크기를 반환하는 메서드
    public float GetPlayerSize()
    {
        return playerSize; // 현재 플레이어의 크기를 반환
    }

    // 플레이어의 속도를 증가시키는 메서드 (일정 시간 동안만 적용)
    public void IncreaseSpeed(float duration)
    {
        if (speedIncreaseCoroutine != null)
            StopCoroutine(speedIncreaseCoroutine); // 기존의 속도 증가 코루틴이 있으면 중지

        speedIncreaseCoroutine = StartCoroutine(IncreaseSpeedForLimitedTime(duration)); // 새로운 속도 증가 코루틴 시작
    }

    // 일정 시간 동안 속도를 증가시키는 코루틴
    private IEnumerator IncreaseSpeedForLimitedTime(float duration)
    {
        speed *= 2; // 속도를 두 배로 증가
        yield return new WaitForSeconds(duration); // 지정된 시간 동안 대기
        speed /= 2; // 속도를 원래대로 되돌림
    }

    // 플레이어의 크기를 줄이는 메서드
    public void ShrinkPlayer()
    {
        transform.localScale *= 0.7f; // 플레이어의 크기를 70%로 줄임
    }
}
