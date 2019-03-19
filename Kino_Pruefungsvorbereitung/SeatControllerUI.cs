using CinemaSeatController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; 
namespace Kino_Pruefungsvorbereitung
{
    public partial class SeatControllerUI : Form
    {
        private static Program instance;
        private int[] angaben;
        private String uhrzeit;
        


        private int price = 0;


        private IList<Sitzplatz> sitzplaetze;//Sitzplätze in "Liste" statt Array weil Liste besser ~Maribot G.

        public SeatControllerUI(int[] angaben, String uhrzeit)
        {


            sitzplaetze = new List<Sitzplatz>(); // init der liste

            int seats = angaben[0] * angaben[1]; // berechnung der gesamten sitze
            int seatsPerRow = angaben[1]; // wie viele sitze in einer Reihe
            int currentRow = 1; // momentane reihe
            price = 0;
            int currentSeatsInRow = 0; // momentane sitze in momentaniger reihe 
            for (int i = 1; i <= seats; i++)
            {

                bool premium = false;

                if (currentSeatsInRow == seatsPerRow)
                {
                    currentRow++;
                    currentSeatsInRow = 0;
                }

                if (currentRow <= 5)
                {
                    premium = true;
                }
                Sitzplatz seat = new Sitzplatz(premium, currentRow);    
                sitzplaetze.Add(seat);
                Console.WriteLine("New Seat created! Premium: " + premium + ", Row: " + seat.getReihe() + ", Number: " + i);
                currentSeatsInRow++;
            }

            this.uhrzeit = uhrzeit;
            this.angaben = angaben;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle; //Fixieren der Größe des grafischen Fensters.
            this.MaximizeBox = false;                           //^
            resizeForm();
            addSitzplaetze();
            setPriceLocation();
            setPrintLocation();
        }

        public void resizeForm()
        {
            int[] size = new int[2]; 
            size = calculateFormSize();
            this.Width = size[0];
            this.Height = size[1];
            this.Update();
        }

        public void setPrintLocation()
        {
            int x = this.Width;
            int y = this.Height;
            y -= 100;
            x -= 125;

            printButton.Location = new Point(x, y);

            printButton.Click += new EventHandler(printButtonClickEvent);
            
        }

        protected void printButtonClickEvent(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"D:\test1.txt");

            string output=("Uhrzeit: "+ Program.csvUhrzeit);
            string output1 = ("Saal: " + Program.csvSaal);
            string output2 = ("Plätze: " + Program.csvPlatz);
            sw.WriteLine("Reservierung!");
            sw.WriteLine(output);
            sw.Write("{0}.         {1}.",output1,output2);
            sw.Close();


        }

        public void setPriceLocation()
        {
            int x = this.Width;
            int y = this.Height;
            y -= 100;
            x -= (this.Width - 70);

          
            premiumPriceLabel.Text = "Premium Preis: " + Sitzplatz.normalPrice + "€";
            priceLabel.Text = "Preis: " + Sitzplatz.premiumPrice + "€";

            costLabel.Size =new Size(100,40);
            costLabel.Location = new Point(x, y);
            costLabel.Name = "Preis";
            costLabel.Font = new Font("Calibri", 20, FontStyle.Underline);
            costLabel.Text = "Gesamtpreis: 0 €";
            costLabel.Update();
            this.Update();
        }

        public void changePriceLabel()
        { 
            costLabel.Text = "Gesamtpreis: " + price + " €";
            costLabel.Update();
            this.Update();
        }

        public void addSitzplaetze()
        {


            int x = 20;             //"Startposition" der Knöpfe/Buttons, veränderbar
            int y = 50;             //^


            int buttonsInRow = 0;   // Wie viele Knöpfe momentan in der momentanen Reihe sind



            for (int i = 0; i < sitzplaetze.Count; i++)
            {
                Sitzplatz sitzplatz = sitzplaetze.ElementAt(i);


                int number = i + 1;


                Button button = new Button();   // erstellen des "Knopfes"
                button.Width = 35;              // Größes des Knopfes
                button.Height = 35;             // ^
                this.Controls.Add(button);      // Knopf zur Form hinzufügen
                button.Text = number + "";      // Text des Buttons ändern.
                button.Location = new Point(x, y); // Button auf die richtige Position in der Form setzten.

                button.Click += new EventHandler(seatButtonClickEvent); // Event hinzufügen das beim Klicken auf dem Button ausgeführt wird.
                button.BackColor = sitzplatz.istVerfuegbar() ? (sitzplatz.istPremium() ? Color.DarkGreen : Color.Green) : Color.Red; // Button farbe
                button.Show(); // anzeigen des buttons

                buttonsInRow++; 


                if (angaben[1] / 2 == buttonsInRow)
                {
                    x += 95;
                }
                else if (angaben[1] == buttonsInRow)
                {
                    x = 20;
                    y += 45;
                    buttonsInRow = 0;
                }
                else
                {
                    x += 45;
                }
            }
            Console.WriteLine("Added seats!");
            this.Update();
        }

        protected void seatButtonClickEvent(object sender, EventArgs e)
        {
            //Instanzbildung
            int[] sitzNummer = new int[sitzplaetze.Count];

            Button button = sender as Button;
            
            Console.WriteLine("Button clicked -> " + button.Text);

            Sitzplatz seat = sitzplaetze.ElementAt((int.Parse(button.Text) - 1));
            /*for (int j=0;j<sitzNummer.Length;j++)
            {
                sitzNummer[j] = (int.Parse(button.Text) - 1);
                
            }*/
            
                
            
           
            seat.setVerfuegbar(!seat.istVerfuegbar());
            button.BackColor = seat.istVerfuegbar() ? (seat.istPremium() ? Color.DarkGreen : Color.Green) : Color.DimGray;


            if (!seat.istVerfuegbar())
            { 

                price += seat.istPremium() ? Sitzplatz.premiumPrice : Sitzplatz.normalPrice;

            }
            else
            {
                price -= seat.istPremium() ? Sitzplatz.premiumPrice : Sitzplatz.normalPrice;
            }

            if(price < 0)
            {
             //   price = 0;
            }
            changePriceLabel();

            // set price

            this.Update();

        }

        private int[] calculateFormSize()
        {
            int[] size = new int[2];


            int seatWidt = 35; //   seat widt
            int seatGap = 10;  // seat gap
            int seatHeight = 35; // seat height



            // Form size
            // we´re starting with the widt

            int corridorWidt = 50; // size of the corridor widt between the seats


            int widt = 40 /* size @ edges */ + (seatWidt * angaben[1]) + (seatGap * angaben[1]) + corridorWidt + 20;
            int height = 100 /* size @ edges */ + (seatHeight * angaben[0]) + (seatGap * angaben[0]) + 90;

            Console.WriteLine("widt: " + widt);
            Console.WriteLine("height: " + height);

            size[0] = widt;
            size[1] = height;
            return size;
        }

    }
}
