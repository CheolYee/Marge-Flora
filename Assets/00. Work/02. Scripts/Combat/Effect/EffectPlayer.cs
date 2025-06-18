using System.Collections;
using UnityEngine;

namespace _00._Work._02._Scripts.Combat.Effect
{
    public class EffectPlayer : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private float _duration;
        private WaitForSeconds _delaySec;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _duration = _particleSystem.main.duration; 
            //이런식으로 파티클 시스템의 각 모듈에 접근하고 값을 가져올 수 있다.(수정도 가능
            _delaySec = new WaitForSeconds(_duration); //미리 만들어놓고 재사용시에 힙메모리 할당을 최적화할 수 있다.
        }

        public void ResetItem()
        {
            _particleSystem.Stop();
            _particleSystem.Simulate(0); //다음 실행을 위하여 되감기.
        }

        public void SetPositionAndPlay(Vector3 position)
        {
            ResetItem();
            transform.position = position;
            _particleSystem.Play(true); //true는 안써줘도 된다. 설명을 위해서 썼다.
            StartCoroutine(DelayAndGotoPool());
        }
        
        public void EffectPlay()
        {
            ResetItem();
            _particleSystem.Play(true); //true는 안써줘도 된다. 설명을 위해서 썼다.
            StartCoroutine(DelayAndGotoPool());
        }

        private IEnumerator DelayAndGotoPool()
        {
            yield return _delaySec;
            Destroy(gameObject);
        }
    }
}
