namespace UI_SettingManager
{
    using UnityEngine;
    using static UnityEngine.PlayerPrefs;
    /// <summary>
    /// �÷��̾��� ���� �÷��̿� ���� �����Ҽ� �ְ� ���ִ� ��ũ��Ʈ �Դϴ�.
    /// �÷��̾��� �ΰ��� ���� ���� �޴��� �Դϴ�,
    /// ������ �������� ������ ������, �������� ������ �ֽ��ϴ�.
    /// </summary>

    public class M_GamePlaySetting : MonoBehaviour
    {
        public static M_GamePlaySetting instance;
        //ȭ�� ���� //���콺 ����
        public float screenSensitivityValue;

        //Music ����
        public float masterMusicValue;
        public float bgmMusicValue;
        public float sfxMusicValue;

        /// <summary>
        /// ����â���� ���������� ����ȴ�.
        /// <summary>
        public void Save()
        {
            SetInt("Screen Sensitivity", (int)screenSensitivityValue);
            SetInt("Master Music Value", (int)masterMusicValue);
            SetInt("Bgm Music Value", (int)bgmMusicValue);
            SetInt("Sfx Music Value", (int)sfxMusicValue);
        }
        /// <summary>
        /// ������ ���۵��ڸ��� �ҷ��� ����.
        /// </summary>
        public void Load()
        {
            if (HasKey("Screen Sensitivity"))
            {
                //���� ������ �ҷ�����
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
    /// ������ ���������� ������ ��ĥ ���� ������ �����մϴ�.
    /// </summary>
    public static class ValueManager
    {
        //�޴� â���� Ȯ�� �뵵.
        public static bool setMenu = false;

        //���ΰ��� �׾����� Ȯ�� �뵵.
        public static bool playerDie = false;

        //�� �ִٸ� �߰�
    }
}