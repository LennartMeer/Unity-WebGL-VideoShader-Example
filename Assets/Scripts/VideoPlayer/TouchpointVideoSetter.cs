using DG.Tweening;
using UnityEngine;

namespace VideoPlayer
{
    public class TouchpointVideoSetter : MonoBehaviour
    {
        [SerializeField] private VideoLoader videoLoader;
        [SerializeField] private string videoFileName;
        [SerializeField] private RenderTexture targetTex;
        [SerializeField] private float fadeDuration;
        [SerializeField] private Ease fadeEase;
        [SerializeField] private float fadeTransparency;
        [Space(20)] 
        [SerializeField] private Renderer contentWorldObject;

        private float _currentAnimVal;
        private Vector2 _ogTileScale;
        private static readonly int VideoFade = Shader.PropertyToID("_VideoFade");
        private static readonly int TileScale = Shader.PropertyToID("_TileScale");
        private static readonly int Alpha = Shader.PropertyToID("_Alpha");

        private void Start()
        {
            videoLoader.SetVideo(videoFileName, targetTex);
            _ogTileScale = contentWorldObject.material.GetVector(TileScale);
        }

        public void PlayVideo()
        {
            videoLoader.PlayVideo();
            TweenMaterials(1);
        }

        public void PauseVideo()
        {
            videoLoader.PauseVideo();
            TweenMaterials(0);
        }

        private void TweenMaterials(float endValue)
        {
            DOVirtual.Float(_currentAnimVal, endValue, fadeDuration, UpdateMat).SetEase(fadeEase);
            _currentAnimVal = endValue;
        }

        private void UpdateMat(float value)
        {
            _currentAnimVal = value;
            
            contentWorldObject.material.SetFloat(VideoFade, _currentAnimVal);
            contentWorldObject.material.SetVector(TileScale, 
                Vector3.Lerp(
                    _ogTileScale,
                    _ogTileScale / 2, 
                    _currentAnimVal));
            contentWorldObject.material.SetFloat(Alpha, Mathf.Lerp(1, fadeTransparency, _currentAnimVal));
        }
    }
}