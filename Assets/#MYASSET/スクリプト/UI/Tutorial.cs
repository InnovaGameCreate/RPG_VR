using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private Text text;
    private SwordMotion swordMotion;
    private GameObject wildBoar;

    // Use this for initialization
    void Start()
    {
        //text
        text = transform.Find("Panel/TitleText").GetComponent<Text>();
        //使うやつ登録
        swordMotion = transform.Find("/[VRTK_Scripts]/Headset/SwordSnapPoint/Sword2").GetComponent<SwordMotion>();

        //チュートリアル開始
        StartCoroutine("StateUpdate");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StateUpdate()
    {
        //剣を握ろう
        text.text = "よし、じゃあ早速剣を握ろう\n";
        text.text += "剣を抜くときは右手の方の後ろに持ってきて、グリップボタンを一度押すんだ\n";
        yield return new WaitUntil(TuWeapon);
        //握れたな
        text.text = "よし、握れたな\n";
        text.text += "戻すときも同じようにすると収めることが出来るぞ\n";
        yield return new WaitForSeconds(5.0f);
        //イノシシを
        text.text = "では、目の前のイノシシを攻撃してみよう\n";
        text.text += "今回は攻撃してこないから、倒しきってしまってくれ\n";
        yield return new WaitForSeconds(3.0f);//変更
        //魔法についてだ
        text.text = "次は魔法についてだ\n";
        text.text += "トリガーボタンを押すと目の前に球体が出てくるから、それをボタンを押しながら触っていくと魔法が発動する\n";
        text.text += "細かく説明するよりも、やってみたほうがわかるだろう、まずはトリガーボタンを押すんだ\n";
        yield return new WaitForSeconds(3.0f);//変更
        //発動できたな
        text.text = "よし、ちゃんと発動できたな\n";
        text.text += "魔法は右手、左手、両手の3つまでセットすることが出来るから、後で見てみるといい\n";
        yield return new WaitForSeconds(5.0f);
        //上手く当てれたみたいだな
        text.text += "次はスキルについてだ\n";
        text.text += "スキルは右の頭の上と足元の2箇所にセットすることが出来る\n";
        yield return new WaitForSeconds(5.0f);
        //スキルが発動する
        text.text = "スキル発動の仕方は剣を握った状態で、右の頭の上、もしくは足元で、トラックパッドのボタンを数秒間押し続けるんだ\n";
        text.text += "すると目の前に剣の軌跡が見えるからそれをなぞると剣が発動するぞ\n";
        text.text += "さあ、やってみよう\n";
        yield return new WaitForSeconds(3.0f);//変更
        //それがスキルだ
        text.text = "それがスキルだ\n";
        text.text += "スキルの発動時間やクールタイムはスキルによって違うからまた見ておいてくれ\n";
        yield return new WaitForSeconds(5.0f);
        //空中移動についてだ
        text.text = "最後に空中移動についてだ\n";
        text.text += "数回、羽ばたくように振りなさい、すると体が空中に浮くんだ\n";
        yield return new WaitForSeconds(3.0f);//変更
        //移動は
        text.text = "空中での水平移動は地上のときと同じだ\n";
        text.text += "上昇したいときはトラックパッドの右側、下降は左側を押すと出来るぞ\n";
        yield return new WaitForSeconds(10.0f);
        text.text = "以上だ";
        //Application.LoadLevel("maincamera");//LoadSceneが何故か使えないので旧形式で
    }

    //剣を握ればtrue
    bool TuWeapon()
    {
        if (swordMotion.IsEquip)
            return true;
        return false;
    }

}
