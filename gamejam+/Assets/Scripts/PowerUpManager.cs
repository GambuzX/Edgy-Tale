using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    private bool activePowerUp;

    private Image powerUpImage;

    // Start is called before the first frame update
    void Start()
    {
        activePowerUp = false;
        powerUpImage = GetComponent<Image>();
    }

    public void activatePowerUp(PowerUpType powerUp)
    {
        string name = PowerUp.PowerUpName(powerUp);
        powerUpImage.sprite = Resources.Load<GameObject>(name).GetComponent<SpriteRenderer>().sprite;

        activePowerUp = true;
    }

    public void disablePowerUp()
    {
        activePowerUp = false;
        powerUpImage.sprite = null;
    }

    public bool isPowerUpActive()
    {
        return activePowerUp;
    }
}
