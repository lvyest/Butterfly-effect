using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class UIManager : MonoBehaviour
{
    public UnityEngine.UI.Text playerSizeText;  // �÷��̾� ũ�� �ؽ�Ʈ�� ǥ���ϴ� UI ���
    public UnityEngine.UI.Text bulletCountText;  // ���� �Ѿ� ���� ǥ���ϴ� UI ���
    public UnityEngine.UI.Image gameOverImage;   // ���� ���� �̹����� ǥ���ϴ� UI ���
    public UnityEngine.UI.Image gameClearImage;  // ���� Ŭ���� �̹����� ǥ���ϴ� UI ���

    public float plusSize; // �߰����� ũ�⸦ ǥ���ϴ� ����
    public string nextSceneName;  // ���� ���� �̸��� �����ϴ� �ʵ�

    private PlayerMovev1 playerMove;  // PlayerMovev1 ��ũ��Ʈ�� �����ϱ� ���� ���� ����
    private Bullet bullet;  // Bullet ��ũ��Ʈ�� �����ϱ� ���� ���� ����
    private bool isGameClear = false; // ���� Ŭ���� ���¸� Ȯ���ϴ� �÷���
    private bool isGameOver = false; // ���� ���� ���¸� Ȯ���ϴ� �÷���

    private void Start()
    {
        // PlayerMovev1 �� Bullet ��ũ��Ʈ�� ����
        playerMove = GameObject.FindObjectOfType<PlayerMovev1>(); // PlayerMovev1 ��ü�� ã�Ƽ� ������ ����
        bullet = GameObject.FindObjectOfType<Bullet>(); // Bullet ��ü�� ã�Ƽ� ������ ����

        // �ʱ⿡�� ���� ������ ���� Ŭ���� �̹����� ��Ȱ��ȭ
        gameOverImage.gameObject.SetActive(false); // ���� ���� �̹����� ��Ȱ��ȭ
        gameClearImage.gameObject.SetActive(false); // ���� Ŭ���� �̹����� ��Ȱ��ȭ
    }

    // �� �����Ӹ��� UI�� ������Ʈ�ϴ� �޼���
    private void Update()
    {
        if (playerMove != null && bullet != null)
        {
            // �÷��̾��� ũ��� ���� �Ѿ� ���� UI�� ������Ʈ
            UpdateUI(playerMove.GetPlayerSize(), bullet.bulletuseable);
        }

        // ���� ���� ���¿��� ���콺 Ŭ���� �����Ͽ� ������ �����
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            RestartGame(); // ���� ����� �޼��� ȣ��
        }

        // ���� Ŭ���� ���¿��� ���콺 Ŭ���� �����Ͽ� ���� ������ �̵�
        if (isGameClear && Input.GetMouseButtonDown(0))
        {
            LoadNextScene(); // ���� �� �ε� �޼��� ȣ��
        }
    }

    // �÷��̾��� ũ��� ���� �Ѿ� ���� UI�� ������Ʈ�ϴ� �޼���
    private void UpdateUI(float playerSize, int bulletuseable)
    {
        float showSize = playerSize + plusSize; // �߰� ũ�⸦ ���� �÷��̾� ũ�⸦ ���
        playerSizeText.text = "�÷��̾� ũ��: " + showSize.ToString("F2"); // �÷��̾� ũ�⸦ �ؽ�Ʈ�� ǥ��
        bulletCountText.text = "����ų �� �ִ� ���� ��ǳ: " + bulletuseable.ToString(); // ���� �Ѿ� ���� �ؽ�Ʈ�� ǥ��
    }

    // ���� ���� UI�� ǥ���ϴ� �޼���
    public void ShowGameOverUI()
    {
        gameOverImage.gameObject.SetActive(true); // ���� ���� �̹����� Ȱ��ȭ
        isGameOver = true; // ���� ���� ���·� ����
    }

    // ���� Ŭ���� UI�� ǥ���ϴ� �޼���
    public void ShowGameClearUI()
    {
        gameClearImage.gameObject.SetActive(true); // ���� Ŭ���� �̹����� Ȱ��ȭ
        isGameClear = true; // ���� Ŭ���� ���·� ����
    }

    // ������ ������ϴ� �޼���
    private void RestartGame()
    {
        // ���� ���� �ٽ� �ε��Ͽ� ������ �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� �ٽ� �ε�
    }

    // Inspector���� ������ ���� ������ �̵��ϴ� �޼���
    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); // Inspector���� ������ ���� ���� �ε�
    }
}
