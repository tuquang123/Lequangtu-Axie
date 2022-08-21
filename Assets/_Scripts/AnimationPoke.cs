using Spine.Unity;
using UnityEngine;

namespace _Scripts
{
    public class AnimationPoke : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;
    
        private void Awake()
        {
            _skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
        }
        private void Start()
        {
            Invoke(nameof(Animation), 3);
            Invoke(nameof(Animation1), 8);
            Invoke(nameof(Animation2), 13);
            
            Invoke(nameof(Animation), 17);
            Invoke(nameof(Animation1), 20);
            Invoke(nameof(Animation2), 25);
            
            //_skeletonAnimation.timeScale = .4f;
            //_skeletonAnimation.AnimationState.SetAnimation(0, "action/idle", false); // shot
        }
        void Animation()
        {
            _skeletonAnimation.timeScale = .5f;
            _skeletonAnimation.AnimationState.SetAnimation(0, "activity/victory-pose-back-flip", false); //att
       
        }
        void Animation1()
        {
            _skeletonAnimation.timeScale = .5f;
            _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot

        }
        void Animation2()
        {
            _skeletonAnimation.timeScale = .5f;
            _skeletonAnimation.AnimationState.SetAnimation(0, "defense/hit-with-shield", false); // shield

        }
    }
}
