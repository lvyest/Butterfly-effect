using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using System.Collections; 

public class GaugeControl2 : MonoBehaviour
{
    public Slider gaugeSlider; // UI 슬라이더
    public GameObject fanBlades; // 선풍기 날 오브젝트
    public float increaseRate = 10f; // 게이지 증가 속도
    public float decreaseAmount = 1f; // 스페이스바 누를 때 감소량

    public Animator butterflyAnimator; // 나비의 애니메이터
    public float initialFanBladeSpeed = 300f; // 선풍기 날 초기 회전 속도
    public float maxFanBladeSpeed = 600f; // 선풍기 날 최대 회전 속도
    public float fanBladeSpeedIncreaseRate = 50f; // 선풍기 날 회전 속도 증가율

    public GameObject[] obstacles; // 장애물 오브젝트들
    public float pushForce = 10f; // 장애물에 가해질 힘

    public Slider spinningGauge; // 새로운 게이지 슬라이더
    public float spinningGaugeIncreaseRate = 5f; // 새로운 게이지 증가 속도

    public GameObject gameClearImage; // 게임 클리어 이미지 오브젝트

    private float gaugeValue = 200f; // 게이지 초기값
    private bool isSpinningGaugeActive = false; // spinningGauge 활성화 여부
    private int currentObstacleIndex = 0; // 현재 밀어낼 장애물의 인덱스
    private bool isFanSpinning = false; // 선풍기 회전 여부
    private float currentFanBladeSpeed; // 현재 선풍기 회전 속도
    private bool isGameClear = false; // 게임 클리어 상태 플래그

    void Start() { 
        gaugeSlider.value = gaugeValue; // 초기 게이지 값 설정

        spinningGauge.gameObject.SetActive(false); // spinningGauge 비활성화
        spinningGauge.maxValue = 20f; // spinningGauge 최대값 설정
        spinningGauge.value = 0f; // spinningGauge 초기값 설정

        currentFanBladeSpeed = initialFanBladeSpeed; // 선풍기 회전 속도 초기화

        gameClearImage.SetActive(false); // 게임 클리어 이미지 비활성화
    }

    void Update() 
    {
        if (isGameClear) // 게임 클리어 상태 확인
        {
            if (Input.GetMouseButtonDown(0)) // 마우스 버튼 클릭 확인
            {
                SceneManager.LoadScene("Stage3-1"); // "Stage3-1" 씬 로드
            }
            return; // 게임 클리어 상태에서는 더 이상 업데이트하지 않음
        }

        fanBlades.transform.Rotate(Vector3.forward * currentFanBladeSpeed * Time.deltaTime); // 선풍기 날 회전

        if (gaugeValue < 200f) // gaugeSlider가 200보다 작은 경우
        {
            gaugeValue += increaseRate * Time.deltaTime; // 게이지 증가
            gaugeSlider.value = gaugeValue; // 슬라이더 업데이트
        }

        if (gaugeValue <= 20f) // gaugeSlider가 20 이하인 경우
        {
            if (!isSpinningGaugeActive) // spinningGauge가 활성화되지 않은 경우
            {
                ActivateSpinningGauge(); // spinningGauge 활성화
            }
            IncreaseSpinningGauge(); // spinningGauge 증가
            RotateFanBlades(); // 선풍기 회전
        }
        else // gaugeSlider가 20 이상인 경우
        {
            if (isSpinningGaugeActive) // spinningGauge가 활성화된 경우
            {
                ResetSpinningGauge(); // spinningGauge 초기화 및 비활성화
            }
            StopFanBlades(); // 선풍기 정지
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바 입력 확인
        {
            OnSpacebarPress(); // 스페이스바 눌림 처리
        }

        if (spinningGauge.value >= 20f) // spinningGauge가 최대값에 도달한 경우
        {
            HandleSpinningGaugeFull(); // spinningGauge 가득 찼을 때 처리
        }

        if (AllObstaclesAreGone()) // 모든 장애물이 제거되었는지 확인
        {
            GameClear(); // 게임 클리어 처리
        }
    }

    void OnSpacebarPress() // 스페이스바 눌림 처리 메서드
    {
        gaugeValue -= decreaseAmount; // 게이지 감소

        if (gaugeValue < 0f) // gaugeSlider 값이 0 이하로 내려가지 않도록 제한
        {
            gaugeValue = 0f; // 최소값으로 설정
        }

        gaugeSlider.value = gaugeValue; // 슬라이더 업데이트

        butterflyAnimator.SetTrigger("flap"); // 나비 날갯짓 애니메이션 트리거
    }

    void ActivateSpinningGauge() // spinningGauge 활성화 메서드
    {
        spinningGauge.gameObject.SetActive(true); // spinningGauge 활성화
        spinningGauge.value = 0f; // spinningGauge 초기화
        isSpinningGaugeActive = true; // spinningGauge 활성화 상태 설정
        isFanSpinning = true; // 선풍기 회전 상태 설정
        currentFanBladeSpeed = initialFanBladeSpeed; // 선풍기 회전 속도 초기화
    }

    void IncreaseSpinningGauge() // spinningGauge 증가 메서드
    {
        spinningGauge.value += spinningGaugeIncreaseRate * Time.deltaTime; // spinningGauge 증가
    }

    void ResetSpinningGauge() // spinningGauge 초기화 및 비활성화 메서드
    {
        spinningGauge.value = 0f; // spinningGauge 초기화
        spinningGauge.gameObject.SetActive(false); // spinningGauge 비활성화
        isSpinningGaugeActive = false; // spinningGauge 활성화 상태 해제
    }

    void HandleSpinningGaugeFull() // spinningGauge가 가득 찼을 때 처리 메서드
    {
        PushCurrentObstacle(); // 현재 장애물 밀어내기

        gaugeValue = 200f; // gaugeSlider 초기화
        gaugeSlider.value = gaugeValue; // 슬라이더 업데이트

        ResetSpinningGauge(); // spinningGauge 초기화 및 비활성화

        currentObstacleIndex++; // 다음 장애물로 이동
        if (currentObstacleIndex >= obstacles.Length) // 모든 장애물을 처리한 경우
        {
            currentObstacleIndex = 0; // 인덱스를 처음으로 되돌림
        }
    }

    void RotateFanBlades() // 선풍기 회전 메서드
    {
        if (isFanSpinning) // 선풍기가 회전 중인지 확인
        {
            fanBlades.transform.Rotate(Vector3.forward * currentFanBladeSpeed * Time.deltaTime); // 선풍기 날 회전

            currentFanBladeSpeed += fanBladeSpeedIncreaseRate * Time.deltaTime; // 회전 속도 증가

            if (currentFanBladeSpeed > maxFanBladeSpeed) // 최대 회전 속도 제한
            {
                currentFanBladeSpeed = maxFanBladeSpeed; // 최대 속도로 설정
            }
        }
    }

    void StopFanBlades() // 선풍기 정지 메서드
    {
        if (isFanSpinning) // 선풍기가 회전 중인지 확인
        {
            isFanSpinning = false; // 선풍기 회전 상태 해제
            currentFanBladeSpeed = initialFanBladeSpeed; // 선풍기 속도 초기화
        }
    }

    void PushCurrentObstacle() // 현재 장애물 밀어내기 메서드
    {
        if (currentObstacleIndex < obstacles.Length) // 유효한 장애물 인덱스인지 확인
        {
            GameObject obstacle = obstacles[currentObstacleIndex]; // 현재 장애물 참조
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>(); // 장애물의 Rigidbody2D 참조
            if (rb != null) // Rigidbody2D가 있는지 확인
            {
                Vector2 pushDirection = (obstacle.transform.position - fanBlades.transform.position).normalized; // 밀어낼 방향 계산
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // 힘을 가하여 장애물 밀어내기
            }
        }
    }

    bool AllObstaclesAreGone() // 모든 장애물이 제거되었는지 확인하는 메서드
    {
        foreach (GameObject obstacle in obstacles) // 각 장애물에 대해 반복
        {
            if (obstacle.activeInHierarchy && obstacle.transform.position.magnitude < 10f) // 장애물이 활성 상태인지 및 충분히 멀리 있는지 확인
            {
                return false; // 아직 제거되지 않은 장애물이 있음
            }
        }
        return true; // 모든 장애물이 제거됨
    }

    void GameClear() // 게임 클리어 처리 메서드
    {
        isGameClear = true; // 게임 클리어 상태 설정
        gameClearImage.SetActive(true); // 게임 클리어 이미지 활성화
        StopFanBlades(); // 선풍기 회전 정지
    }
}
