using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tutorial : MonoBehaviour
{
    public Animator ANI;


  public void OnBtnClose()
    {
        //Ʃ�丮�� �ݴ� �ִϸ��̼� ����
        ANI.SetTrigger("close");
    }
    
  public void OnAnimationEnd()
    {
        transform.gameObject.SetActive(false);
    }
}
