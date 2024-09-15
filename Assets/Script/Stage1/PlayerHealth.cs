using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // 최대 목숨
    private int currentHealth; // 현재 목숨
    public GameObject healthUI; // 목숨 표시 UI (빈 오브젝트)
    public GameObject gameOverPanel; // 게임 오버 패널

    private UnityEngine.UI.Image[] healthImages; // 목숨 이미지 배열
    private bool isGameOver = false; // 게임 오버 상태 플래그

    void Start()
    {
        currentHealth = maxHealth;

        // HealthUI 오브젝트의 자식들 중에서 Image 컴포넌트를 가진 것들을 찾습니다.
        healthImages = healthUI.GetComponentsInChildren<UnityEngine.UI.Image>();

        // 초기 목숨 UI 업데이트
        UpdateHealthUI();

        // 게임 오버 패널 초기 비활성화
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // 게임 오버 상태에서 마우스 클릭을 감지
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            RestartGame();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // 불씨 제거
        }
    }

    void TakeDamage(int damage)
    {
        if (isGameOver) return; // 게임 오버 상태에서는 더 이상 데미지를 받지 않음

        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthUI()
    {
        // 현재 목숨에 따라 이미지 활성화/비활성화 설정
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].enabled = true; // 활성화
            }
            else
            {
                healthImages[i].enabled = false; // 비활성화
            }
        }
    }

    void GameOver()
    {
        // 게임 오버 상태 설정
        isGameOver = true;

        // 게임 오버 패널과 버튼 활성화
        gameOverPanel.SetActive(true);

        // 게임 일시 정지
        Time.timeScale = 0;
    }

    void RestartGame()
    {
        // 게임 재시작: 현재 씬을 다시 로드
        Time.timeScale = 1; // 게임 속도 복구
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
