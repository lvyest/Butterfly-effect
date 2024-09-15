using UnityEngine; 
using UnityEngine.UI; 
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // 페이드 효과 적용 Image 컴포넌트 연결
    public float fadeSpeed = 1f; // 페이드 속도 설정
                                 
                                 
    private void Start() 
    {
        StartCoroutine(FadeIn()); //FadeIn 코루틴 시작
    }

    public IEnumerator FadeIn() // 페이드인 코루틴 메서드 
    {
        fadeImage.gameObject.SetActive(true); // 페이드 이미지 게임 오브젝트 활성화
        fadeImage.color = Color.black; // 페이드 이미지의 색상 설정해 불투명하게

        while (fadeImage.color.a > 0) // 이미지의 알파 값이 0보다 클 때까지 반복
        {
            fadeImage.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime); // 알파 값 감소시켜 점점 투명하게
            yield return null; // 다음 프레임까지 대기
        }

        fadeImage.gameObject.SetActive(false); // 페이드 이미지 게임 오브젝트 비활성화
    }

    public IEnumerator FadeOut(string sceneName) // 페이드 아웃(점점 불투명해짐), 새로운 씬 로드하는 루틴 메서드
    {
        fadeImage.gameObject.SetActive(true); // 페이드 이미지 게임 오브젝트 활성화
        fadeImage.color = new Color(0, 0, 0, 0); // 페이드 이미지의 색상을 투명한 검정색으로 설정.

        while (fadeImage.color.a < 1) // 이미지의 알파 값 1보다 작을 때까지 반복
        {
            fadeImage.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime); // 알파 값을 증가시켜 점점 불투명하게 
            yield return null; // 다음 프레임까지 대기
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); // 주어진 씬 이름을 사용해 새로운 씬을 로드
    }
}
