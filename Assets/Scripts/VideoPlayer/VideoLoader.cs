using UnityEngine;

namespace VideoPlayer
{
    public class VideoLoader : MonoBehaviour
    {
        private UnityEngine.Video.VideoPlayer _videoPlayer;
        private bool _videoIsPrepared;

        private void Awake()
        {
            _videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        }
        
        public void SetVideo(string fileName, RenderTexture targetTex)
        {
            _videoPlayer.targetTexture = targetTex;
            _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath + "/Videos", fileName);
            _videoPlayer.loopPointReached += LoopVideo;
            _videoPlayer.Pause();
        }

        public void PlayVideo()
        {
            _videoPlayer.Play();
        }

        public void PauseVideo()
        {
            _videoPlayer.Pause();
        }

        //Done to avoid weird bug with video getting choppier with each loop
        private void LoopVideo(UnityEngine.Video.VideoPlayer videoPlayer)
        {
            videoPlayer.time = 0;
            videoPlayer.Play();
        }
    }
}
