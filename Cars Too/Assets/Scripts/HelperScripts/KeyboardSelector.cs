using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Used to interface menus with keyboards
public class KeyboardSelector : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private int index =0;
    [SerializeField] private GameObject indicator;
    [SerializeField] private int highlightwidth = 10;
    [SerializeField] private int highlightheight = 10;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = indicator.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckEnter();
        UpdateIndicator();
        
    }

    private void OnEnable()
    {
        index = 0;
    }

    private void CheckMovement()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
        {
            index -= 1;

            if (index < 0)
            {
                index = buttons.Count-1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            index += 1;

            if (index >= buttons.Count)
            {
                index = 0;
            }
        }
    }

    private void CheckEnter()
    {
        if (Input.GetKeyDown(KeyCode.Return)&&buttons[index].interactable==true)
        {
            buttons[index].onClick.Invoke();
        }
    }

    private void UpdateIndicator()
    {
        indicator.transform.position = buttons[index].transform.position;
        RectTransform brt = buttons[index].GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(brt.rect.width+highlightwidth, brt.rect.height+highlightheight);
    }
}
