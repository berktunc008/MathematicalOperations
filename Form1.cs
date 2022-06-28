/********************************************************************************************************************
**                                              SAKARYA ÜNİVERSİTESİ 
**                                   BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**                                    BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
**                                      NESENEYE DAYALI PROGRAMLAMA DERSİ
**                                           2018-2019 BAHAR DÖNEMİ
**
**
**                                                  ÖDEV NUMARASI:2
**                                               ÖĞRENCİ ADI:BERK TUNÇ
**                                                   NO:B171200016
**
**************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathematicalOperations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void buttonCalculate_Click(object sender, EventArgs e)    //calculate(hesaplama) butonuna tıklanıldığı zaman girer.
        {
            textBoxOutput.Text = string.Empty;            //Boşsa  çıkmak için.

            string text = textBoxInput.Text;              //Bir şey girildiyse.

            string replacedText = text.Replace("+", ".+").Replace("-", ".-");

            char[] seps = {'.'}; 

            string[] stringArray = replacedText.Split(seps);    //String içerisinde bir string i başka bir string ile değiştirebiliriz.

            int[] intArray = new int[stringArray.Length]; 

            for (int i = 0; i < stringArray.Length; i++) //1 er 1 er arttırıyoruz.
            {
                if (stringArray[i].Contains('*') || stringArray[i].Contains('/'))       //Eğer çarpma ve bölme durumunda ise.
                {
                    if (stringArray[i].Contains('+'))  ////Belirtilen bir alt dizeyi bu dizede oluşup oluşmadığını belirten bir değer döndürür.(+) için
                    {
                        intArray[i] = Int32.Parse("+" + Calculate(stringArray[i].Replace("+", "")));
                    }
                    else if (stringArray[i].Contains('-')) //Belirtilen bir alt dizeyi bu dizede oluşup oluşmadığını belirten bir değer döndürür.(-) için
                    {
                        intArray[i] = Int32.Parse("-" + Calculate(stringArray[i].Replace("-", "")));
                    }
                    else
                    {
                        intArray[i] = Int32.Parse("+" + Calculate(stringArray[i]));
                    }
                }
                else
                {
                    if (stringArray[i] != "")  //eğer boşluk yoksa çevir.
                    {
                        intArray[i] = Int32.Parse(stringArray[i]);
                    }
                }
            }
            Array.Sort(intArray); //Sıralama işlemi için.

            Array.Reverse(intArray); //elemanları ters çevirme için.

            string matText = "";  //ifade yok.

            for (int i = 0; i < intArray.Length; i++)   //i küçük array.lenght ten ise.
            {
                if (intArray[i] > 0 && i< intArray.Length-1)  //eğer i küçük  array.lenght in 1 eksiği ve 0 dan büyük olma durumu.
                {
                    matText += intArray[i] + "+";     //durum gerçekleşirse + yı yaz
                }
                else
                {
                    matText += intArray[i];  // üstteki durum gerçekleşmez ise.
                }
            }
            matText = matText.Replace("+-", "-");  

            text = Calculate(matText);  //text içine hesap sonucu yazıcak

            textBoxOutput.Text = text;  
        }


        string Calculate(string matText)
        {
            string result = matText;
            string replaced = string.Empty;
            do                                                  //do while döngüsü içine alırız.
            {
                    //Çarpma ve bolme islemi
                if (result.Contains("*"))   //* işaretini arıyoruz bulunca döngüye girecek.
                {
                    int count = result.Count(u => new List<char>() { '*' }.Contains(u));  //list yöntemi kullandık.

                    for (int i = 0; i < count; i++) 
                    {
                        char[] seps = { '*' };

                        string[] numbers = result.Split(seps);

                        if (!numbers[0].Contains("/") && !numbers[1].Contains("/"))  //ilk ve ondan sonraki indexte bölmeyi bulamassa dögü gir.
                        {
                            int buffer = Int32.Parse(numbers[0]) * Int32.Parse(numbers[1]);  //ilk ve ondan sonraki sayıının çarpımının koruma işlemi.

                            replaced = numbers[0] + "*" + numbers[1];     //değiştirme işlemi.

                            result = result.Replace(replaced, buffer.ToString());     //sonuç değişen değer ile aynı ise .
                        }
                        else if (numbers[0].Contains("/") && !numbers[1].Contains("/"))  //en başta bölme ve sonrakinde bölme yok ise.
                        {
                            char[] sepsMinus = {'/'};

                            string[] n2 = numbers[0].Split(sepsMinus); //2. sayı olarak ilk ten sonrakini split ederek al. 

                            int buffer2 = Int32.Parse(n2[1]) * Int32.Parse(numbers[1]);  // normal çarpma işlemi.

                            replaced = n2[1] + "*" + numbers[1];  //değiştirme işlemi yapıyoruz.

                            result = result.Replace(replaced, buffer2.ToString()); ////değişen ile korunan aynı ise sonuçtur.
                        }

                        else if (!numbers[0].Contains("/") && numbers[1].Contains("/")) //ilk bölme yoksa sonrakinde bölme bulunursa gir.
                        {
                            char[] sepsMinus = { '/' };

                            string[] n2 = numbers[1].Split(sepsMinus);

                            int buffer2 = Int32.Parse(numbers[0]) * Int32.Parse(n2[0]); //tanımladığımız sayıları çarpıyoruzve koruyoruz.

                            replaced = numbers[0] + "*" + n2[0];  //değiştirme işlemi.

                            result = result.Replace(replaced, buffer2.ToString());  //değişen ile korunan aynı ise sonuçtur.
                        }
                    }
                }
                else if (result.Contains("/")) //Bölme işaretini arıyoruz ve bulunca döngüye giriyoruz.
                {
                    int count = result.Count(u => new List<char>() {'/'}.Contains(u));

                    for (int i = 0; i < count; i++)
                    {
                        char[] seps = { '/' };

                        string[] numbers = result.Split(seps); 

                        int buffer = Int32.Parse(numbers[0]) / Int32.Parse(numbers[1]);  //ik ile ondan sonraki indexi bölüp hafızada koruyoruz.

                        replaced = numbers[0] + "/" + numbers[1];  //ilk ile ondan sonrakini değiştir.

                        result = result.Replace(replaced, buffer.ToString()); // sonuç ters çevirme ile aynı ise.
                    }
                }


                                                     //Toplama ve cikarma islemi için oluşturduk.
                else if (result.Contains("+"))
                {
                    int count = result.Count(u => new List<char>() {'+'}.Contains(u)); //+ yı arama işlemleri

                    for (int i = 0; i < count; i++)
                    {
                        char[] seps = {'+'};

                        string[] numbers = result.Split(seps);            // split ile  ayrıştırma yaparız.
                        if (numbers.Length > 1)                                             //eğer uzunluk 1 den büyükse.
                        {
                            if (!numbers[0].Contains("-") && !numbers[1].Contains("-"))   //eğer ilk index ve ondan sonraki de - değil ise gir.
                            {
                                int buffer = Int32.Parse(numbers[0]) + Int32.Parse(numbers[1]);  //sayıyı korumak için.

                                replaced = numbers[0] + "+" + numbers[1]; //iki sayının yerinideğiştirme.

                                result = result.Replace(replaced, buffer.ToString());  //korunan ile değiştirilip işlem yapılmasından sonra sonuçları karşılaştırma.
                            }
                            else if (numbers[0].Contains("-") && !numbers[1].Contains("-")) //eğer ilk -  sonra - değil ise gir.
                            {
                                char[] sepsMinus = {'-'};           

                                string[] n2 = numbers[0].Split(sepsMinus);   //n2 tanımlası yaptık

                                int buffer2 = Int32.Parse(n2[0]) + Int32.Parse(numbers[1]);   //toplamayı yap ve koru.

                                replaced = n2[0] + "+" + numbers[1];  // yer değiştir. ve öyle yap.

                                result = result.Replace(replaced, buffer2.ToString()); //korunan ile yerdeğiştirme kıyas.
                            }

                            else if (!numbers[0].Contains("-") && numbers[1].Contains("-")) //eğer ilki -değil sonraki - ise gir.
                            {
                                char[] sepsMinus = {'-'};           

                                string[] n2 = numbers[1].Split(sepsMinus);

                                int buffer2 = Int32.Parse(numbers[0]) + Int32.Parse(n2[0]);

                                replaced = numbers[0] + "+" + n2[0];

                                result = result.Replace(replaced, buffer2.ToString());
                            }
                        }
                    }
                }

                else if (result.Contains("-"))   //Eğer listede belirtilen - ise
                {
                    int count = result.Count(u => new List<char>() {'-'}.Contains(u));

                    for (int i = 0; i < count; i++)  
                    {
                        char[] seps = {'-'};

                        string[] numbers = result.Split(seps);

                        foreach (var n in numbers) //foreach döngüsü ile sayılar içinde n  arar.
                        {
                            if (n == string.Empty)     //n dediğimiz şey boşluk olunca yani.
                            {
                                return result;   //sonucu döndür.
                            }
                        }

                        int buffer = Int32.Parse(numbers[0]) - Int32.Parse(numbers[1]); //2 sayıyı çıkarıp koruma işlemi.

                        replaced = numbers[0] + "-" + numbers[1];     //iki sayının yerlerini değişme.

                        result = result.Replace(replaced, buffer.ToString());  //değiştirilen ile korununanı kıyaslayıp eşit olma durumuna bakma.
                    }
                }

            } while (TextBoxContainsOperator(result)); 

            return result;       //son olarak sonucu döndürme işlemi.
        }

        bool TextBoxContainsOperator(string text)
        {
            if (text.Contains("+")) return true;
            else if (text.Contains("-")) return true;        // döndürme işlemleri.
            else if (text.Contains("*")) return true;
            else if (text.Contains("/")) return true;
            else return false;




        }
    }
}
