namespace PlayerContolloer
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UI_boolManager;
    using System.Collections;

    /// <summary>
    /// 플레이어가 직접 움직이며 행더동하게될 스크립트 입니다.
    /// 이곳에서 플레이어의 모든것을 처리합니다/
    /// </summary>
    public class Player_Controller : WeaponTypeClass , IDamagebal
    {
        [SerializeField] private Camera MainCmaera;

        private WaitForSeconds Tick = new WaitForSeconds(0.1f);

        private CharacterController controller;
        private Animation_Controller AC;

        private float PosX, PosY;
        public float zoomInSpeed = 50f;
        public float fireTime =0.3f;            //탄발당 쏘는 간격
        float lastAttackTime = 0;               //마지막 발사시간 체크용
        public float attackDistance = 150f;            //공격 사거리;
        bool isReload = false;      //재장전
        public bool isZoom = false;
        /// <summary>
        /// 더블탭 변수
        /// </summary>
        bool W_isdoubleTap = false;   
        bool A_isdoubleTap = false;
        bool D_isdoubleTap = false;
        private int W_doubletapcount;
        private int D_doubletapcount;
        private int A_doubletapcount;
        public float DoubleTapLifeTime = 0;       //더블탭 활성화 후 남은시간
        public float DoubleTapCoolTime = 0.2f;    //하위 3개의 시간 관리용
        private float W_doubletapcurTime = 0;     //더블탭 활성화 시 제약 시간
        private float A_doubletapcurTime = 0;     //더블탭 활성화 시 제약 시간
        private float D_doubletapcurTime = 0;     //더블탭 활성화 시 제약 시간

        /// <summary>
        /// 손전등에 관한 변수 입니다.
        /// </summary>
        public GameObject Flashlight;

        /// <summary>
        /// 무기 타입의 관한 타입형 변수 입니다.
        /// </summary>
        public WeaponType weaponeTypeValue = 0;
        public int weaponIndexCount = 0;
        public float normalWeaponFireSpeed = 50f;

        //0 = 피스톨
        //1 = akm
        [SerializeField] private Transform ShotingPosition;       //총알 발사시 생성 위치
        [SerializeField] private Transform[] Hands;                 //손 프리팹 

        [SerializeField] private Transform[] null_HandPosition;     //아무것도 없을때

        [Header("Hand Gun Assets")]
        //권총에 쓰이는 변수
        public int handGun_HaveAmount;     //가지고 있는 총알 수.
        public int handGun_CurAmount;      //현재 쏠수 있는 총알 갯수
        public int handGun_MaxAmount = 12;
        [SerializeField] private GameObject handGun_Prefab;         //권총 걍 맵에 있는 총 주면됨
        //[SerializeField] private GameObject handGun_Magazine;     //권총 탄창 프리팹
        [SerializeField] private Transform[] handGun_HandPosition;  //권총 손 위치 프리팹 
        [SerializeField] private AudioClip handGun_AudioFireClip;                   //HandGun Fire Sound
        [SerializeField] private AudioClip handGun_AudioReloadClip;                 //HandGun Reload Sound
        [SerializeField] private AudioClip handGun_AudioNullAmountFireClip;         //권총 발사 소리


        public GameObject muzzleFlashPrefab;                //플래쉬 이펙트
        [SerializeField] private float destroyTimer = 2f;   //사라질 시간

        [Header("AKM Assets")]
        //AKM에 쓰이는 변수   //안씀
        //private int akm_CurAmount;
        //private int akm_MaxAmount = 30;
        //[SerializeField] private GameObject akmPrefab;              //걍 맵에 있는 총 주면됨
        //[SerializeField] private GameObject akm_Magazine;           //AKM 탄창 프리팹
        //[SerializeField] private Transform[] akm_HandPosition;      //AKM 손 위치 프리팹 

        //플레이어 움직임 값 위치 저장
        Vector3 direction, MoveForce;

        //[Header("VFX")][SerializeField] private GameObject hitVFX;
        private void Start()
        {
            StartCoroutine();
            SetStartValue();

            controller = GetComponentInChildren<CharacterController>();
            AC = GetComponent<Animation_Controller>();
        }
        private void SetStartValue()
        {
            handGun_CurAmount = handGun_MaxAmount;
            //akm_CurAmount = akm_MaxAmount;

            handGun_Prefab.SetActive(false);
            isZoom = false;
            //akmPrefab.SetActive(false);
        }
        void StartCoroutine()
        {
            StartCoroutine(CheackGetGun_HandGun());
            StartCoroutine(CheackGetLight_Flashlight());

            //StartCoroutine(CheackGetGun_AKM());
        }
        private void Update()
        {
            if(!ValueManager.setMenu)
            {
                UpdateInputKey();
                PlayerController();
            }
        }
        void UpdateInputKey()
        {
            PosY = Input.GetAxisRaw("Vertical");
            PosX = Input.GetAxisRaw("Horizontal");

            //더블 탭 활성화 조건문
            Input_DobuleTap();
            Input_Zooming();
            if(Input.GetKeyDown(PlayerStats.Instance.K_Shot))
            {
                Input_Fire();
            }
            //무기 타입을 변경 하는 용도
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(PlayerSituationCheack.getHandGun)
                weaponIndexCount = 1;
            }
            //if(Input.GetKeyDown(KeyCode.Alpha2)) 
            //{
            //    if(PlayerSituationCheack.getAKM)
            //    weaponIndexCount = 2;
            //}


            switch(weaponIndexCount)
            {
                case 0:
                    weaponeTypeValue = WeaponType.Null; 
                break;

                case 1:
                    weaponeTypeValue = WeaponType.Handgun;
                    handGun_Prefab.SetActive(true);
                    //akmPrefab.SetActive(false);
                break;

                //case 2:
                //    weaponeTypeValue = WeaponType.AKM;
                //    handGun_Prefab.SetActive(false);
                //    akmPrefab.SetActive(true);
                //    break;
            }
        }
        void Input_DobuleTap()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //더블탭 임시 활성화
                W_isdoubleTap = true;
                W_doubletapcount++;
                if (W_doubletapcount == 2)
                {
                    W_Dash();
                    Invoke("ReDash", 0.1f);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                //더블탭 임시 활성화
                A_isdoubleTap = true;
                A_doubletapcount++;
                if (A_doubletapcount == 2)
                {
                    A_Dash();
                    Invoke("ReDash", 0.1f);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //더블탭 임시 활성화
                D_isdoubleTap = true;
                D_doubletapcount++;
                if (D_doubletapcount == 2)
                {
                    D_Dash();
                    Invoke("ReDash", 0.1f);
                }
            }
        }

        #region 더블 탭

        void DoubleTapColeTime() //더블탭 시간
        {
            W_DobuleTapColeTimeCheack();
            A_DobuleTapColeTimeCheack();
            D_DobuleTapColeTimeCheack();
        }
        void W_DobuleTapColeTimeCheack()
        {
            if (!W_isdoubleTap)   //Ture가 되기 전까진 실행 안함.
                return;

            A_isdoubleTap = false;
            D_isdoubleTap = false;
            if (W_doubletapcurTime < DoubleTapCoolTime)
            {
                W_doubletapcurTime += Time.deltaTime;
            }
            else
            {
                //제한 시간이 지날시 초기화
                W_isdoubleTap = false;
                W_doubletapcurTime = 0;
                W_doubletapcount = 0;

                return;
            }
        }
        void A_DobuleTapColeTimeCheack()
        {
            if (!A_isdoubleTap)   //Ture가 되기 전까진 실행 안함.
                return;

            D_isdoubleTap = false;
            W_isdoubleTap = false;
            if (A_doubletapcurTime < DoubleTapCoolTime)
            {
                A_doubletapcurTime += Time.deltaTime;
            }
            else
            {
                //제한 시간이 지날시 초기화
                A_isdoubleTap = false;
                A_doubletapcurTime = 0;
                A_doubletapcount = 0;

                return;
            }
        }
        void D_DobuleTapColeTimeCheack()
        {
            if (!D_isdoubleTap)   //Ture가 되기 전까진 실행 안함.
                return;
            //중복 대쉬하지 않게 하기 위한 초기화
            A_isdoubleTap = false;
            W_isdoubleTap = false;
            if (D_doubletapcurTime < DoubleTapCoolTime)
            {
                D_doubletapcurTime += Time.deltaTime;
            }
            else
            {
                //제한 시간이 지날시 초기화
                D_isdoubleTap = false;
                D_doubletapcurTime = 0;
                D_doubletapcount = 0;

                return;
            }
        }
        void W_Dash()
        {
            if (W_isdoubleTap)
            {
                if (PlayerSituationCheack.IsRun)
                    return;
                PlayerStats.Instance.speed = 15;
            }
            else PlayerStats.Instance.speed = PlayerStats.Instance.curSpeed;
        }
        void A_Dash()
        {
            if (A_isdoubleTap)
            {
                if (PlayerSituationCheack.IsRun)
                    return;
                PlayerStats.Instance.speed = 15;
            }
            else PlayerStats.Instance.speed = PlayerStats.Instance.curSpeed;
        }
        void D_Dash()
        {
            if (D_isdoubleTap)
            {
                if (PlayerSituationCheack.IsRun)
                    return;
                PlayerStats.Instance.speed = 15;
            }
            else PlayerStats.Instance.speed = PlayerStats.Instance.curSpeed;
        }
        void ReDash()   //대쉬 초기화
        {
            PlayerStats.Instance.speed = PlayerStats.Instance.curSpeed;
        }

        #endregion


        private void PlayerController()
        {
            Jump();                         //플레이어의 점프 및 바닥으로 내려오게 하는 함수
            Moveing();                      //움직임 및 달리기를 관리하는 함수
            DoubleTapColeTime();            //더블탭 활성화시 실행됨
            SetUpdate_Move_Animation();     //움직임 애니메이션을 관리하는 함수
            HandTransform();                //손의 위치를 총의 집는 부분으로 이동시키는 함수
        }

        #region Player Move Controller magazine

        void Moveing()
        {
            direction = transform.rotation * new Vector3(PosX, transform.position.y, PosY);
            MoveForce = new Vector3(direction.x, MoveForce.y, direction.z);
            if (Input.GetKey(PlayerStats.Instance.K_Run))//달리기 키를 
            {
                if(!isZoom) //줌이 아닌동안
                {
                    controller.Move(MoveForce * PlayerStats.Instance.speed * PlayerStats.Instance.runSpeed * Time.deltaTime);
                    PlayerSituationCheack.IsRun = true;
                }
                else
                {
                    controller.Move(MoveForce * PlayerStats.Instance.speed * Time.deltaTime);
                    PlayerSituationCheack.IsRun = false;
                }
            }
            else //걷기
            {
                controller.Move(MoveForce * PlayerStats.Instance.speed * Time.deltaTime);
                PlayerSituationCheack.IsRun = false;
            }
        }
        void Jump()
        {
            //if (controller.isGrounded)
            //{
            //    if (Input.GetKey(PlayerStats.Instance.K_Jump))
            //        MoveForce.y = PlayerStats.Instance.JumpForce;
            //}
            //땅에 안닿고 있다면, 떨어지게 만듬.
            if (!controller.isGrounded)
            {
                MoveForce.y += -20 * Time.deltaTime;
            }
        }

        #endregion

        void Input_Fire()
        {
            RaycastHit hit;
            switch (weaponeTypeValue)
            {
                case WeaponType.Handgun:
                    Debug.Log("1");
                    if(handGun_CurAmount > 0)
                    {
                        handGun_CurAmount--;
                        FireEffect();
                        Debug.DrawRay(ShotingPosition.position, ShotingPosition.forward * 100, Color.red, 1);
                        if(Physics.Raycast(ShotingPosition.position, ShotingPosition.forward, out hit, attackDistance))
                        {
                            M_SFXSound.instance.PlayFireAudioClip(handGun_AudioFireClip, ShotingPosition, 1f);
                            IDamagebal damageable = hit.transform.GetComponent<IDamagebal>();
                            if (damageable != null)
                            {
                                Debug.Log("Hit");
                                damageable.Damaged(PlayerStats.Instance.Damage);
                            }
                        }
                        AC.Fire(weaponeTypeValue);
                    }
                    else
                    {
                        M_SFXSound.instance.PlayNullAmountFireClip(handGun_AudioNullAmountFireClip,1f);
                    }
                    return;
                    //a
                //case WeaponType.AKM:
                //    if(akm_CurAmount > 0)
                //    {
                //        akm_CurAmount--;
                //        if (Physics.Raycast(ray.origin, ray.direction, out hit, 500f))
                //        {
                //            if (hit.transform.GetComponent<IDamagebal>() != null)
                //            {
                //                IDamagebal damagebal = hit.transform.GetComponent<IDamagebal>();
                //                damagebal.Damaged(15);
                //            }
                //        }
                //        AC.Fire(weaponeTypeValue);
                //    }
                //    return;
            }
        }
        void FireEffect()
        {
            if (muzzleFlashPrefab)
            {
                //Create the muzzle flash
                GameObject tempFlash;
                tempFlash = Instantiate(muzzleFlashPrefab, ShotingPosition.position, ShotingPosition.rotation);

                //Destroy the muzzle flash effect
                Destroy(tempFlash, destroyTimer);
            }
        }

        void Input_Zooming()
        {
            //줌 인 아웃 조건문
            if (Input.GetKey(PlayerStats.Instance.K_Zoom))
            {
                if (!isReload)
                {
                    isZoom = true;
                    AC.Zooming(true, weaponeTypeValue);
                }
                else
                {
                    AC.Zooming(false, weaponeTypeValue);
                }
            }
            else if(Input.GetKeyUp(PlayerStats.Instance.K_Zoom))
            {
                isZoom = false;
                AC.Zooming(false, weaponeTypeValue);
            }
        }
        void SetUpdate_Move_Animation()//움직임 애니메이션 실행 관리
        {
            //Player walk CheackIn a Animation
            MoveAnimaton();

            //재장전 조건문
            if(Input.GetKeyDown(PlayerStats.Instance.K_Reload))
            {
                isReload = true;
                AC.Reload(isReload, weaponeTypeValue);
                switch (weaponeTypeValue)
                {
                    case WeaponType.Handgun:
                        StartCoroutine(HandGun_ReloadReturn());
                    return;

                    //case WeaponType.AKM:
                    //    StartCoroutine(AKM_ReloadReturn());
                    //return;
                }
            }
        }
        void MoveAnimaton()
        {
            if (PosX != 0 || PosY != 0)
            {
                AC.Movemnent((int)PosX + (int)PosY,PlayerSituationCheack.IsRun, weaponeTypeValue);
            }
            else
            {
                AC.Movemnent(0, false, weaponeTypeValue); //움직이지 않을때는 기본으로 초기화
            }
        }
        //손 위치 조정용
        void HandTransform()
        {
            switch(weaponeTypeValue)
            {
                case WeaponType.Null:
                    HandPosition_Null();
                    return;
                case WeaponType.Handgun: HandPosition_HandGun();
                    return;
                //case WeaponType.AKM: HandPosition_AKM();
                //    return;
            }
        }
        void HandPosition_Null()
        {
            for (int i = 0; i < Hands.Length; i++)
            {
                Hands[i].position = null_HandPosition[i].position;
            }
        }
        void HandPosition_HandGun()
        {
            for (int i = 0; i < Hands.Length; i++)
            {
                Hands[i].position = handGun_HandPosition[i].position;
            }
        }
        //void HandPosition_AKM()
        //{
        //    for (int i = 0; i < Hands.Length; i++)
        //    {
        //        Hands[i].position = akm_HandPosition[i].position;
        //    }
        //}


        public void Damaged(float Damage)
        {
            if (Damage <= PlayerStats.Instance.defance)//공격력이 방어력 보다 낮을경우 고정 1을 준다.
            {
                if (Damage < PlayerStats.Instance.defance - (Damage + 1))
                {
                    Debug.Log("허접 쉑");
                }
                else PlayerStats.Instance.hp -= 1;
            }
            else//공격력이 방어력 보다 높을경우 실행 
                PlayerStats.Instance.hp += PlayerStats.Instance.defance - (int)Damage;
        }

        IEnumerator HandGun_ReloadReturn()
        {
            M_SFXSound.instance.PlayRelaodAudioClip(handGun_AudioReloadClip, 1f);
            yield return new WaitForSeconds(1.3f);

            if (handGun_HaveAmount > 0)
            {
                int a = 0;
                //현재 장전되어 있는 총알이 있을경우 그만큼 빼서 넣는다.
                if(handGun_CurAmount > 0)
                {
                    int bv = handGun_MaxAmount - handGun_CurAmount;
                    for (int i = 0; i < bv; i++)
                    {
                        if (handGun_HaveAmount > 0)
                        {
                            handGun_HaveAmount--;
                            a++;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < handGun_MaxAmount; i++)
                    {
                        if (handGun_HaveAmount > 0)
                        {
                            handGun_HaveAmount--;
                            a++;
                        }
                    }
                }
                handGun_CurAmount += a;
            }

            isReload = false;
            AC.Reload(isReload, weaponeTypeValue);
            yield break;
        }
        //IEnumerator AKM_ReloadReturn()
        //{
        //    yield return new WaitForSeconds(2.5f);
        //    akm_CurAmount = akm_MaxAmount;
        //    isReload = false;
        //    AC.Reload(isReload, weaponeTypeValue);
        //    yield break;
        //}

        IEnumerator CheackGetGun_HandGun()
        {
            while(true)
            {
                yield return Tick;
                if (PlayerSituationCheack.getHandGun == true)
                {
                    weaponIndexCount = 1;
                    yield break;
                }

            }
        }
        IEnumerator CheackGetLight_Flashlight()
        {
            while (true)
            {
                yield return Tick;
                if (PlayerSituationCheack.getFlashlight == true)
                {
                    Flashlight.SetActive(true);
                    yield break;
                }
            }
        }
        //IEnumerator CheackGetGun_AKM()
        //{
        //    while(true)
        //    {
        //        yield return new WaitForSeconds(0.1f);
        //        if (PlayerSituationCheack.getAKM == true)
        //        {
        //            weaponIndexCount = 2;
        //            yield break;
        //        }
        //    }
        //}
    }
}
