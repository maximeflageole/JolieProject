using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    protected Image m_barVisual;

    // Start is called before the first frame update
    void Start()
    {
        m_barVisual = GetComponent<Image>();
    }

    // Update is called once per frame
    public void UpdateBar(float percentage)
    {
        m_barVisual.fillAmount = percentage;
    }
}
