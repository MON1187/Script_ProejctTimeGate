namespace PlayerContolloer
{
    using Unity.VisualScripting;
    using UnityEngine;
    public class Animation_Controller : MonoBehaviour
    {
        [SerializeField] private Animator HandGun_Animator;
        [SerializeField] private Animator AKM_Animator;
        #region Gun
        public void Movemnent(int moveValue,bool isRun , WeaponType weaponeTypeValue)
        {
            switch (weaponeTypeValue)
            {
                case WeaponType.Handgun:
                    if (isRun && moveValue > 0) //달리기중일떄
                    {
                        HandGun_Animator.SetBool("Runing", true);
                        HandGun_Animator.SetBool("Walk", false);
                    }
                    else if(!isRun && moveValue == 0)
                    {
                        HandGun_Animator.SetBool("Walk", false);
                    }
                    else
                    {
                        HandGun_Animator.SetBool("Runing", false);
                        HandGun_Animator.SetBool("Walk", true);
                    }
                break;
                   
                case WeaponType.AKM:
                    if (isRun && moveValue > 0) //달리기중일떄
                    {
                        AKM_Animator.SetBool("Runging", true);
                        AKM_Animator.SetBool("Walk", false);
                    }
                    else if (moveValue > 0)
                    {
                        AKM_Animator.SetBool("Runing", false);
                        AKM_Animator.SetBool("Walk", true);
                    }
                    else
                    {
                        AKM_Animator.SetBool("Runging", false);
                        AKM_Animator.SetBool("Walk", false);
                    }
                break;
            }
        }
        public void Zooming(bool isZoom , WeaponType weaponeTypeValue)
        {
            switch (weaponeTypeValue)
            {
                case WeaponType.Handgun:
                    if (isZoom) //줌일때
                    {
                        HandGun_Animator.SetBool("Zoom",true);
                    }
                    else if (!isZoom)
                    {
                        HandGun_Animator.SetBool("Zoom", false);
                    }
                return;

                //case WeaponType.AKM:
                //    if (isZoom) //줌일때
                //        AKM_Animator.SetBool("Zoom",true);
                //    else if(!isZoom)
                //        AKM_Animator.SetBool("Zoom", false);
                //return;
            }
        }
        public void Reload(bool isReload , WeaponType weaponeTypeValue)
        {
            Debug.Log("실행 완료");
            switch (weaponeTypeValue)
            {
                case WeaponType.Null: Debug.Log("tewt Text");
                    break;

                case WeaponType.Handgun:
                    if (isReload) //장전 중일때
                        HandGun_Animator.SetBool("Reload", isReload);
                    else
                        HandGun_Animator.SetBool("Reload", isReload);
                break;

                case WeaponType.AKM:
                    if (isReload) //장전 중일때
                        AKM_Animator.SetBool("Reload", isReload);
                    else
                        AKM_Animator.SetBool("Reload", isReload);
                break;
            }
        }

        public void Fire(WeaponType weaponeTypeValue)
        {
            switch (weaponeTypeValue)
            {
                case WeaponType.Handgun:
                    HandGun_Animator.SetTrigger("Fire");
                break;

                case WeaponType.AKM:
                    AKM_Animator.SetTrigger("Fire");
                break;
            }
        }
        #endregion
    }
}
