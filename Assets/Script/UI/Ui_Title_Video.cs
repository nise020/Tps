using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public partial class Ui_Title : Actor
{
    public VideoPlayer VIDEOPLAYER;
    public RawImage RAWIMG;
    IEnumerable video;
    VideoClip clip = null;
    private void Start()
    {
        StartCoroutine(playingVideo());
    }
    IEnumerator SetVideo() 
    {
        //string File = "Prefabs/Video" + "EF_Normal";
        string File = "Video/" + "EF_Normal";//파일위치/파일명

        clip = Resources.Load(File) as VideoClip;

        VIDEOPLAYER.gameObject.SetActive(true);

        RAWIMG.texture = VIDEOPLAYER.texture;

        VIDEOPLAYER.clip = clip;
        VIDEOPLAYER.Prepare();
        VIDEOPLAYER.Play();
        Debug.Log("On");
        yield return new WaitForSeconds(5);
        
        //VIDEOPLAYER.Stop();
        Debug.Log("Off");

    }
    IEnumerator playingVideo() 
    {
        //string File = "Prefabs/Video" + "EF_Normal";
        string File = "Video/" + "EF_Normal";//파일위치/파일명

        clip = Resources.Load(File) as VideoClip;

        VIDEOPLAYER.gameObject.SetActive(true);

        RAWIMG.texture = VIDEOPLAYER.texture;

        VIDEOPLAYER.clip = clip;
        VIDEOPLAYER.Prepare();
        VIDEOPLAYER.Play();
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (true == VIDEOPLAYER.isPlaying)
            {
                RAWIMG.texture = VIDEOPLAYER.texture;

                continue;
            }
            break;
        }
        VIDEOPLAYER.transform.gameObject.SetActive(false);
    }
}
