using UnityEngine;

public class PlayerHealth : MonoBehaviour
    {
        public float currentHealth = 100f;

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, 100f);
        
            Debug.Log($"{gameObject.name}가(이) {amount}만큼 데미지를 받음. 현재 체력: {currentHealth}");

            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} 사망");
            // 사망 처리 부분 넣어야 함
        }
    }
