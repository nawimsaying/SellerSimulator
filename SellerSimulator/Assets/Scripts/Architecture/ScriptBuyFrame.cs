using Assets.Scripts.Architecture.MainDB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBuyFrame : MonoBehaviour
{
    public BuyFrameRepository _buyFrameRepository;

    // Start is called before the first frame update
    void Start()
    {
        // ������� ��������� ������ BuyFrameRepository
        _buyFrameRepository = new BuyFrameRepository(new BuyFrameDbMock());

        // �������� ����� GetAll() �� ���������� _buyFrameRepository
        List<ModelsBuyFrame> allItems = _buyFrameRepository.GetAll();

        Debug.Log("�������� ������");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
