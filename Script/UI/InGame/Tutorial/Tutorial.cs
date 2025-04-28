using System.Collections;
using System.Threading;
using UnityEngine;
using PlayerContolloer;
/// <summary>
/// UI Code Script
/// this Code execution an Playing the Gmae first the Base Start
/// Tutorial UI Fade In/Out
/// </summary>
public class Tutorial : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;

    [SerializeField] private CanvasGroup moveTutorial;     //Move UI Get Turtorial Obj
    [SerializeField] private CanvasGroup uiTutorial;       //Tab UI Get Turtorial Obj
    [SerializeField] private CanvasGroup uiInteraction;    //Interaction Ui Get Turtorial Obj
    [SerializeField] private CanvasGroup handGunTutorial;  //Weapone UI Get Turtorial Obj

    public float runTime = 6;
    WaitForSeconds Tick = new WaitForSeconds(0.1f);

    private void Awake()
    {
        ResetAlpha();
    }

    void ResetAlpha()
    {
        //All alpha Value the zero Settings
        moveTutorial.alpha = 0;
        uiTutorial.alpha = 0;
        uiInteraction.alpha = 0;
        handGunTutorial.alpha = 0;
    }
    private void Start()
    {
        StartCoroutine(MoveTutorial());
    }

    IEnumerator MoveTutorial()
    {
        bool inFade = true;      //���̵尡 �Ϸ� �ƴ��� üũ
        bool outFade = false;    //���̵尡 ��� �������� üũ

        float fadeAmount = fadeSpeed * Time.deltaTime;  //���̵� �ƿ� ȿ��(����)

        while (true)
        {
            yield return Tick;
            if(inFade)
            {
                moveTutorial.alpha += fadeAmount;

                if(moveTutorial.alpha >= 1)
                {
                    yield return new WaitForSeconds(runTime);
                    inFade = false;
                    outFade = true;
                }
            }
            else if(outFade)
            {
                moveTutorial.alpha -= fadeAmount;
                //��� ��ٸ�
                if(moveTutorial.alpha <= 0) 
                { 
                    StartCoroutine(UITutorial());
                    outFade = false;
                }
            }
            else
            {
                //Coroutine the Exit
                yield break;
            }  
        }
    }
    IEnumerator UITutorial()
    {
        bool inFade = true;      //���̵尡 �Ϸ� �ƴ��� üũ
        bool outFade = false;    //���̵尡 ��� �������� üũ

        float fadeAmount = (fadeSpeed * Time.deltaTime);  //���̵� �ƿ� ȿ��(����)
        
        while (true)
        {
            yield return Tick;
            if (inFade)
            {
                uiTutorial.alpha += fadeAmount;

                if (uiTutorial.alpha >= 1)
                {
                    yield return new WaitForSeconds(runTime);
                    inFade = false;
                    outFade = true;
                }
            }
            else if (outFade)
            {
                uiTutorial.alpha -= fadeAmount;

                if(uiTutorial.alpha <= 0)
                {
                    StartCoroutine(InteractionTutorial());
                    outFade = false;
                }
            }
            else
            {
                //Coroutine the Exit
                yield break;
            }
        }
    }
    IEnumerator InteractionTutorial()
    {
        bool inFade = true;      //���̵尡 �Ϸ� �ƴ��� üũ
        bool outFade = false;    //���̵尡 ��� �������� üũ

        PlayerSituationCheack.openInteraction = true;

        float fadeAmount = (fadeSpeed * Time.deltaTime);  //���̵� �ƿ� ȿ��(����)

        while (true)
        {
            yield return Tick;
            if (inFade)
            {
                uiInteraction.alpha += fadeAmount;

                if (uiInteraction.alpha >= 1)
                {
                    yield return new WaitForSeconds(runTime);
                    inFade = false;
                    outFade = true;
                }
            }
            else if (outFade)
            {
                uiInteraction.alpha -= fadeAmount;

                if (uiInteraction.alpha <= 0)
                {
                    outFade = false;
                }
            }
            else
            {
                //Coroutine the Exit
                yield break;
            }
        }
    }

    IEnumerator HandGunTutorial()
    {
        bool inFade = true;    //���̵尡 �Ϸ� �ƴ��� üũ
        bool outFade = false;    //���̵尡 ��� �������� üũ

        float fadeAmount = handGunTutorial.alpha - (fadeSpeed * Time.deltaTime);  //���̵� �ƿ� ȿ��(����)

        while (true)
        {
            yield return Tick;
            if (inFade)
            {
                handGunTutorial.alpha = fadeAmount;

                if (handGunTutorial.alpha >= 1)
                {
                    inFade = false;
                    outFade = true;
                }
            }
            else if (outFade)
            {
                //��� ��ٸ�
                yield return new WaitForSeconds(runTime);
                outFade = false;
                handGunTutorial.alpha = 0;
            }
            else
            {
                yield break;
            }
        }
    }
}
