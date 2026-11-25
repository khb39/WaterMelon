using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleMerge : MonoBehaviour
{
    private Dongle myDongle;

    private void Awake()
    {
        myDongle = GetComponent<Dongle>();
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryMerge(collision);
    private void OnCollisionStay2D(Collision2D collision) => TryMerge(collision);

    private void TryMerge(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Dongle otherDongle)) return; //Dongel스크립트 부재의 경우

        if(!CanMerge(myDongle, otherDongle)) return;

        if (myDongle.GetInstanceID() > otherDongle.GetInstanceID()) return;

        MergeDongles(myDongle, otherDongle);
    }

    private bool CanMerge(Dongle a, Dongle b)
    {
        if(!a.IsDrop || !b.IsDrop) return false; // 떨어뜨리기 전의 경우
        if(a.IsMerg || b.IsMerg) return false; // 이미 합쳐지는 중일 경우
        if(a.DongleLevel != b.DongleLevel) return false; // 레벨이 다른 경우
        if(a.DongleLevel >= GameManager.Instance.MaxLevel) return false; //최대 레벨 달성의 경우
        return true;
    }

    private void MergeDongles(Dongle dongleA, Dongle dongleB) //동글 생성 중간위치에 다음레벨 동글 생성
    {
        dongleA.IsMerg = true;
        dongleB.IsMerg = true;

        Vector3 midPosition = (dongleA.transform.position + dongleB.transform.position) / 2f;
        int nextLevel = dongleA.DongleLevel + 1;

        Destroy(dongleA.gameObject);
        Destroy(dongleB.gameObject);

        Dongle merged = GameManager.Instance.CreateMergedDongle(nextLevel, midPosition);
        merged.Drop();

    }
}
