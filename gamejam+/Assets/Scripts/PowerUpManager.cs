using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    private bool activePowerUp;

    private Image powerUpImage;

    private Sprite background;

    // Start is called before the first frame update
    void Start()
    {
        activePowerUp = false;
        powerUpImage = GetComponent<Image>();
        background = powerUpImage.sprite;
    }

    public void activatePowerUp(PowerUpType powerUp)
    {
        string name = PowerUp.PowerUpName(powerUp);
        powerUpImage.sprite = Resources.Load<GameObject>(name).GetComponent<SpriteRenderer>().sprite;
        //powerUpImage.color = new Color(255f, 255f, 255f, 255f);

        activePowerUp = true;
    }

    public void disablePowerUp()
    {
        activePowerUp = false;
        powerUpImage.sprite = background;
        //powerUpImage.color = new Color(108f, 108f, 108f, 168f);
    }

    public bool isPowerUpActive()
    {
        return activePowerUp;
    }
}
