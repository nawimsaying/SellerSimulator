using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsDb : MonoBehaviour
{
    public int idProduct;
    public string productName;
    public int price;
    public string imageName;
    public BoxProductInfo idBoxProduct;



}

public class BoxProductInfo // вынести в отдельный класс
{
    public int id;
    public int countBox;
    public string boxName;
    public int countProduct;
}
