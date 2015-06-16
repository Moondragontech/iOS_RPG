using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PassButton : MonoBehaviour
{
	public Animator anim;
//	private bool passFlag = false;
	public void pass ()
	{
		anim.enabled = false;
		/*assFlag = true;
        anim.DOPause();
        anim.SetBool("passFlag",passFlag);*/
        
		//GameObject.Find() is a CPU heavy method, more use of it can cause paerformance issues
		//if the elements are not dynamic, please reference it in the editor and use it
        
		float cameraPos = GameObject.Find ("Main Camera").transform.position.x;
		Tweener tweener = GameObject.Find ("Main Camera").transform.DOMoveX (cameraPos + 2.5F, 1F);
		float pos = GameObject.Find ("Player").transform.position.x;
		Vector3 temp = GameObject.Find ("Player").transform.position;
		GameObject.Find ("Player").transform.DOJump (temp, 0.7F, 2, 1F);
		Tweener tweener2 = GameObject.Find ("Player").transform.DOLocalMoveX (pos + 2.5F, 1F);
		tweener2.OnComplete (startAnimation);
	}
	void startAnimation ()
	{
		anim.enabled = true;
		//passFlag = false;
		//anim.SetBool("passFlag", passFlag);
		//GameObject.Find ("Player").GetComponent<Animator>().SetBool("Player",true);
	}

}
