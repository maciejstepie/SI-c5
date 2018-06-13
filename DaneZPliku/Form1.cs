using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaneZPlikuOkienko
{
    
    

    public partial class DaneZPliku : Form
    {
        //Tu przechowuje dane 
        List<DataRow> ListDataRows;
        private string[][] systemDecyzyjny;
        int Rows, Columns;
        int Positives;
        double EntropyS; //Entropia głowna

        //Wilgotność
        double EntropyHHigh; // Entropia Dużej wilgotnosci
        double EntrophyHLow; //Entropia normalnej
        double EntrophyHGain; //Zysk entropi wilgotnosci

        //Wiatr
        double EntrophyWStrong; // Entropia wiator mocny
        double EntrophyWLow; // Entropia wiator suaby
        double EntrophyWGain; // Zysk entropi wiatr

        //Temperatura
        double EntrophyTHot; // Entropia temp goraco
        double EntrophyTCold; // Entropia temp chlodno
        double EntrophyTSoftly; // Entropia temp lagodnie
        double EntrophyTGain; // Zysk entropi temp

        //Pogoda
        double EntrophyWeaS;
        double EntrophyWeaC;
        double EntrophyWeaR;
        double EntrophyWeaGain;


        //listy
        List<double> ld;
        List<string> GrupyA = new List<string>(new string[] { "Wilgotnosc", "Wiatr", "Temperatura", "Pogoda" });
        int indexGeneralRoot = 0;
        public DaneZPliku()
        {
            InitializeComponent();
        }

        private void btnWybierzSystemDecyzyjny_Click(object sender, EventArgs e)
        {
            DialogResult wynikWyboruPliku = ofd.ShowDialog(); // wybieramy plik
            if (wynikWyboruPliku != DialogResult.OK)
                return;

            tbSciezkaDoSystemuDecyzyjnego.Text = ofd.FileName;
            string trescPliku = System.IO.File.ReadAllText(ofd.FileName); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            systemDecyzyjny = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku
            Rows = wiersze.Length; // Przypisuje liczbe wierszy do zmiennej
            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                systemDecyzyjny[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                Columns = cyfry.Length;  // Przypisuje liczbe column do zmiennej
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    systemDecyzyjny[i][j] = cyfra;  
                }
            }

            tbSystemDecyzyjny.Text = TablicaDoString(systemDecyzyjny);
        }

        public string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }

            return wynik;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!LetsBetterCheckFirstWeHaveAnyDataToAnalize())
            {
                MessageBox.Show("Brak danych do analizy!");
                return;
            }

            //czyszczenie
            ListDataRows = new List<DataRow>();
            Positives = 0;
            EntropyS = 0;
            textBox1.Text = "";

            //Umieść dane w liscie
            DataToList();
            //Obliczamy Entropie
            CalculateAllEntropys();
            CalculateAllEntropysHumidity();
            CalculateAllEntropysWind();
            CalculateAllEntropysTemperature();
            CalculateAllEntropysWeather();
            ShowGeneralEntrophy(); //Entropia podstawowa sie wyswietla


            ld = new List<double>(new double[] { EntrophyHGain, EntrophyWGain, EntrophyTGain, EntrophyWeaGain });

            //Głowny korzeń drzewa
            ChooseFristGroup();

        }

        
        private void ChooseFristGroup()
        {


            int index = 0;
            double tmp = 0;
            for (int i =0; i < ld.Count; i++)
            {
               
                if (ld[i] >= tmp)
                {
                    index = i;
                    tmp = ld[i];
                }
            }

            textBox1.Text += "Najwieszy zysk danych ma grupa: " + GrupyA[index] + " " + ld[index] + ", stanowi ona głowny korzeń naszego drzewa";
            indexGeneralRoot = index;
        }

        bool LetsBetterCheckFirstWeHaveAnyDataToAnalize()
        {
            bool tmp = true;
            if (tbSystemDecyzyjny.Text == "" || tbSystemDecyzyjny.Text == null)
                if (systemDecyzyjny == null) tmp = false;
                else tbSystemDecyzyjny.Text = TablicaDoString(systemDecyzyjny);

            return tmp;
        }
        void DataToList()
        {
            for (int i = 0; i < Rows; i++)
            {
                ListDataRows.Add(new DataRow());
                for (int j = 1; j < Columns; j++)
                {
                    if (j == 1) ListDataRows[i].Pogoda = systemDecyzyjny[i][j];
                    if (j == 2) ListDataRows[i].Temperatura = systemDecyzyjny[i][j];
                    if (j == 3) ListDataRows[i].Wilgotnosc = systemDecyzyjny[i][j];
                    if (j == 4) ListDataRows[i].Wiatr = systemDecyzyjny[i][j];
                    if (j == 5) ListDataRows[i].Gram_w_Tenisa = systemDecyzyjny[i][j];

                }

            }

        }

        //Oblicza entropie ze wzoru, argumentami jest ilosc pozytynych oraz ilosc wsyztskich
        double CalculateEntropy(int pos, int all)
        {

            int neg = all - pos;
            double posAll = ((double)pos / (double)all);
            double negAll = ((double)neg / (double)all);

            double tmpA = Math.Pow(posAll, posAll);
            //textBox1.Text += tmpA.ToString() + Environment.NewLine;

            double tmpB = Math.Pow(negAll, negAll);
            //textBox1.Text += tmpB.ToString() + Environment.NewLine;

            double tmpC = tmpA * tmpB;
            // textBox1.Text += tmpC.ToString() + Environment.NewLine;

            double tmpD = -(Math.Log(tmpC, 2));

            //Math.Log(num, base)
            /*Entropy(S) = −plog2p − p log2p Entropy(S) = Entropy[9+, 5−] = − 9 14 log2 9 14 − 5 14 log2 5 14 = −(log2( 9 14 9 14  5 14 5 14 )) = −log2(0.5211295762)  0.940 */
            return tmpD;
        }

        //Oblicza glwona entropie
        void CalculateAllEntropys()
        {
            //Obliczamy głowna entropie
            foreach (var i in ListDataRows)
            {
                if (i.Gram_w_Tenisa == "Tak") Positives++;
            }
            EntropyS = CalculateEntropy(Positives, Rows);


        }

        //Oblicza entropie wilgotnosci
        void CalculateAllEntropysHumidity()
        {
            int tmpH = 0; //Wysokie na tak

            int tmpHQ = 0; //Ilosc z wysokim

            int tmpL = 0; //Ilość normalnego na tak

            int tmpLQ = 0; //ilosc normalnego


            //Obliczamy  entropie wilgotnosci
            foreach (var i in ListDataRows)
            {
                if (i.Wilgotnosc == "Wysoka")
                {
                    if (i.Gram_w_Tenisa == "Tak") tmpH++;
                    tmpHQ++;
                }

                else
                {
                    tmpLQ++;
                    if (i.Gram_w_Tenisa == "Tak") tmpL++;
                }

            }

            EntropyHHigh = CalculateEntropy(tmpH, tmpHQ);
            EntrophyHLow = CalculateEntropy(tmpL,tmpLQ);


            //Gain(S,Wilgotnosc) = 0.940−( 7/14 * 0.985)−( 7/14 * 0.592) = 0.940−0.4935−0.296 = 0.151 
            //                              
            EntrophyHGain = EntropyS - (((double)(tmpHQ) / (double)(Rows)) * EntropyHHigh) - (((double)(tmpLQ) / (double)(Rows)) * EntrophyHLow);

        }
        
        //Oblicza entropie wiatru
        void CalculateAllEntropysWind()
        {
            int tmpS = 0; //Strong na tak

            int tmpSQ = 0; //Ilosc z strongem

            int tmpL = 0; //Ilość nieksiego na tak

            int tmpLQ = 0; //ilosc niskeiego


            //Obliczamy  entropie wilgotnosci
            foreach (var i in ListDataRows)
            {
                if (i.Wiatr == "Mocny")
                {
                    if (i.Gram_w_Tenisa == "Tak") tmpS++;
                    tmpSQ++;
                }

                else
                {
                    tmpLQ++;
                    if (i.Gram_w_Tenisa == "Tak") tmpL++;
                }

            }
 
            EntrophyWStrong = CalculateEntropy(tmpS, tmpSQ);
            EntrophyWLow = CalculateEntropy(tmpL, tmpLQ);
            
            //Gain(S,Wilgotnosc) = 0.940−( 7/14 * 0.985)−( 7/14 * 0.592) = 0.940−0.4935−0.296 = 0.151 
            //                              
            EntrophyWGain = EntropyS - (((double)(tmpSQ) / (double)(Rows)) * EntrophyWStrong) - (((double)(tmpLQ) / (double)(Rows)) * EntrophyWLow);

        }

        //Oblicza entropie temperatury
        void CalculateAllEntropysTemperature()
        {
            int tmpH = 0; //Gorąco na tak ,temporatryHot

            int tmpHQ = 0; //Ilosc z goracem, temporaryHotQuantity

            int tmpC = 0; //Ilość chlodne na tak

            int tmpCQ = 0; //ilosc chlodne

            int tmpS = 0; //ilość lagodnie na tak , temporarySoflty

            int tmpSQ = 0; //ilosc lagodnie

            //Obliczamy  entropie temperatury
            foreach (var i in ListDataRows)
            {
                if (i.Temperatura == "Goraco")
                {
                    if (i.Gram_w_Tenisa == "Tak") tmpH++;
                    tmpHQ++;
                }

                if (i.Temperatura == "Chlodno")
                {
                    tmpCQ++;
                    if (i.Gram_w_Tenisa == "Tak") tmpC++;
                }

                if (i.Temperatura == "Lagodnie")
                {
                    tmpSQ++;
                    if (i.Gram_w_Tenisa == "Tak") tmpS++;
                }

            }
            EntrophyTHot = CalculateEntropy(tmpH, tmpHQ);
            EntrophyTCold = CalculateEntropy(tmpC, tmpCQ);
            EntrophyTSoftly = CalculateEntropy(tmpS, tmpSQ);



            //Gain(S,Wilgotnosc) = 0.940−( 7/14 * 0.985)−( 7/14 * 0.592) = 0.940−0.4935−0.296 = 0.151 
            //                              
            EntrophyTGain = EntropyS - (((double)(tmpHQ) / (double)(Rows)) * EntrophyTHot) - (((double)(tmpCQ) / (double)(Rows)) * EntrophyTCold) - (((double)(tmpSQ) / (double)(Rows)) * EntrophyTSoftly);

        }

        //Oblicza entropie pogody
        void CalculateAllEntropysWeather()
        {

            //Sloneczna Pochmurna Deszczowa 
            int tmpR = 0; //Gorąco na tak ,temporatryRainy

            int tmpRQ = 0; //Ilosc z deszczem, temporaryRainyQuantity

            int tmpC = 0; //Ilość cloduy na tak

            int tmpCQ = 0; //ilosc cloudy

            int tmpS = 0; //ilość slonecznie na tak , temporarySunny

            int tmpSQ = 0; //ilosc slonecznie

            //Obliczamy  entropie temperatury
            foreach (var i in ListDataRows)
            {
                if (i.Pogoda == "Deszczowa")
                {
                    if (i.Gram_w_Tenisa == "Tak") tmpR++;
                    tmpRQ++;
                }

                if (i.Pogoda == "Pochmurna")
                {
                    tmpCQ++;
                    if (i.Gram_w_Tenisa == "Tak") tmpC++;
                }

                if (i.Pogoda == "Sloneczna")
                {
                    tmpSQ++;
                    if (i.Gram_w_Tenisa == "Tak") tmpS++;
                }

            }
           
            

            EntrophyWeaR = CalculateEntropy(tmpR, tmpRQ);
            EntrophyWeaS = CalculateEntropy(tmpS, tmpSQ);
            EntrophyWeaC = CalculateEntropy(tmpC, tmpCQ);
            



            //Gain(S,Wilgotnosc) = 0.940−( 7/14 * 0.985)−( 7/14 * 0.592) = 0.940−0.4935−0.296 = 0.151 
            //                              
            EntrophyWeaGain = EntropyS - (((double)(tmpRQ) / (double)(Rows)) * EntrophyWeaR) - (((double)(tmpCQ) / (double)(Rows)) * EntrophyWeaC) - (((double)(tmpSQ) / (double)(Rows)) * EntrophyWeaS);

        }

        #region Wyświetlanie




        void ShowGeneralEntrophy()
        {
            //textBox1.Text = "Pozytywnych : " + Positives.ToString() + " | " + EntropyS.ToString();

            textBox1.Text += "Entrophy S: " + EntropyS.ToString() + Environment.NewLine;
            textBox1.Text += "Entropia H: " + EntropyHHigh + " Entropia L:  " + EntrophyHLow + Environment.NewLine;
            textBox1.Text += "EntropiaGainHumilite: " + EntrophyHGain + Environment.NewLine;
            textBox1.Text += "EntropiaGainWind: " + EntrophyWGain + Environment.NewLine;
            textBox1.Text += "EntropiaGainTemperature: " + EntrophyTGain + Environment.NewLine;
            textBox1.Text += "EntropiaGainWeather: " + EntrophyWeaGain + Environment.NewLine;

        }


#endregion
    }


    //Klasa w której przechowujemy dane
    public class DataRow
    {
        public string Pogoda, Temperatura, Wilgotnosc, Wiatr, Gram_w_Tenisa;
 
    }
}
