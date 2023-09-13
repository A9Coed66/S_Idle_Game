using UnityEngine; 
using TMPro;
using Unity.Mathematics;
using System;

public class IdleTutorialGame : MonoBehaviour
{
    [Header("Coins")]
    public double coins;
    public TextMeshProUGUI coinsText;


    [Header("Auto Increase Coin")]
    public double coinsPerSec;
    public TextMeshProUGUI coinsPerSecText;


    [Header("Click Button")]
    public double coinsClickValue;
    public TextMeshProUGUI clickValueText;


    [Header("Upgrade Button 1")]
    public double clickUpgrade1Cost;
    public int clickUpgrade1Level;
    public TextMeshProUGUI clickUpgrade1Text;
    

    [Header("Upgrade Button 2")]
    public double clickUpgrade2Cost;
    public int clickUpgrade2Level;
    public TextMeshProUGUI clickUpgrade2Text;


    [Header("Auto Production Upgrade Button 1")]
    public double productionUpgrade1Cost;
    public int productionUpgrade1Level;
    public TextMeshProUGUI productionUpgrade1Text;

    [Header("Auto Production Upgrade Button 2")]
    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;
    public int productionUpgrade2Level;
    public TextMeshProUGUI productionUpgrade2Text;

    [Header("Prestige System")]
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI gemBoostText;
    public TextMeshProUGUI gemsToGetText;

    public double gems;
    public double gemBoost;
    public double gemsToGet;

    
    private void Start()
    {
        Load();
    }

    public void Load()
    {
        // Convert numbers to strings
        // Float: float.Parse(string)
        // double: double.Parse(string)
        // int: int.Parse(string)
        //double.Parse convert string into double

        //Default là giá trị khởi tạo nếu trước đó chưa có
        coins = double.Parse(PlayerPrefs.GetString("coins", "0"));
        coinsClickValue = double.Parse(PlayerPrefs.GetString("coinsClickValue", "1"));
        clickUpgrade1Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade1Cost", "10"));
        clickUpgrade2Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade2Cost", "100"));
        productionUpgrade1Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade1Cost", "250"));
        productionUpgrade2Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade2Cost", "25"));
        productionUpgrade2Power = double.Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5"));

        gems = double.Parse(PlayerPrefs.GetString("gems", "0"));

        clickUpgrade1Level = PlayerPrefs.GetInt("clickUpgrade1Level",0);
        clickUpgrade2Level = PlayerPrefs.GetInt("clickUpgrade2Level",0);
        productionUpgrade1Level = PlayerPrefs.GetInt("productionUpgrade1Level",0);
        productionUpgrade2Level = PlayerPrefs.GetInt("productionUpgrade2Level",0);
    }

    public void Save()
    {
        PlayerPrefs.SetString("coins", coins.ToString());
        PlayerPrefs.SetString("coinsClickValue", coinsClickValue.ToString());
        PlayerPrefs.SetString("clickUpgrade1Cost", clickUpgrade1Cost.ToString());
        PlayerPrefs.SetString("clickUpgrade2Cost", clickUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade1Cost", productionUpgrade1Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Cost", productionUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Power", productionUpgrade2Power.ToString());

        PlayerPrefs.SetString("gems", gems.ToString());
        
        PlayerPrefs.SetInt("clickUpgrade1Level", clickUpgrade1Level);
        PlayerPrefs.SetInt("clickUpgrade2Level", clickUpgrade2Level);
        PlayerPrefs.SetInt("productionUpgrade1Level", productionUpgrade1Level);
        PlayerPrefs.SetInt("productionUpgrade2Level", productionUpgrade2Level);
    }

    private void Update() {
        gemsToGet=  (150 * System.Math.Sqrt(coins / 1e7)) + 1;
        gemBoost = (gems * 0.01) + 1;

        gemsToGetText.text = "Prestige:\n " + System.Math.Floor(gemsToGet).ToString("F0") + "Gems";
        gemsText.text = "Gems: " + System.Math.Floor(gems).ToString("F0");
        gemBoostText.text = gemBoost.ToString("F2") + "x boost";

        coinsPerSec = (productionUpgrade1Level + productionUpgrade2Power * productionUpgrade2Level) * gemBoost;
        
        //Thực hiện làm tròn coinsClickValue (bao gồm cả coinsValueText) khi giá trị lớn hơn 1000
        if(coinsClickValue > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(Math.Abs(coinsClickValue))));
            var mantissa = (coinsClickValue / System.Math.Pow(10, exponent));
            clickValueText.text = "Click\n" + mantissa.ToString("F2") + "e" + exponent + "coins";
        }else
        {
            clickValueText.text = "Click\n" + coinsClickValue.ToString("F0") + "coins";
        }

        //Thực hiện làm tròn coins (bao gồm cả coinsText) khi giá trị lớn hơn 1000
        if(coins > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(Math.Abs(coins))));
            var mantissa = (coins / System.Math.Pow(10, exponent));
            coinsText.text = "Coins: " + mantissa.ToString("F2") + "e" + exponent;
        }else
        {
            coinsText.text = "Coins: " + coins.ToString("F0");
        }

        coinsPerSecText.text = coinsPerSec.ToString("F0") + " coins/s";

        string clickUpgrade1CostString;
        if(clickUpgrade1Cost > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(Math.Abs(clickUpgrade1Cost))));
            var mantissa = (clickUpgrade1Cost / System.Math.Pow(10, exponent));
            clickUpgrade1CostString = mantissa.ToString("F2") + "e" + exponent;
        }else
        {
            clickUpgrade1CostString = clickUpgrade1Cost.ToString("F0");
        }

        string clickUpgrade1LevelString;
        if(clickUpgrade1Level > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(Math.Abs(clickUpgrade1Level))));
            var mantissa = (clickUpgrade1Level / System.Math.Pow(10, exponent));
            Debug.Log(exponent);
            clickUpgrade1LevelString = mantissa.ToString("F2") + "e" + exponent;
        }else
        {
            clickUpgrade1LevelString = clickUpgrade1Level.ToString("F0");
        }

        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString
                                +" coins\n Power: +1 click\n Level" + clickUpgrade1LevelString;
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2Cost.ToString("F0")
                                +" coins\n Power: +5 click\n Level" + clickUpgrade2Level.ToString("F0");
        productionUpgrade1Text.text = "Click Upgrade 1\nCost: " + productionUpgrade1Cost.ToString("F0")
                                +" coins\n Power: +" + gemBoost.ToString("F2") + " coins/s\n Level" + productionUpgrade1Level.ToString("F0");
        productionUpgrade2Text.text = "Click Upgrade 2\nCost: " + productionUpgrade2Cost.ToString("F0")
                                +" coins\n Power: +" + (5*gemBoost).ToString("F2") + " coins/s\n Level" + productionUpgrade2Level.ToString("F0");
        coins += coinsPerSec * Time.deltaTime;

        Save();
    }

    public void Prestige()
    {
        if (coins > 1000) 
        {
            coins = 0;
            coinsClickValue = 1;
            clickUpgrade1Cost = 10;
            clickUpgrade2Cost = 100;
            productionUpgrade1Cost = 250;
            productionUpgrade2Cost = 25;
            productionUpgrade2Power = 5;

            clickUpgrade1Level = 0;
            clickUpgrade2Level = 0;
            productionUpgrade1Level = 0;
            productionUpgrade2Level = 0;

            gems+=gemsToGet;
        }
    }

    //Click buttons;
    public void Click()
    {
        coins+=coinsClickValue;
    }

    //Increase coins per click
    public void BuyClickUpgrade1()
    {
        if(coins>clickUpgrade1Cost)
        {
            clickUpgrade1Level ++;
            coins-=clickUpgrade1Cost;
            clickUpgrade1Cost *=1.07;
            coinsClickValue++;
        }
    }

    //Increase coins per click
    public void BuyClickUpgrade2()
    {
        if(coins>clickUpgrade2Cost)
        {
            clickUpgrade2Level ++;
            coins-=clickUpgrade2Cost;
            clickUpgrade2Cost *=1.09;
            coinsClickValue += 5;
        }
    }

    //Increase coins gain per sec
    public void BuyProductionUpgrade1()
    {
        if(coins>productionUpgrade1Cost)
        {
            productionUpgrade1Level ++;
            coins-=productionUpgrade1Cost;
            productionUpgrade1Cost *=1.07;
            // coinsPerSec++;
        }
    }

    //Increase coins gain per sec
    public void BuyProductionUpgrade2()
    {
        if(coins>productionUpgrade2Cost)
        {
            productionUpgrade2Level ++;
            coins-=productionUpgrade2Cost;
            productionUpgrade2Cost *=1.07;
            // coinsPerSec++;
        }
    }
}
