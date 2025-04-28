namespace UI_SettingManager
{
    using UnityEngine;
    using static UnityEngine.PlayerPrefs;
    /// <summary>
    /// 플레이어의 게임 플레이에 대해 설정할수 있게 해주는 스크립트 입니다.
    /// 플레이어의 인게임 설정 관리 메니저 입니다,
    /// 게임의 직접적인 영향은 없으나, 간적접인 영향은 있습니다.
    /// </summary>

    public class M_GamePlaySetting : MonoBehaviour
    {
        public static M_GamePlaySetting instance;
        //화면 감도 //마우스 감도
        public float screenSensitivityValue;

        //Music 관리
        public float masterMusicValue;
        public float bgmMusicValue;
        public float sfxMusicValue;

        /// <summary>
        /// 설정창에서 나갈때마다 저장된다.
        /// <summary>
        public void Save()
        {
            SetInt("Screen Sensitivity", (int)screenSensitivityValue);
            SetInt("Master Music Value", (int)masterMusicValue);
            SetInt("Bgm Music Value", (int)bgmMusicValue);
            SetInt("Sfx Music Value", (int)sfxMusicValue);
        }
        /// <summary>
        /// 게임이 시작되자마자 불러와 진다.
        /// </summary>
        public void Load()
        {
            if (HasKey("Screen Sensitivity"))
            {
                //기존 데이터 불러오기
                screenSensitivityValue = GetInt("Screen Sensitivity");
                masterMusicValue = GetInt("Master Music Value");
                bgmMusicValue = GetInt("Bgm Music Value");
                sfxMusicValue = GetInt("Sfx Music Value");
            }
        }
    }
}
namespace UI_boolManager
{
    /// <summary>
    /// 게임의 직접적으로 영향을 끼칠 논리형 변수를 관리합니다.
    /// </summary>
    public static class ValueManager
    {
        //메뉴 창인지 확인 용도.
        public static bool setMenu = false;

        //주인공이 죽었는지 확인 용도.
        public static bool playerDie = false;

        //더 있다면 추가
    }
}