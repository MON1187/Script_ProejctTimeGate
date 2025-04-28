namespace PlayerContolloer
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UI_boolManager;
    using System.Collections;

    /// <summary>
    /// �÷��̾ ���� �����̸� ������ϰԵ� ��ũ��Ʈ �Դϴ�.
    /// �̰����� �÷��̾��� ������ ó���մϴ�/
    /// </summary>
    public class Player_Controller : WeaponTypeClass , IDamagebal
    {
        [SerializeField] private Camera MainCmaera;

        private WaitForSeconds Tick = new WaitForSeconds(0.1f);

        private CharacterController controller;
        private Animation_Controller AC;

        private float PosX, PosY;
        public float zoomInSpeed = 50f;
        public float fireTime =0.3f;            //ź�ߴ� ��� ����
        float lastAttackTime = 0;               //������ �߻�ð� üũ��
        public float attackDistance = 150f;            //���� ��Ÿ�;
        bool isReload = false;      //������
        public bool isZoom = false;
        /// <summary>
        /// ������ ����
        /// </summary>
        bool W_isdoubleTap = false;   
        bool A_isdoubleTap = false;
        bool D_isdoubleTap = false;
        private int W_doubletapcount;
        private int D_doubletapcount;
        private int A_doubletapcount;
        public float DoubleTapLifeTime = 0;       //������ Ȱ��ȭ �� �����ð�
        public float DoubleTapCoolTime = 0.2f;    //���� 3���� �ð� ������
        private float W_doubletapcurTime = 0;     //������ Ȱ��ȭ �� ���� �ð�
        private float A_doubletapcurTime = 0;     //������ Ȱ��ȭ �� ���� �ð�
        private float D_doubletapcurTime = 0;     //������ Ȱ��ȭ �� ���� �ð�

        /// <summary>
        /// ����� ���� ���� �Դϴ�.
        /// </summary>
        public GameObject Flashlight;

        /// <summary>
        /// ���� Ÿ���� ���� Ÿ���� ���� �Դϴ�.
        /// </summary>
        public WeaponType weaponeTypeValue = 0;
        public int weaponIndexCount = 0;
        public float normalWeaponFireSpeed = 50f;

        //0 = �ǽ���
        //1 = akm
        [SerializeField] private Transform ShotingPosition;       //�Ѿ� �߻�� ���� ��ġ
        [SerializeField] private Transform[] Hands;                 //�� ������ 

        [SerializeField] private Transform[] null_HandPosition;     //�ƹ��͵� ������

        [Header("Hand Gun Assets")]
        //���ѿ� ���̴� ����
        public int handGun_HaveAmount;     //������ �ִ� �Ѿ� ��.
        public int handGun_CurAmount;      //���� ��� �ִ� �Ѿ� ����
        public int handGun_MaxAmount = 12;
        [SerializeField] private GameObject handGun_Prefab;         //���� �� �ʿ� �ִ� �� �ָ��
        //[SerializeField] private GameObject handGun_Magazine;     //���� źâ ������
        [SerializeField] private Transform[] handGun_HandPosition;  //���� �� ��ġ ������ 
        [SerializeField] private AudioClip handGun_AudioFireClip;                   //HandGun Fire Sound
        [SerializeField] private AudioClip handGun_AudioReloadClip;                 //HandGun Reload Sound
        [SerializeField] private AudioClip handGun_AudioNullAmountFireClip;         //���� �߻� �Ҹ�


        public GameObject muzzleFlashPrefab;                //�÷��� ����Ʈ
        [SerializeField] private float destroyTimer = 2f;   //����� �ð�

        [Header("AKM Assets")]
        //AKM�� ���̴� ����   //�Ⱦ�
        //private int akm_CurAmount;
        //private int akm_MaxAmount = 30;
        //[SerializeField] private GameObject akmPrefab;              //�� �ʿ� �ִ� �� �ָ��
        //[SerializeField] private GameObject akm_Magazine;           //AKM źâ ������
        //[SerializeField] private Transform[] akm_HandPosition;      //AKM �� ��ġ ������ 

        //�÷��̾� ������ �� ��ġ ����
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

            //���� �� Ȱ��ȭ ���ǹ�
            Input_DobuleTap();
            Input_Zooming();
            if(Input.GetKeyDown(PlayerStats.Instance.K_Shot))
            {
                Input_Fire();
            }
            //���� Ÿ���� ���� �ϴ� �뵵
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
                //������ �ӽ� Ȱ��ȭ
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
                //������ �ӽ� Ȱ��ȭ
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
                //������ �ӽ� Ȱ��ȭ
                D_isdoubleTap = true;
                D_doubletapcount++;
                if (D_doubletapcount == 2)
                {
                    D_Dash();
                    Invoke("ReDash", 0.1f);
                }
            }
        }

        #region ���� ��

        void DoubleTapColeTime() //������ �ð�
        {
            W_DobuleTapColeTimeCheack();
            A_DobuleTapColeTimeCheack();
            D_DobuleTapColeTimeCheack();
        }
        void W_DobuleTapColeTimeCheack()
        {
            if (!W_isdoubleTap)   //Ture�� �Ǳ� ������ ���� ����.
                return;

            A_isdoubleTap = false;
            D_isdoubleTap = false;
            if (W_doubletapcurTime < DoubleTapCoolTime)
            {
                W_doubletapcurTime += Time.deltaTime;
            }
            else
            {
                //���� �ð��� ������ �ʱ�ȭ
                W_isdoubleTap = false;
                W_doubletapcurTime = 0;
                W_doubletapcount = 0;

                return;
            }
        }
        void A_DobuleTapColeTimeCheack()
        {
            if (!A_isdoubleTap)   //Ture�� �Ǳ� ������ ���� ����.
                return;

            D_isdoubleTap = false;
            W_isdoubleTap = false;
            if (A_doubletapcurTime < DoubleTapCoolTime)
            {
                A_doubletapcurTime += Time.deltaTime;
            }
            else
            {
                //���� �ð��� ������ �ʱ�ȭ
                A_isdoubleTap = false;
                A_doubletapcurTime = 0;
                A_doubletapcount = 0;

                return;
            }
        }
        void D_DobuleTapColeTimeCheack()
        {
            if (!D_isdoubleTap)   //Ture�� �Ǳ� ������ ���� ����.
                return;
            //�ߺ� �뽬���� �ʰ� �ϱ� ���� �ʱ�ȭ
            A_isdoubleTap = false;
            W_isdoubleTap = false;
            if (D_doubletapcurTime < DoubleTapCoolTime)
            {
                D_doubletapcurTime += Time.deltaTime;
            }
            else
            {
                //���� �ð��� ������ �ʱ�ȭ
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
        void ReDash()   //�뽬 �ʱ�ȭ
        {
            PlayerStats.Instance.speed = PlayerStats.Instance.curSpeed;
        }

        #endregion


        private void PlayerController()
        {
            Jump();                         //�÷��̾��� ���� �� �ٴ����� �������� �ϴ� �Լ�
            Moveing();                      //������ �� �޸��⸦ �����ϴ� �Լ�
            DoubleTapColeTime();            //������ Ȱ��ȭ�� �����
            SetUpdate_Move_Animation();     //������ �ִϸ��̼��� �����ϴ� �Լ�
            HandTransform();                //���� ��ġ�� ���� ���� �κ����� �̵���Ű�� �Լ�
        }

        #region Player Move Controller magazine

        void Moveing()
        {
            direction = transform.rotation * new Vector3(PosX, transform.position.y, PosY);
            MoveForce = new Vector3(direction.x, MoveForce.y, direction.z);
            if (Input.GetKey(PlayerStats.Instance.K_Run))//�޸��� Ű�� 
            {
                if(!isZoom) //���� �ƴѵ���
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
            else //�ȱ�
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
            //���� �ȴ�� �ִٸ�, �������� ����.
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
            //�� �� �ƿ� ���ǹ�
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
        void SetUpdate_Move_Animation()//������ �ִϸ��̼� ���� ����
        {
            //Player walk CheackIn a Animation
            MoveAnimaton();

            //������ ���ǹ�
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
                AC.Movemnent(0, false, weaponeTypeValue); //�������� �������� �⺻���� �ʱ�ȭ
            }
        }
        //�� ��ġ ������
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
            if (Damage <= PlayerStats.Instance.defance)//���ݷ��� ���� ���� ������� ���� 1�� �ش�.
            {
                if (Damage < PlayerStats.Instance.defance - (Damage + 1))
                {
                    Debug.Log("���� ��");
                }
                else PlayerStats.Instance.hp -= 1;
            }
            else//���ݷ��� ���� ���� ������� ���� 
                PlayerStats.Instance.hp += PlayerStats.Instance.defance - (int)Damage;
        }

        IEnumerator HandGun_ReloadReturn()
        {
            M_SFXSound.instance.PlayRelaodAudioClip(handGun_AudioReloadClip, 1f);
            yield return new WaitForSeconds(1.3f);

            if (handGun_HaveAmount > 0)
            {
                int a = 0;
                //���� �����Ǿ� �ִ� �Ѿ��� ������� �׸�ŭ ���� �ִ´�.
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
