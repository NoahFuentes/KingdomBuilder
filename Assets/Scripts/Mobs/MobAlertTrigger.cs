using UnityEngine;

public class MobAlertTrigger : MonoBehaviour
{
    private MobBase self;

    private void Awake()
    {
        self = GetComponentInParent<MobBase>();
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (self.stateMachine.CurrentState != self.defaultState) return;
        if (other.CompareTag("Player"))
        {
            MobBase self = transform.parent.GetComponentInChildren<MobBase>();
            self.stateMachine.ChangeState(self.alert);

        }
    }
    */
    private void OnTriggerStay(Collider other)
    {
        if (self.stateMachine.CurrentState != self.defaultState) return;
        if (other.CompareTag("Player"))
        {
            MobBase self = transform.parent.GetComponentInChildren<MobBase>();
            self.stateMachine.ChangeState(self.alert);

        }
    }
}
