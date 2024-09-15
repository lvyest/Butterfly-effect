using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class UIManager : MonoBehaviour
{
    public UnityEngine.UI.Text playerSizeText;  // 플레이어 크기 텍스트를 표시하는 UI 요소
    public UnityEngine.UI.Text bulletCountText;  // 남은 총알 수를 표시하는 UI 요소
    public UnityEngine.UI.Image gameOverImage;   // 게임 오버 이미지를 표시하는 UI 요소
    public UnityEngine.UI.Image gameClearImage;  // 게임 클리어 이미지를 표시하는 UI 요소

    public float plusSize; // 추가적인 크기를 표시하는 변수
    public string nextSceneName;  // 다음 씬의 이름을 저장하는 필드

    private PlayerMovev1 playerMove;  // PlayerMovev1 스크립트를 참조하기 위한 변수 선언
    private Bullet bullet;  // Bullet 스크립트를 참조하기 위한 변수 선언
    private bool isGameClear = false; // 게임 클리어 상태를 확인하는 플래그
    private bool isGameOver = false; // 게임 오버 상태를 확인하는 플래그

    private void Start()
    {
        // PlayerMovev1 및 Bullet 스크립트에 접근
        playerMove = GameObject.FindObjectOfType<PlayerMovev1>(); // PlayerMovev1 객체를 찾아서 참조를 저장
        bullet = GameObject.FindObjectOfType<Bullet>(); // Bullet 객체를 찾아서 참조를 저장

        // 초기에는 게임 오버와 게임 클리어 이미지를 비활성화
        gameOverImage.gameObject.SetActive(false); // 게임 오버 이미지를 비활성화
        gameClearImage.gameObject.SetActive(false); // 게임 클리어 이미지를 비활성화
    }

    // 매 프레임마다 UI를 업데이트하는 메서드
    private void Update()
    {
        if (playerMove != null && bullet != null)
        {
            // 플레이어의 크기와 남은 총알 수를 UI에 업데이트
            UpdateUI(playerMove.GetPlayerSize(), bullet.bulletuseable);
        }

        // 게임 오버 상태에서 마우스 클릭을 감지하여 게임을 재시작
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            RestartGame(); // 게임 재시작 메서드 호출
        }

        // 게임 클리어 상태에서 마우스 클릭을 감지하여 다음 씬으로 이동
        if (isGameClear && Input.GetMouseButtonDown(0))
        {
            LoadNextScene(); // 다음 씬 로드 메서드 호출
        }
    }

    // 플레이어의 크기와 남은 총알 수를 UI에 업데이트하는 메서드
    private void UpdateUI(float playerSize, int bulletuseable)
    {
        float showSize = playerSize + plusSize; // 추가 크기를 더한 플레이어 크기를 계산
        playerSizeText.text = "플레이어 크기: " + showSize.ToString("F2"); // 플레이어 크기를 텍스트로 표시
        bulletCountText.text = "일으킬 수 있는 작은 태풍: " + bulletuseable.ToString(); // 남은 총알 수를 텍스트로 표시
    }

    // 게임 오버 UI를 표시하는 메서드
    public void ShowGameOverUI()
    {
        gameOverImage.gameObject.SetActive(true); // 게임 오버 이미지를 활성화
        isGameOver = true; // 게임 오버 상태로 설정
    }

    // 게임 클리어 UI를 표시하는 메서드
    public void ShowGameClearUI()
    {
        gameClearImage.gameObject.SetActive(true); // 게임 클리어 이미지를 활성화
        isGameClear = true; // 게임 클리어 상태로 설정
    }

    // 게임을 재시작하는 메서드
    private void RestartGame()
    {
        // 현재 씬을 다시 로드하여 게임을 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬을 다시 로드
    }

    // Inspector에서 설정한 다음 씬으로 이동하는 메서드
    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); // Inspector에서 설정한 다음 씬을 로드
    }
}
