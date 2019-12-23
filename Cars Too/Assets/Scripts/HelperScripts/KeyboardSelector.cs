using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Used to interface menus with keyboards
public class KeyboardSelector : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private int index =0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckEnter();
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[index].onClick.Invoke();
        }
    }
}
