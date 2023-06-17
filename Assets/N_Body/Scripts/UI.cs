using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject body;
    public N_Body script;
    public GameObject orbitPred;
    public Transform cam;
    public GameObject gConst;
    private Vector3 prevCamPos;
    public GameObject COM;


    public void OnStart()
    {
        if (script.bodies.Count != 0)
        {
            COM.SetActive(true);
            script.start = true;
            orbitPred.SetActive(false);
            gConst.SetActive(false);
        }

    }

    public void OnPause()
    {
        if (script.start)
            script.pause = 0;
    }

    public void OnResume()
    {
        script.pause = 1;
    }

    public void OnReset()
    {
        OnFollowCenter();
        script.start = false;

        for (int i = 0; i < script.bodies.Count; i++)
        {
            Destroy(script.bodies[i]);

        }

        script.bodies.Clear();
    }

    public void OnFollow()
    {
        if (cam.parent == SelectBody.SelectedGameObj.transform) return;
        if (cam.parent == null)
            prevCamPos = cam.position;
        cam.parent = SelectBody.SelectedGameObj.transform;
        cam.GetComponent<FreeMouseMovement>().isChild = true;
        cam.localPosition = Vector3.zero;
        cam.GetChild(0).localPosition += new Vector3(0,0,-1) * 5 * SelectBody.SelectedGameObj.transform.localScale.x;
    }

    public void OnFollowCenter()
    {
        if (cam.parent != null)
        {
            cam.parent = null;
            cam.position = prevCamPos;
            cam.GetComponent<FreeMouseMovement>().isChild = false;
            cam.GetChild(0).localPosition = Vector3.zero;
        }
    }

    public void OnExit()
    {
        SceneManager.LoadScene(0);
    }
}
