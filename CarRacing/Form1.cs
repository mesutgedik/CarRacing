using Car_Racing.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Car_Racing
{
    public partial class Form1 : Form
    {

        int gasValue = 100;
        public Form1()
        {
            InitializeComponent();
            progressBar1.Value =gasValue;
                     
        }

        int  Road = 0, Speed = 20; 
        Random R = new Random();
        int Carlocation_X = 210;
        int TotalCar=0;
        
        class Gas // benzin ile ilgili değişkenleri bu classta tanımladık.
        {
            public bool GasHave=false; 
            public PictureBox gas;
            public bool OnScreen = false;
        }


        class Random_CAR // araba ile ilgili değişkenleri bu classta tanımladık.
        {
            public bool EnemyCarHave = false;
            public PictureBox EnemyCar;
            public bool OnScreen = false; 
        }

        
        Gas CarGas = new Gas(); // gas sınıfından yeni bir nesne oluşturdum.
        Random_CAR[] rndCar = new Random_CAR[2];// random_car sınıfından 3 tane nesne oluşturdum.
        
        void BringRandomCar(PictureBox pb)
        {
          
            int rnd = R.Next(0, 4); // 0 ile 3 arasında bir değeri rndye atıyor.

            switch (rnd)
            {
                case 0:
                    pb.Image = Properties.Resources.car0;
                    break;

                case 1:
                    pb.Image = Properties.Resources.car1;
                    break;

                case 2:
                    pb.Image = Properties.Resources.car2;
                    break;

                case 3:
                    pb.Image = Properties.Resources.car3;
                    break;
            }

            pb.SizeMode = PictureBoxSizeMode.StretchImage; 

        }

        private void StartLocationCar()
        {
            Carlocation_X = 230;
            RedCar.Location = new Point(Carlocation_X , 373); // bizim arabımızın başlangıç konumunu ayarlıyoruz.
        }

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (Carlocation_X < 350)
                {
                    Carlocation_X = Carlocation_X + 170;
                    RedCar.Location = new Point(Carlocation_X, 373);

                }
                     
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (Carlocation_X>60)
                {
                    Carlocation_X = Carlocation_X - 170;
                    RedCar.Location = new Point(Carlocation_X, 373);
                }
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
               
                if (timerRandomCar.Interval < 240)
                {
                    timerRandomCar.Interval += 20;
                    timerLine.Interval += 5;
                    Speed -= 10;
                }

             

            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if(timerRandomCar.Interval > 20) { 

                    timerRandomCar.Interval -= 20;
                    timerLine.Interval -= 5;
                    Speed += 10;
                }
                

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < rndCar.Length; i++)
            {
                rndCar[i] = new Random_CAR(); 
            }
            rndCar[0].OnScreen = true;  // arabamız ekranda var.

            StartLocationCar(); // arabamızı ekrana yerleştirme fonksiyonunu çağırıyoruz.

            
        }

        private void timerRandomCar_Tick(object sender, EventArgs e)
        {
             

            for (int i = 0; i < rndCar.Length; i++)
            {
                if (!CarGas.OnScreen && !CarGas.GasHave)
                {
                    CarGas.gas = new PictureBox();
                    CarGas.gas.Image = Properties.Resources.gas; // benzin bidonu resmini atama işlemi yapıyorum.
                    CarGas.gas.SizeMode = PictureBoxSizeMode.StretchImage; // benzin bidonunu kutuya sığrıdırıyoruz
                    CarGas.gas.Size = new Size(50, 50); //benzinin bidonu boyutunu ayarlıyoruz.
                    CarGas.gas.Top = -CarGas.gas.Height; // benzin bidonu ekranda görünmesini engelliyorum
                    CarGas.GasHave = true;
                }

                if (!rndCar[i].EnemyCarHave && rndCar[i].OnScreen)
                {
                    rndCar[i].EnemyCar = new PictureBox();
                    BringRandomCar(rndCar[i].EnemyCar); // rastgele bir araba resmi atama işlemini yapıyorum.
                    rndCar[i].EnemyCar.Size = new Size(90, 150); // arabanın boyutunu ayarlıyoruz.
                    rndCar[i].EnemyCar.Top = -rndCar[i].EnemyCar.Height; // arabanın ekranda görünmesini engelliyorum.
                   

                    int LocateEnemyCar = R.Next(0, 3);  // locateEnemyCar'a 0 ile 3 arasında bir değer atıyor.

                    if (LocateEnemyCar == 0)
                    {

                        rndCar[i].EnemyCar.Left = 55; // karşıdan gelen arabanın başlangıç pozisyonunu
                        if (TotalCar==3)
                        {
                            CarGas.OnScreen = true; // gaz artık ekrana gelebilir.
                            CarGas.gas.Left = 410; // gazım en sağdan konumlandırılıyor.
                            TotalCar = 0; // her 3 arabada bir olması için sıfırlıyorum.
                        }

                    }
                    else if (LocateEnemyCar == 1)
                    {
                        rndCar[i].EnemyCar.Left = 210;// karşıdan gelen arabanın başlangıç pozisyonunu
                        if (TotalCar == 3)
                        {
                            CarGas.OnScreen = true;
                            CarGas.gas.Left = 75;
                            TotalCar = 0;
                        }
                    }

                    else if (LocateEnemyCar == 2)
                    {
                        rndCar[i].EnemyCar.Left = 390;// karşıdan gelen arabanın başlangıç pozisyonunu
                        if (TotalCar == 3)
                        {
                            CarGas.OnScreen = true;
                            CarGas.gas.Left = 240;
                            TotalCar = 0;
                        }
                    }

                    this.Controls.Add(CarGas.gas);
                    this.Controls.Add(rndCar[i].EnemyCar);
                    rndCar[i].EnemyCarHave = true;

                }

                else
                {
                    if (rndCar[i].OnScreen)
                    {
                        rndCar[i].EnemyCar.Top += 10; // arabayı herseferinde 10 aşağı yonde ilerlet.
                        if (rndCar[i].EnemyCar.Top >= 250) // yeni arabanın gelmesi için gereken aralık
                        {
                            for (int j = 0; j < rndCar.Length; j++)
                            {
                                if (!rndCar[j].OnScreen) // 2. arabamız ekranda değil ise
                                {
                                    rndCar[j].OnScreen = true; // artık 2. arabada ekrana gelsin istiyorum.
                                    break;
                                }
                            }
                        }
                        if (rndCar[i].EnemyCar.Top >= this.Height - 20) //eğer araba ekrandan gittiyse hafızada yok et.
                        {
                            rndCar[i].EnemyCar.Dispose();
                            rndCar[i].EnemyCarHave = false;
                            rndCar[i].OnScreen = false;
                            TotalCar++; // toplam yok olan araba sayısını tutuyorum. Eğer bunun sayısı 3 olursa benzin bidonu ekrana gelebilir.
                         
                        }
                    }
                    

                }

                if (rndCar[i].OnScreen)
                {
                    float AbsoluteX = Math.Abs((RedCar.Left + (RedCar.Width / 2)) - (rndCar[i].EnemyCar.Left + (rndCar[i].EnemyCar.Width / 2)));
                    float AbsoluteY = Math.Abs((RedCar.Top + (RedCar.Height / 2)) - (rndCar[i].EnemyCar.Top + (rndCar[i].EnemyCar.Height / 2)));
                    float WidthDistance = (RedCar.Width / 2) + (rndCar[i].EnemyCar.Width / 2);
                    float HeightDistance = (RedCar.Height / 2) + (rndCar[i].EnemyCar.Height / 2);


                    if ((WidthDistance > AbsoluteX) && (HeightDistance > AbsoluteY))
                    {
                        Crash();
                    }

                }


            }
            if (CarGas.OnScreen)
            {
                CarGas.gas.Top += 10; //  benzin bidonu her saniyede 10 birim aşağı inecek
                if (CarGas.gas.Top >= this.Height - 20) // eğer benzin bidonu ekrandan çıkarsa onu yok et.
                {
                    CarGas.gas.Dispose(); // yok etme işlemi
                    CarGas.GasHave = false; // biraz benzine sahip değilim
                    CarGas.OnScreen = false; //ekranda bir benzin bidonu yok.
                }
                float AbsoluteX = Math.Abs((RedCar.Left + (RedCar.Width / 2)) - (CarGas.gas.Left + (CarGas.gas.Width / 2)));
                float AbsoluteY = Math.Abs((RedCar.Top + (RedCar.Height / 2)) - (CarGas.gas.Top + (CarGas.gas.Height / 2)));
                float WidthDistance = (RedCar.Width / 2) + (CarGas.gas.Width / 2);
                float HeightDistance = (RedCar.Height / 2) + (CarGas.gas.Height / 2);
                if((WidthDistance > AbsoluteX) && (HeightDistance > AbsoluteY))
                {
                    Take_Gas(); // gaz al işlemini gerçekleştir.
                }
           
                
            }

        }

        private void Visible_change()
        {
         
            if (labelGameOver.Visible == false && gasValue!=0) // eğer kaza yaptıysam bunları ekranda göster
            { 
                labelHigh.Visible = true;
                labelScore.Visible = true;
                button1.Visible = true;
                labelGameOver.Visible = true;
                button2.Visible = true;
            }
            else if (gasValue == 0) // eğerki benzinim bittiyse bunları göster. Burada ekstradan Benzinin bitti yazısı gözüküyor.
            {
                labelHigh.Visible = true;
                labelScore.Visible = true;
                button1.Visible = true;
                labelGameOver.Visible = true;
                button2.Visible = true;
                label5.Visible = true;

            }
            else // oyunu yeniden başlattıgım için ekrandaki yazıları tekrardan görünmez yapıyorum.
            {
               
                labelHigh.Visible = false;
                labelScore.Visible = false;
                button1.Visible = false;
                labelGameOver.Visible = false;
                axWindowsMediaPlayer1.Visible = false;
                button2.Visible = false;
                label5.Visible = false;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control controls in Controls)
            {
                controls.Visible = true;
            }

            Visible_change(); 
            StartLocationCar(); // arabayı merkeze yerleştirme fonksiyonu.
            Road = 0;
            Speed = 20;
            rndCar[0].OnScreen = true;
            timerRandomCar.Enabled = true; // timerı tekrar başlatıyorum. Arabaların yüklenmesi ve hareket etmesi için.
            timerRandomCar.Interval = 200; // saniyenin 5 katı hızında tekrar etmesi için 200 verdik. yani arabanın saniyede 5 kez ileri gitme hareketi yapmasını sağlıyoruz
            timerLine.Interval = 200;  // saniyenin 5 katı hızında tekrar etmesi için 200 verdik.
            timerLine.Enabled = true; // timerı tekrar başlatıyorum. yolun şeritlerinin hareketi için.
        }

        void Take_Gas()
        {
            gasValue += 25;
            if (gasValue >= 100) // gaz değeri yüzü geçerse hata vermemesi için 100 e çekiyorum.
            {
                gasValue = 100;
            }
            progressBar1.Value = gasValue; // benzin yüzdesini gösteriyor.
            CarGas.gas.Dispose(); // gas picturebox ını yok ediyorum.
            CarGas.OnScreen = false; //ekrandaki benzin bidonu yok olduğu için ekranda yok diyorum.
            CarGas.GasHave = false; // tekrar tekrar oluşturmayı engellemek için.


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // exiti basınca uygulamayı kapat
        }
        private void Crash()
        {
          
            timerRandomCar.Enabled = false; // timerı arabalar ve gazlar için olanı durduruyorum.
            timerLine.Enabled = false; // timer şeritler için olanıda durduruyorum.
            if (gasValue != 0) // çarpma olmuş ise aşağıdaki müzüği çal. yoksa es geç.
            {
                axWindowsMediaPlayer1.URL = "music/crash.mp3";
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            

            foreach (Control controls in Controls) // bütün ekrandaki herşeyi görünmez yap.
            {
                controls.Visible = false;
            }
            for (int j = 0; j < rndCar.Length; j++)
            {
                rndCar[j].EnemyCar.Dispose(); // arbayı yok ettim.
                rndCar[j].EnemyCarHave = false; // arbaya sahip olmayı yok ettim.
                rndCar[j].OnScreen = false; // arabam ekranda değil.
            }
            CarGas.gas.Dispose(); // gas picturebox ını yok ediyorum.
            CarGas.OnScreen = false; //ekrandaki benzin bidonu yok olduğu için ekranda yok diyorum.
            CarGas.GasHave = false; // tekrar tekrar oluşturmayı engellemek için.

            if (Road > Settings1.Default.HighScore)
            {
                Settings1.Default.HighScore = Road; // eğer yeni skorum en yüksek değerden yüksek ise, yeni en yüksek değerim şuanki skorum oluyor
            }

            labelHigh.Text = "HIGH SCORE: " + Settings1.Default.HighScore.ToString(); // .ToString() fonksiyonu sayıyı string yapmaya yarar.
            labelScore.Text = "YOUR SCORE: " + Road.ToString();
            Visible_change();
            gasValue = 100;
            progressBar1.Value = gasValue; // tekrar gazı 100 den başlatıyoruz.
        }
        bool LineMovement = false;
        private void timerSerit_Tick(object sender, EventArgs e)
        {
            Road += 1; // gidilen yol miktarı sayılıyor.

            gasValue -= 1; // gaz değeri düşüyor
            progressBar1.Value = gasValue; // her seferinde progres barı gasValue ile eşitliyor
            if (gasValue == 0) // benzin bittiyse oyunu bitir.
            {
                Crash();
            }

            if (LineMovement == false)
            {
                for (int i = 1; i < 7; i++)
                {
                    this.Controls.Find("LeftLine" + i.ToString(), true)[0].Top -= 25; // herbir sol şeritteki şeritleri 25 birim yukarıya cıkarıyor
                    this.Controls.Find("RightLine" + i.ToString(), true)[0].Top -= 25;// herbir sağ şeritteki şeritleri 25 birim yukarıya cıkarıyor
                    LineMovement = true;
                }
            }
            else
            {
                for (int i = 1; i < 7; i++)
                {
                    this.Controls.Find("LeftLine" + i.ToString(), true)[0].Top += 25;// herbir sol şeritteki şeritleri 25 birim aşağı indiriyor
                    this.Controls.Find("RightLine" + i.ToString(), true)[0].Top += 25;// herbir sağ şeritteki şeritleri 25 birim aşağı indiriyor
                    LineMovement = false;
                }
            }

            labelRoad.Text = Road.ToString() + "m"; // gidilen yolu ekrana yazdırıyor her saniye
            labelSpeed.Text = Speed.ToString() + "km/h"; // gidilen hızı ekrana yazdırıyor her saniye

        }
    }
}
