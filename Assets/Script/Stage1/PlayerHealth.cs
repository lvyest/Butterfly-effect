using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // �ִ� ���
    private int currentHealth; // ���� ���
    public GameObject healthUI; // ��� ǥ�� UI (�� ������Ʈ)
    public GameObject gameOverPanel; // ���� ���� �г�

    private UnityEngine.UI.Image[] healthImages; // ��� �̹��� �迭
    private bool isGameOver = false; // ���� ���� ���� �÷���

    void Start()
    {
        currentHealth = maxHealth;

        // HealthUI ������Ʈ�� �ڽĵ� �߿��� Image ������Ʈ�� ���� �͵��� ã���ϴ�.
        healthImages = healthUI.GetComponentsInChildren<UnityEngine.UI.Image>();

        // �ʱ� ��� UI ������Ʈ
        UpdateHealthUI();

        // ���� ���� �г� �ʱ� ��Ȱ��ȭ
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // ���� ���� ���¿��� ���콺 Ŭ���� ����
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
            Destroy(collision.gameObject); // �Ҿ� ����
        }
    }

    void TakeDamage(int damage)
    {
        if (isGameOver) return; // ���� ���� ���¿����� �� �̻� �������� ���� ����

        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthUI()
    {
        // ���� ����� ���� �̹��� Ȱ��ȭ/��Ȱ��ȭ ����
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].enabled = true; // Ȱ��ȭ
            }
            else
            {
                healthImages[i].enabled = false; // ��Ȱ��ȭ
            }
        }
    }

    void GameOver()
    {
        // ���� ���� ���� ����
        isGameOver = true;

        // ���� ���� �гΰ� ��ư Ȱ��ȭ
        gameOverPanel.SetActive(true);

        // ���� �Ͻ� ����
        Time.timeScale = 0;
    }

    void RestartGame()
    {
        // ���� �����: ���� ���� �ٽ� �ε�
        Time.timeScale = 1; // ���� �ӵ� ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
